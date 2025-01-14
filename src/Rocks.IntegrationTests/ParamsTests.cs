﻿using NUnit.Framework;

namespace Rocks.IntegrationTests;

public interface IHaveParams
{
	void Foo(int a, params string[] b);
	int this[int a, params string[] b] { get; }
}

public static class ParamsTests
{
	[Test]
	public static void CreateMembersWithParamsArgumentsSpecified()
	{
		var returnValue = 3;
		var expectations = Rock.Create<IHaveParams>();
		expectations.Methods().Foo(1, new[] { "b" });
		expectations.Indexers().Getters().This(1, new[] { "b" }).Returns(returnValue);

		var mock = expectations.Instance();
		mock.Foo(1, "b");
		var value = mock[1, "b"];

		expectations.Verify();

		Assert.That(value, Is.EqualTo(returnValue));
	}

	[Test]
	public static void MakeMembersWithParamsArgumentsSpecified()
	{
		var mock = Rock.Make<IHaveParams>().Instance();
		var value = mock[1, "b"];

		Assert.Multiple(() =>
		{
			Assert.That(value, Is.EqualTo(default(int)));
			Assert.That(() => mock.Foo(1, "b"), Throws.Nothing);
		});
	}

	[Test]
	public static void CreateMembersWithParamsArgumentsNotSpecified()
	{
		var returnValue = 3;
		var expectations = Rock.Create<IHaveParams>();
		expectations.Methods().Foo(1, Array.Empty<string>());
		expectations.Indexers().Getters().This(1, Array.Empty<string>()).Returns(returnValue);

		var mock = expectations.Instance();
		mock.Foo(1);
		var value = mock[1];

		expectations.Verify();

		Assert.That(value, Is.EqualTo(returnValue));
	}

	[Test]
	public static void MakeMembersWithParamsArgumentsNotSpecified()
	{
		var mock = Rock.Make<IHaveParams>().Instance();
		var value = mock[1];

		Assert.Multiple(() =>
		{
			Assert.That(value, Is.EqualTo(default(int)));
			Assert.That(() => mock.Foo(1), Throws.Nothing);
		});
	}
}