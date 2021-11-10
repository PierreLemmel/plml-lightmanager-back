using LightManager.Reflection;
using LightManager.Tests.Reflection.SampleClasses;

namespace LightManager.Tests.Reflection
{
    public class TypeFiltersShould
    {
        [Test]
        public void Classes_Returns_Expected_Result()
        {
            IEnumerable<Type> input = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
                typeof(SomeOtherDerivedClass),
                typeof(ISomeInterface),
                typeof(SomeStruct),
                typeof(SomeAbstractClass)
            };

            IEnumerable<Type> result = input.Classes();

            IEnumerable<Type> expected = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
                typeof(SomeOtherDerivedClass),
                typeof(SomeAbstractClass)
            };

            Check.That(result).IsEquivalentTo(expected);
        }

        [Test]
        public void ConcreteTypes_Returns_Expected_Result()
        {
            IEnumerable<Type> input = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
                typeof(SomeOtherDerivedClass),
                typeof(ISomeInterface),
                typeof(SomeStruct),
                typeof(SomeAbstractClass)
            };

            IEnumerable<Type> result = input.ConcreteTypes();

            IEnumerable<Type> expected = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
                typeof(SomeOtherDerivedClass),
            };

            Check.That(result).IsEquivalentTo(expected);
        }

        [Test]
        public void Inheriting_From_Generic_Version_Contains_Derived_Classes_But_Not_Self_When_CanBeEqualToClassType_Is_False()
        {
            IEnumerable<Type> input = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
                typeof(SomeOtherDerivedClass),
                typeof(ISomeInterface),
                typeof(SomeStruct)
            };

            IEnumerable<Type> result = input.InheritingFrom<SomeClass>(canBeEqualToClassType: false);

            IEnumerable<Type> expected = new Type[]
            {
                typeof(SomeOtherDerivedClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
            };

            Check.That(result).IsEquivalentTo(expected);
        }

        [Test]
        public void Inheriting_From_Generic_Version_Contains_Derived_Classes_Including_Self_When_CanBeEqualToClassType_Is_True()
        {
            IEnumerable<Type> input = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
                typeof(SomeOtherDerivedClass),
                typeof(ISomeInterface),
                typeof(SomeStruct)
            };

            IEnumerable<Type> result = input.InheritingFrom<SomeClass>(canBeEqualToClassType : true);

            IEnumerable<Type> expected = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherDerivedClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
            };

            Check.That(result).IsEquivalentTo(expected);
        }

        [Test]
        public void Inheriting_From_Non_Generic_Version_Contains_Derived_Classes_But_Not_Self_When_CanBeEqualToClassType_Is_False()
        {
            IEnumerable<Type> input = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
                typeof(SomeOtherDerivedClass),
                typeof(ISomeInterface),
                typeof(SomeStruct)
            };

            IEnumerable<Type> result = input.InheritingFrom(typeof(SomeClass), canBeEqualToClassType: false);

            IEnumerable<Type> expected = new Type[]
            {
                typeof(SomeOtherDerivedClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
            };

            Check.That(result).IsEquivalentTo(expected);
        }

        [Test]
        public void Inheriting_From_Non_Generic_Version_Contains_Derived_Classes_Including_Self_When_CanBeEqualToClassType_Is_True()
        {
            IEnumerable<Type> input = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
                typeof(SomeOtherDerivedClass),
                typeof(ISomeInterface),
                typeof(SomeStruct)
            };

            IEnumerable<Type> result = input.InheritingFrom(typeof(SomeClass), canBeEqualToClassType: true);

            IEnumerable<Type> expected = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherDerivedClass),
                typeof(SomeDerivedClass),
                typeof(SomeMoreDerivedClass),
            };

            Check.That(result).IsEquivalentTo(expected);
        }

        [Test]
        public void MakeGenericsFor_Returns_Expected_Result()
        {
            IEnumerable<Type> input = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeStruct)
            };

            IEnumerable<Type> result = input.MakeGenericsFor(typeof(SomeGenericClass<>));

            IEnumerable<Type> expected = new Type[]
            {
                typeof(SomeGenericClass<SomeClass>),
                typeof(SomeGenericClass<SomeOtherClass>),
                typeof(SomeGenericClass<SomeDerivedClass>),
                typeof(SomeGenericClass<SomeStruct>)
            };

            Check.That(result).IsEquivalentTo(expected);
        }

        [Test]
        public void ExcludeGenericTypeDefinitions_Returns_Expected_Result()
        {
            IEnumerable<Type> input = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeGenericClass<>),
                typeof(SomeGenericClass<int>),
                typeof(SomeGenericClassWith1Argument<>),
                typeof(SomeGenericClassWith1Argument<int>),
                typeof(SomeGenericClassWith2Arguments<,>),
                typeof(SomeGenericClassWith2Arguments<int, int>),
                typeof(SomeGenericClassWith3Arguments<,,>),
                typeof(SomeGenericClassWith3Arguments<int, int, int>),
                typeof(SomeGenericClassWith4Arguments<,,,>),
                typeof(SomeGenericClassWith4Arguments<int, int, int, int>),
                typeof(SomeGenericClassWith5Arguments<,,,,>),
                typeof(SomeGenericClassWith5Arguments<int, int, int, int, int>),
                typeof(SomeGenericClassWith6Arguments<,,,,,>),
                typeof(SomeGenericClassWith6Arguments<int, int, int, int, int, int>),
                typeof(SomeGenericClassWith7Arguments<,,,,,,>),
                typeof(SomeGenericClassWith7Arguments<int, int, int, int, int, int, int>),
                typeof(SomeGenericClassWith8Arguments<,,,,,,,>),
                typeof(SomeGenericClassWith8Arguments<int, int, int, int, int, int, int, int>),
                typeof(SomeStruct)
            };

            IEnumerable<Type> result = input.ExcludeGenericTypeDefinitions();

            IEnumerable<Type> expected = new Type[]
            {
                typeof(SomeClass),
                typeof(SomeOtherClass),
                typeof(SomeDerivedClass),
                typeof(SomeGenericClass<int>),
                typeof(SomeGenericClassWith1Argument<int>),
                typeof(SomeGenericClassWith2Arguments<int, int>),
                typeof(SomeGenericClassWith3Arguments<int, int, int>),
                typeof(SomeGenericClassWith4Arguments<int, int, int, int>),
                typeof(SomeGenericClassWith5Arguments<int, int, int, int, int>),
                typeof(SomeGenericClassWith6Arguments<int, int, int, int, int, int>),
                typeof(SomeGenericClassWith7Arguments<int, int, int, int, int, int, int>),
                typeof(SomeGenericClassWith8Arguments<int, int, int, int, int, int, int, int>),
                typeof(SomeStruct)
            };

            Check.That(result).IsEquivalentTo(expected);
        }


        [Test]
        [TestCaseSource(nameof(MapGenericDefinitionsTestCases))]
        public void MapGenericDefinitions_Returns_Expected_Result(IEnumerable<Type> types, IEnumerable<GenericTypeMap> typeMaps, IEnumerable<Type> expected) => Check
            .That(types.MapGenericDefinitions(typeMaps))
            .IsEquivalentTo(expected);

        public static IEnumerable<object[]> MapGenericDefinitionsTestCases => new object[][]
        {
            new object[]
            {
                new Type[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(string),
                    typeof(SomeGenericClass<int>),
                    typeof(SomeClass)
                },
                Array.Empty<GenericTypeMap>(),
                new Type[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(string),
                    typeof(SomeGenericClass<int>),
                    typeof(SomeClass)
                },
            },
            new object[]
            {
                new Type[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(string),
                    typeof(SomeGenericClass<>),
                    typeof(SomeClass)
                },
                new GenericTypeMap[]
                {
                    new GenericTypeMap(
                        typeof(SomeGenericClass<>),
                        new Type[]
                        {
                            typeof(int),
                            typeof(SomeClass),
                            typeof(string),
                        })
                },
                new Type[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(string),
                    typeof(SomeGenericClass<int>),
                    typeof(SomeGenericClass<SomeClass>),
                    typeof(SomeGenericClass<string>),
                    typeof(SomeClass)
                },
            },
            new object[]
            {
                new Type[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(string),
                    typeof(SomeClass)
                },
                new GenericTypeMap[]
                {
                    new GenericTypeMap(
                        typeof(SomeOtherGenericClass<>),
                        new Type[]
                        {
                            typeof(int),
                            typeof(SomeClass),
                            typeof(string),
                        })
                },
                new Type[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(string),
                    typeof(SomeClass)
                },
            },
            new object[]
            {
                new Type[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(string),
                    typeof(SomeGenericClass<>),
                    typeof(SomeClass),
                    typeof(SomeOtherGenericClass<>),
                },
                new GenericTypeMap[]
                {
                    new GenericTypeMap(
                        typeof(SomeGenericClass<>),
                        new Type[]
                        {
                            typeof(int),
                            typeof(SomeClass),
                            typeof(string),
                            typeof(SomeOtherClass),
                        }),
                    new GenericTypeMap(
                        typeof(SomeOtherGenericClass<>),
                        new Type[]
                        {
                            typeof(ISomeInterface),
                            typeof(int),
                            typeof(string),
                            typeof(float),
                            typeof(SomeStruct),
                        }),
                },
                new Type[]
                {
                    typeof(int),
                    typeof(float),
                    typeof(string),
                    typeof(SomeGenericClass<int>),
                    typeof(SomeGenericClass<SomeClass>),
                    typeof(SomeGenericClass<string>),
                    typeof(SomeGenericClass<SomeOtherClass>),
                    typeof(SomeClass),
                    typeof(SomeOtherGenericClass<ISomeInterface>),
                    typeof(SomeOtherGenericClass<int>),
                    typeof(SomeOtherGenericClass<string>),
                    typeof(SomeOtherGenericClass<float>),
                    typeof(SomeOtherGenericClass<SomeStruct>),
                },
            },
        };
    }
}