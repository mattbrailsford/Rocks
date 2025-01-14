﻿using NUnit.Framework;

namespace Rocks.IntegrationTests;

public interface IHaveArgument
{
	void Foo(int a);
	void Bar(int a = 3);
	string this[int a] { get; set; }
}

public static class ArgTests
{
	[Test]
	public static void DeclareArgumentFromIndexerWithNull()
	{
		var expectations = Rock.Create<IHaveArgument>();
		Assert.Multiple(() =>
		{
			Assert.That(() => expectations.Indexers().Getters().This(null!), Throws.TypeOf<ArgumentNullException>());
			Assert.That(() => expectations.Indexers().Setters().This(null!, "value"), Throws.TypeOf<ArgumentNullException>());
			Assert.That(() => expectations.Indexers().Setters().This(1, null!), Throws.TypeOf<ArgumentNullException>());
		});
	}

	[Test]
	public static void DeclareArgumentFromMethodWithNull()
	{
		var expectations = Rock.Create<IHaveArgument>();
		Assert.That(() => expectations.Methods().Foo(null!), Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void DeclareArgumentWithValue()
	{
		var expectations = Rock.Create<IHaveArgument>();
		expectations.Methods().Foo(3);

		var mock = expectations.Instance();
		mock.Foo(3);

		expectations.Verify();
	}

	[Test]
	public static void DeclareArgumentWithIs()
	{
		var expectations = Rock.Create<IHaveArgument>();
		expectations.Methods().Foo(Arg.Is(3));

		var mock = expectations.Instance();
		mock.Foo(3);

		expectations.Verify();
	}

	[Test]
	public static void DeclareArgumentWithAny()
	{
		var expectations = Rock.Create<IHaveArgument>();
		expectations.Methods().Foo(Arg.Any<int>());

		var mock = expectations.Instance();
		mock.Foo(3);

		expectations.Verify();
	}

	[Test]
	public static void DeclareArgumentWithValidate()
	{
		var expectations = Rock.Create<IHaveArgument>();
		expectations.Methods().Foo(Arg.Validate<int>(_ => _ > 20 && _ < 30));

		var mock = expectations.Instance();
		mock.Foo(25);

		expectations.Verify();
	}

	[Test]
	public static void DeclareArgumentWithDefault()
	{
		var expectations = Rock.Create<IHaveArgument>();
		expectations.Methods().Bar(Arg.IsDefault<int>());

		var mock = expectations.Instance();
		mock.Bar(3);

		expectations.Verify();
	}
}