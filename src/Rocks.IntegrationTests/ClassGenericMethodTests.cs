﻿using NUnit.Framework;
using Rocks.Exceptions;

namespace Rocks.IntegrationTests;

public class ClassGenericMethod<T>
{
	public virtual void Foo(List<string> values) { }
	public virtual void Quux(T value) { }
	public virtual void Bar<TParam>(TParam value) { }
	public virtual List<string>? FooReturn() => default;
	public virtual T? QuuxReturn() => default;
	public virtual TReturn BarReturn<TReturn>() => default!;
	public virtual TData? NullableValues<TData>(TData? data) => default!;
}

public static class ClassGenericMethodTests
{
	[Test]
	public static void CreateUsingGenericType()
	{
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().Foo(Arg.Any<List<string>>());

		var mock = expectations.Instance();
		mock.Foo(new List<string>());

		expectations.Verify();
	}

	[Test]
	public static void MakeUsingGenericType()
	{
		var mock = Rock.Make<ClassGenericMethod<int>>().Instance();

		Assert.That(() => mock.Foo(new List<string>()), Throws.Nothing);
	}

	[Test]
	public static void CreateWithGenericTypeParameter()
	{
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().Quux(Arg.Any<int>());

		var mock = expectations.Instance();
		mock.Quux(3);

		expectations.Verify();
	}

	[Test]
	public static void MakeWithGenericTypeParameter()
	{
		var mock = Rock.Make<ClassGenericMethod<int>>().Instance();

		Assert.That(() => mock.Quux(3), Throws.Nothing);
	}

	[Test]
	public static void CreateWithGenericParameterType()
	{
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().Bar(Arg.Any<int>());

		var mock = expectations.Instance();
		mock.Bar(3);

		expectations.Verify();
	}

	[Test]
	public static void MakeWithGenericParameterType()
	{
		var mock = Rock.Make<ClassGenericMethod<int>>().Instance();

		Assert.That(() => mock.Bar(3), Throws.Nothing);
	}

	[Test]
	public static void CreateWithGenericParameterTypeThatDoesNotMatch()
	{
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().Bar(Arg.Any<int>());

		var mock = expectations.Instance();

		Assert.That(() => mock.Bar("3"), Throws.TypeOf<ExpectationException>());
	}

	[Test]
	public static void CreateUsingGenericTypeAsReturn()
	{
		var returnValue = new List<string>();
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().FooReturn().Returns(returnValue);

		var mock = expectations.Instance();
		var value = mock.FooReturn();

		expectations.Verify();

		Assert.That(value, Is.SameAs(returnValue));
	}

	[Test]
	public static void MakeUsingGenericTypeAsReturn()
	{
		var mock = Rock.Make<ClassGenericMethod<int>>().Instance();
		var value = mock.FooReturn();

		Assert.That(value, Is.SameAs(default(List<string>)));
	}

	[Test]
	public static void CreateWithGenericTypeParameterAsReturn()
	{
		var returnValue = 3;
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().QuuxReturn().Returns(returnValue);

		var mock = expectations.Instance();
		var value = mock.QuuxReturn();

		expectations.Verify();

		Assert.That(value, Is.EqualTo(returnValue));
	}

	[Test]
	public static void MakeWithGenericTypeParameterAsReturn()
	{
		var mock = Rock.Make<ClassGenericMethod<int>>().Instance();
		var value = mock.QuuxReturn();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithGenericParameterTypeAsReturn()
	{
		var returnValue = 3;
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().BarReturn<int>().Returns(returnValue);

		var mock = expectations.Instance();
		var value = mock.BarReturn<int>();

		expectations.Verify();

		Assert.That(value, Is.EqualTo(returnValue));
	}

	[Test]
	public static void MakeWithGenericParameterTypeAsReturn()
	{
		var mock = Rock.Make<ClassGenericMethod<int>>().Instance();
		var value = mock.BarReturn<int>();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithGenericParameterTypeAsReturnThatDoesNotMatch()
	{
		var returnValue = 3;
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().BarReturn<int>().Returns(returnValue);

		var mock = expectations.Instance();

		Assert.That(mock.BarReturn<string>, Throws.TypeOf<NoReturnValueException>());
	}

	[Test]
	public static void CreateWithNullableGenericParameterTypes()
	{
		var returnValue = "c";
		var expectations = Rock.Create<ClassGenericMethod<int>>();
		expectations.Methods().NullableValues<string>("b").Returns(returnValue);

		var mock = expectations.Instance();
		var value = mock.NullableValues("b");

		expectations.Verify();

		Assert.That(value, Is.EqualTo(returnValue));
	}

	[Test]
	public static void MakeWithNullableGenericParameterTypes()
	{
		var mock = Rock.Make<ClassGenericMethod<int>>().Instance();
		var value = mock.NullableValues("b");

		Assert.That(value, Is.EqualTo(default(string)));
	}
}