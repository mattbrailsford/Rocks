﻿using NUnit.Framework;
using Rocks.Exceptions;

namespace Rocks.IntegrationTests;

public interface IInterfaceMethodReturn
{
	int NoParameters();
	int OneParameter(int a);
	int MultipleParameters(int a, string b);
}

public static class InterfaceMethodReturnTests
{
	[Test]
	public static void CreateWithNoParameters()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().NoParameters();

		var mock = expectations.Instance();
		var value = mock.NoParameters();

		expectations.Verify();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void MakeWithNoParameters()
	{
		var mock = Rock.Make<IInterfaceMethodReturn>().Instance();
		var value = mock.NoParameters();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithNoParametersWithReturn()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().NoParameters().Returns(3);

		var mock = expectations.Instance();
		var value = mock.NoParameters();

		expectations.Verify();

		Assert.That(value, Is.EqualTo(3));
	}

	[Test]
	public static void MakeWithNoParametersWithReturn()
	{
		var mock = Rock.Make<IInterfaceMethodReturn>().Instance();
		var value = mock.NoParameters();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithNoParametersMultipleCalls()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().NoParameters().CallCount(2);

		var mock = expectations.Instance();
		mock.NoParameters();
		mock.NoParameters();

		expectations.Verify();
	}

	[Test]
	public static void CreateWithNoParametersMultipleCallsNotMet()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().NoParameters().CallCount(2);

		var mock = expectations.Instance();
		mock.NoParameters();

		Assert.That(expectations.Verify, Throws.TypeOf<VerificationException>());
	}

	[Test]
	public static void CreateWithNoParametersAndCallback()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().NoParameters().Callback(() => 3);

		var mock = expectations.Instance();
		var value = mock.NoParameters();

		expectations.Verify();

		Assert.That(value, Is.EqualTo(3));
	}

	[Test]
	public static void CreateWithNoParametersNoExpectationSet()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();

		var chunk = expectations.Instance();

		Assert.That(chunk.NoParameters, Throws.TypeOf<ExpectationException>());
	}

	[Test]
	public static void CreateWithNoParametersExpectationsNotMet()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().NoParameters();

		_ = expectations.Instance();

		Assert.That(expectations.Verify, Throws.TypeOf<VerificationException>());
	}

	[Test]
	public static void CreateWithOneParameter()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().OneParameter(3);

		var mock = expectations.Instance();
		var value = mock.OneParameter(3);

		expectations.Verify();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void MakeWithOneParameter()
	{
		var mock = Rock.Make<IInterfaceMethodReturn>().Instance();
		var value = mock.OneParameter(3);

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithOneParameterWithReturn()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().OneParameter(3).Returns(3);

		var mock = expectations.Instance();
		var value = mock.OneParameter(3);

		expectations.Verify();

		Assert.That(value, Is.EqualTo(3));
	}

	[Test]
	public static void MakeWithOneParameterWithReturn()
	{
		var mock = Rock.Make<IInterfaceMethodReturn>().Instance();
		var value = mock.OneParameter(3);

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithOneParameterWithCallback()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().OneParameter(3).Callback(_ => 3);

		var mock = expectations.Instance();
		var value = mock.OneParameter(3);

		expectations.Verify();

		Assert.That(value, Is.EqualTo(3));
	}

	[Test]
	public static void CreateWithOneParameterArgExpectationNotMet()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().OneParameter(3);

		var mock = expectations.Instance();

		Assert.That(() => mock.OneParameter(1), Throws.TypeOf<ExpectationException>());
	}

	[Test]
	public static void CreateWithMultipleParameters()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().MultipleParameters(3, "b");

		var mock = expectations.Instance();
		var value = mock.MultipleParameters(3, "b");

		expectations.Verify();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void MakeWithMultipleParameters()
	{
		var mock = Rock.Make<IInterfaceMethodReturn>().Instance();
		var value = mock.MultipleParameters(3, "b");

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithMultipleParametersWithReturn()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().MultipleParameters(3, "b").Returns(3);

		var mock = expectations.Instance();
		var value = mock.MultipleParameters(3, "b");

		expectations.Verify();

		Assert.That(value, Is.EqualTo(3));
	}

	[Test]
	public static void MakeWithMultipleParametersWithReturn()
	{
		var mock = Rock.Make<IInterfaceMethodReturn>().Instance();
		var value = mock.MultipleParameters(3, "b");

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateWithMultipleParametersWithCallback()
	{
		var aValue = 0;
		var bValue = string.Empty;
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().MultipleParameters(3, "b").Callback((a, b) =>
		{
			(aValue, bValue) = (a, b);
			return 3;
		});

		var mock = expectations.Instance();
		var value = mock.MultipleParameters(3, "b");

		expectations.Verify();

		Assert.Multiple(() =>
		{
			Assert.That(aValue, Is.EqualTo(3));
			Assert.That(bValue, Is.EqualTo("b"));
			Assert.That(value, Is.EqualTo(3));
		});
	}

	[Test]
	public static void CreateWithMultipleParametersArgExpectationNotMet()
	{
		var expectations = Rock.Create<IInterfaceMethodReturn>();
		expectations.Methods().MultipleParameters(3, "b");

		var mock = expectations.Instance();

		Assert.That(() => mock.MultipleParameters(3, "a"), Throws.TypeOf<ExpectationException>());
	}
}