namespace LightManager.Tests.Reflection.SampleClasses
{
    public class SomeClassImplementingOnce_ISomeGenericInterfaceWith1Argument :
        ISomeGenericInterfaceWith1Argument<int> { }

    public class SomeClassImplementingOnce_ISomeGenericInterfaceWith2Arguments :
        ISomeGenericInterfaceWith2Arguments<int, int> { }

    public class SomeClassImplementingOnce_ISomeGenericInterfaceWith3Arguments :
        ISomeGenericInterfaceWith3Arguments<int, int, int> { }

    public class SomeClassImplementingOnce_ISomeGenericInterfaceWith4Arguments :
        ISomeGenericInterfaceWith4Arguments<int, int, int, int> { }

    public class SomeClassImplementingOnce_ISomeGenericInterfaceWith5Arguments :
        ISomeGenericInterfaceWith5Arguments<int, int, int, int, int> { }

    public class SomeClassImplementingOnce_ISomeGenericInterfaceWith6Arguments :
        ISomeGenericInterfaceWith6Arguments<int, int, int, int, int, int> { }

    public class SomeClassImplementingOnce_ISomeGenericInterfaceWith7Arguments :
        ISomeGenericInterfaceWith7Arguments<int, int, int, int, int, int, int> { }

    public class SomeClassImplementingOnce_ISomeGenericInterfaceWith8Arguments :
        ISomeGenericInterfaceWith8Arguments<int, int, int, int, int, int, int, int> { }
}