﻿using Microsoft.CodeAnalysis;
using Rocks.Extensions;
using System.Globalization;

namespace Rocks.Diagnostics;

internal static class TypeHasNoMockableMembersDiagnostic
{
	internal static Diagnostic Create(ITypeSymbol type) =>
		Diagnostic.Create(new(TypeHasNoMockableMembersDiagnostic.Id, TypeHasNoMockableMembersDiagnostic.Title,
			string.Format(CultureInfo.CurrentCulture, TypeHasNoMockableMembersDiagnostic.Message,
				type.GetName()),
			DiagnosticConstants.Usage, DiagnosticSeverity.Error, true,
			helpLinkUri: HelpUrlBuilder.Build(
				TypeHasNoMockableMembersDiagnostic.Id, TypeHasNoMockableMembersDiagnostic.Title)),
			type.Locations.Length > 0 ? type.Locations[0] : null);

	internal const string Id = "ROCK3";
	internal const string Message = "The type {0} has no members that can be overriden";
	internal const string Title = "Type Has No Mockable Members";
}