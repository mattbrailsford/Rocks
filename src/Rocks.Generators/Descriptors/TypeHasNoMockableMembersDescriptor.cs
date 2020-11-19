﻿using Microsoft.CodeAnalysis;
using System.Globalization;

namespace Rocks.Descriptors
{
	public static class TypeHasNoMockableMembersDescriptor
	{
		internal static Diagnostic Create(ITypeSymbol type) =>
			Diagnostic.Create(new(TypeHasNoMockableMembersDescriptor.Id, TypeHasNoMockableMembersDescriptor.Title,
				string.Format(CultureInfo.CurrentCulture, TypeHasNoMockableMembersDescriptor.Message, 
					type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)),
				DescriptorConstants.Usage, DiagnosticSeverity.Error, true,
				helpLinkUri: HelpUrlBuilder.Build(
					TypeHasNoMockableMembersDescriptor.Id, TypeHasNoMockableMembersDescriptor.Title)), type.Locations[0]);

		public const string Id = "ROCK3";
		public const string Message = "The type {0} has no members that can be overriden";
		public const string Title = "Type Has No Mockable Members";
	}
}