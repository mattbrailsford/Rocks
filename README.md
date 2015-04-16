# Rocks
A mocking library based on the Compiler APIs (Roslyn + Mocks)

There are great mocking libraries out there, like Moq (https://github.com/Moq/moq4) and NSubstitute (http://nsubstitute.github.io/), so why create YAML (yet another mocking library)? There are essentially two reasons.

The first reason relates to how code generation was done with mocking libraries. Most (if not all) use an approach that ends up using System.Reflection.Emit, which requires knowledge of IL. Furthermore, the generated code can't be stepped into during a debugging process. I wanted to write a mocking library with the new Compiler APIs (Roslyn) to see if I could make the code generation process for the mock much easier and allow a developer to step into that code if necessary.

The other reason is being able to pre-generate the mocks for a given assembly, rather than dynamically generate them in a test. This is what the Fakes library can do (https://msdn.microsoft.com/en-us/library/hh549175.aspx), but I wanted to be able to do it where I could easily modify a project file and automatically generate those mocks.

This is what Rocks can do (well, almost at this point). Mocks are created by generating C# code on the fly and compiling it with the Compiler APIs. This makes it trivial to step into the mock code. You can also pre-generate the mocks into its own assembly and reference that in your tests if you like (the custom MSBuild task is feature in flight at the moment but should be completed soon).

So, feel free to test Rocks out, and see what you think. It's not complete yet (events aren't supported, there's no Nuget package yet), but if you have VS2015 CTP6 you can get the code, compile it, and try it out. Even if you don't use it as your primary mocking library, you may see just how easy it to generate code on the fly with the new Compiler APIs. Enjoy!
