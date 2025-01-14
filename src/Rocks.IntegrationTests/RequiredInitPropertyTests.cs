﻿using NUnit.Framework;

namespace Rocks.IntegrationTests;

public class Requireds
{
	public virtual void Foo() { }

	public required int NonNullableValueType { get; set; }
	public required int? NullableValueType { get; init; }
	public required string NonNullableReferenceType { get; init; }
	public required string? NullableReferenceType { get; init; }
}

public class Inits
{
	public virtual void Foo() { }

	public int NonNullableValueType { get; init; }
	public int? NullableValueType { get; init; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public string NonNullableReferenceType { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public string? NullableReferenceType { get; init; }
}

public static class RequiredInitPropertyTests
{
	[Test]
	public static void InitPropertiesWithCreate()
	{
		var expectations = Rock.Create<Inits>();
		expectations.Methods().Foo();

		var mock = expectations.Instance(
			new() { NonNullableValueType = 3, NullableValueType = 2, NonNullableReferenceType = "3", NullableReferenceType = "2" });
		mock.Foo();

		Assert.Multiple(() =>
		{
			Assert.That(mock.NonNullableValueType, Is.EqualTo(3));
			Assert.That(mock.NullableValueType, Is.EqualTo(2));
			Assert.That(mock.NonNullableReferenceType, Is.EqualTo("3"));
			Assert.That(mock.NullableReferenceType, Is.EqualTo("2"));
		});

		expectations.Verify();
	}

	[Test]
	public static void InitPropertiesWithMake()
	{
		var mock = Rock.Make<Inits>().Instance(
			new() { NonNullableValueType = 3, NullableValueType = 2, NonNullableReferenceType = "3", NullableReferenceType = "2" });
		mock.Foo();

		Assert.Multiple(() =>
		{
			Assert.That(mock.NonNullableValueType, Is.EqualTo(3));
			Assert.That(mock.NullableValueType, Is.EqualTo(2));
			Assert.That(mock.NonNullableReferenceType, Is.EqualTo("3"));
			Assert.That(mock.NullableReferenceType, Is.EqualTo("2"));
		});
	}

	[Test]
	public static void InitPropertiesWithNullWithCreate()
	{
		var expectations = Rock.Create<Inits>();
		expectations.Methods().Foo();

		var mock = expectations.Instance(null);
		mock.Foo();

		Assert.Multiple(() =>
		{
			Assert.That(mock.NonNullableValueType, Is.EqualTo(0));
			Assert.That(mock.NullableValueType, Is.Null);
			Assert.That(mock.NonNullableReferenceType, Is.Null);
			Assert.That(mock.NullableReferenceType, Is.Null);
		});

		expectations.Verify();
	}

	[Test]
	public static void InitPropertiesWithNullWithMake()
	{
		var mock = Rock.Make<Inits>().Instance(null);
		mock.Foo();

		Assert.Multiple(() =>
		{
			Assert.That(mock.NonNullableValueType, Is.EqualTo(0));
			Assert.That(mock.NullableValueType, Is.Null);
			Assert.That(mock.NonNullableReferenceType, Is.Null);
			Assert.That(mock.NullableReferenceType, Is.Null);
		});
	}

	[Test]
	public static void RequiredPropertiesWithCreate()
	{
		var expectations = Rock.Create<Requireds>();
		expectations.Methods().Foo();

		var mock = expectations.Instance(
			new() { NonNullableValueType = 3, NullableValueType = 2, NonNullableReferenceType = "3", NullableReferenceType = "2" });
		mock.Foo();

		Assert.Multiple(() =>
		{
			Assert.That(mock.NonNullableValueType, Is.EqualTo(3));
			Assert.That(mock.NullableValueType, Is.EqualTo(2));
			Assert.That(mock.NonNullableReferenceType, Is.EqualTo("3"));
			Assert.That(mock.NullableReferenceType, Is.EqualTo("2"));
		});

		expectations.Verify();
	}

	[Test]
	public static void RequiredPropertiesWithMake()
	{
		var mock = Rock.Make<Requireds>().Instance(
			new() { NonNullableValueType = 3, NullableValueType = 2, NonNullableReferenceType = "3", NullableReferenceType = "2" });
		mock.Foo();

		Assert.Multiple(() =>
		{
			Assert.That(mock.NonNullableValueType, Is.EqualTo(3));
			Assert.That(mock.NullableValueType, Is.EqualTo(2));
			Assert.That(mock.NonNullableReferenceType, Is.EqualTo("3"));
			Assert.That(mock.NullableReferenceType, Is.EqualTo("2"));
		});
	}

	[Test]
	public static void RequiredPropertiesWithNullWithCreate()
	{
		var expectations = Rock.Create<Requireds>();
		expectations.Methods().Foo();

		Assert.That(() => expectations.Instance(null!), Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void RequiredPropertiesWithNullWithMake() => 
		Assert.That(() => Rock.Make<Requireds>().Instance(null!), Throws.TypeOf<ArgumentNullException>());
}