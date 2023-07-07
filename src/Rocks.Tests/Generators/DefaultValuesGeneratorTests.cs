﻿using Microsoft.CodeAnalysis.Testing;
using NUnit.Framework;

namespace Rocks.Tests.Generators;

public static class DefaultValuesGeneratorTests
{
	[Test]
	public static async Task CreateWhenExplicitImplementationHasDefaultValuesAsync()
	{
		var code =
			"""
			using Rocks;
			using System;
			using System.Threading.Tasks;

			namespace MockTests
			{
				public struct SomeStruct { }

				public interface IRequest<T>
					where T : class
				{
					Task<T> Send(object values, SomeStruct someStruct = default);
					Task Send(T message, SomeStruct someStruct = default);
				}	

				public static class Test
				{
					public static void Generate()
					{
						var rock = Rock.Create<IRequest<object>>();
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
				internal static class CreateExpectationsOfIRequestOfobjectExtensions
				{
					internal static global::Rocks.Expectations.MethodExpectations<global::MockTests.IRequest<object>> Methods(this global::Rocks.Expectations.Expectations<global::MockTests.IRequest<object>> @self) =>
						new(@self);
					
					internal static global::Rocks.Expectations.ExplicitMethodExpectations<global::MockTests.IRequest<object>, global::MockTests.IRequest<object>> ExplicitMethodsForIRequestOfobject(this global::Rocks.Expectations.Expectations<global::MockTests.IRequest<object>> @self) =>
						new(@self);
					
					internal static global::MockTests.IRequest<object> Instance(this global::Rocks.Expectations.Expectations<global::MockTests.IRequest<object>> @self)
					{
						if (!@self.WasInstanceInvoked)
						{
							@self.WasInstanceInvoked = true;
							var @mock = new RockIRequestOfobject(@self);
							@self.MockType = @mock.GetType();
							return @mock;
						}
						else
						{
							throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
						}
					}
					
					private sealed class RockIRequestOfobject
						: global::MockTests.IRequest<object>
					{
						private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
						
						public RockIRequestOfobject(global::Rocks.Expectations.Expectations<global::MockTests.IRequest<object>> @expectations)
						{
							this.handlers = @expectations.Handlers;
						}
						
						[global::Rocks.MemberIdentifier(0, "global::System.Threading.Tasks.Task<object> Send(object @values, global::MockTests.SomeStruct @someStruct)")]
						public global::System.Threading.Tasks.Task<object> Send(object @values, global::MockTests.SomeStruct @someStruct = default)
						{
							if (this.handlers.TryGetValue(0, out var @methodHandlers))
							{
								foreach (var @methodHandler in @methodHandlers)
								{
									if (((global::Rocks.Argument<object>)@methodHandler.Expectations[0]).IsValid(@values) &&
										((global::Rocks.Argument<global::MockTests.SomeStruct>)@methodHandler.Expectations[1]).IsValid(@someStruct))
									{
										@methodHandler.IncrementCallCount();
										var @result = @methodHandler.Method is not null ?
											((global::System.Func<object, global::MockTests.SomeStruct, global::System.Threading.Tasks.Task<object>>)@methodHandler.Method)(@values, @someStruct) :
											((global::Rocks.HandlerInformation<global::System.Threading.Tasks.Task<object>>)@methodHandler).ReturnValue;
										return @result!;
									}
								}
								
								throw new global::Rocks.Exceptions.ExpectationException("No handlers match for global::System.Threading.Tasks.Task<object> Send(object @values, global::MockTests.SomeStruct @someStruct = default)");
							}
							
							throw new global::Rocks.Exceptions.ExpectationException("No handlers were found for global::System.Threading.Tasks.Task<object> Send(object @values, global::MockTests.SomeStruct @someStruct = default)");
						}
						
						[global::Rocks.MemberIdentifier(1, "global::System.Threading.Tasks.Task global::MockTests.IRequest<object>.Send(object @message, global::MockTests.SomeStruct @someStruct)")]
						global::System.Threading.Tasks.Task global::MockTests.IRequest<object>.Send(object @message, global::MockTests.SomeStruct @someStruct)
						{
							if (this.handlers.TryGetValue(1, out var @methodHandlers))
							{
								foreach (var @methodHandler in @methodHandlers)
								{
									if (((global::Rocks.Argument<object>)@methodHandler.Expectations[0]).IsValid(@message) &&
										((global::Rocks.Argument<global::MockTests.SomeStruct>)@methodHandler.Expectations[1]).IsValid(@someStruct))
									{
										@methodHandler.IncrementCallCount();
										var @result = @methodHandler.Method is not null ?
											((global::System.Func<object, global::MockTests.SomeStruct, global::System.Threading.Tasks.Task>)@methodHandler.Method)(@message, @someStruct) :
											((global::Rocks.HandlerInformation<global::System.Threading.Tasks.Task>)@methodHandler).ReturnValue;
										return @result!;
									}
								}
								
								throw new global::Rocks.Exceptions.ExpectationException("No handlers match for global::System.Threading.Tasks.Task global::MockTests.IRequest<object>.Send(object @message, global::MockTests.SomeStruct @someStruct)");
							}
							
							throw new global::Rocks.Exceptions.ExpectationException("No handlers were found for global::System.Threading.Tasks.Task global::MockTests.IRequest<object>.Send(object @message, global::MockTests.SomeStruct @someStruct)");
						}
						
					}
				}
				
				internal static class MethodExpectationsOfIRequestOfobjectExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.IRequest<object>, global::System.Func<object, global::MockTests.SomeStruct, global::System.Threading.Tasks.Task<object>>, global::System.Threading.Tasks.Task<object>> Send(this global::Rocks.Expectations.MethodExpectations<global::MockTests.IRequest<object>> @self, global::Rocks.Argument<object> @values, global::Rocks.Argument<global::MockTests.SomeStruct> @someStruct)
					{
						global::System.ArgumentNullException.ThrowIfNull(@values);
						global::System.ArgumentNullException.ThrowIfNull(@someStruct);
						return new global::Rocks.MethodAdornments<global::MockTests.IRequest<object>, global::System.Func<object, global::MockTests.SomeStruct, global::System.Threading.Tasks.Task<object>>, global::System.Threading.Tasks.Task<object>>(@self.Add<global::System.Threading.Tasks.Task<object>>(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(2) { @values, @someStruct.Transform(default) }));
					}
				}
				internal static class ExplicitMethodExpectationsOfIRequestOfobjectForIRequestOfobjectExtensions
				{
					internal static global::Rocks.MethodAdornments<global::MockTests.IRequest<object>, global::System.Func<object, global::MockTests.SomeStruct, global::System.Threading.Tasks.Task>, global::System.Threading.Tasks.Task> Send(this global::Rocks.Expectations.ExplicitMethodExpectations<global::MockTests.IRequest<object>, global::MockTests.IRequest<object>> @self, global::Rocks.Argument<object> @message, global::Rocks.Argument<global::MockTests.SomeStruct> @someStruct)
					{
						global::System.ArgumentNullException.ThrowIfNull(@message);
						global::System.ArgumentNullException.ThrowIfNull(@someStruct);
						return new global::Rocks.MethodAdornments<global::MockTests.IRequest<object>, global::System.Func<object, global::MockTests.SomeStruct, global::System.Threading.Tasks.Task>, global::System.Threading.Tasks.Task>(@self.Add<global::System.Threading.Tasks.Task>(1, new global::System.Collections.Generic.List<global::Rocks.Argument>(2) { @message, @someStruct }));
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "IRequestOfobject_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task CreateWhenGenericParameterHasOptionalDefaultValueAsync()
	{
		var code =
			"""
			using Rocks;
			using System;
			
			public interface IGenericDefault
			{
			  void Setup<T>(T initialValue = default(T));
			}
			
			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Create<IGenericDefault>();
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			internal static class CreateExpectationsOfIGenericDefaultExtensions
			{
				internal static global::Rocks.Expectations.MethodExpectations<global::IGenericDefault> Methods(this global::Rocks.Expectations.Expectations<global::IGenericDefault> @self) =>
					new(@self);
				
				internal static global::IGenericDefault Instance(this global::Rocks.Expectations.Expectations<global::IGenericDefault> @self)
				{
					if (!@self.WasInstanceInvoked)
					{
						@self.WasInstanceInvoked = true;
						var @mock = new RockIGenericDefault(@self);
						@self.MockType = @mock.GetType();
						return @mock;
					}
					else
					{
						throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
					}
				}
				
				private sealed class RockIGenericDefault
					: global::IGenericDefault
				{
					private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
					
					public RockIGenericDefault(global::Rocks.Expectations.Expectations<global::IGenericDefault> @expectations)
					{
						this.handlers = @expectations.Handlers;
					}
					
					[global::Rocks.MemberIdentifier(0, "void Setup<T>(T @initialValue)")]
					public void Setup<T>(T @initialValue = default!)
					{
						if (this.handlers.TryGetValue(0, out var @methodHandlers))
						{
							var @foundMatch = false;
							
							foreach (var @methodHandler in @methodHandlers)
							{
								if (((@methodHandler.Expectations[0] as global::Rocks.Argument<T>)?.IsValid(@initialValue) ?? false))
								{
									@foundMatch = true;
									
									@methodHandler.IncrementCallCount();
									if (@methodHandler.Method is not null && @methodHandler.Method is global::System.Action<T> @method)
									{
										@method(@initialValue);
									}
									break;
								}
							}
							
							if (!@foundMatch)
							{
								throw new global::Rocks.Exceptions.ExpectationException("No handlers match for void Setup<T>(T @initialValue = default!)");
							}
						}
						else
						{
							throw new global::Rocks.Exceptions.ExpectationException("No handlers were found for void Setup<T>(T @initialValue = default!)");
						}
					}
					
				}
			}
			
			internal static class MethodExpectationsOfIGenericDefaultExtensions
			{
				internal static global::Rocks.MethodAdornments<global::IGenericDefault, global::System.Action<T>> Setup<T>(this global::Rocks.Expectations.MethodExpectations<global::IGenericDefault> @self, global::Rocks.Argument<T> @initialValue)
				{
					global::System.ArgumentNullException.ThrowIfNull(@initialValue);
					return new global::Rocks.MethodAdornments<global::IGenericDefault, global::System.Action<T>>(@self.Add(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @initialValue.Transform(default!) }));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "IGenericDefault_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task MakeWhenGenericParameterHasOptionalDefaultValueAsync()
	{
		var code =
			"""
			using Rocks;
			using System;
			
			public interface IGenericDefault
			{
			  void Setup<T>(T initialValue = default(T));
			}
			
			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Make<IGenericDefault>();
				}
			}
			""";

		var generatedCode =
			"""
			#nullable enable
			
			internal static class MakeExpectationsOfIGenericDefaultExtensions
			{
				internal static global::IGenericDefault Instance(this global::Rocks.MakeGeneration<global::IGenericDefault> @self)
				{
					return new RockIGenericDefault();
				}
				
				private sealed class RockIGenericDefault
					: global::IGenericDefault
				{
					public RockIGenericDefault()
					{
					}
					
					public void Setup<T>(T @initialValue = default!)
					{
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockMakeGenerator>(code,
			new[] { (typeof(RockMakeGenerator), "IGenericDefault_Rock_Make.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task CreateWithPositiveInfinityAsync()
	{
		var code =
			"""
			using Rocks;
			using System;

			public interface IUseInfinity
			{
			  void Use(double value = double.PositiveInfinity);
			}

			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Create<IUseInfinity>();
				}
			}
			""";

		var generatedCode =
			"""
			using Rocks.Extensions;
			using System.Collections.Generic;
			using System.Collections.Immutable;
			#nullable enable
			
			internal static class CreateExpectationsOfIUseInfinityExtensions
			{
				internal static global::Rocks.Expectations.MethodExpectations<global::IUseInfinity> Methods(this global::Rocks.Expectations.Expectations<global::IUseInfinity> @self) =>
					new(@self);
				
				internal static global::IUseInfinity Instance(this global::Rocks.Expectations.Expectations<global::IUseInfinity> @self)
				{
					if (!@self.WasInstanceInvoked)
					{
						@self.WasInstanceInvoked = true;
						var @mock = new RockIUseInfinity(@self);
						@self.MockType = @mock.GetType();
						return @mock;
					}
					else
					{
						throw new global::Rocks.Exceptions.NewMockInstanceException("Can only create a new mock once.");
					}
				}
				
				private sealed class RockIUseInfinity
					: global::IUseInfinity
				{
					private readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Rocks.HandlerInformation>> handlers;
					
					public RockIUseInfinity(global::Rocks.Expectations.Expectations<global::IUseInfinity> @expectations)
					{
						this.handlers = @expectations.Handlers;
					}
					
					[global::Rocks.MemberIdentifier(0, "void Use(double @value)")]
					public void Use(double @value = double.PositiveInfinity)
					{
						if (this.handlers.TryGetValue(0, out var @methodHandlers))
						{
							var @foundMatch = false;
							
							foreach (var @methodHandler in @methodHandlers)
							{
								if (((global::Rocks.Argument<double>)@methodHandler.Expectations[0]).IsValid(@value))
								{
									@foundMatch = true;
									
									@methodHandler.IncrementCallCount();
									if (@methodHandler.Method is not null)
									{
										((global::System.Action<double>)@methodHandler.Method)(@value);
									}
									break;
								}
							}
							
							if (!@foundMatch)
							{
								throw new global::Rocks.Exceptions.ExpectationException("No handlers match for void Use(double @value = double.PositiveInfinity)");
							}
						}
						else
						{
							throw new global::Rocks.Exceptions.ExpectationException("No handlers were found for void Use(double @value = double.PositiveInfinity)");
						}
					}
					
				}
			}
			
			internal static class MethodExpectationsOfIUseInfinityExtensions
			{
				internal static global::Rocks.MethodAdornments<global::IUseInfinity, global::System.Action<double>> Use(this global::Rocks.Expectations.MethodExpectations<global::IUseInfinity> @self, global::Rocks.Argument<double> @value)
				{
					global::System.ArgumentNullException.ThrowIfNull(@value);
					return new global::Rocks.MethodAdornments<global::IUseInfinity, global::System.Action<double>>(@self.Add(0, new global::System.Collections.Generic.List<global::Rocks.Argument>(1) { @value.Transform(double.PositiveInfinity) }));
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockCreateGenerator>(code,
			new[] { (typeof(RockCreateGenerator), "IUseInfinity_Rock_Create.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}

	[Test]
	public static async Task MakeWithPositiveInfinityAsync()
	{
		var code =
			"""
			using Rocks;
			using System;

			public interface IUseInfinity
			{
			  void Use(double value = double.PositiveInfinity);
			}

			public static class Test
			{
				public static void Generate()
				{
					var rock = Rock.Make<IUseInfinity>();
				}
			}
			""";

		var generatedCode =
			"""
			#nullable enable
			
			internal static class MakeExpectationsOfIUseInfinityExtensions
			{
				internal static global::IUseInfinity Instance(this global::Rocks.MakeGeneration<global::IUseInfinity> @self)
				{
					return new RockIUseInfinity();
				}
				
				private sealed class RockIUseInfinity
					: global::IUseInfinity
				{
					public RockIUseInfinity()
					{
					}
					
					public void Use(double @value = double.PositiveInfinity)
					{
					}
				}
			}
			
			""";

		await TestAssistants.RunAsync<RockMakeGenerator>(code,
			new[] { (typeof(RockMakeGenerator), "IUseInfinity_Rock_Make.g.cs", generatedCode) },
			Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(false);
	}
}