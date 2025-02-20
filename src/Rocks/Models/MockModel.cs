﻿using Microsoft.CodeAnalysis;
using Rocks.Builders;
using Rocks.Diagnostics;
using Rocks.Extensions;
using System.Collections.Immutable;

namespace Rocks.Models;

internal sealed record MockModel
{
	internal static MockModel? Create(ITypeSymbol typeToMock, SemanticModel model, BuildType buildType, bool shouldResolveShims)
	{
		if (typeToMock.ContainsDiagnostics())
		{
			return null;
		}

		var compilation = model.Compilation;
		var treatWarningsAsErrors = compilation.Options.GeneralDiagnosticOption == ReportDiagnostic.Error;

		// Do all the work to see if this is a type to mock.
		var diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();

		if (typeToMock.SpecialType == SpecialType.System_Delegate ||
			typeToMock.SpecialType == SpecialType.System_MulticastDelegate ||
			typeToMock.SpecialType == SpecialType.System_Enum ||
			typeToMock.SpecialType == SpecialType.System_ValueType)
		{
			diagnostics.Add(CannotMockSpecialTypesDiagnostic.Create(typeToMock));
		}

		if (typeToMock.IsSealed)
		{
			diagnostics.Add(CannotMockSealedTypeDiagnostic.Create(typeToMock));
		}

		if (typeToMock is INamedTypeSymbol namedTypeToMock &&
			namedTypeToMock.HasOpenGenerics())
		{
			diagnostics.Add(CannotSpecifyTypeWithOpenGenericParametersDiagnostic.Create(typeToMock));
		}

		var attributes = typeToMock.GetAttributes();
		var obsoleteAttribute = model.Compilation.GetTypeByMetadataName(typeof(ObsoleteAttribute).FullName)!;

		if (attributes.Any(_ => _.AttributeClass!.Equals(obsoleteAttribute, SymbolEqualityComparer.Default) &&
			(_.ConstructorArguments.Any(_ => _.Value is bool error && error) || treatWarningsAsErrors)))
		{
			diagnostics.Add(CannotMockObsoleteTypeDiagnostic.Create(typeToMock));
		}

		var memberIdentifier = 0u;
		var shims = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
		var containingAssembly = compilation.Assembly;

		var constructors = typeToMock.GetMockableConstructors(containingAssembly);
		var methods = typeToMock.GetMockableMethods(compilation.Assembly, shims, compilation, ref memberIdentifier);
		var properties = typeToMock.GetMockableProperties(containingAssembly, shims, ref memberIdentifier);
		var events = typeToMock.GetMockableEvents(containingAssembly);

		if (constructors.Length > 1)
		{
			var uniqueConstructors = new List<IMethodSymbol>(constructors.Length);

			foreach (var constructor in constructors)
			{
				if (uniqueConstructors.Any(_ => _.Match(constructor) == MethodMatch.Exact))
				{
					// We found a rare case where there are duplicate constructors.
					diagnostics.Add(DuplicateConstructorsDiagnostic.Create(typeToMock));
					break;
				}
				else
				{
					uniqueConstructors.Add(constructor);
				}
			}
		}

		foreach (var constructor in constructors)
		{
			diagnostics.AddRange(constructor.GetObsoleteDiagnostics(obsoleteAttribute, treatWarningsAsErrors));
		}

		foreach (var method in methods.Results)
		{
			diagnostics.AddRange(method.Value.GetObsoleteDiagnostics(obsoleteAttribute, treatWarningsAsErrors));
		}

		foreach (var property in properties.Results)
		{
			diagnostics.AddRange(property.Value.GetObsoleteDiagnostics(obsoleteAttribute, treatWarningsAsErrors));
		}

		foreach (var @event in events.Results)
		{
			diagnostics.AddRange(@event.Value.GetObsoleteDiagnostics(obsoleteAttribute, treatWarningsAsErrors));
		}

		if (methods.InaccessibleAbstractMembers.Length > 0 || properties.InaccessibleAbstractMembers.Length > 0 ||
			events.InaccessibleAbstractMembers.Length > 0)
		{
			diagnostics.Add(TypeHasInaccessibleAbstractMembersDiagnostic.Create(typeToMock));
		}

		if (methods.HasMatchWithNonVirtual)
		{
			diagnostics.Add(TypeHasMatchWithNonVirtualDiagnostic.Create(typeToMock));
		}

		if (methods.Results.Any(_ => _.Value.IsAbstract && _.Value.IsStatic) ||
			properties.Results.Any(_ => _.Value.IsAbstract && _.Value.IsStatic))
		{
			diagnostics.Add(InterfaceHasStaticAbstractMembersDiagnostic.Create(typeToMock));
		}

		if (buildType == BuildType.Create && methods.Results.Length == 0 && properties.Results.Length == 0)
		{
			diagnostics.Add(TypeHasNoMockableMembersDiagnostic.Create(typeToMock));
		}

		if (typeToMock.TypeKind == TypeKind.Class && constructors.Length == 0)
		{
			diagnostics.Add(TypeHasNoAccessibleConstructorsDiagnostic.Create(typeToMock));
		}

		var isMockable = !diagnostics.Any(_ => _.Severity == DiagnosticSeverity.Error);

		return new(!isMockable ? null : new TypeMockModel(typeToMock, compilation, model, constructors, methods, properties, events, shims, shouldResolveShims),
			typeToMock.GetFullyQualifiedName(),
			diagnostics.ToImmutable());
	}

	private MockModel(TypeMockModel? type, string typeFullyQualifiedName,
		EquatableArray<Diagnostic> diagnostics) =>
		(this.Type, this.FullyQualifiedName, this.Diagnostics) =
			(type, typeFullyQualifiedName, diagnostics);

	internal EquatableArray<Diagnostic> Diagnostics { get; }
	internal string FullyQualifiedName { get; }
	internal TypeMockModel? Type { get; }
}
