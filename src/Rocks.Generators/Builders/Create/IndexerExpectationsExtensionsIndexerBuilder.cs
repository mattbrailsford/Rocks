﻿using Rocks.Extensions;
using System.CodeDom.Compiler;
using System.Linq;

namespace Rocks.Builders.Create
{
	internal static class IndexerExpectationsExtensionsIndexerBuilder
	{
		private static void BuildGetter(IndentedTextWriter writer, PropertyMockableResult result, uint memberIdentifier)
		{
			var property = result.Value;
			var propertyReturnValue = property.Type.GetName();
			var mockTypeName = result.MockType.GetName();
			var thisParameter = $"this IndexerGetterExpectations<{mockTypeName}> self";
			var adornmentsType = $"IndexerAdornments<{mockTypeName}, {DelegateBuilder.Build(property.Parameters, property.Type)}, {propertyReturnValue}>";
			var (returnValue, newAdornments) = (adornmentsType, $"new {adornmentsType}");

			var instanceParameters = string.Join(", ", thisParameter,
				string.Join(", ", property.GetMethod!.Parameters.Select(_ =>
				{
					return $"Arg<{_.Type.GetName()}> {_.Name}";
				})));

			writer.WriteLine($"internal static {returnValue} This({instanceParameters}) =>");
			writer.Indent++;

			var parameters = string.Join(", ", property.GetMethod!.Parameters.Select(
				_ => _.HasExplicitDefaultValue ? $"{_.Name}.Transform({_.ExplicitDefaultValue.GetDefaultValue()})" : _.Name));
			writer.WriteLine($"{newAdornments}(self.Add<{propertyReturnValue}>({memberIdentifier}, new List<Arg> {{ {parameters} }}));");
			writer.Indent--;
		}

		private static void BuildSetter(IndentedTextWriter writer, PropertyMockableResult result, uint memberIdentifier)
		{
			var property = result.Value;
			var mockTypeName = result.MockType.GetName();
			var thisParameter = $"this IndexerSetterExpectations<{mockTypeName}> self";
			var adornmentsType = $"IndexerAdornments<{mockTypeName}, {DelegateBuilder.Build(property.SetMethod!.Parameters)}>";
			var (returnValue, newAdornments) = (adornmentsType, $"new {adornmentsType}");

			var instanceParameters = string.Join(", ", thisParameter,
				string.Join(", ", property.SetMethod!.Parameters.Select(_ =>
				{
					return $"Arg<{_.Type.GetName()}> {_.Name}";
				})));

			writer.WriteLine($"internal static {returnValue} This({instanceParameters}) =>");
			writer.Indent++;

			var parameters = string.Join(", ", property.SetMethod!.Parameters.Select(
				_ => _.HasExplicitDefaultValue ? $"{_.Name}.Transform({_.ExplicitDefaultValue.GetDefaultValue()})" : _.Name));
			writer.WriteLine($"{newAdornments}(self.Add({memberIdentifier}, new List<Arg> {{ {parameters} }}));");
			writer.Indent--;
		}

		internal static void Build(IndentedTextWriter writer, PropertyMockableResult result, PropertyAccessor accessor)
		{
			var memberIdentifier = result.MemberIdentifier;

			if(accessor == PropertyAccessor.Get)
			{
				IndexerExpectationsExtensionsIndexerBuilder.BuildGetter(writer, result, memberIdentifier);
			}
			else
			{
				if (result.Accessors == PropertyAccessor.GetAndSet)
				{
					memberIdentifier++;
				}

				IndexerExpectationsExtensionsIndexerBuilder.BuildSetter(writer, result, memberIdentifier);
			}
		}
	}
}