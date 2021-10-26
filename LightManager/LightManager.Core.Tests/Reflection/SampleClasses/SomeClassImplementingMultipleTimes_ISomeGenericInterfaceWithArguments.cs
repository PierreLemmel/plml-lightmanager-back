namespace LightManager.Tests.Reflection.SampleClasses
{
    public class SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith1Argument :
        ISomeGenericInterfaceWith1Argument<int>,
        ISomeGenericInterfaceWith1Argument<string>,
        ISomeGenericInterfaceWith1Argument<SomeClass>
    {
    }

    public class SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith2Arguments :
        ISomeGenericInterfaceWith2Arguments<int, int>,
        ISomeGenericInterfaceWith2Arguments<string, string>,
        ISomeGenericInterfaceWith2Arguments<SomeClass, SomeClass>
    {
    }

    public class SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith3Arguments :
        ISomeGenericInterfaceWith3Arguments<int, int, int>,
        ISomeGenericInterfaceWith3Arguments<string, string, string>,
        ISomeGenericInterfaceWith3Arguments<SomeClass, SomeClass, SomeClass>
    {
    }

    public class SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith4Arguments :
        ISomeGenericInterfaceWith4Arguments<int, int, int, int>,
        ISomeGenericInterfaceWith4Arguments<string, string, string, string>,
        ISomeGenericInterfaceWith4Arguments<SomeClass, SomeClass, SomeClass, SomeClass>
    {
    }

    public class SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith5Arguments :
        ISomeGenericInterfaceWith5Arguments<int, int, int, int, int>,
        ISomeGenericInterfaceWith5Arguments<string, string, string, string, string>,
        ISomeGenericInterfaceWith5Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>
    {
    }

    public class SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith6Arguments :
        ISomeGenericInterfaceWith6Arguments<int, int, int, int, int, int>,
        ISomeGenericInterfaceWith6Arguments<string, string, string, string, string, string>,
        ISomeGenericInterfaceWith6Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>
    {
    }

    public class SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith7Arguments :
        ISomeGenericInterfaceWith7Arguments<int, int, int, int, int, int, int>,
        ISomeGenericInterfaceWith7Arguments<string, string, string, string, string, string, string>,
        ISomeGenericInterfaceWith7Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>
    {
    }

    public class SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith8Arguments :
        ISomeGenericInterfaceWith8Arguments<int, int, int, int, int, int, int, int>,
        ISomeGenericInterfaceWith8Arguments<string, string, string, string, string, string, string, string>,
        ISomeGenericInterfaceWith8Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>
    {
    }
}