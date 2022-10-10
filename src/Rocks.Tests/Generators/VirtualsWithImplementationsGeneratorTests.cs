﻿using Microsoft.CodeAnalysis.Testing;
using NUnit.Framework;

namespace Rocks.Tests.Generators;

public static class VirtualsWithImplementationsGeneratorTests
{
	[Test]
	public static async Task GenerateForMethodWithParamsReturnsVoidAsync()
	{
		var code =
			"""
			using Rocks;
			using System;

			namespace MockTests
			{
				public class VoidMethodWithParams
				{
					public virtual void CallMe(params string[] values) { }
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<VoidMethodWithParams>();
					}
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			namespace MockTests
			{
				internal static class CreateExpectationsOfVoidMethodWithParamsExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.VoidMethodWithParams> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.VoidMethodWithParams> @self) =>
						new(@self);
					
					internal static global::MockTests.VoidMethodWithParams Instance(this global::Rocks.Expectations.Expectations<global::MockTests.VoidMethodWithParams> @self)
					{
						if (!@self.WasInstanceInvoked)
						{
							@self.WasInstanceInvoked = true;
							return new RockVoidMethodWithParams(@self);
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockVoidMethodWithParams
						: global::MockTests.VoidMethodWithParams
					{
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockVoidMethodWithParams(global::Rocks.Expectations.Expectations<global::MockTests.VoidMethodWithParams> @expectations) =>
							this.handlers = @expectations.Handlers;
						
						[global::Rocks.MemberIdentifier(0, "bool Equals(object? @obj)")]
						public override bool Equals(object? @obj)
						{
							if (this.handlers.TryGetValue(0, out var @methodHandlers))
							{
								foreach (var @methodHandler in @methodHandlers)
								{
									if (global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.Argument<object?>>(@methodHandler.Expectations[0]).IsValid(@obj))
									{
										var @result = @methodHandler.Method is not null ?
											global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<object?, bool>>(@methodHandler.Method)(@obj) :
											global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<bool>>(@methodHandler).ReturnValue;
										@methodHandler.IncrementCallCount();
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
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
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
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<string?>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<string?>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
								return @result!;
							}
							else
							{
								return base.ToString();
							}
						}
						
						[global::Rocks.MemberIdentifier(3, "void CallMe(params string[] @values)")]
						public override void CallMe(params string[] @values)
						{
							if (this.handlers.TryGetValue(3, out var @methodHandlers))
							{
								var @foundMatch = false;
								
								foreach (var @methodHandler in @methodHandlers)
								{
									if (global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.Argument<string[]>>(@methodHandler.Expectations[0]).IsValid(@values))
									{
										@foundMatch = true;
										
										if (@methodHandler.Method is not null)
										{
											global::System.Runtime.CompilerServices.Unsafe.As<global::System.Action<string[]>>(@methodHandler.Method)(@values);
										}
										
										@methodHandler.IncrementCallCount();
										break;
									}
								}
								
								if (!@foundMatch)
								{
									throw new global::Rocks.Exceptions.ExpectationException("No handlers match for void CallMe(params string[] @values)");
								}
							}
							else
							{
								base.CallMe(@values);
							}
						}
						
					}
				}
				
				internal static class MethodExpectationsOfVoidMethodWithParamsExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.VoidMethodWithParams, global::System.Func<object?, bool>, bool> Equals(this global::Rocks.Expectations.MethodExpectations<global::MockTests.VoidMethodWithParams> @self, global::Rocks.Argument<object?> @obj)
					{
						global::System.ArgumentNullException.ThrowIfNull(@obj);
						return new global::Rocks.MethodAdornments<global::MockTests.VoidMethodWithParams, global::System.Func<object?, bool>, bool>(@self.Add<bool>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @obj }));
					}
					internal static global::Rocks.MethodAdornments<global::MockTests.VoidMethodWithParams, global::System.Func<int>, int> GetHashCode(this global::Rocks.Expectations.MethodExpectations<global::MockTests.VoidMethodWithParams> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.VoidMethodWithParams, global::System.Func<int>, int>(@self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.VoidMethodWithParams, global::System.Func<string?>, string?> ToString(this global::Rocks.Expectations.MethodExpectations<global::MockTests.VoidMethodWithParams> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.VoidMethodWithParams, global::System.Func<string?>, string?>(@self.Add<string?>(2, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.VoidMethodWithParams, global::System.Action<string[]>> CallMe(this global::Rocks.Expectations.MethodExpectations<global::MockTests.VoidMethodWithParams> @self, global::Rocks.Argument<string[]> @values)
					{
						global::System.ArgumentNullException.ThrowIfNull(@values);
						return new global::Rocks.MethodAdornments<global::MockTests.VoidMethodWithParams, global::System.Action<string[]>>(@self.Add(3, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @values }));
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "VoidMethodWithParams_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateForMethodWithParamsReturnsValueAsync()
	{
		var code =
			"""
			using Rocks;
			using System;

			namespace MockTests
			{
				public class ValueMethodWithParams
				{
					public virtual int CallMe(params string[] values) => default;
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<ValueMethodWithParams>();
					}
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			namespace MockTests
			{
				internal static class CreateExpectationsOfValueMethodWithParamsExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.ValueMethodWithParams> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.ValueMethodWithParams> @self) =>
						new(@self);
					
					internal static global::MockTests.ValueMethodWithParams Instance(this global::Rocks.Expectations.Expectations<global::MockTests.ValueMethodWithParams> @self)
					{
						if (!@self.WasInstanceInvoked)
						{
							@self.WasInstanceInvoked = true;
							return new RockValueMethodWithParams(@self);
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockValueMethodWithParams
						: global::MockTests.ValueMethodWithParams
					{
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockValueMethodWithParams(global::Rocks.Expectations.Expectations<global::MockTests.ValueMethodWithParams> @expectations) =>
							this.handlers = @expectations.Handlers;
						
						[global::Rocks.MemberIdentifier(0, "bool Equals(object? @obj)")]
						public override bool Equals(object? @obj)
						{
							if (this.handlers.TryGetValue(0, out var @methodHandlers))
							{
								foreach (var @methodHandler in @methodHandlers)
								{
									if (global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.Argument<object?>>(@methodHandler.Expectations[0]).IsValid(@obj))
									{
										var @result = @methodHandler.Method is not null ?
											global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<object?, bool>>(@methodHandler.Method)(@obj) :
											global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<bool>>(@methodHandler).ReturnValue;
										@methodHandler.IncrementCallCount();
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
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
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
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<string?>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<string?>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
								return @result!;
							}
							else
							{
								return base.ToString();
							}
						}
						
						[global::Rocks.MemberIdentifier(3, "int CallMe(params string[] @values)")]
						public override int CallMe(params string[] @values)
						{
							if (this.handlers.TryGetValue(3, out var @methodHandlers))
							{
								foreach (var @methodHandler in @methodHandlers)
								{
									if (global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.Argument<string[]>>(@methodHandler.Expectations[0]).IsValid(@values))
									{
										var @result = @methodHandler.Method is not null ?
											global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<string[], int>>(@methodHandler.Method)(@values) :
											global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(@methodHandler).ReturnValue;
										@methodHandler.IncrementCallCount();
										return @result!;
									}
								}
								
								throw new global::Rocks.Exceptions.ExpectationException("No handlers match for int CallMe(params string[] @values)");
							}
							else
							{
								return base.CallMe(@values);
							}
						}
						
					}
				}
				
				internal static class MethodExpectationsOfValueMethodWithParamsExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.ValueMethodWithParams, global::System.Func<object?, bool>, bool> Equals(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ValueMethodWithParams> @self, global::Rocks.Argument<object?> @obj)
					{
						global::System.ArgumentNullException.ThrowIfNull(@obj);
						return new global::Rocks.MethodAdornments<global::MockTests.ValueMethodWithParams, global::System.Func<object?, bool>, bool>(@self.Add<bool>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @obj }));
					}
					internal static global::Rocks.MethodAdornments<global::MockTests.ValueMethodWithParams, global::System.Func<int>, int> GetHashCode(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ValueMethodWithParams> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.ValueMethodWithParams, global::System.Func<int>, int>(@self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.ValueMethodWithParams, global::System.Func<string?>, string?> ToString(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ValueMethodWithParams> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.ValueMethodWithParams, global::System.Func<string?>, string?>(@self.Add<string?>(2, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.ValueMethodWithParams, global::System.Func<string[], int>, int> CallMe(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ValueMethodWithParams> @self, global::Rocks.Argument<string[]> @values)
					{
						global::System.ArgumentNullException.ThrowIfNull(@values);
						return new global::Rocks.MethodAdornments<global::MockTests.ValueMethodWithParams, global::System.Func<string[], int>, int>(@self.Add<int>(3, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @values }));
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "ValueMethodWithParams_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateForInterfaceReturnsVoidAsync()
	{
		var code =
			"""
			using Rocks;
			using System;

			namespace MockTests
			{
				public interface IHaveImplementation
				{
					void Foo() { }
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<IHaveImplementation>();
					}
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			namespace MockTests
			{
				internal static class CreateExpectationsOfIHaveImplementationExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.IHaveImplementation> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.IHaveImplementation> @self) =>
						new(@self);
					
					internal static global::MockTests.IHaveImplementation Instance(this global::Rocks.Expectations.Expectations<global::MockTests.IHaveImplementation> @self)
					{
						if (!@self.WasInstanceInvoked)
						{
							@self.WasInstanceInvoked = true;
							return new RockIHaveImplementation(@self);
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockIHaveImplementation
						: global::MockTests.IHaveImplementation
					{
						private readonly global::MockTests.IHaveImplementation shimForIHaveImplementation;
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockIHaveImplementation(global::Rocks.Expectations.Expectations<global::MockTests.IHaveImplementation> @expectations) =>
							(this.handlers, this.shimForIHaveImplementation) = (@expectations.Handlers, new ShimIHaveImplementation43912089203065484038465384033944109657192660075(this));
						
						[global::Rocks.MemberIdentifier(0, "void Foo()")]
						public void Foo()
						{
							if (this.handlers.TryGetValue(0, out var @methodHandlers))
							{
								var @methodHandler = @methodHandlers[0];
								if (@methodHandler.Method is not null)
								{
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Action>(@methodHandler.Method)();
								}
								
								@methodHandler.IncrementCallCount();
							}
							else
							{
								this.shimForIHaveImplementation.Foo();
							}
						}
						
						
						private sealed class ShimIHaveImplementation43912089203065484038465384033944109657192660075
							: global::MockTests.IHaveImplementation
						{
							private readonly RockIHaveImplementation mock;
							
							public ShimIHaveImplementation43912089203065484038465384033944109657192660075(RockIHaveImplementation @mock) =>
								this.mock = @mock;
						}
					}
				}
				
				internal static class MethodExpectationsOfIHaveImplementationExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.IHaveImplementation, global::System.Action> Foo(this global::Rocks.Expectations.MethodExpectations<global::MockTests.IHaveImplementation> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.IHaveImplementation, global::System.Action>(@self.Add(0, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "IHaveImplementation_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateForInterfaceReturnsValueAsync()
	{
		var code =
			"""
			using Rocks;
			using System;

			namespace MockTests
			{
				public interface IHaveImplementation
				{
					int Foo() => 3;
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<IHaveImplementation>();
					}
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			namespace MockTests
			{
				internal static class CreateExpectationsOfIHaveImplementationExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.IHaveImplementation> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.IHaveImplementation> @self) =>
						new(@self);
					
					internal static global::MockTests.IHaveImplementation Instance(this global::Rocks.Expectations.Expectations<global::MockTests.IHaveImplementation> @self)
					{
						if (!@self.WasInstanceInvoked)
						{
							@self.WasInstanceInvoked = true;
							return new RockIHaveImplementation(@self);
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockIHaveImplementation
						: global::MockTests.IHaveImplementation
					{
						private readonly global::MockTests.IHaveImplementation shimForIHaveImplementation;
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockIHaveImplementation(global::Rocks.Expectations.Expectations<global::MockTests.IHaveImplementation> @expectations) =>
							(this.handlers, this.shimForIHaveImplementation) = (@expectations.Handlers, new ShimIHaveImplementation43912089203065484038465384033944109657192660075(this));
						
						[global::Rocks.MemberIdentifier(0, "int Foo()")]
						public int Foo()
						{
							if (this.handlers.TryGetValue(0, out var @methodHandlers))
							{
								var @methodHandler = @methodHandlers[0];
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
								return @result!;
							}
							else
							{
								return this.shimForIHaveImplementation.Foo();
							}
						}
						
						
						private sealed class ShimIHaveImplementation43912089203065484038465384033944109657192660075
							: global::MockTests.IHaveImplementation
						{
							private readonly RockIHaveImplementation mock;
							
							public ShimIHaveImplementation43912089203065484038465384033944109657192660075(RockIHaveImplementation @mock) =>
								this.mock = @mock;
						}
					}
				}
				
				internal static class MethodExpectationsOfIHaveImplementationExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.IHaveImplementation, global::System.Func<int>, int> Foo(this global::Rocks.Expectations.MethodExpectations<global::MockTests.IHaveImplementation> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.IHaveImplementation, global::System.Func<int>, int>(@self.Add<int>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "IHaveImplementation_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateForClassReturnsVoidAsync()
	{
		var code =
			"""
			using Rocks;
			using System;

			namespace MockTests
			{
				public class HaveImplementation
				{
					public virtual void Foo() { }
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<HaveImplementation>();
					}
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			namespace MockTests
			{
				internal static class CreateExpectationsOfHaveImplementationExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.HaveImplementation> @self) =>
						new(@self);
					
					internal static global::MockTests.HaveImplementation Instance(this global::Rocks.Expectations.Expectations<global::MockTests.HaveImplementation> @self)
					{
						if (!@self.WasInstanceInvoked)
						{
							@self.WasInstanceInvoked = true;
							return new RockHaveImplementation(@self);
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockHaveImplementation
						: global::MockTests.HaveImplementation
					{
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockHaveImplementation(global::Rocks.Expectations.Expectations<global::MockTests.HaveImplementation> @expectations) =>
							this.handlers = @expectations.Handlers;
						
						[global::Rocks.MemberIdentifier(0, "bool Equals(object? @obj)")]
						public override bool Equals(object? @obj)
						{
							if (this.handlers.TryGetValue(0, out var @methodHandlers))
							{
								foreach (var @methodHandler in @methodHandlers)
								{
									if (global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.Argument<object?>>(@methodHandler.Expectations[0]).IsValid(@obj))
									{
										var @result = @methodHandler.Method is not null ?
											global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<object?, bool>>(@methodHandler.Method)(@obj) :
											global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<bool>>(@methodHandler).ReturnValue;
										@methodHandler.IncrementCallCount();
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
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
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
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<string?>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<string?>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
								return @result!;
							}
							else
							{
								return base.ToString();
							}
						}
						
						[global::Rocks.MemberIdentifier(3, "void Foo()")]
						public override void Foo()
						{
							if (this.handlers.TryGetValue(3, out var @methodHandlers))
							{
								var @methodHandler = @methodHandlers[0];
								if (@methodHandler.Method is not null)
								{
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Action>(@methodHandler.Method)();
								}
								
								@methodHandler.IncrementCallCount();
							}
							else
							{
								base.Foo();
							}
						}
						
					}
				}
				
				internal static class MethodExpectationsOfHaveImplementationExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<object?, bool>, bool> Equals(this global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> @self, global::Rocks.Argument<object?> @obj)
					{
						global::System.ArgumentNullException.ThrowIfNull(@obj);
						return new global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<object?, bool>, bool>(@self.Add<bool>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @obj }));
					}
					internal static global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<int>, int> GetHashCode(this global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<int>, int>(@self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<string?>, string?> ToString(this global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<string?>, string?>(@self.Add<string?>(2, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Action> Foo(this global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Action>(@self.Add(3, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "HaveImplementation_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateForClassReturnsValueAsync()
	{
		var code =
			"""
			using Rocks;
			using System;

			namespace MockTests
			{
				public class HaveImplementation
				{
					public virtual int Foo() => 3;
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<HaveImplementation>();
					}
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			namespace MockTests
			{
				internal static class CreateExpectationsOfHaveImplementationExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.HaveImplementation> @self) =>
						new(@self);
					
					internal static global::MockTests.HaveImplementation Instance(this global::Rocks.Expectations.Expectations<global::MockTests.HaveImplementation> @self)
					{
						if (!@self.WasInstanceInvoked)
						{
							@self.WasInstanceInvoked = true;
							return new RockHaveImplementation(@self);
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockHaveImplementation
						: global::MockTests.HaveImplementation
					{
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockHaveImplementation(global::Rocks.Expectations.Expectations<global::MockTests.HaveImplementation> @expectations) =>
							this.handlers = @expectations.Handlers;
						
						[global::Rocks.MemberIdentifier(0, "bool Equals(object? @obj)")]
						public override bool Equals(object? @obj)
						{
							if (this.handlers.TryGetValue(0, out var @methodHandlers))
							{
								foreach (var @methodHandler in @methodHandlers)
								{
									if (global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.Argument<object?>>(@methodHandler.Expectations[0]).IsValid(@obj))
									{
										var @result = @methodHandler.Method is not null ?
											global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<object?, bool>>(@methodHandler.Method)(@obj) :
											global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<bool>>(@methodHandler).ReturnValue;
										@methodHandler.IncrementCallCount();
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
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
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
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<string?>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<string?>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
								return @result!;
							}
							else
							{
								return base.ToString();
							}
						}
						
						[global::Rocks.MemberIdentifier(3, "int Foo()")]
						public override int Foo()
						{
							if (this.handlers.TryGetValue(3, out var @methodHandlers))
							{
								var @methodHandler = @methodHandlers[0];
								var @result = @methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(@methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(@methodHandler).ReturnValue;
								@methodHandler.IncrementCallCount();
								return @result!;
							}
							else
							{
								return base.Foo();
							}
						}
						
					}
				}
				
				internal static class MethodExpectationsOfHaveImplementationExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<object?, bool>, bool> Equals(this global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> @self, global::Rocks.Argument<object?> @obj)
					{
						global::System.ArgumentNullException.ThrowIfNull(@obj);
						return new global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<object?, bool>, bool>(@self.Add<bool>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @obj }));
					}
					internal static global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<int>, int> GetHashCode(this global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<int>, int>(@self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<string?>, string?> ToString(this global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<string?>, string?>(@self.Add<string?>(2, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<int>, int> Foo(this global::Rocks.Expectations.MethodExpectations<global::MockTests.HaveImplementation> @self) =>
						new global::Rocks.MethodAdornments<global::MockTests.HaveImplementation, global::System.Func<int>, int>(@self.Add<int>(3, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "HaveImplementation_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}
}