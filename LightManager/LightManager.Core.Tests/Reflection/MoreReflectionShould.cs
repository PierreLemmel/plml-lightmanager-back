using NFluent;
using NUnit.Framework;
using LightManager.Tests.Reflection.SampleClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LightManager.Reflection;

namespace LightManager.Tests.Reflection
{
    public class MoreReflectionShould
    {
        #region GetGenericName / GetGenericFullName
        [Test]
        [TestCase(typeof(string), nameof(String))]
        [TestCase(typeof(SomeNonGenericClass), nameof(SomeNonGenericClass))]
        public void GetGenericName_Should_Return_Name_When_Provided_Non_Generic_Type(Type type, string expected)
        {
            string result = type.GetGenericName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(SomeGenericClassWith1Argument<string>), "SomeGenericClassWith1Argument<String>")]
        [TestCase(typeof(SomeGenericClassWith1Argument<SomeClass>), "SomeGenericClassWith1Argument<SomeClass>")]
        public void GetGenericName_Should_Return_Name_When_Provided_With_Generic_Type_With_1_Argument(Type type, string expected)
        {
            string result = type.GetGenericName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(SomeGenericClassWith1Argument<Func<int>>), "SomeGenericClassWith1Argument<Func<Int32>>")]
        [TestCase(typeof(SomeGenericClassWith1Argument<Action<List<string>>>), "SomeGenericClassWith1Argument<Action<List<String>>>")]
        public void GetGenericName_Should_Return_Name_When_Provided_With_Generic_Type_With_1_Argument_With_Nested_Gerenic(Type type, string expected)
        {
            string result = type.GetGenericName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(SomeGenericClassWith2Arguments<string, int>), "SomeGenericClassWith2Arguments<String, Int32>")]
        [TestCase(typeof(SomeGenericClassWith2Arguments<SomeClass, SomeClass>), "SomeGenericClassWith2Arguments<SomeClass, SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith3Arguments<string, int, string>), "SomeGenericClassWith3Arguments<String, Int32, String>")]
        [TestCase(typeof(SomeGenericClassWith3Arguments<SomeClass, SomeClass, SomeClass>), "SomeGenericClassWith3Arguments<SomeClass, SomeClass, SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith4Arguments<string, int, string, int>), "SomeGenericClassWith4Arguments<String, Int32, String, Int32>")]
        [TestCase(typeof(SomeGenericClassWith4Arguments<SomeClass, SomeClass, SomeClass, SomeClass>), "SomeGenericClassWith4Arguments<SomeClass, SomeClass, SomeClass, SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith5Arguments<string, int, string, int, string>), "SomeGenericClassWith5Arguments<String, Int32, String, Int32, String>")]
        [TestCase(typeof(SomeGenericClassWith5Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>), "SomeGenericClassWith5Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith6Arguments<string, int, string, int, string, int>), "SomeGenericClassWith6Arguments<String, Int32, String, Int32, String, Int32>")]
        [TestCase(typeof(SomeGenericClassWith6Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>), "SomeGenericClassWith6Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith7Arguments<string, int, string, int, string, int, string>), "SomeGenericClassWith7Arguments<String, Int32, String, Int32, String, Int32, String>")]
        [TestCase(typeof(SomeGenericClassWith7Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>), "SomeGenericClassWith7Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith8Arguments<string, int, string, int, string, int, string, int>), "SomeGenericClassWith8Arguments<String, Int32, String, Int32, String, Int32, String, Int32>")]
        [TestCase(typeof(SomeGenericClassWith8Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>), "SomeGenericClassWith8Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>")]
        public void GetGenericName_Should_Return_Name_When_Provided_With_Generic_Type_With_Multiple_Arguments(Type type, string expected)
        {
            string result = type.GetGenericName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(SomeGenericClassWith2Arguments<Action<List<string>>, Func<int>>), "SomeGenericClassWith2Arguments<Action<List<String>>, Func<Int32>>")]
        [TestCase(typeof(SomeGenericClassWith3Arguments<Action<List<string>>, SomeClass, Func<int>>), "SomeGenericClassWith3Arguments<Action<List<String>>, SomeClass, Func<Int32>>")]
        public void GetGenericName_Should_Return_Name_When_Provided_With_Generic_Type_With_Multiple_Arguments_With_Nested_Gerenics(Type type, string expected)
        {
            string result = type.GetGenericName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(string), "System.String")]
        [TestCase(typeof(SomeNonGenericClass), "LightManager.Tests.Reflection.SampleClasses.SomeNonGenericClass")]
        public void GetGenericFullName_Should_Return_FullName_When_Provided_Non_Generic_Type(Type type, string expected)
        {
            string result = type.GetGenericFullName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(SomeGenericClassWith1Argument<int>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith1Argument<System.Int32>")]
        [TestCase(typeof(SomeGenericClassWith1Argument<SomeClass>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith1Argument<LightManager.Tests.Reflection.SampleClasses.SomeClass>")]
        public void GetGenericFullName_Should_Return_FullName_When_Provided_With_Generic_Type_With_1_Argument(Type type, string expected)
        {
            string result = type.GetGenericFullName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(SomeGenericClassWith1Argument<Func<int>>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith1Argument<System.Func<System.Int32>>")]
        [TestCase(typeof(SomeGenericClassWith1Argument<Action<List<string>>>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith1Argument<System.Action<System.Collections.Generic.List<System.String>>>")]
        public void GetGenericFullName_Should_Return_FullName_When_Provided_With_Generic_Type_With_1_Argument_With_Nested_Gerenic(Type type, string expected)
        {
            string result = type.GetGenericFullName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(SomeGenericClassWith2Arguments<string, int>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith2Arguments<System.String, System.Int32>")]
        [TestCase(typeof(SomeGenericClassWith2Arguments<SomeClass, SomeClass>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith2Arguments<LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith3Arguments<string, int, string>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith3Arguments<System.String, System.Int32, System.String>")]
        [TestCase(typeof(SomeGenericClassWith3Arguments<SomeClass, SomeClass, SomeClass>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith3Arguments<LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith4Arguments<string, int, string, int>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith4Arguments<System.String, System.Int32, System.String, System.Int32>")]
        [TestCase(typeof(SomeGenericClassWith4Arguments<SomeClass, SomeClass, SomeClass, SomeClass>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith4Arguments<LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith5Arguments<string, int, string, int, string>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith5Arguments<System.String, System.Int32, System.String, System.Int32, System.String>")]
        [TestCase(typeof(SomeGenericClassWith5Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith5Arguments<LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith6Arguments<string, int, string, int, string, int>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith6Arguments<System.String, System.Int32, System.String, System.Int32, System.String, System.Int32>")]
        [TestCase(typeof(SomeGenericClassWith6Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith6Arguments<LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith7Arguments<string, int, string, int, string, int, string>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith7Arguments<System.String, System.Int32, System.String, System.Int32, System.String, System.Int32, System.String>")]
        [TestCase(typeof(SomeGenericClassWith7Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith7Arguments<LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass>")]
        [TestCase(typeof(SomeGenericClassWith8Arguments<string, int, string, int, string, int, string, int>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith8Arguments<System.String, System.Int32, System.String, System.Int32, System.String, System.Int32, System.String, System.Int32>")]
        [TestCase(typeof(SomeGenericClassWith8Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith8Arguments<LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass, LightManager.Tests.Reflection.SampleClasses.SomeClass>")]
        public void GetGenericFullName_Should_Return_FullName_When_Provided_With_Generic_Type_With_Multiple_Arguments(Type type, string expected)
        {
            string result = type.GetGenericFullName();
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        [TestCase(typeof(SomeGenericClassWith2Arguments<Action<List<string>>, Func<int>>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith2Arguments<System.Action<System.Collections.Generic.List<System.String>>, System.Func<System.Int32>>")]
        [TestCase(typeof(SomeGenericClassWith3Arguments<Action<List<string>>, SomeClass, Func<int>>), "LightManager.Tests.Reflection.SampleClasses.SomeGenericClassWith3Arguments<System.Action<System.Collections.Generic.List<System.String>>, LightManager.Tests.Reflection.SampleClasses.SomeClass, System.Func<System.Int32>>")]
        public void GetGenericFullName_Should_Return_FullName_When_Provided_With_Generic_Type_With_Multiple_Arguments_With_Nested_Generics(Type type, string expected)
        {
            string result = type.GetGenericFullName();
            Check.That(result).IsEqualTo(expected);
        }
        #endregion

        #region IsGenericVersionOf / GetGenericVersionOf
        [Test]
        [TestCase(typeof(SomeClass), typeof(string))]
        [TestCase(typeof(SomeClass), null)]
        [TestCase(typeof(SomeClass), typeof(SomeNonGenericClass))]
        public void IsGenericVersionOf_Should_Throw_InvalidOperation_When_Parameter_Is_Not_Open_Generic(Type input, Type param) => Check
            .ThatCode(() => input.IsGenericVersionOf(param))
            .Throws<InvalidOperationException>();

        [Test]
        [TestCase(typeof(SomeGenericClassWith1Argument<int>), typeof(SomeGenericClassWith1Argument<>))]
        [TestCase(typeof(SomeGenericClassWith2Arguments<int, int>), typeof(SomeGenericClassWith2Arguments<,>))]
        [TestCase(typeof(SomeGenericClassWith3Arguments<int, int, int>), typeof(SomeGenericClassWith3Arguments<,,>))]
        [TestCase(typeof(SomeGenericClassWith4Arguments<int, int, int, int>), typeof(SomeGenericClassWith4Arguments<,,,>))]
        [TestCase(typeof(SomeGenericClassWith5Arguments<int, int, int, int, int>), typeof(SomeGenericClassWith5Arguments<,,,,>))]
        [TestCase(typeof(SomeGenericClassWith6Arguments<int, int, int, int, int, int>), typeof(SomeGenericClassWith6Arguments<,,,,,>))]
        [TestCase(typeof(SomeGenericClassWith7Arguments<int, int, int, int, int, int, int>), typeof(SomeGenericClassWith7Arguments<,,,,,,>))]
        [TestCase(typeof(SomeGenericClassWith8Arguments<int, int, int, int, int, int, int, int>), typeof(SomeGenericClassWith8Arguments<,,,,,,,>))]
        public void IsGenericVersionOf_Should_Return_True_When_Input_Is_Generic_Version_Of_Parameter(Type input, Type param) => Check
            .That(input.IsGenericVersionOf(param)).IsTrue();

        [Test]
        [TestCase(typeof(int), typeof(SomeGenericClassWith1Argument<>))]
        [TestCase(typeof(SomeGenericClassWith1Argument<>), typeof(SomeGenericClassWith1Argument<>))]
        [TestCase(typeof(SomeOtherGenericClassWith1Argument<int>), typeof(SomeGenericClassWith1Argument<>))]
        [TestCase(typeof(int), typeof(SomeGenericClassWith2Arguments<,>))]
        [TestCase(typeof(SomeGenericClassWith2Arguments<,>), typeof(SomeGenericClassWith2Arguments<,>))]
        [TestCase(typeof(SomeOtherGenericClassWith2Arguments<int, int>), typeof(SomeGenericClassWith2Arguments<,>))]
        [TestCase(typeof(int), typeof(SomeGenericClassWith3Arguments<,,>))]
        [TestCase(typeof(SomeGenericClassWith3Arguments<,,>), typeof(SomeGenericClassWith3Arguments<,,>))]
        [TestCase(typeof(SomeOtherGenericClassWith3Arguments<int, int, int>), typeof(SomeGenericClassWith3Arguments<,,>))]
        [TestCase(typeof(int), typeof(SomeGenericClassWith4Arguments<,,,>))]
        [TestCase(typeof(SomeGenericClassWith4Arguments<,,,>), typeof(SomeGenericClassWith4Arguments<,,,>))]
        [TestCase(typeof(SomeOtherGenericClassWith4Arguments<int, int, int, int>), typeof(SomeGenericClassWith4Arguments<,,,>))]
        [TestCase(typeof(int), typeof(SomeGenericClassWith5Arguments<,,,,>))]
        [TestCase(typeof(SomeGenericClassWith5Arguments<,,,,>), typeof(SomeGenericClassWith5Arguments<,,,,>))]
        [TestCase(typeof(SomeOtherGenericClassWith5Arguments<int, int, int, int, int>), typeof(SomeGenericClassWith5Arguments<,,,,>))]
        [TestCase(typeof(int), typeof(SomeGenericClassWith6Arguments<,,,,,>))]
        [TestCase(typeof(SomeGenericClassWith6Arguments<,,,,,>), typeof(SomeGenericClassWith6Arguments<,,,,,>))]
        [TestCase(typeof(SomeOtherGenericClassWith6Arguments<int, int, int, int, int, int>), typeof(SomeGenericClassWith6Arguments<,,,,,>))]
        [TestCase(typeof(int), typeof(SomeGenericClassWith7Arguments<,,,,,,>))]
        [TestCase(typeof(SomeGenericClassWith7Arguments<,,,,,,>), typeof(SomeGenericClassWith7Arguments<,,,,,,>))]
        [TestCase(typeof(SomeOtherGenericClassWith7Arguments<int, int, int, int, int, int, int>), typeof(SomeGenericClassWith7Arguments<,,,,,,>))]
        [TestCase(typeof(int), typeof(SomeGenericClassWith8Arguments<,,,,,,,>))]
        [TestCase(typeof(SomeGenericClassWith8Arguments<,,,,,,,>), typeof(SomeGenericClassWith8Arguments<,,,,,,,>))]
        [TestCase(typeof(SomeOtherGenericClassWith8Arguments<int, int, int, int, int, int, int, int>), typeof(SomeGenericClassWith8Arguments<,,,,,,,>))]
        public void IsGenericVersionOf_Should_Return_False_When_Input_Is_not_Generic_Version_Of_Parameter(Type input, Type param) => Check
            .That(input.IsGenericVersionOf(param)).IsFalse();

        [Test]
        [TestCase(typeof(SomeClass), typeof(string))]
        [TestCase(typeof(SomeClass), null)]
        [TestCase(typeof(SomeClass), typeof(SomeNonGenericClass))]
        [TestCase(typeof(SomeClass), typeof(SomeGenericClassWith1Argument<>))]
        [TestCase(typeof(SomeClass), typeof(ISomeInterface))]
        public void GetGenericVersionsOfInterface_Should_Throw_InvalidOperation_When_Parameter_Is_Not_Open_Generic_Interface(Type input, Type param) => Check
            .ThatCode(() => input.GetGenericVersionsOfInterface(param))
            .Throws<InvalidOperationException>();

        [Test]
        [TestCase(typeof(ISomeInterface), typeof(ISomeGenericInterfaceWith1Argument<>))]
        [TestCase(typeof(SomeClass), typeof(ISomeGenericInterfaceWith1Argument<>))]
        [TestCase(typeof(ISomeInterface), typeof(ISomeGenericInterfaceWith2Arguments<,>))]
        [TestCase(typeof(SomeClass), typeof(ISomeGenericInterfaceWith2Arguments<,>))]
        [TestCase(typeof(ISomeInterface), typeof(ISomeGenericInterfaceWith3Arguments<,,>))]
        [TestCase(typeof(SomeClass), typeof(ISomeGenericInterfaceWith3Arguments<,,>))]
        [TestCase(typeof(ISomeInterface), typeof(ISomeGenericInterfaceWith4Arguments<,,,>))]
        [TestCase(typeof(SomeClass), typeof(ISomeGenericInterfaceWith4Arguments<,,,>))]
        [TestCase(typeof(ISomeInterface), typeof(ISomeGenericInterfaceWith5Arguments<,,,,>))]
        [TestCase(typeof(SomeClass), typeof(ISomeGenericInterfaceWith5Arguments<,,,,>))]
        [TestCase(typeof(ISomeInterface), typeof(ISomeGenericInterfaceWith6Arguments<,,,,,>))]
        [TestCase(typeof(SomeClass), typeof(ISomeGenericInterfaceWith6Arguments<,,,,,>))]
        [TestCase(typeof(ISomeInterface), typeof(ISomeGenericInterfaceWith7Arguments<,,,,,,>))]
        [TestCase(typeof(SomeClass), typeof(ISomeGenericInterfaceWith7Arguments<,,,,,,>))]
        [TestCase(typeof(ISomeInterface), typeof(ISomeGenericInterfaceWith8Arguments<,,,,,,,>))]
        [TestCase(typeof(SomeClass), typeof(ISomeGenericInterfaceWith8Arguments<,,,,,,,>))]
        public void GetGenericVersionsOfInterface_Should_Return_Empty_When_Input_Does_Not_Implement_Generic_Versions_Of_Interface(Type input, Type param) => Check
            .That(input.GetGenericVersionsOfInterface(param))
            .IsEmpty();

        [Test]
        [TestCase(typeof(ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith1Argument), typeof(ISomeGenericInterfaceWith1Argument<>), typeof(ISomeGenericInterfaceWith1Argument<int>))]
        [TestCase(typeof(ISomeInterfaceImplementingMultipleTimes_ISomeGenericInterfaceWith1Argument), typeof(ISomeGenericInterfaceWith1Argument<>), typeof(ISomeGenericInterfaceWith1Argument<int>), typeof(ISomeGenericInterfaceWith1Argument<string>), typeof(ISomeGenericInterfaceWith1Argument<SomeClass>))]
        [TestCase(typeof(ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith2Arguments), typeof(ISomeGenericInterfaceWith2Arguments<,>), typeof(ISomeGenericInterfaceWith2Arguments<int, int>))]
        [TestCase(typeof(ISomeInterfaceImplementingMultipleTimes_ISomeGenericInterfaceWith2Arguments), typeof(ISomeGenericInterfaceWith2Arguments<,>), typeof(ISomeGenericInterfaceWith2Arguments<int, int>), typeof(ISomeGenericInterfaceWith2Arguments<string, string>), typeof(ISomeGenericInterfaceWith2Arguments<SomeClass, SomeClass>))]
        [TestCase(typeof(ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith3Arguments), typeof(ISomeGenericInterfaceWith3Arguments<,,>), typeof(ISomeGenericInterfaceWith3Arguments<int, int, int>))]
        [TestCase(typeof(ISomeInterfaceImplementingMultipleTimes_ISomeGenericInterfaceWith3Arguments), typeof(ISomeGenericInterfaceWith3Arguments<,,>), typeof(ISomeGenericInterfaceWith3Arguments<int, int, int>), typeof(ISomeGenericInterfaceWith3Arguments<string, string, string>), typeof(ISomeGenericInterfaceWith3Arguments<SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith4Arguments), typeof(ISomeGenericInterfaceWith4Arguments<,,,>), typeof(ISomeGenericInterfaceWith4Arguments<int, int, int, int>))]
        [TestCase(typeof(ISomeInterfaceImplementingMultipleTimes_ISomeGenericInterfaceWith4Arguments), typeof(ISomeGenericInterfaceWith4Arguments<,,,>), typeof(ISomeGenericInterfaceWith4Arguments<int, int, int, int>), typeof(ISomeGenericInterfaceWith4Arguments<string, string, string, string>), typeof(ISomeGenericInterfaceWith4Arguments<SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith5Arguments), typeof(ISomeGenericInterfaceWith5Arguments<,,,,>), typeof(ISomeGenericInterfaceWith5Arguments<int, int, int, int, int>))]
        [TestCase(typeof(ISomeInterfaceImplementingMultipleTimes_ISomeGenericInterfaceWith5Arguments), typeof(ISomeGenericInterfaceWith5Arguments<,,,,>), typeof(ISomeGenericInterfaceWith5Arguments<int, int, int, int, int>), typeof(ISomeGenericInterfaceWith5Arguments<string, string, string, string, string>), typeof(ISomeGenericInterfaceWith5Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith6Arguments), typeof(ISomeGenericInterfaceWith6Arguments<,,,,,>), typeof(ISomeGenericInterfaceWith6Arguments<int, int, int, int, int, int>))]
        [TestCase(typeof(ISomeInterfaceImplementingMultipleTimes_ISomeGenericInterfaceWith6Arguments), typeof(ISomeGenericInterfaceWith6Arguments<,,,,,>), typeof(ISomeGenericInterfaceWith6Arguments<int, int, int, int, int, int>), typeof(ISomeGenericInterfaceWith6Arguments<string, string, string, string, string, string>), typeof(ISomeGenericInterfaceWith6Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith7Arguments), typeof(ISomeGenericInterfaceWith7Arguments<,,,,,,>), typeof(ISomeGenericInterfaceWith7Arguments<int, int, int, int, int, int, int>))]
        [TestCase(typeof(ISomeInterfaceImplementingMultipleTimes_ISomeGenericInterfaceWith7Arguments), typeof(ISomeGenericInterfaceWith7Arguments<,,,,,,>), typeof(ISomeGenericInterfaceWith7Arguments<int, int, int, int, int, int, int>), typeof(ISomeGenericInterfaceWith7Arguments<string, string, string, string, string, string, string>), typeof(ISomeGenericInterfaceWith7Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(ISomeInterfaceImplementingOnce_ISomeGenericInterfaceWith8Arguments), typeof(ISomeGenericInterfaceWith8Arguments<,,,,,,,>), typeof(ISomeGenericInterfaceWith8Arguments<int, int, int, int, int, int, int, int>))]
        [TestCase(typeof(ISomeInterfaceImplementingMultipleTimes_ISomeGenericInterfaceWith8Arguments), typeof(ISomeGenericInterfaceWith8Arguments<,,,,,,,>), typeof(ISomeGenericInterfaceWith8Arguments<int, int, int, int, int, int, int, int>), typeof(ISomeGenericInterfaceWith8Arguments<string, string, string, string, string, string, string, string>), typeof(ISomeGenericInterfaceWith8Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(SomeClassImplementingOnce_ISomeGenericInterfaceWith1Argument), typeof(ISomeGenericInterfaceWith1Argument<>), typeof(ISomeGenericInterfaceWith1Argument<int>))]
        [TestCase(typeof(SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith1Argument), typeof(ISomeGenericInterfaceWith1Argument<>), typeof(ISomeGenericInterfaceWith1Argument<int>), typeof(ISomeGenericInterfaceWith1Argument<string>), typeof(ISomeGenericInterfaceWith1Argument<SomeClass>))]
        [TestCase(typeof(SomeClassImplementingOnce_ISomeGenericInterfaceWith2Arguments), typeof(ISomeGenericInterfaceWith2Arguments<,>), typeof(ISomeGenericInterfaceWith2Arguments<int, int>))]
        [TestCase(typeof(SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith2Arguments), typeof(ISomeGenericInterfaceWith2Arguments<,>), typeof(ISomeGenericInterfaceWith2Arguments<int, int>), typeof(ISomeGenericInterfaceWith2Arguments<string, string>), typeof(ISomeGenericInterfaceWith2Arguments<SomeClass, SomeClass>))]
        [TestCase(typeof(SomeClassImplementingOnce_ISomeGenericInterfaceWith3Arguments), typeof(ISomeGenericInterfaceWith3Arguments<,,>), typeof(ISomeGenericInterfaceWith3Arguments<int, int, int>))]
        [TestCase(typeof(SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith3Arguments), typeof(ISomeGenericInterfaceWith3Arguments<,,>), typeof(ISomeGenericInterfaceWith3Arguments<int, int, int>), typeof(ISomeGenericInterfaceWith3Arguments<string, string, string>), typeof(ISomeGenericInterfaceWith3Arguments<SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(SomeClassImplementingOnce_ISomeGenericInterfaceWith4Arguments), typeof(ISomeGenericInterfaceWith4Arguments<,,,>), typeof(ISomeGenericInterfaceWith4Arguments<int, int, int, int>))]
        [TestCase(typeof(SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith4Arguments), typeof(ISomeGenericInterfaceWith4Arguments<,,,>), typeof(ISomeGenericInterfaceWith4Arguments<int, int, int, int>), typeof(ISomeGenericInterfaceWith4Arguments<string, string, string, string>), typeof(ISomeGenericInterfaceWith4Arguments<SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(SomeClassImplementingOnce_ISomeGenericInterfaceWith5Arguments), typeof(ISomeGenericInterfaceWith5Arguments<,,,,>), typeof(ISomeGenericInterfaceWith5Arguments<int, int, int, int, int>))]
        [TestCase(typeof(SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith5Arguments), typeof(ISomeGenericInterfaceWith5Arguments<,,,,>), typeof(ISomeGenericInterfaceWith5Arguments<int, int, int, int, int>), typeof(ISomeGenericInterfaceWith5Arguments<string, string, string, string, string>), typeof(ISomeGenericInterfaceWith5Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(SomeClassImplementingOnce_ISomeGenericInterfaceWith6Arguments), typeof(ISomeGenericInterfaceWith6Arguments<,,,,,>), typeof(ISomeGenericInterfaceWith6Arguments<int, int, int, int, int, int>))]
        [TestCase(typeof(SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith6Arguments), typeof(ISomeGenericInterfaceWith6Arguments<,,,,,>), typeof(ISomeGenericInterfaceWith6Arguments<int, int, int, int, int, int>), typeof(ISomeGenericInterfaceWith6Arguments<string, string, string, string, string, string>), typeof(ISomeGenericInterfaceWith6Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(SomeClassImplementingOnce_ISomeGenericInterfaceWith7Arguments), typeof(ISomeGenericInterfaceWith7Arguments<,,,,,,>), typeof(ISomeGenericInterfaceWith7Arguments<int, int, int, int, int, int, int>))]
        [TestCase(typeof(SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith7Arguments), typeof(ISomeGenericInterfaceWith7Arguments<,,,,,,>), typeof(ISomeGenericInterfaceWith7Arguments<int, int, int, int, int, int, int>), typeof(ISomeGenericInterfaceWith7Arguments<string, string, string, string, string, string, string>), typeof(ISomeGenericInterfaceWith7Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>))]
        [TestCase(typeof(SomeClassImplementingOnce_ISomeGenericInterfaceWith8Arguments), typeof(ISomeGenericInterfaceWith8Arguments<,,,,,,,>), typeof(ISomeGenericInterfaceWith8Arguments<int, int, int, int, int, int, int, int>))]
        [TestCase(typeof(SomeClassImplementingMultipleTimes_ISomeGenericInterfaceWith8Arguments), typeof(ISomeGenericInterfaceWith8Arguments<,,,,,,,>), typeof(ISomeGenericInterfaceWith8Arguments<int, int, int, int, int, int, int, int>), typeof(ISomeGenericInterfaceWith8Arguments<string, string, string, string, string, string, string, string>), typeof(ISomeGenericInterfaceWith8Arguments<SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass, SomeClass>))]
        public void GetGenericVersionsOfInterface_Should_Return_Correct_Value_When_Input_Does_Implement_Versions_Of_Interface(Type input, Type param, params Type[] expected) => Check
            .That(input.GetGenericVersionsOfInterface(param))
            .IsEquivalentTo(expected);

        [Test]
        [TestCase(typeof(SomeClass), typeof(SomeGenericClass<>))]
        [TestCase(typeof(SomeDerivedClass), typeof(SomeGenericClass<>))]
        public void GetGenericVersionOfClassInInheritanceTree_Should_Throw_InvalidOperationException_When_Class_Does_Not_Have_Open_Generic_In_Its_Inheritance_Tree(Type input, Type param) => Check
            .ThatCode(() => input.GetGenericVersionOfClassInInheritanceTree(param))
            .Throws<InvalidOperationException>();

        [Test]
        [TestCase(typeof(SomeNonGenericClassDerivedFromSomeGenericClass), typeof(SomeGenericClass<>), typeof(SomeGenericClass<SomeClass>))]
        public void GetGenericVersionOfClassInInheritanceTree_Should_Return_Expected_When_Input_Has_Open_Generic_In_Its_Inheritance_Tree(Type input, Type param, Type expected) => Check
            .That(input.GetGenericVersionOfClassInInheritanceTree(param))
            .IsEqualTo(expected);
        #endregion

        #region InheritsFrom / Implements
        [Test]
        [TestCase(typeof(SomeDerivedClass))]
        [TestCase(typeof(SomeMoreDerivedClass))]
        public void InheritsFrom_Returns_True_When_Class_Inherits_From(Type input) => Check.That(input.InheritsFrom<SomeClass>()).IsTrue();

        [Test]
        [TestCase(typeof(object))]
        [TestCase(typeof(SomeOtherClass))]
        public void InheritsFrom_Returns_False_When_Class_Does_Not_Inherits_From(Type input) => Check.That(input.InheritsFrom<SomeClass>()).IsFalse();

        [Test]
        [TestCase(typeof(SomeEnum))]
        [TestCase(typeof(SomeStruct))]
        [TestCase(typeof(ISomeInterface))]
        public void InheritsFrom_Returns_False_When_Input_Is_Not_Class(Type input) => Check.That(input.InheritsFrom<SomeClass>()).IsFalse();

        [Test]
        public void InheritsFrom_Returns_False_When_Input_Is_Same_Class_And_CanBeEqualToClassType_Is_False() => Check.That(typeof(SomeClass).InheritsFrom<SomeClass>(canBeEqualToClassType : false)).IsFalse();

        [Test]
        public void InheritsFrom_Returns_True_When_Input_Is_Same_Class_And_CanBeEqualToClassType_Is_True() => Check.That(typeof(SomeClass).InheritsFrom<SomeClass>(canBeEqualToClassType: true)).IsTrue();

        [Test]
        [TestCase(typeof(SomeDerivedGenericClass<>), typeof(SomeGenericClass<>))]
        [TestCase(typeof(SomeMoreDerivedGenericClass<>), typeof(SomeGenericClass<>))]
        public void InheritsFrom_Supports_OpenGenerics(Type input, Type baseClass) => Check.That(input.InheritsFrom(baseClass)).IsTrue();

        [Test]
        [TestCase(typeof(int), typeof(SomeGenericClass<>))]
        [TestCase(typeof(SomeOtherGenericClass<>), typeof(SomeGenericClass<>))]
        public void InheritsFrom_Returns_False_If_Input_Does_Not_Inherit_From_Generic_Input(Type input, Type baseClass) => Check.That(input.InheritsFrom(baseClass)).IsFalse();

        [Test]
        public void InheritsFrom_Returns_False_When_Input_Is_Same_Class_And_CanBeEqualToClassType_Is_False_With_Open_Generics() => Check.That(typeof(SomeGenericClass<>).InheritsFrom(typeof(SomeGenericClass<>), canBeEqualToClassType: false)).IsFalse();

        [Test]
        public void InheritsFrom_Returns_True_When_Input_Is_Same_Class_And_CanBeEqualToClassType_Is_True_With_Open_Generics() => Check.That(typeof(SomeGenericClass<>).InheritsFrom(typeof(SomeGenericClass<>), canBeEqualToClassType: true)).IsTrue();

        [Test]
        [TestCase(typeof(SomeGenericClassDerivedFromSomeNonGenericParent<>), typeof(SomeNonGenericClass))]
        public void InheritsFrom_Returns_True_When_OpenGeneric_Inherits_From_Non_Generic_Class(Type openGenericChild, Type nonGenericParent) => Check.That(openGenericChild.InheritsFrom(nonGenericParent)).IsTrue();

        [Test]
        [TestCase(typeof(SomeNonGenericClassDerivedFromSomeGenericClass), typeof(SomeGenericClass<>))]
        public void InheritsFrom_Returns_True_When_NonGeneric_Class_Has_Generic_Parent_In_Inheritance_Tree(Type nonGenericDerivedFromGeneric, Type genericParent) => Check.That(nonGenericDerivedFromGeneric.InheritsFrom(genericParent)).IsTrue();

        [Test]
        [TestCase(typeof(SomeClassImplementingSomeInterface))]
        [TestCase(typeof(SomeClassDerivedFromSomeClassImplementingSomeInterface))]
        public void Implements_Returns_True_When_Class_Implements_Interface(Type input) => Check.That(input.Implements<ISomeInterface>()).IsTrue();

        [Test]
        [TestCase(typeof(object))]
        [TestCase(typeof(SomeClass))]
        [TestCase(typeof(SomeClassImplementingSomeOtherInterface))]
        public void Implements_Returns_False_When_Class_Does_Not_Implement_Interface(Type input) => Check.That(input.Implements<ISomeInterface>()).IsFalse();

        [Test]
        [TestCase(typeof(ISomeDerivedInterface))]
        public void Implements_Returns_False_When_Input_Is_Interface(Type input) => Check.That(input.Implements<ISomeInterface>()).IsFalse(); 
        #endregion

    }
}