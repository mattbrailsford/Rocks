﻿using Microsoft.CodeAnalysis.Testing;
using NUnit.Framework;

namespace Rocks.Tests.Generators;

public static class NullableAnnotationTests
{
	[Test]
	public static async Task GenerateCreateWithConstructorWhenParameterWithNullDefaultIsNotAnnotatedAsync()
	{
		var code =
			"""
			using Rocks;

			public class NeedNullable
			{
				public NeedNullable(object initializationData = null) { }
			}
			
			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Create<NeedNullable>();
				}
			}
			""";

		var generatedCode =
			"""
			// <auto-generated/>
			
			#nullable enable
			
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			
			internal static class CreateExpectationsOfNeedNullableExtensions
			{
				internal static global::Rocks.Expectations.MethodExpectations<global::NeedNullable> Methods(this global::Rocks.Expectations.Expectations<global::NeedNullable> @self) =>
					new(@self);
				
				internal static global::NeedNullable Instance(this global::Rocks.Expectations.Expectations<global::NeedNullable> @self, object? @initializationData)
				{
					if (!@self.WasInstanceInvoked)
					{
						@self.WasInstanceInvoked = true;
						var @mock = new RockNeedNullable(@self, @initializationData!);
						@self.MockType = @mock.GetType();
						return @mock;
					}
					else
					{
						throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
					}
				}
				
				private sealed class RockNeedNullable
					: global::NeedNullable
				{
					private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
					
					public RockNeedNullable(global::Rocks.Expectations.Expectations<global::NeedNullable> @expectations, object? @initializationData)
						: base(@initializationData!)
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
					
				}
			}
			
			internal static class MethodExpectationsOfNeedNullableExtensions
			{
				internal static global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<object?, bool>, bool> Equals(this global::Rocks.Expectations.MethodExpectations<global::NeedNullable> @self, global::Rocks.Argument<object?> @obj)
				{
					global::System.ArgumentNullException.ThrowIfNull(@obj);
					return new global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<object?, bool>, bool>(@self.Add<bool>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @obj }));
				}
				internal static global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<int>, int> GetHashCode(this global::Rocks.Expectations.MethodExpectations<global::NeedNullable> @self) =>
					new global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<int>, int>(@self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				internal static global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<string?>, string?> ToString(this global::Rocks.Expectations.MethodExpectations<global::NeedNullable> @self) =>
					new global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<string?>, string?>(@self.Add<string?>(2, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "NeedNullable_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateMakeWithConstructorWhenParameterWithNullDefaultIsNotAnnotatedAsync()
	{
		var code =
			"""
			using Rocks;

			public class NeedNullable
			{
				public NeedNullable(object initializationData = null) { }
			}
			
			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Make<NeedNullable>();
				}
			}
			""";

		var generatedCode =
			"""
			// <auto-generated/>
			
			#nullable enable
			
			internal static class MakeExpectationsOfNeedNullableExtensions
			{
				internal static global::NeedNullable Instance(this global::Rocks.MakeGeneration<global::NeedNullable> @self, object? @initializationData)
				{
					return new RockNeedNullable(@initializationData!);
				}
				
				private sealed class RockNeedNullable
					: global::NeedNullable
				{
					public RockNeedNullable(object? @initializationData)
						: base(@initializationData!)
					{
					}
					
					public override bool Equals(object? @obj)
					{
						return default!;
					}
					public override int GetHashCode()
					{
						return default!;
					}
					public override string? ToString()
					{
						return default!;
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockMakeGenerator>(code,
			new[] { (typeof(RockMakeGenerator), "NeedNullable_Rock_Make.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateCreateWithMethodsWhenParameterWithNullDefaultIsNotAnnotatedAsync()
	{
		var code =
			"""
			using Rocks;

			public class NeedNullable
			{
			    public virtual int IntReturn(object initializationData = null) => 0;
			    public virtual void VoidReturn(object initializationData = null) { }
			}
			
			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Create<NeedNullable>();
				}
			}
			""";

		var generatedCode =
			"""
			// <auto-generated/>
			
			#nullable enable
			
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			
			internal static class CreateExpectationsOfNeedNullableExtensions
			{
				internal static global::Rocks.Expectations.MethodExpectations<global::NeedNullable> Methods(this global::Rocks.Expectations.Expectations<global::NeedNullable> @self) =>
					new(@self);
				
				internal static global::NeedNullable Instance(this global::Rocks.Expectations.Expectations<global::NeedNullable> @self)
				{
					if (!@self.WasInstanceInvoked)
					{
						@self.WasInstanceInvoked = true;
						var @mock = new RockNeedNullable(@self);
						@self.MockType = @mock.GetType();
						return @mock;
					}
					else
					{
						throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
					}
				}
				
				private sealed class RockNeedNullable
					: global::NeedNullable
				{
					private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
					
					public RockNeedNullable(global::Rocks.Expectations.Expectations<global::NeedNullable> @expectations)
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
					
					[global::Rocks.MemberIdentifier(3, "int IntReturn(object? @initializationData)")]
					public override int IntReturn(object? @initializationData = null)
					{
						if (this.handlers.TryGetValue(3, out var @methodHandlers))
						{
							foreach (var @methodHandler in @methodHandlers)
							{
								if (((global::Rocks.Argument<object?>)@methodHandler.Expectations[0]).IsValid(@initializationData))
								{
									@methodHandler.IncrementCallCount();
									var @result = @methodHandler.Method is not null ?
										((global::System.Func<object?, int>)@methodHandler.Method)(@initializationData) :
										((global::Rocks.HandlerInformation<int>)@methodHandler).ReturnValue;
									return @result!;
								}
							}
							
							throw new global::Rocks.Exceptions.ExpectationException("No handlers match for int IntReturn(object? @initializationData = null)");
						}
						else
						{
							return base.IntReturn(@initializationData!);
						}
					}
					
					[global::Rocks.MemberIdentifier(4, "void VoidReturn(object? @initializationData)")]
					public override void VoidReturn(object? @initializationData = null)
					{
						if (this.handlers.TryGetValue(4, out var @methodHandlers))
						{
							var @foundMatch = false;
							
							foreach (var @methodHandler in @methodHandlers)
							{
								if (((global::Rocks.Argument<object?>)@methodHandler.Expectations[0]).IsValid(@initializationData))
								{
									@foundMatch = true;
									
									@methodHandler.IncrementCallCount();
									if (@methodHandler.Method is not null)
									{
										((global::System.Action<object?>)@methodHandler.Method)(@initializationData);
									}
									break;
								}
							}
							
							if (!@foundMatch)
							{
								throw new global::Rocks.Exceptions.ExpectationException("No handlers match for void VoidReturn(object? @initializationData = null)");
							}
						}
						else
						{
							base.VoidReturn(@initializationData!);
						}
					}
					
				}
			}
			
			internal static class MethodExpectationsOfNeedNullableExtensions
			{
				internal static global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<object?, bool>, bool> Equals(this global::Rocks.Expectations.MethodExpectations<global::NeedNullable> @self, global::Rocks.Argument<object?> @obj)
				{
					global::System.ArgumentNullException.ThrowIfNull(@obj);
					return new global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<object?, bool>, bool>(@self.Add<bool>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @obj }));
				}
				internal static global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<int>, int> GetHashCode(this global::Rocks.Expectations.MethodExpectations<global::NeedNullable> @self) =>
					new global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<int>, int>(@self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				internal static global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<string?>, string?> ToString(this global::Rocks.Expectations.MethodExpectations<global::NeedNullable> @self) =>
					new global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<string?>, string?>(@self.Add<string?>(2, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				internal static global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<object?, int>, int> IntReturn(this global::Rocks.Expectations.MethodExpectations<global::NeedNullable> @self, global::Rocks.Argument<object?> @initializationData)
				{
					global::System.ArgumentNullException.ThrowIfNull(@initializationData);
					return new global::Rocks.MethodAdornments<global::NeedNullable, global::System.Func<object?, int>, int>(@self.Add<int>(3, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @initializationData.Transform(null) }));
				}
				internal static global::Rocks.MethodAdornments<global::NeedNullable, global::System.Action<object?>> VoidReturn(this global::Rocks.Expectations.MethodExpectations<global::NeedNullable> @self, global::Rocks.Argument<object?> @initializationData)
				{
					global::System.ArgumentNullException.ThrowIfNull(@initializationData);
					return new global::Rocks.MethodAdornments<global::NeedNullable, global::System.Action<object?>>(@self.Add(4, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @initializationData.Transform(null) }));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "NeedNullable_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateMakeWithMethodsWhenParameterWithNullDefaultIsNotAnnotatedAsync()
	{
		var code =
			"""
			using Rocks;

			public class NeedNullable
			{
			    public virtual int IntReturn(object initializationData = null) => 0;
			    public virtual void VoidReturn(object initializationData = null) { }
			}
			
			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Make<NeedNullable>();
				}
			}
			""";

		var generatedCode =
			"""
			// <auto-generated/>
			
			#nullable enable
			
			internal static class MakeExpectationsOfNeedNullableExtensions
			{
				internal static global::NeedNullable Instance(this global::Rocks.MakeGeneration<global::NeedNullable> @self)
				{
					return new RockNeedNullable();
				}
				
				private sealed class RockNeedNullable
					: global::NeedNullable
				{
					public RockNeedNullable()
					{
					}
					
					public override bool Equals(object? @obj)
					{
						return default!;
					}
					public override int GetHashCode()
					{
						return default!;
					}
					public override string? ToString()
					{
						return default!;
					}
					public override int IntReturn(object? @initializationData = null)
					{
						return default!;
					}
					public override void VoidReturn(object? @initializationData = null)
					{
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockMakeGenerator>(code,
			new[] { (typeof(RockMakeGenerator), "NeedNullable_Rock_Make.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}
}