using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Core.Tests.Helpers.SampleClasses
{
    internal class EnumerableThatThrowsAfterACertainNumberOfIterations<T> : IEnumerable<T> where T : struct
    {
        private readonly int nbOfIterations;

        public EnumerableThatThrowsAfterACertainNumberOfIterations(int nbOfIterations)
        {
            this.nbOfIterations = nbOfIterations;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < nbOfIterations; i++)
                yield return default;
            throw new EnumeratedMoreThanItShouldException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public class EnumeratedMoreThanItShouldException : Exception
        {
            public EnumeratedMoreThanItShouldException() : base() { }
        }
    }
}