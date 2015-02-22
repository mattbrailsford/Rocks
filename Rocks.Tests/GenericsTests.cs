﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocks.Tests
{
	[TestFixture]
	public sealed class GenericsTests
	{
		[Test]
		public void HandleWithNoConstraints()
		{
			var argumentA = 0;
			var argumentB = default(Base);

			var rock = Rock.Create<IGenerics>();
			rock.HandleFunc<int, Base, Base>(
				_ => _.TargetWithNoConstraints(default(int), default(Base)),
				(a, b) => { argumentA = a; argumentB = b; return new Base(); });
			var chunk = rock.Make();
			var result = chunk.TargetWithNoConstraints<Base>(1, new Base());

			Assert.AreEqual(1, argumentA, nameof(argumentA));
			Assert.IsNotNull(argumentB, nameof(argumentB));

			rock.Verify();
		}
	}

	public class Base { }

	public interface IGenerics
	{
		U TargetWithNoConstraints<U>(int a, U b);
		//U TargetWithNonTypeConstrains<U>(int a, U b) where U : class, new();
		//U TargetWithTypeConstraints<U>(int a, U b) where U : Base;
	}
}