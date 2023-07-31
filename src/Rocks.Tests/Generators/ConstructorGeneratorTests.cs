﻿using Microsoft.CodeAnalysis.Testing;
using NUnit.Framework;

namespace Rocks.Tests.Generators;

public static class ConstructorGeneratorTests
{
	[Test]
	public static async Task GenerateWhenTypeArgumentsCreateDuplicateConstructorsAsync()
	{
		var code =
			"""
			using Rocks;

			#nullable enable

			public class AnyOf<T1, T2>
			{
				public AnyOf(T1 value) { }

				public AnyOf(T2 value) { }

				public virtual object GetValue() => new();			
			}

			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Create<AnyOf<object, object>>();
				}
			}
			""";


		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			Enumerable.Empty<(Type, string, string)>(),
			new[] { DiagnosticResult.CompilerError("ROCK12").WithSpan(5, 14, 5, 19) }).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateWhenTypeArgumentsDoNotCreateDuplicateConstructorsAsync()
	{
		var code =
			"""
			using Rocks;

			#nullable enable

			public class AnyOf<T1, T2>
			{
				public AnyOf(T1 value) { }

				public AnyOf(T2 value) { }

				public virtual object GetValue() => new();			
			}

			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Create<AnyOf<string, int>>();
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			internal static class CreateExpectationsOfAnyOfOfstring_intExtensions
			{
				internal static global::Rocks.Expectations.MethodExpectations<global::AnyOf<string, int>> Methods(this global::Rocks.Expectations.Expectations<global::AnyOf<string, int>> @self) =>
					new(@self);
				
				internal static global::AnyOf<string, int> Instance(this global::Rocks.Expectations.Expectations<global::AnyOf<string, int>> @self, string @value)
				{
					if (!@self.WasInstanceInvoked)
					{
						@self.WasInstanceInvoked = true;
						var @mock = new RockAnyOfOfstring_int(@self, @value);
						@self.MockType = @mock.GetType();
						return @mock;
					}
					else
					{
						throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
					}
				}
				internal static global::AnyOf<string, int> Instance(this global::Rocks.Expectations.Expectations<global::AnyOf<string, int>> @self, int @value)
				{
					if (!@self.WasInstanceInvoked)
					{
						@self.WasInstanceInvoked = true;
						var @mock = new RockAnyOfOfstring_int(@self, @value);
						@self.MockType = @mock.GetType();
						return @mock;
					}
					else
					{
						throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
					}
				}
				
				private sealed class RockAnyOfOfstring_int
					: global::AnyOf<string, int>
				{
					private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
					
					public RockAnyOfOfstring_int(global::Rocks.Expectations.Expectations<global::AnyOf<string, int>> @expectations, string @value)
						: base(@value)
					{
						this.handlers = @expectations.Handlers;
					}
					public RockAnyOfOfstring_int(global::Rocks.Expectations.Expectations<global::AnyOf<string, int>> @expectations, int @value)
						: base(@value)
					{
						this.handlers = @expectations.Handlers;
					}
					
					[global::Rocks.MemberIdentifier(0, "bool Equals(object? @obj)")]
					public override bool Equals(object? @obj)
					{
						if (this.handlers.TryGetValue(0, out var @methodHandlers))
						{
							foreach (var @methodHandler in @methodHandlers)
							{
								if (((global::Rocks.Argument<object?>)@methodHandler.Expectations[0]).IsValid(@obj))
								{
									@methodHandler.IncrementCallCount();
									var @result = @methodHandler.Method is not null ?
										((global::System.Func<object?, bool>)@methodHandler.Method)(@obj) :
										((global::Rocks.HandlerInformation<bool>)@methodHandler).ReturnValue;
									return @result!;
								}
							}
							
							throw new global::Rocks.Exceptions.ExpectationException("No handlers match for bool Equals(object? @obj)");
						}
						else
						{
							return base.Equals(@obj);
						}
					}
					
					[global::Rocks.MemberIdentifier(1, "int GetHashCode()")]
					public override int GetHashCode()
					{
						if (this.handlers.TryGetValue(1, out var @methodHandlers))
						{
							var @methodHandler = @methodHandlers[0];
							@methodHandler.IncrementCallCount();
							var @result = @methodHandler.Method is not null ?
								((global::System.Func<int>)@methodHandler.Method)() :
								((global::Rocks.HandlerInformation<int>)@methodHandler).ReturnValue;
							return @result!;
						}
						else
						{
							return base.GetHashCode();
						}
					}
					
					[global::Rocks.MemberIdentifier(2, "string? ToString()")]
					public override string? ToString()
					{
						if (this.handlers.TryGetValue(2, out var @methodHandlers))
						{
							var @methodHandler = @methodHandlers[0];
							@methodHandler.IncrementCallCount();
							var @result = @methodHandler.Method is not null ?
								((global::System.Func<string?>)@methodHandler.Method)() :
								((global::Rocks.HandlerInformation<string?>)@methodHandler).ReturnValue;
							return @result!;
						}
						else
						{
							return base.ToString();
						}
					}
					
					[global::Rocks.MemberIdentifier(3, "object GetValue()")]
					public override object GetValue()
					{
						if (this.handlers.TryGetValue(3, out var @methodHandlers))
						{
							var @methodHandler = @methodHandlers[0];
							@methodHandler.IncrementCallCount();
							var @result = @methodHandler.Method is not null ?
								((global::System.Func<object>)@methodHandler.Method)() :
								((global::Rocks.HandlerInformation<object>)@methodHandler).ReturnValue;
							return @result!;
						}
						else
						{
							return base.GetValue();
						}
					}
					
				}
			}
			
			internal static class MethodExpectationsOfAnyOfOfstring_intExtensions
			{
				internal static global::Rocks.MethodAdornments<global::AnyOf<string, int>, global::System.Func<object?, bool>, bool> Equals(this global::Rocks.Expectations.MethodExpectations<global::AnyOf<string, int>> @self, global::Rocks.Argument<object?> @obj)
				{
					global::System.ArgumentNullException.ThrowIfNull(@obj);
					return new global::Rocks.MethodAdornments<global::AnyOf<string, int>, global::System.Func<object?, bool>, bool>(@self.Add<bool>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @obj }));
				}
				internal static global::Rocks.MethodAdornments<global::AnyOf<string, int>, global::System.Func<int>, int> GetHashCode(this global::Rocks.Expectations.MethodExpectations<global::AnyOf<string, int>> @self) =>
					new global::Rocks.MethodAdornments<global::AnyOf<string, int>, global::System.Func<int>, int>(@self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				internal static global::Rocks.MethodAdornments<global::AnyOf<string, int>, global::System.Func<string?>, string?> ToString(this global::Rocks.Expectations.MethodExpectations<global::AnyOf<string, int>> @self) =>
					new global::Rocks.MethodAdornments<global::AnyOf<string, int>, global::System.Func<string?>, string?>(@self.Add<string?>(2, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				internal static global::Rocks.MethodAdornments<global::AnyOf<string, int>, global::System.Func<object>, object> GetValue(this global::Rocks.Expectations.MethodExpectations<global::AnyOf<string, int>> @self) =>
					new global::Rocks.MethodAdornments<global::AnyOf<string, int>, global::System.Func<object>, object>(@self.Add<object>(3, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "AnyOfstring, int_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}
}