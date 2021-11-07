using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Tests.Reflection.SampleClasses
{
    public class SomeNonGenericClassDerivedFromSomeGenericClass : SomeGenericClass<SomeClass>
    {
    }
}