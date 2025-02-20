﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Rocks.Extensions;

namespace Rocks.Tests.Extensions;

public static class ITypeSymbolExtensionsIsObsoleteTests
{
	[TestCase("public class Target { } public class Usage { public void Foo(Target t) { } }", false, false)]
	[TestCase("[System.Obsolete(\"obsolete\")] public class Target { } public class Usage { public void Foo(Target t) { } }", false, false)]
	[TestCase("[System.Obsolete(\"obsolete\")] public class Target { } public class Usage { public void Foo(Target t) { } }", true, true)]
	[TestCase("[System.Obsolete(\"obsolete\", true)] public class Target { } public class Usage { public void Foo(Target t) { } }", false, true)]
	[TestCase("[System.Obsolete(\"obsolete\", true)] public class Target { } public class Usage { public void Foo(Target t) { } }", true, true)]
	[TestCase("public class GenericTarget<T> { } public class Usage { public void Foo(GenericTarget<string> t) { } }", true, false)]
	[TestCase("[System.Obsolete(\"obsolete\")] public class Target { } public class SubTarget { } public class GenericTarget<T> where T : Target { } public class Usage { public void Foo(GenericTarget<SubTarget> t) { } }", true, true)]
	public static void IsTypeObsolete(string code, bool treatWarningsAsErrors, bool expectedValue)
	{
		(var type, var obsoleteAttribute) = ITypeSymbolExtensionsIsObsoleteTests.GetSymbol(code);

		Assert.That(type.IsObsolete(obsoleteAttribute, treatWarningsAsErrors), Is.EqualTo(expectedValue));
	}

	private static (ITypeSymbol type, INamedTypeSymbol obsoleteAttribute) GetSymbol(string source)
	{
		var syntaxTree = CSharpSyntaxTree.ParseText(source);
		var references = AppDomain.CurrentDomain.GetAssemblies()
			.Where(_ => !_.IsDynamic && !string.IsNullOrWhiteSpace(_.Location))
			.Select(_ => MetadataReference.CreateFromFile(_.Location));
		var compilation = CSharpCompilation.Create("generator", new SyntaxTree[] { syntaxTree },
			references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
		var model = compilation.GetSemanticModel(syntaxTree, true);

		var obsoleteAttribute = model.Compilation.GetTypeByMetadataName(typeof(ObsoleteAttribute).FullName!)!;

		var methodSyntax = syntaxTree.GetRoot().DescendantNodes(_ => true)
			.OfType<MethodDeclarationSyntax>().Single();
		return (model.GetDeclaredSymbol(methodSyntax)!.Parameters[0].Type, obsoleteAttribute);
	}
}