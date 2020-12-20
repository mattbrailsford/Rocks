﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Rocks.Builders.Create;
using Rocks.Configuration;
using Rocks.Descriptors;
using Rocks.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Rocks
{
	[Generator]
	public sealed class RockCreateGenerator
		: ISourceGenerator
	{
		private static (ImmutableArray<Diagnostic> diagnostics, string? name, SourceText? text) GenerateMapping(
			ITypeSymbol typeToMock, IAssemblySymbol containingAssemblySymbol, SemanticModel model,
			ConfigurationValues configurationValues)
		{
			var information = new MockInformation(typeToMock, containingAssemblySymbol, model, configurationValues);

			if (!information.Diagnostics.Any(_ => _.Severity == DiagnosticSeverity.Error))
			{
				var builder = new RockCreateBuilder(information, configurationValues);
				return (builder.Diagnostics, builder.Name, builder.Text);
			}
			else
			{
				return (information.Diagnostics, null, null);
			}
		}

		/// <summary>
		/// I'm following the lead I got from StrongInject, though this 
		/// was recently taken out there. It's because of this: https://github.com/dotnet/roslyn/pull/46804
		/// I'm getting an exception and I'm getting no information in VS, so I have to keep this in
		/// until I <b>know</b> that full exception information will be shown.
		/// </summary>
		public void Execute(GeneratorExecutionContext context)
		{
			try
			{
				this.PrivateExecute(context);
			}
			catch (Exception e)
			{
				context.ReportDiagnostic(UnexpectedExceptionDescriptor.Create(e));
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void PrivateExecute(GeneratorExecutionContext context)
		{
			if (context.SyntaxReceiver is RockCreateReceiver receiver)
			{
				var compilation = context.Compilation;

				var typesToMock = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);

				foreach (var candidateInvocation in receiver.Candidates)
				{
					context.CancellationToken.ThrowIfCancellationRequested();
					var model = compilation.GetSemanticModel(candidateInvocation.SyntaxTree);

					var invocationSymbol = (IMethodSymbol)model.GetSymbolInfo(candidateInvocation).Symbol!;

					var rockCreateSymbol = compilation.GetTypeByMetadataName(typeof(Rock).FullName)!
						.GetMembers().Single(_ => _.Name == nameof(Rock.Create));

					if (rockCreateSymbol.Equals(invocationSymbol.ConstructedFrom, SymbolEqualityComparer.Default))
					{
						var typeToMock = invocationSymbol.TypeArguments[0];

						if (!typeToMock.ContainsDiagnostics())
						{
							if (typesToMock.Add(typeToMock))
							{
								var configurationValues = new ConfigurationValues(context, candidateInvocation.SyntaxTree);
								var (diagnostics, name, text) = RockCreateGenerator.GenerateMapping(
									typeToMock, compilation.Assembly, model, configurationValues);

								foreach (var diagnostic in diagnostics)
								{
									context.ReportDiagnostic(diagnostic);
								}

								if (name is not null && text is not null)
								{
									context.AddSource(name, text);
								}
							}
						}
					}
				}
			}
		}

		public void Initialize(GeneratorInitializationContext context) =>
			context.RegisterForSyntaxNotifications(() => new RockCreateReceiver());
	}
}