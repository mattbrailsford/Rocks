﻿using Microsoft.CodeAnalysis;
using Rocks.Extensions;
using Rocks.Models;
using System.CodeDom.Compiler;
using System.Collections.Immutable;

namespace Rocks.Builders.Create;

internal static class MockMethodValueBuilderV3
{
	internal static void Build(IndentedTextWriter writer, MethodModel method, bool raiseEvents,
		Compilation compilation)
	{
		var shouldThrowDoesNotReturnException = method.ShouldThrowDoesNotReturnException;

		var returnByRef = method.ReturnsByRef ? "ref " : method.ReturnsByRefReadOnly ? "ref readonly " : string.Empty;
		var returnType = $"{returnByRef}{method.ReturnType.FullyQualifiedName}";
		var parametersDescription = string.Join(", ", method.Parameters.Select(_ =>
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
		}));
		var explicitTypeNameDescription = method.RequiresExplicitInterfaceImplementation == RequiresExplicitInterfaceImplementation.Yes ?
			$"{method.ContainingTypeFullyQualifiedName}." : string.Empty;
		var methodDescription = $"{returnType} {explicitTypeNameDescription}{method.Name}({parametersDescription})";

		var methodParameters = string.Join(", ", method.Parameters.Select(_ =>
		{
			var requiresNullable = _.RequiresNullableAnnotation ? "?" : string.Empty;
			var defaultValue = _.HasExplicitDefaultValue ? $" = {_.ExplicitDefaultValue}" : string.Empty;
			var direction = _.RefKind switch
			{
				RefKind.Ref => "ref ",
				RefKind.Out => "out ",
				RefKind.In => "in ",
				_ => string.Empty
			};
			var parameter = $"{direction}{(_.IsParams ? "params " : string.Empty)}{_.Type.FullyQualifiedName}{requiresNullable} @{_.Name}{defaultValue}";
			var attributes = _.AttributesDescription;
			return $"{(attributes.Length > 0 ? $"{attributes} " : string.Empty)}{parameter}";
		}));
		var isUnsafe = method.IsUnsafe ? "unsafe " : string.Empty;
		var methodSignature =
			$"{isUnsafe}{returnType} {explicitTypeNameDescription}{method.Name}({methodParameters})";
		var methodException =
			$"{returnType} {explicitTypeNameDescription}{method.Name}({string.Join(", ", method.Parameters.Select(_ => $"{{@{_.Name}}}"))})";

		var attributes = method.AttributesDescription;

		if (method.AttributesDescription.Length > 0)
		{
			writer.WriteLine(method.AttributesDescription);
		}

		if (method.ReturnType.AttributesDescription.Length > 0)
		{
			writer.WriteLine(method.ReturnType.AttributesDescription);
		}

		writer.WriteLine($@"[global::Rocks.MemberIdentifier({method.MemberIdentifier}, ""{methodDescription}"")]");
		var isPublic = method.RequiresExplicitInterfaceImplementation == RequiresExplicitInterfaceImplementation.No ?
			$"{method.OverridingCodeValue} " : string.Empty;
		writer.WriteLine($"{isPublic}{(method.RequiresOverride == RequiresOverride.Yes ? "override " : string.Empty)}{methodSignature}");

		var constraints = ImmutableArray<string>.Empty;

		if (method.RequiresExplicitInterfaceImplementation == RequiresExplicitInterfaceImplementation.No &&
			method.ContainingTypeKind == TypeKind.Interface)
		{
			constraints = method.Constraints;
		}

		if (method.RequiresExplicitInterfaceImplementation == RequiresExplicitInterfaceImplementation.Yes ||
			method.RequiresOverride == RequiresOverride.Yes)
		{
			constraints = constraints.AddRange(method.DefaultConstraints);
		}

		if (constraints.Length > 0)
		{
			writer.Indent++;

			foreach (var constraint in constraints)
			{
				writer.WriteLine(constraint);
			}

			writer.Indent--;
		}

		writer.WriteLine("{");
		writer.Indent++;

		foreach (var outParameter in method.Parameters.Where(_ => _.RefKind == RefKind.Out))
		{
			writer.WriteLine($"@{outParameter.Name} = default!;");
		}

		var namingContext = new VariableNamingContextV3(method);

		writer.WriteLine($"if (this.handlers.TryGetValue({method.MemberIdentifier}, out var @{namingContext["methodHandlers"]}))");
		writer.WriteLine("{");
		writer.Indent++;

		if (method.Parameters.Length > 0)
		{
			MockMethodValueBuilderV3.BuildMethodValidationHandlerWithParameters(
				writer, method, model.MockType, namingContext,
				raiseEvents, shouldThrowDoesNotReturnException, method.MemberIdentifier);
		}
		else
		{
			MockMethodValueBuilderV3.BuildMethodValidationHandlerNoParameters(
				writer, method, model.MockType, namingContext,
				raiseEvents, shouldThrowDoesNotReturnException, method.MemberIdentifier);
		}

		if (method.Parameters.Length > 0)
		{
			writer.WriteLine();
			writer.WriteLine($"throw new global::Rocks.Exceptions.ExpectationException(\"No handlers match for {methodSignature.Replace("\"", "\\\"")}\");");
		}

		writer.Indent--;
		writer.WriteLine("}");

		if (!method.IsAbstract)
		{
			writer.WriteLine("else");
			writer.WriteLine("{");
			writer.Indent++;

			// We'll call the base implementation if an expectation wasn't provided.
			// We'll do this as well for interfaces with a DIM through a shim.
			// If something like this is added in the future, then I'll revisit this:
			// https://github.com/dotnet/csharplang/issues/2337
			// Note that if the method has [DoesNotReturn], calling the base method
			// and returning its' value should not trip a compiler warning,
			// as the base method is responsible for not returning.
			var passedParameter = string.Join(", ", method.Parameters.Select(_ =>
			{
				var requiresNullable = _.RequiresForcedNullableAnnotation() ? "!" : string.Empty;
				var direction = _.RefKind switch
				{
					RefKind.Ref => "ref ",
					RefKind.Out => "out ",
					RefKind.In => "in ",
					_ => string.Empty
				};
				return $"{direction}@{_.Name}{requiresNullable}";
			}));
			var target = method.ContainingTypeKind == TypeKind.Interface ?
				$"this.shimFor{method.ContainingTypeFlattenedName}" : "base";
			writer.WriteLine($"return {target}.{method.Name}({passedParameter});");

			writer.Indent--;
			writer.WriteLine("}");
		}
		else
		{
			writer.WriteLine();
			writer.WriteLine($"throw new global::Rocks.Exceptions.ExpectationException(\"No handlers were found for {methodSignature.Replace("\"", "\\\"")}\");");
		}

		writer.Indent--;
		writer.WriteLine("}");
		writer.WriteLine();
	}

	internal static void BuildMethodHandler(IndentedTextWriter writer, IMethodSymbol method, ITypeSymbol typeToMock,
		VariableNamingContextV3 namingContext, bool raiseEvents, bool shouldThrowDoesNotReturnException, uint memberIndentifier)
	{
		writer.WriteLine($"@{namingContext["methodHandler"]}.IncrementCallCount();");

		var methodCast = method.RequiresProjectedDelegate() ?
			MockProjectedDelegateBuilder.GetProjectedCallbackDelegateFullyQualifiedName(method, typeToMock) :
			DelegateBuilder.Build(method.Parameters, method.ReturnType);

		if (method.ReturnsByRef || method.ReturnsByRefReadonly)
		{
			writer.WriteLine(
				method.ReturnType.TypeKind != TypeKind.TypeParameter ?
					$"this.rr{memberIndentifier} = @{namingContext["methodHandler"]}.Method is not null ?" :
					$"this.rr{memberIndentifier} = @{namingContext["methodHandler"]}.Method is not null && @{namingContext["methodHandler"]}.Method is {methodCast} @{namingContext["methodReturn"]} ? ");
		}
		else
		{
			if (shouldThrowDoesNotReturnException)
			{
				writer.WriteLine(
					method.ReturnType.TypeKind != TypeKind.TypeParameter ?
						$"_ = @{namingContext["methodHandler"]}.Method is not null ?" :
						$"_ = @{namingContext["methodHandler"]}.Method is not null && @{namingContext["methodHandler"]}.Method is {methodCast} @{namingContext["methodReturn"]} ?");
			}
			else
			{
				writer.WriteLine(
					method.ReturnType.TypeKind != TypeKind.TypeParameter ?
						$"var @{namingContext["result"]} = @{namingContext["methodHandler"]}.Method is not null ?" :
						$"var @{namingContext["result"]} = @{namingContext["methodHandler"]}.Method is not null && @{namingContext["methodHandler"]}.Method is {methodCast} @{namingContext["methodReturn"]} ?");
			}
		}

		writer.Indent++;

		var methodArguments = method.Parameters.Length == 0 ? string.Empty :
			string.Join(", ", method.Parameters.Select(
				_ => _.RefKind == RefKind.Ref || _.RefKind == RefKind.Out ? $"{(_.RefKind == RefKind.Ref ? "ref" : "out")} @{_.Name}" : $"@{_.Name}"));
		var methodReturnType = method.ReturnType.IsRefLikeType ?
			MockProjectedDelegateBuilder.GetProjectedReturnValueDelegateFullyQualifiedName(method, typeToMock) : method.ReturnType.GetFullyQualifiedName();
		var handlerName = method.ReturnType.IsPointer() ?
			MockProjectedTypesAdornmentsBuilder.GetProjectedHandlerInformationFullyQualifiedNameName(method.ReturnType, typeToMock) :
			$"global::Rocks.HandlerInformation<{methodReturnType}>";

		writer.WriteLine(
			method.ReturnType.TypeKind != TypeKind.TypeParameter ?
				$"global::System.Runtime.CompilerServices.Unsafe.As<{methodCast}>(@{namingContext["methodHandler"]}.Method)({methodArguments}) :" :
				$"@{namingContext["methodReturn"]}({methodArguments}) :");

		if (method.ReturnType.IsPointer() || !method.ReturnType.IsRefLikeType)
		{
			if (method.ReturnType.TypeKind != TypeKind.TypeParameter)
			{
				writer.WriteLine($"global::System.Runtime.CompilerServices.Unsafe.As<{handlerName}>(@{namingContext["methodHandler"]}).ReturnValue;");
			}
			else
			{
				writer.WriteLines(
					$$"""
					@{{namingContext["methodHandler"]}} is {{handlerName}} @{{namingContext["returnValue"]}} ?
						@{{namingContext["returnValue"]}}.ReturnValue :
						throw new global::Rocks.Exceptions.NoReturnValueException($"No return value could be obtained for {{method.ReturnType.Name}} of type {typeof({{method.ReturnType.Name}}).FullName}.");
					"""
				);
			}
		}
		else
		{
			if (method.ReturnType.TypeKind != TypeKind.TypeParameter)
			{
				writer.WriteLine($"global::System.Runtime.CompilerServices.Unsafe.As<{handlerName}>(@{namingContext["methodHandler"]}).ReturnValue!.Invoke();");
			}
			else
			{
				writer.WriteLines(
					$$"""
					@{{namingContext["methodHandler"]}} is {{handlerName}} @{{namingContext["returnValue"]}} ?
						@{{namingContext["returnValue"]}}.ReturnValue!.Invoke() :
						throw new global::Rocks.Exceptions.NoReturnValueException($"No return value could be obtained for {{method.ReturnType.Name}} of type {typeof({{method.ReturnType.Name}}).FullName}.");
					"""
				);
			}
		}

		writer.Indent--;

		if (raiseEvents)
		{
			writer.WriteLine($"@{namingContext["methodHandler"]}.RaiseEvents(this);");
		}

		if (shouldThrowDoesNotReturnException)
		{
			writer.WriteLine($"throw new global::Rocks.Exceptions.DoesNotReturnException();");
		}
		else
		{
			if (method.ReturnsByRef || method.ReturnsByRefReadonly)
			{
				writer.WriteLine($"return ref this.rr{memberIndentifier};");
			}
			else
			{
				writer.WriteLine($"return @{namingContext["result"]}!;");
			}
		}
	}

	private static void BuildMethodValidationHandlerWithParameters(IndentedTextWriter writer,
		IMethodSymbol method, ITypeSymbol typeToMock, VariableNamingContext namingContext,
		bool raiseEvents, bool shouldThrowDoesNotReturnException, uint memberIdentifier)
	{
		writer.WriteLine($"foreach (var @{namingContext["methodHandler"]} in @{namingContext["methodHandlers"]})");
		writer.WriteLine("{");
		writer.Indent++;

		for (var i = 0; i < method.Parameters.Length; i++)
		{
			var parameter = method.Parameters[i];
			var requiresNullable = parameter.RequiresForcedNullableAnnotation() ? "?" : string.Empty;
			var argType = parameter.Type.IsPointer() ?
				PointerArgTypeBuilder.GetProjectedFullyQualifiedName(parameter.Type, typeToMock) :
					parameter.Type.IsRefLikeType ?
						RefLikeArgTypeBuilder.GetProjectedFullyQualifiedName(parameter.Type, typeToMock) :
						$"global::Rocks.Argument<{parameter.Type.GetFullyQualifiedName()}{requiresNullable}>";

			if (i == 0)
			{
				writer.WriteLine(
					parameter.Type.TypeKind != TypeKind.TypeParameter ?
						$"if (global::System.Runtime.CompilerServices.Unsafe.As<{argType}>(@{namingContext["methodHandler"]}.Expectations[{i}]).IsValid(@{parameter.Name}){(i == method.Parameters.Length - 1 ? ")" : " &&")}" :
						$"if (((@{namingContext["methodHandler"]}.Expectations[{i}] as {argType})?.IsValid(@{parameter.Name}) ?? false){(i == method.Parameters.Length - 1 ? ")" : " &&")}");
			}
			else
			{
				if (i == 1)
				{
					writer.Indent++;
				}

				writer.WriteLine(
					parameter.Type.TypeKind != TypeKind.TypeParameter ?
						$"global::System.Runtime.CompilerServices.Unsafe.As<{argType}>(@{namingContext["methodHandler"]}.Expectations[{i}]).IsValid(@{parameter.Name}){(i == method.Parameters.Length - 1 ? ")" : " &&")}" :
						$"((@{namingContext["methodHandler"]}.Expectations[{i}] as {argType})?.IsValid(@{parameter.Name}) ?? false){(i == method.Parameters.Length - 1 ? ")" : " &&")}");

				if (i == method.Parameters.Length - 1)
				{
					writer.Indent--;
				}
			}
		}

		writer.WriteLine("{");
		writer.Indent++;

		MockMethodValueBuilder.BuildMethodHandler(
			writer, method, typeToMock, namingContext, raiseEvents, shouldThrowDoesNotReturnException, memberIdentifier);
		writer.Indent--;
		writer.WriteLine("}");

		writer.Indent--;
		writer.WriteLine("}");
	}

	private static void BuildMethodValidationHandlerNoParameters(IndentedTextWriter writer, IMethodSymbol method,
		ITypeSymbol typeToMock, VariableNamingContext namingContext, bool raiseEvents, bool shouldThrowDoesNotReturnException, uint memberIdentifier)
	{
		writer.WriteLine($"var @{namingContext["methodHandler"]} = @{namingContext["methodHandlers"]}[0];");
		MockMethodValueBuilder.BuildMethodHandler(
			writer, method, typeToMock, namingContext, raiseEvents, shouldThrowDoesNotReturnException, memberIdentifier);
	}
}