﻿using System;

namespace Rocks.Task.TestContainer.Contracts
{
	public interface Interface1<T>
	{
		void Method1();
		string Method2(T a);
		Guid Method3(string a, int b);
	}
}