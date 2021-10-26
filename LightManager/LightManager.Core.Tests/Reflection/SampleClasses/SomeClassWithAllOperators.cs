using System;

namespace LightManager.Tests.Reflection.SampleClasses
{
    public class SomeClassWithAllOperators
    {
        public static SomeClassWithAllOperators operator +(SomeClassWithAllOperators foo) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator -(SomeClassWithAllOperators foo) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator ++(SomeClassWithAllOperators foo) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator --(SomeClassWithAllOperators foo) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator !(SomeClassWithAllOperators foo) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator ~(SomeClassWithAllOperators foo) => throw new NotImplementedException();
        public static bool operator true(SomeClassWithAllOperators foo) => throw new NotImplementedException();
        public static bool operator false(SomeClassWithAllOperators foo) => throw new NotImplementedException();

        public static SomeClassWithAllOperators operator +(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator -(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator *(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator /(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator &(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator |(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator ^(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator ==(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator !=(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator >=(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator >(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator <=(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator <(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();

        public static SomeClassWithAllOperators operator >>(SomeClassWithAllOperators lhs, int offset) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator <<(SomeClassWithAllOperators lhs, int offset) => throw new NotImplementedException();
        public static SomeClassWithAllOperators operator %(SomeClassWithAllOperators lhs, SomeClassWithAllOperators rhs) => throw new NotImplementedException();

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}