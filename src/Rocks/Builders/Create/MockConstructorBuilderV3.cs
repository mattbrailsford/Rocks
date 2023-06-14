﻿using Microsoft.CodeAnalysis;
using Rocks.Builders.Shim;
using Rocks.Extensions;
using Rocks.Models;
using System.CodeDom.Compiler;
using System.Collections.Immutable;

namespace Rocks.Builders.Create;

internal static class MockConstructorBuilderV3
{
	internal static void Build(IndentedTextWriter writer, TypeMockModel type, 
		ImmutableArray<ParameterModel> parameters, ImmutableArray<TypeMockModel> shims)
	{
		var typeToMockName = type.Type.FullyQualifiedName;
		var namingContext = new VariableNamingContextV3(parameters);
		var hasRequiredProperties = type.ConstructorProperties.Any(_ => _.IsRequired);

		var contextParameters = type.ConstructorProperties.Length == 0 ?
			$"global::Rocks.Expectations.Expectations<{typeToMockName}> @{namingContext["expectations"]}" :
			$"global::Rocks.Expectations.Expectations<{typeToMockName}> @{namingContext["expectations"]}, ConstructorProperties{(!hasRequiredProperties ? "?" : string.Empty)} @{namingContext["constructorProperties"]}";
		var instanceParameters = parameters.Length == 0 ?
			contextParameters :
			string.Join(", ", contextParameters,
				string.Join(", ", parameters.Select(_ =>
				{
					var requiresNullable = _.RequiresNullableAnnotation ? "?" : string.Empty;
					var direction = _.RefKind switch
					{
						RefKind.Ref => "ref ",
						RefKind.Out => "out ",
						RefKind.In => "in ",
						_ => string.Empty
					};
					return $"{direction}{(_.IsParams ? "params " : string.Empty)}{_.Type.FullyQualifiedName}{requiresNullable} @{_.Name}";
				})));

		var mockTypeName = $"Rock{type.Type.FlattenedName}";

		if (hasRequiredProperties)
		{
			writer.WriteLine("[global::System.Diagnostics.CodeAnalysis.SetsRequiredMembers]");
		}

		if (parameters.Length > 0)
		{
			var passedParameter = string.Join(", ", parameters.Select(_ =>
			{
				var requiresNullable = _.RequiresNullableAnnotation ? "!" : string.Empty;
				var direction = _.RefKind switch
				{
					RefKind.Ref => "ref ",
					RefKind.Out => "out ",
					RefKind.In => "in ",
					_ => string.Empty
				};
				return $"{direction}@{_.Name}{requiresNullable}";
			}));

			var isUnsafe = parameters.Any(_ => _.Type.IsPointer) ? "unsafe " : string.Empty;

			writer.WriteLine($"public {isUnsafe}{mockTypeName}({instanceParameters})");
			writer.Indent++;
			writer.WriteLine($": base({passedParameter})");
			writer.Indent--;
			writer.WriteLine("{");
			writer.Indent++;
			MockConstructorBuilderV3.BuildFieldSetters(writer, namingContext, shims, type.ConstructorProperties, hasRequiredProperties);
			writer.Indent--;
			writer.WriteLine("}");
		}
		else
		{
			writer.WriteLine($"public {mockTypeName}({instanceParameters})");
			writer.WriteLine("{");
			writer.Indent++;
			MockConstructorBuilderV3.BuildFieldSetters(writer, namingContext, shims, type.ConstructorProperties, hasRequiredProperties);
			writer.Indent--;
			writer.WriteLine("}");
		}
	}

	private static void BuildFieldSetters(IndentedTextWriter writer, 
		VariableNamingContextV3 namingContext, EquatableArray<TypeMockModel> shims,
		EquatableArray<ConstructorPropertyModel> constructorProperties, bool hasRequiredProperties)
	{
		if (shims.Length == 0)
		{
			writer.WriteLine($"this.handlers = @{namingContext["expectations"]}.Handlers;");
		}
		else
		{
			var shimFields = string.Join(", ", shims.Select(_ => $"this.shimFor{_.Type.FlattenedName}"));
			var shimConstructors = string.Join(", ", shims.Select(_ => $"new {ShimBuilderV3.GetShimName(_.Type)}(this)"));
			writer.WriteLine($"(this.handlers, {shimFields}) = (@{namingContext["expectations"]}.Handlers, {shimConstructors});");
		}

		if (constructorProperties.Length > 0)
		{
			var initIndexers = constructorProperties.Where(
				_ => _.IsIndexer && (_.Accessors == PropertyAccessor.Init || _.Accessors == PropertyAccessor.GetAndInit) &&
					_.CanBeSeenByContainingAssembly).ToArray();
			var enumerableTypes = initIndexers.Select(_ =>
				_.Parameters.Length == 1 ?
					_.Parameters[0].Type.FullyQualifiedName :
					$"({string.Join(", ", _.Parameters.Select(p => p.Type.FullyQualifiedName))})").ToArray();
			var forEachTypes = initIndexers.Select(_ =>
				_.Parameters.Length == 1 ?
					$"var {_.Parameters[0].Name}" :
					$"({string.Join(", ", _.Parameters.Select(p => $"var {p.Name}"))})").ToArray();
			var indexerNames = initIndexers.Select(_ => string.Join(", ", _.Parameters.Select(p => p.Name))).ToArray();

			if(!hasRequiredProperties)
			{
				writer.WriteLine($"if (@{namingContext["constructorProperties"]} is not null)");
				writer.WriteLine("{");
				writer.Indent++;
			}

			foreach (var constructorProperty in constructorProperties.Where(_ => !_.IsIndexer))
			{
				var propertyAssignment = (constructorProperty.NullableAnnotation == NullableAnnotation.None ||
					constructorProperty.NullableAnnotation == NullableAnnotation.NotAnnotated) && constructorProperty.IsReferenceType ?
						"!" : string.Empty;
				writer.WriteLine($"this.{constructorProperty.Name} = @{namingContext["constructorProperties"]}.{constructorProperty.Name}{propertyAssignment};");
			}

			for (var i = 0; i < initIndexers.Length; i++)
			{
				var initIndexer = initIndexers[i];
				var enumerableType = enumerableTypes[i];
				var forEachType = forEachTypes[i];
				var indexerName = indexerNames[i];

				writer.WriteLines(
					$$"""
					foreach ({{forEachType}} in ((global::System.Collections.Generic.IEnumerable<{{enumerableType}}>)@{{namingContext["constructorProperties"]}}))
					{
						this[{{indexerName}}] = constructorProperties[{{indexerName}}];
					}
					""");
			}

			if (!hasRequiredProperties)
			{
				writer.Indent--;
				writer.WriteLine("}");
			}
		}
	}
}