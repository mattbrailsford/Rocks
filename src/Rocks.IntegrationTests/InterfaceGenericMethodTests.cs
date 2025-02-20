﻿using NUnit.Framework;
using Rocks.Exceptions;

namespace Rocks.IntegrationTests;

public interface IInterfaceGenericMethod<T>
{
	void Foo(List<string> values);
	void Quux(T value);
	void Bar<TParam>(TParam value);
	List<string> FooReturn();
	T QuuxReturn();
	TReturn BarReturn<TReturn>();
	TData? NullableValues<TData>(TData? data);
}

public interface IRequest<T>
	where T : class
{
	Task<T> Send(Guid requestId, object values);
	Task Send(Guid requestId, T message);
}

public static class InterfaceGenericMethodTests
{
	[Test]
	public static async Task CreateWhenMatchesOccurAsync()
	{
		var requestId = Guid.NewGuid();
		var values = new object();
		var message = new object();
		var result = new object();

		var expectations = Rock.Create<IRequest<object>>();
		expectations.Methods().Send(requestId, values).Returns(Task.FromResult(result));
		expectations.ExplicitMethodsForIRequestOfobject().Send(requestId, message).Returns(Task.CompletedTask);

		var mock = expectations.Instance();
		var sendResult = await mock.Send(requestId, values: values).ConfigureAwait(false);
		await mock.Send(requestId, message: message).ConfigureAwait(false);

		Assert.That(sendResult, Is.SameAs(result));

		expectations.Verify();
	}

	[Test]
	public static void CreateUsingGenericType()
	{
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().Foo(Arg.Any<List<string>>());

		var mock = expectations.Instance();
		mock.Foo(new List<string>());

		expectations.Verify();
	}

	[Test]
	public static void MakeUsingGenericType()
	{
		var mock = Rock.Make<IInterfaceGenericMethod<int>>().Instance();

		Assert.That(() => mock.Foo(new List<string>()), Throws.Nothing);
	}

	[Test]
	public static void CreateWithGenericTypeParameter()
	{
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().Quux(Arg.Any<int>());

		var mock = expectations.Instance();
		mock.Quux(3);

		expectations.Verify();
	}

	[Test]
	public static void MakeWithGenericTypeParameter()
	{
		var mock = Rock.Make<IInterfaceGenericMethod<int>>().Instance();

		Assert.That(() => mock.Quux(3), Throws.Nothing);
	}

	[Test]
	public static void CreateWithGenericParameterType()
	{
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().Bar(Arg.Any<int>());

		var mock = expectations.Instance();
		mock.Bar(3);

		expectations.Verify();
	}

	[Test]
	public static void MakeWithGenericParameterType()
	{
		var mock = Rock.Make<IInterfaceGenericMethod<int>>().Instance();

		Assert.That(() => mock.Bar(3), Throws.Nothing);
	}

	[Test]
	public static void CreateWithGenericParameterTypeThatDoesNotMatch()
	{
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().Bar(Arg.Any<int>());

		var mock = expectations.Instance();

		Assert.That(() => mock.Bar("3"), Throws.TypeOf<ExpectationException>());
	}

	[Test]
	public static void CreateUsingGenericTypeAsReturn()
	{
		var returnValue = new List<string>();
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().FooReturn().Returns(returnValue);

		var mock = expectations.Instance();
		var value = mock.FooReturn();

		expectations.Verify();

		Assert.That(value, Is.SameAs(returnValue));
	}

	[Test]
	public static void MakeUsingGenericTypeAsReturn()
	{
		var mock = Rock.Make<IInterfaceGenericMethod<int>>().Instance();
		var value = mock.FooReturn();

		Assert.That(value, Is.SameAs(default(List<string>)));
	}

	[Test]
	public static void CreateWithGenericTypeParameterAsReturn()
	{
		var returnValue = 3;
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().QuuxReturn().Returns(returnValue);

		var mock = expectations.Instance();
		var value = mock.QuuxReturn();

		expectations.Verify();

		Assert.That(value, Is.EqualTo(returnValue));
	}

	[Test]
	public static void MakeWithGenericTypeParameterAsReturn()
	{
		var mock = Rock.Make<IInterfaceGenericMethod<int>>().Instance();
		var value = mock.QuuxReturn();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithGenericParameterTypeAsReturn()
	{
		var returnValue = 3;
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().BarReturn<int>().Returns(returnValue);

		var mock = expectations.Instance();
		var value = mock.BarReturn<int>();

		expectations.Verify();

		Assert.That(value, Is.EqualTo(returnValue));
	}

	[Test]
	public static void MakeWithGenericParameterTypeAsReturn()
	{
		var mock = Rock.Make<IInterfaceGenericMethod<int>>().Instance();
		var value = mock.BarReturn<int>();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithGenericParameterTypeAsReturnThatDoesNotMatch()
	{
		var returnValue = 3;
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().BarReturn<int>().Returns(returnValue);

		var mock = expectations.Instance();

		Assert.That(mock.BarReturn<string>, Throws.TypeOf<NoReturnValueException>());
	}

	[Test]
	public static void CreateWithNullableGenericParameterTypes()
	{
		var returnValue = "c";
		var expectations = Rock.Create<IInterfaceGenericMethod<int>>();
		expectations.Methods().NullableValues<string>("b").Returns(returnValue);

		var mock = expectations.Instance();
		var value = mock.NullableValues("b");

		expectations.Verify();

		Assert.That(value, Is.EqualTo(returnValue));
	}

	[Test]
	public static void MakeWithNullableGenericParameterTypes()
	{
		var mock = Rock.Make<IInterfaceGenericMethod<int>>().Instance();
		var value = mock.NullableValues("b");

		Assert.That(value, Is.EqualTo(default(string)));
	}
}