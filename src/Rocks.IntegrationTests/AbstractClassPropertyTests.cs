﻿using NUnit.Framework;

namespace Rocks.IntegrationTests;

public abstract class AbstractClassProperty
{
	public abstract int GetData { get; }
	public abstract int GetAndInitData { get; init; }
	public abstract int GetAndSetData { get; set; }
#pragma warning disable CA1044 // Properties should not be write only
   public abstract int InitData { init; }
   public abstract int SetData { set; }
#pragma warning restore CA1044 // Properties should not be write only

	public abstract event EventHandler MyEvent;
}

public static class AbstractClassPropertyTests
{
	[Test]
	public static void CreateGet()
	{
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Getters().GetData();

		var mock = expectations.Instance(null);
		var value = mock.GetData;

		expectations.Verify();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void MakeGet()
	{
		var mock = Rock.Make<AbstractClassProperty>().Instance(null);
		var value = mock.GetData;

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateGetWithRaiseEvent()
	{
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Getters().GetData().RaisesMyEvent(EventArgs.Empty);

		var wasEventRaised = false;
		var mock = expectations.Instance(null);
		mock.MyEvent += (s, e) => wasEventRaised = true;
		var value = mock.GetData;

		expectations.Verify();

		Assert.Multiple(() =>
		{
			Assert.That(value, Is.EqualTo(default(int)));
			Assert.That(wasEventRaised, Is.True);
		});
	}

	[Test]
	public static void CreateGetWithCallback()
	{
		var wasCallbackInvoked = false;
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Getters().GetData().Callback(() =>
		{
			wasCallbackInvoked = true;
			return 3;
		});

		var mock = expectations.Instance(null);
		var value = mock.GetData;

		expectations.Verify();

		Assert.Multiple(() =>
		{
			Assert.That(value, Is.EqualTo(3));
			Assert.That(wasCallbackInvoked, Is.True);
		});
	}

	[Test]
	public static void CreateGetWithRaiseEventAndCallback()
	{
		var wasCallbackInvoked = false;
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Getters().GetData().Callback(() =>
		{
			wasCallbackInvoked = true;
			return 3;
		}).RaisesMyEvent(EventArgs.Empty);

		var wasEventRaised = false;
		var mock = expectations.Instance(null);
		mock.MyEvent += (s, e) => wasEventRaised = true;
		var value = mock.GetData;

		expectations.Verify();

		Assert.Multiple(() =>
		{
			Assert.That(value, Is.EqualTo(3));
			Assert.That(wasEventRaised, Is.True);
			Assert.That(wasCallbackInvoked, Is.True);
		});
	}

	[Test]
	public static void CreateSet()
	{
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Setters().SetData(Arg.Any<int>());

		var mock = expectations.Instance(null);
		mock.SetData = 1;

		expectations.Verify();
	}

	[Test]
	public static void MakeSet()
	{
		var mock = Rock.Make<AbstractClassProperty>().Instance(null);

		Assert.That(() => mock.SetData = 1, Throws.Nothing);
	}

	[Test]
	public static void CreateSetWithRaiseEvent()
	{
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Setters().SetData(Arg.Any<int>())
			.RaisesMyEvent(EventArgs.Empty);

		var wasEventRaised = false;
		var mock = expectations.Instance(null);
		mock.MyEvent += (s, e) => wasEventRaised = true;
		mock.SetData = 1;

		expectations.Verify();

		Assert.That(wasEventRaised, Is.True);
	}

	[Test]
	public static void CreateSetWithCallback()
	{
		var wasCallbackInvoked = false;
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Setters().SetData(Arg.Any<int>())
			.Callback(_ => wasCallbackInvoked = true);

		var mock = expectations.Instance(null);
		mock.SetData = 1;

		expectations.Verify();

		Assert.That(wasCallbackInvoked, Is.True);
	}

	[Test]
	public static void CreateSetWithRaiseEventAndCallback()
	{
		var wasCallbackInvoked = false;
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Setters().SetData(Arg.Any<int>())
			.RaisesMyEvent(EventArgs.Empty)
			.Callback(_ => wasCallbackInvoked = true);

		var wasEventRaised = false;
		var mock = expectations.Instance(null);
		mock.MyEvent += (s, e) => wasEventRaised = true;
		mock.SetData = 1;

		expectations.Verify();

		Assert.Multiple(() =>
		{
			Assert.That(wasEventRaised, Is.True);
			Assert.That(wasCallbackInvoked, Is.True);
		});
	}

	[Test]
	public static void CreateGetAndInit()
	{
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Getters().GetAndInitData();

		var mock = expectations.Instance(null);
		var value = mock.GetAndInitData;

		expectations.Verify();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void CreateGetAndSet()
	{
		var expectations = Rock.Create<AbstractClassProperty>();
		expectations.Properties().Getters().GetAndSetData();
		expectations.Properties().Setters().GetAndSetData(Arg.Any<int>());

		var mock = expectations.Instance(null);
		var value = mock.GetAndSetData;
		mock.GetAndSetData = value;

		expectations.Verify();

		Assert.That(value, Is.EqualTo(default(int)));
	}

	[Test]
	public static void MakeGetAndInit()
	{
		var mock = Rock.Make<AbstractClassProperty>().Instance(null);
		var value = mock.GetAndInitData;

		Assert.Multiple(() =>
		{
			Assert.That(value, Is.EqualTo(default(int)));
		});
	}

	[Test]
	public static void MakeGetAndSet()
	{
		var mock = Rock.Make<AbstractClassProperty>().Instance(null);
		var value = mock.GetAndSetData;

		Assert.Multiple(() =>
		{
			Assert.That(value, Is.EqualTo(default(int)));
			Assert.That(() => mock.GetAndSetData = value, Throws.Nothing);
		});
	}
}