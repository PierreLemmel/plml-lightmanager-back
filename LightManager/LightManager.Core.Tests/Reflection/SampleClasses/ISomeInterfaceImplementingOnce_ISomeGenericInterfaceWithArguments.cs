namespace LightManager.Tests.Reflection.SampleClasses
{
    public interface ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith1Argument :
        ISomeGenericInterfaceWith1Argument<int> { }

    public interface ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith2Arguments :
        ISomeGenericInterfaceWith2Arguments<int, int> { }

    public interface ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith3Arguments :
        ISomeGenericInterfaceWith3Arguments<int, int, int> { }

    public interface ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith4Arguments :
        ISomeGenericInterfaceWith4Arguments<int, int, int, int> { }

    public interface ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith5Arguments :
        ISomeGenericInterfaceWith5Arguments<int, int, int, int, int> { }

    public interface ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith6Arguments :
        ISomeGenericInterfaceWith6Arguments<int, int, int, int, int, int> { }

    public interface ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith7Arguments :
        ISomeGenericInterfaceWith7Arguments<int, int, int, int, int, int, int> { }

    public interface ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith8Arguments :
        ISomeGenericInterfaceWith8Arguments<int, int, int, int, int, int, int, int> { }
}