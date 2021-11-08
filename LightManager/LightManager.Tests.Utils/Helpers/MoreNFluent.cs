using NFluent;
using System;

namespace LightManager
{
    public static class MoreNFluent
    {
        public static ICheckLink<ICheck<string>> IsNotNullOrEmpty(this ICheck<string> checker) => checker.IsNotNull().And.IsNotEmpty();

        public static ICheck<RunTrace> DoesNotThrow<TException>(this ICheck<RunTrace> check) where TException : Exception
        {
            check.Not.Throws<TException>();

            return check;
        }
    }
}