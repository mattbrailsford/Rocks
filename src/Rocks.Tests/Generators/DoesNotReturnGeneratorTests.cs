﻿using Microsoft.CodeAnalysis.Testing;
using NUnit.Framework;

namespace Rocks.Tests.Generators;

public static class DoesNotReturnGeneratorTests
{
	[Test]
	public static async Task GenerateClassCreateAsync()
	{
		var code =
			"""
			using Rocks;
			using System;
			using System.Diagnostics.CodeAnalysis;

			namespace MockTests
			{
				public class ClassTest
				{
					[DoesNotReturn]
					public virtual void VoidMethod() => throw new NotSupportedException();

					[DoesNotReturn]
					public virtual int IntMethod() => throw new NotSupportedException();
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<ClassTest>();
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
				internal static class CreateExpectationsOfClassTestExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.ClassTest> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.ClassTest> self) =>
						new(self);
					
					internal static global::MockTests.ClassTest Instance(this global::Rocks.Expectations.Expectations<global::MockTests.ClassTest> self)
					{
						if (!self.WasInstanceInvoked)
						{
							self.WasInstanceInvoked = true;
							return new RockClassTest(self);
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockClassTest
						: global::MockTests.ClassTest
					{
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockClassTest(global::Rocks.Expectations.Expectations<global::MockTests.ClassTest> expectations) =>
							this.handlers = expectations.Handlers;
						
						[global::Rocks.MemberIdentifier(0, "bool Equals(object? obj)")]
						public override bool Equals(object? obj)
						{
							if (this.handlers.TryGetValue(0, out var methodHandlers))
							{
								foreach (var methodHandler in methodHandlers)
								{
									if (global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.Argument<object?>>(methodHandler.Expectations[0]).IsValid(obj))
									{
										var result = methodHandler.Method is not null ?
											global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<object?, bool>>(methodHandler.Method)(obj) :
											global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<bool>>(methodHandler).ReturnValue;
										methodHandler.IncrementCallCount();
										return result!;
									}
								}
								
								throw new global::Rocks.Exceptions.ExpectationException("No handlers match for bool Equals(object? obj)");
							}
							else
							{
								return base.Equals(obj);
							}
						}
						
						[global::Rocks.MemberIdentifier(1, "int GetHashCode()")]
						public override int GetHashCode()
						{
							if (this.handlers.TryGetValue(1, out var methodHandlers))
							{
								var methodHandler = methodHandlers[0];
								var result = methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(methodHandler).ReturnValue;
								methodHandler.IncrementCallCount();
								return result!;
							}
							else
							{
								return base.GetHashCode();
							}
						}
						
						[global::Rocks.MemberIdentifier(2, "string? ToString()")]
						public override string? ToString()
						{
							if (this.handlers.TryGetValue(2, out var methodHandlers))
							{
								var methodHandler = methodHandlers[0];
								var result = methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<string?>>(methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<string?>>(methodHandler).ReturnValue;
								methodHandler.IncrementCallCount();
								return result!;
							}
							else
							{
								return base.ToString();
							}
						}
						
						[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						[global::Rocks.MemberIdentifier(3, "void VoidMethod()")]
						public override void VoidMethod()
						{
							if (this.handlers.TryGetValue(3, out var methodHandlers))
							{
								var methodHandler = methodHandlers[0];
								if (methodHandler.Method is not null)
								{
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Action>(methodHandler.Method)();
								}
								
								methodHandler.IncrementCallCount();
								throw new global::Rocks.Exceptions.DoesNotReturnException();
							}
							else
							{
								base.VoidMethod();
							}
						}
						
						[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						[global::Rocks.MemberIdentifier(4, "int IntMethod()")]
						public override int IntMethod()
						{
							if (this.handlers.TryGetValue(4, out var methodHandlers))
							{
								var methodHandler = methodHandlers[0];
								_ = methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(methodHandler).ReturnValue;
								methodHandler.IncrementCallCount();
								throw new global::Rocks.Exceptions.DoesNotReturnException();
							}
							else
							{
								return base.IntMethod();
							}
						}
						
					}
				}
				
				internal static class MethodExpectationsOfClassTestExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Func<object?, bool>, bool> Equals(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ClassTest> self, global::Rocks.Argument<object?> obj)
					{
						global::System.ArgumentNullException.ThrowIfNull(obj);
						return new global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Func<object?, bool>, bool>(self.Add<bool>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { obj }));
					}
					internal static global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Func<int>, int> GetHashCode(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ClassTest> self) =>
						new global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Func<int>, int>(self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Func<string?>, string?> ToString(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ClassTest> self) =>
						new global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Func<string?>, string?>(self.Add<string?>(2, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Action> VoidMethod(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ClassTest> self) =>
						new global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Action>(self.Add(3, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Func<int>, int> IntMethod(this global::Rocks.Expectations.MethodExpectations<global::MockTests.ClassTest> self) =>
						new global::Rocks.MethodAdornments<global::MockTests.ClassTest, global::System.Func<int>, int>(self.Add<int>(4, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "ClassTest_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateClassMakeAsync()
	{
		var code =
			"""
			using Rocks;
			using System;
			using System.Diagnostics.CodeAnalysis;

			namespace MockTests
			{
				public class ClassTest
				{
					[DoesNotReturn]
					public virtual void VoidMethod() => throw new NotSupportedException();

					[DoesNotReturn]
					public virtual int IntMethod() => throw new NotSupportedException();
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Make<ClassTest>();
					}
				}
			}
			""";

		var generatedCode =
			"""
			#nullable enable
			
			namespace MockTests
			{
				internal static class MakeExpectationsOfClassTestExtensions
				{
					internal static global::MockTests.ClassTest Instance(this global::Rocks.MakeGeneration<global::MockTests.ClassTest> self) =>
						new RockClassTest();
					
					private sealed class RockClassTest
						: global::MockTests.ClassTest
					{
						public RockClassTest() { }
						
						public override bool Equals(object? obj)
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
						[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						public override void VoidMethod()
						{
							throw new global::Rocks.Exceptions.DoesNotReturnException();
						}
						[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						public override int IntMethod()
						{
							throw new global::Rocks.Exceptions.DoesNotReturnException();
						}
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockMakeGenerator>(code,
			new[] { (typeof(RockMakeGenerator), "ClassTest_Rock_Make.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateInterfaceCreateAsync()
	{
		var code =
			"""
			using Rocks;
			using System;
			using System.Diagnostics.CodeAnalysis;

			namespace MockTests
			{
				public interface IInterfaceTest
				{
					[DoesNotReturn]
					void VoidMethod();

					[DoesNotReturn]
					int IntMethod();
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<IInterfaceTest>();
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
				internal static class CreateExpectationsOfIInterfaceTestExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.IInterfaceTest> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.IInterfaceTest> self) =>
						new(self);
					
					internal static global::MockTests.IInterfaceTest Instance(this global::Rocks.Expectations.Expectations<global::MockTests.IInterfaceTest> self)
					{
						if (!self.WasInstanceInvoked)
						{
							self.WasInstanceInvoked = true;
							return new RockIInterfaceTest(self);
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockIInterfaceTest
						: global::MockTests.IInterfaceTest
					{
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockIInterfaceTest(global::Rocks.Expectations.Expectations<global::MockTests.IInterfaceTest> expectations) =>
							this.handlers = expectations.Handlers;
						
						[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						[global::Rocks.MemberIdentifier(0, "void VoidMethod()")]
						public void VoidMethod()
						{
							if (this.handlers.TryGetValue(0, out var methodHandlers))
							{
								var methodHandler = methodHandlers[0];
								if (methodHandler.Method is not null)
								{
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Action>(methodHandler.Method)();
								}
								
								methodHandler.IncrementCallCount();
								throw new global::Rocks.Exceptions.DoesNotReturnException();
							}
							else
							{
								throw new global::Rocks.Exceptions.ExpectationException("No handlers were found for void VoidMethod()");
							}
						}
						
						[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						[global::Rocks.MemberIdentifier(1, "int IntMethod()")]
						public int IntMethod()
						{
							if (this.handlers.TryGetValue(1, out var methodHandlers))
							{
								var methodHandler = methodHandlers[0];
								_ = methodHandler.Method is not null ?
									global::System.Runtime.CompilerServices.Unsafe.As<global::System.Func<int>>(methodHandler.Method)() :
									global::System.Runtime.CompilerServices.Unsafe.As<global::Rocks.HandlerInformation<int>>(methodHandler).ReturnValue;
								methodHandler.IncrementCallCount();
								throw new global::Rocks.Exceptions.DoesNotReturnException();
							}
							
							throw new global::Rocks.Exceptions.ExpectationException("No handlers were found for int IntMethod()");
						}
						
					}
				}
				
				internal static class MethodExpectationsOfIInterfaceTestExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.IInterfaceTest, global::System.Action> VoidMethod(this global::Rocks.Expectations.MethodExpectations<global::MockTests.IInterfaceTest> self) =>
						new global::Rocks.MethodAdornments<global::MockTests.IInterfaceTest, global::System.Action>(self.Add(0, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
					internal static global::Rocks.MethodAdornments<global::MockTests.IInterfaceTest, global::System.Func<int>, int> IntMethod(this global::Rocks.Expectations.MethodExpectations<global::MockTests.IInterfaceTest> self) =>
						new global::Rocks.MethodAdornments<global::MockTests.IInterfaceTest, global::System.Func<int>, int>(self.Add<int>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>()));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "IInterfaceTest_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task GenerateInterfaceMakeAsync()
	{
		var code =
			"""
			using Rocks;
			using System;
			using System.Diagnostics.CodeAnalysis;

			namespace MockTests
			{
				public interface IInterfaceTest
				{
					[DoesNotReturn]
					void VoidMethod();

					[DoesNotReturn]
					int IntMethod();
				}

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Make<IInterfaceTest>();
					}
				}
			}
			""";

		var generatedCode =
			"""
			#nullable enable
			
			namespace MockTests
			{
				internal static class MakeExpectationsOfIInterfaceTestExtensions
				{
					internal static global::MockTests.IInterfaceTest Instance(this global::Rocks.MakeGeneration<global::MockTests.IInterfaceTest> self) =>
						new RockIInterfaceTest();
					
					private sealed class RockIInterfaceTest
						: global::MockTests.IInterfaceTest
					{
						public RockIInterfaceTest() { }
						
						[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						public void VoidMethod()
						{
							throw new global::Rocks.Exceptions.DoesNotReturnException();
						}
						[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						public int IntMethod()
						{
							throw new global::Rocks.Exceptions.DoesNotReturnException();
						}
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockMakeGenerator>(code,
			new[] { (typeof(RockMakeGenerator), "IInterfaceTest_Rock_Make.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}
}