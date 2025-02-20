﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Rocks.Diagnostics;
using System.Globalization;

namespace Rocks.Tests.Diagnostics;

public static class TypeHasNoMockableMembersDiagnosticTests
{
	[Test]
	public static void Create()
	{
		var syntaxTree = CSharpSyntaxTree.ParseText("public class X { }");
		var references = AppDomain.CurrentDomain.GetAssemblies()
			.Where(_ => !_.IsDynamic && !string.IsNullOrWhiteSpace(_.Location))
			.Select(_ => MetadataReference.CreateFromFile(_.Location));
		var compilation = CSharpCompilation.Create("generator", new SyntaxTree[] { syntaxTree },
			references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
		var model = compilation.GetSemanticModel(syntaxTree, true);

		var typeSyntax = syntaxTree.GetRoot().DescendantNodes(_ => true)
			.OfType<TypeDeclarationSyntax>().Single();

		var descriptor = TypeHasNoMockableMembersDiagnostic.Create(model.GetDeclaredSymbol(typeSyntax)!);

		Assert.Multiple(() =>
		{
			Assert.That(descriptor.GetMessage(CultureInfo.InvariantCulture), Is.EqualTo("The type X has no members that can be overriden"));
			Assert.That(descriptor.Descriptor.Title.ToString(CultureInfo.CurrentCulture), Is.EqualTo(TypeHasNoMockableMembersDiagnostic.Title));
			Assert.That(descriptor.Id, Is.EqualTo(TypeHasNoMockableMembersDiagnostic.Id));
			Assert.That(descriptor.Severity, Is.EqualTo(DiagnosticSeverity.Error));
		});
	}
}