﻿namespace Rocks.Templates
{
	public static class MethodTemplates
	{
		public static string GetAssemblyDelegate(string returnType, string delegateName, string arguments, bool isUnsafe) =>
			$"public {CodeTemplates.GetIsUnsafe(isUnsafe)} delegate {returnType} {delegateName}({arguments});";

		public static string GetNonPublicActionImplementation(string visibility, string methodName, string outInitializers, string requiresNew) =>
$@"{visibility} {requiresNew} override {methodName}
{{
	{outInitializers}	
}}";

		public static string GetNonPublicFunctionImplementation(string visibility, string methodName, string outInitializers, string returnTypeName, string requiresNew, string returnTypeAttributes) =>
$@"{returnTypeAttributes}{visibility} {requiresNew} override {methodName}
{{
	{outInitializers}	
	
	return default({returnTypeName});
}}";

		public static string GetRefOutNotImplementedMethod(string methodNameWithOverride) =>
$@"public {methodNameWithOverride}
{{
	throw new NotImplementedException();
}}";

		public static string GetActionMethod(int methodHandle, string argumentNames, string expectationTemplateInstances, string delegateCast,
			string outInitializers, string methodWithArgumentValues, string methodNameWithOverride, string visibility) =>
$@"{visibility} {methodNameWithOverride}
{{
	{outInitializers}
	ReadOnlyCollection<HandlerInformation> methodHandlers = null;

	if (this.handlers.TryGetValue({methodHandle}, out methodHandlers))
	{{
		var foundMatch = false;
				
		foreach(var methodHandler in methodHandlers)
		{{
			if({expectationTemplateInstances})
			{{
				foundMatch = true;

				if(methodHandler.Method != null)
				{{
					(methodHandler.Method as {delegateCast})({argumentNames});
				}}
	
				methodHandler.RaiseEvents(this);
				methodHandler.IncrementCallCount();
				break;
			}}
		}}

		if(!foundMatch)
		{{
			throw new ExpectationException($""No handlers were found for {methodWithArgumentValues}"");
		}}
	}}
	else
	{{
		throw new NotImplementedException();
	}}
}}";

		public static string GetActionMethodWithNoArguments(int methodHandle, string argumentNames, string delegateCast, string outInitializers,
			string methodNameWithOverride, string visibility) =>
$@"{visibility} {methodNameWithOverride}
{{
	{outInitializers}
	ReadOnlyCollection<HandlerInformation> methodHandlers = null;

	if (this.handlers.TryGetValue({methodHandle}, out methodHandlers))
	{{
		var methodHandler = methodHandlers[0];
		if(methodHandler.Method != null)
		{{
			(methodHandler.Method as {delegateCast})({argumentNames});
		}}
	
		methodHandler.RaiseEvents(this);
		methodHandler.IncrementCallCount();
	}}
	else
	{{
		throw new NotImplementedException();
	}}
}}";

		public static string GetFunctionWithReferenceTypeReturnValue(int methodHandle, string argumentNames, string returnTypeName, string expectationTemplateInstances,
			string delegateCast, string outInitializers, string methodWithArgumentValues, string methodNameWithOverride, string visibility, string requiresNew, string returnTypeAttributes) =>
$@"{returnTypeAttributes}{visibility} {requiresNew} {methodNameWithOverride}
{{
	{outInitializers}
	ReadOnlyCollection<HandlerInformation> methodHandlers = null;

	if (this.handlers.TryGetValue({methodHandle}, out methodHandlers))
	{{
		foreach(var methodHandler in methodHandlers)
		{{
			if({expectationTemplateInstances})
			{{
				var result = methodHandler.Method != null ?
					(methodHandler.Method as {delegateCast})({argumentNames}) as {returnTypeName} :
					(methodHandler as HandlerInformation<{returnTypeName}>).ReturnValue;
				methodHandler.RaiseEvents(this);
				methodHandler.IncrementCallCount();
				return result;
			}}
		}}

		throw new ExpectationException($""No handlers were found for {methodWithArgumentValues}"");
	}}
	else
	{{
		throw new NotImplementedException();
	}}
}}";

		public static string GetFunctionWithReferenceTypeReturnValueAndNoArguments(int methodHandle, string argumentNames, string returnTypeName,
			string delegateCast, string outInitializers, string methodNameWithOverride, string visibility, string requiresNew, string returnTypeAttributes) =>
$@"{returnTypeAttributes}{visibility} {requiresNew} {methodNameWithOverride}
{{
	{outInitializers}
	ReadOnlyCollection<HandlerInformation> methodHandlers = null;

	if (this.handlers.TryGetValue({methodHandle}, out methodHandlers))
	{{
		var methodHandler = methodHandlers[0];
		var result = methodHandler.Method != null ?
			(methodHandler.Method as {delegateCast})({argumentNames}) as {returnTypeName} :
			(methodHandler as HandlerInformation<{returnTypeName}>).ReturnValue;
		methodHandler.RaiseEvents(this);
		methodHandler.IncrementCallCount();
		return result;
	}}
	else
	{{
		throw new NotImplementedException();
	}}
}}";

		public static string GetFunctionWithValueTypeReturnValue(int methodHandle, string argumentNames, string returnTypeName, string expectationTemplateInstances,
			string delegateCast, string outInitializers, string methodWithArgumentValues, string methodNameWithOverride, string visibility, string requiresNew, string returnTypeAttributes) =>
$@"{returnTypeAttributes}{visibility} {requiresNew} {methodNameWithOverride}
{{
	{outInitializers}
	ReadOnlyCollection<HandlerInformation> methodHandlers = null;

	if (this.handlers.TryGetValue({methodHandle}, out methodHandlers))
	{{
		foreach(var methodHandler in methodHandlers)
		{{
			if({expectationTemplateInstances})
			{{
				var result = methodHandler.Method != null ?
					({returnTypeName})(methodHandler.Method as {delegateCast})({argumentNames}) :
					(methodHandler as HandlerInformation<{returnTypeName}>).ReturnValue;
				methodHandler.RaiseEvents(this);
				methodHandler.IncrementCallCount();
				return result;
			}}
		}}

		throw new ExpectationException($""No handlers were found for {methodWithArgumentValues}"");
	}}
	else
	{{
		throw new NotImplementedException();
	}}
}}";

		public static string GetFunctionWithValueTypeReturnValueAndNoArguments(int methodHandle, string argumentNames, string returnTypeName,
			string delegateCast, string outInitializers, string methodNameWithOverride, string visibility, string requiresNew, string returnTypeAttributes) =>
$@"{returnTypeAttributes}{visibility} {requiresNew} {methodNameWithOverride}
{{
	{outInitializers}
	ReadOnlyCollection<HandlerInformation> methodHandlers = null;

	if (this.handlers.TryGetValue({methodHandle}, out methodHandlers))
	{{
		var methodHandler = methodHandlers[0];
		var result = methodHandler.Method != null ?
			({returnTypeName})(methodHandler.Method as {delegateCast})({argumentNames}) :
			(methodHandler as HandlerInformation<{returnTypeName}>).ReturnValue;
		methodHandler.RaiseEvents(this);
		methodHandler.IncrementCallCount();
		return result;
	}}
	else
	{{
		throw new NotImplementedException();
	}}
}}";
	}
}