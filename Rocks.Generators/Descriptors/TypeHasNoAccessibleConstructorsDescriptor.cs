﻿using Microsoft.CodeAnalysis;
using System.Globalization;

namespace Rocks.Descriptors
{
	public static class TypeHasNoAccessibleConstructorsDescriptor
	{
		internal static Diagnostic Create(ITypeSymbol type) =>
			Diagnostic.Create(new DiagnosticDescriptor(
				TypeHasNoAccessibleConstructorsDescriptor.Id, TypeHasNoAccessibleConstructorsDescriptor.Title,
				string.Format(CultureInfo.CurrentCulture, TypeHasNoAccessibleConstructorsDescriptor.Message, type.Name),
				DescriptorConstants.Usage, DiagnosticSeverity.Info, true,
				helpLinkUri: HelpUrlBuilder.Build(
					TypeHasNoAccessibleConstructorsDescriptor.Id, TypeHasNoAccessibleConstructorsDescriptor.Title)), type.Locations[0]);

		public const string Id = "ROCK0004";
		public const string Message = "The type {0} has no constructors that are accessible.";
		public const string Title = "Type Has No Accessible Constructors";
	}
}