using LightManager.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightManager.Tests.Utils.Sources
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class GenericTestCaseSourceAttribute : TestCaseSourceAttribute, ITestBuilder
    {
        public GenericTestCaseSourceAttribute(string sourceName) : base(sourceName) { }

        IEnumerable<TestMethod> ITestBuilder.BuildFrom(IMethodInfo method, Test suite)
        {
            if (!method.IsGenericMethodDefinition)
                throw new InvalidOperationException($"{nameof(GenericTestCaseSourceAttribute)} should only apply to generic methods");

            if (method.GetGenericArguments().Length != 1)
                throw new NotSupportedException($"Only generic methods with 1 argument are supported yet");

            Type fixtureType = method.TypeInfo.Type;
            PropertyInfo sourceProperty = fixtureType.GetProperty(SourceName) ?? throw new InvalidOperationException($"Source name must refer to a public static property. Provided source name: {SourceName}");

            object? objValue = sourceProperty.GetValue(null);
            if (objValue is not IEnumerable<Type> types)
                throw new InvalidOperationException($"{SourceName} is not a type source.");

            NUnitTestCaseBuilder testCaseBuilder = new NUnitTestCaseBuilder();

            IEnumerable<TestMethod> tests = types.Select(type =>
            {
                IMethodInfo genericMethod = method.MakeGenericMethod(type);

                string testName = $"{method.Name}<{type.GetGenericName()}>";
                TestCaseData data = new TestCaseData().SetName(testName);
                return testCaseBuilder.BuildTestMethod(genericMethod, suite, new TestCaseParameters(data));
            }).ToList();

            return tests;
        }
    }
}