using LightManager.Core.Tests.Helpers.SampleClasses;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Core.Tests.Helpers
{
    public class MoreLinqShould
    {
        [Test]
        public void IsEmpty_Returns_True_If_Sequence_Is_Empty()
        {
            IEnumerable<int> empty = Enumerable.Empty<int>();
            bool isEmpty = empty.IsEmpty();

            Check.That(isEmpty).IsTrue();
        }

        [Test]
        public void IsEmpty_Returns_False_If_Sequence_Is_Not_Empty()
        {
            IEnumerable<int> notEmpty = Enumerable.Range(0, 18000);
            bool isEmpty = notEmpty.IsEmpty();

            Check.That(isEmpty).IsFalse();
        }

        [Test]
        public void IsEmpty_Should_Not_Iterate_Over_The_Whole_Sequence()
        {
            IEnumerable<int> sequence = new EnumerableThatThrowsAfterACertainNumberOfIterations<int>(5);
            Check.ThatCode(() => sequence.IsEmpty())
                .DoesNotThrow();
        }

        [Test]
        public void IsSingle_Returns_False_If_Sequence_Is_Empty()
        {
            IEnumerable<int> empty = Enumerable.Empty<int>();
            bool isSingle = empty.IsSingle();

            Check.That(isSingle).IsFalse();
        }

        [Test]
        public void IsSingle_Returns_True_If_Sequence_Has_Single_Element()
        {
            IEnumerable<int> single = new int[] { 42 };
            bool isSingle = single.IsSingle();

            Check.That(isSingle).IsTrue();
        }

        [Test]
        public void IsSingle_Returns_False_If_Sequence_Has_Multiple_Elements()
        {
            IEnumerable<int> multiple = Enumerable.Range(18, 42);
            bool isSingle = multiple.IsSingle();

            Check.That(isSingle).IsFalse();
        }

        [Test]
        public void IsSingle_Should_Not_Iterate_Over_The_Whole_Sequence()
        {
            IEnumerable<int> sequence = new EnumerableThatThrowsAfterACertainNumberOfIterations<int>(5);
            Check.ThatCode(() => sequence.IsSingle())
                .DoesNotThrow();
        }

        [Test]
        public void TryGetSingle_Returns_False_If_Sequence_Is_Empty()
        {
            IEnumerable<int> empty = Enumerable.Empty<int>();
            bool isSingle = empty.TryGetSingle(out _);

            Check.That(isSingle).IsFalse();
        }

        [Test]
        public void TryGetSingle_Returns_True_If_Sequence_Has_Single_Element()
        {
            IEnumerable<int> single = new int[] { 42 };
            bool isSingle = single.TryGetSingle(out _);

            Check.That(isSingle).IsTrue();
        }

        [Test]
        public void TryGetSingle_Returns_False_If_Sequence_Has_Multiple_Elements()
        {
            IEnumerable<int> multiple = Enumerable.Range(18, 42);
            bool isSingle = multiple.TryGetSingle(out _);

            Check.That(isSingle).IsFalse();
        }

        [Test]
        public void TryGetSingle_Should_Not_Iterate_Over_The_Whole_Sequence()
        {
            IEnumerable<int> sequence = new EnumerableThatThrowsAfterACertainNumberOfIterations<int>(5);
            Check.ThatCode(() => sequence.TryGetSingle(out _))
                .DoesNotThrow();
        }

        [Test]
        public void TryGetSingle_Returns_Null_When_False_On_Reference_Types()
        {
            IEnumerable<string> sequenceReturningFalse = Enumerable.Empty<string>();

            bool result = sequenceReturningFalse.TryGetSingle(out string? value);

            Check.That(result).IsFalse();
            Check.That(value).IsNull();
        }

        [Test]
        public void TryGetSingle_Returns_Default_When_False_On_Value_Types()
        {
            IEnumerable<int> sequenceReturningFalse = Enumerable.Empty<int>();

            bool result = sequenceReturningFalse.TryGetSingle(out int value);

            Check.That(result).IsFalse();
            Check.That(value).IsDefaultValue();
        }

        [Test]
        public void TryGetSingle_Returns_Expected_When_Element_Is_Single()
        {
            const int singleValue = 17;

            IEnumerable<int> single = new int[] { singleValue };

            bool result = single.TryGetSingle(out int value);

            Check.That(result).IsTrue();
            Check.That(value).IsEqualTo(singleValue);
        }


        [Test]
        public void TryFind_Returns_False_And_Has_Default_Output_When_Element_Cannot_Be_Found_Value_Type()
        {
            IEnumerable<int> sequence = Enumerables.Create(3, 421, 55);

            bool result = sequence.TryFind(i => i % 2 == 0, out int value);

            Check.That(result).IsFalse();
            Check.That(value).IsDefaultValue();
        }

        [Test]
        public void TryFind_Returns_False_And_Has_Default_Output_When_Element_Cannot_Be_Found_Reference_Type()
        {
            IEnumerable<string> sequence = Enumerables.Create("abc", "bac", "cba");

            bool result = sequence.TryFind(s => s!.Length > 3, out string? value);

            Check.That(result).IsFalse();
            Check.That(value).IsNull();
        }

        [Test]
        public void TryFind_Returns_True_And_Outputs_First_Matching_Element_when_Mathching_Elements_Can_Be_Found_Value_Type()
        {
            IEnumerable<int> sequence = Enumerables.Create(3, 421, 55, 433);

            bool result = sequence.TryFind(i => i > 400, out int value);

            Check.That(result).IsTrue();
            Check.That(value).IsEqualTo(421);
        }

        [Test]
        public void TryFind_Returns_True_And_Outputs_First_Matching_Element_when_Mathching_Elements_Can_Be_Found_Reference_Type()
        {
            IEnumerable<string> sequence = Enumerables.Create("abc", "hello", "bac", "cba", "world");

            bool result = sequence.TryFind(s => s!.Length > 3, out string? value);

            Check.That(result).IsTrue();
            Check.That(value).IsEqualTo("hello");
        }


        [Test]
        public void TryFind_Allow_Usage_Of_Output_Variable_When_Result_Is_True_With_Null_Safety()
        {
            IEnumerable<string> sequence = Enumerables.Create("abc", "bac", "foo");

            void SomeFunctionRequiringSomeNonNullString(string foo) { }

            if (sequence.TryFind(s => false, out string? value))
            {
                SomeFunctionRequiringSomeNonNullString(value);
            }
        }

        [Test]
        public void AreAllDistincts_Returns_True_When_All_Elements_Are_Distinct()
        {
            IEnumerable<int> sequence = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            Check.That(sequence.AreAllDistinct()).IsTrue();
        }

        [Test]
        public void AreAllDistincts_Returns_False_When_All_Elements_Are_Not_Distinct()
        {
            IEnumerable<int> sequence = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 3 };

            Check.That(sequence.AreAllDistinct()).IsFalse();
        }


        [Test]
        [TestCaseSource(nameof(AppendTestCaseData))]
        public void Append_Returns_Expected_Result(IEnumerable<int> sequence, IEnumerable<int> items, IEnumerable<int> expected)
        {
            IEnumerable<int> result = sequence.Append(items);

            Check.That(result).IsEquivalentTo(expected);
        }

        public static IEnumerable<object[]> AppendTestCaseData => new object[][]
        {
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                new int[] { 3, 9, 12 },
                new int[] { 4, 5, 8, 9, 12, 3, 9, 12 },
            },
            new object[]
            {
                Array.Empty<int>(),
                new int[] { 3, 9, 12 },
                new int[] { 3, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                Array.Empty<int>(),
                new int[] { 4, 5, 8, 9, 12 },
            },
        };

        #region WithoutIndex
        [Test]
        [TestCaseSource(nameof(WithoutIndexTestCaseData))]
        public void WithoutIndex_Returns_Expected_Result(IEnumerable<int> sequence, int index, IEnumerable<int> expected)
        {
            IEnumerable<int> result = sequence.WithoutIndex(index);

            Check.That(result).IsEquivalentTo(expected);
        }

        public static IEnumerable<object[]> WithoutIndexTestCaseData => new object[][]
        {
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                2,
                new int[] { 4, 5, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                0,
                new int[] { 5, 8, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                4,
                new int[] { 4, 5, 8, 9 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                666,
                new int[] { 4, 5, 8, 9, 12 },
            },
        };

        [Test]
        [TestCaseSource(nameof(WithoutIndexesTestCaseData))]
        public void WithoutIndexes_Returns_Expected_Result(IEnumerable<int> sequence, IEnumerable<int> indexes, IEnumerable<int> expected)
        {
            IEnumerable<int> result = sequence.WithoutIndexes(indexes);

            Check.That(result).IsEquivalentTo(expected);
        }

        public static IEnumerable<object[]> WithoutIndexesTestCaseData => new object[][]
        {
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                Array.Empty<int>(),
                new int[] { 4, 5, 8, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                new int[] { 0, 3, 4 },
                new int[] { 5, 8 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                new int[] { 666, 1231, -4},
                new int[] { 4, 5, 8, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                new int[] { 2, 1},
                new int[] { 4, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                new int[] { 1, 1, 1},
                new int[] { 4, 8, 9, 12 },
            },
        };
        #endregion

        #region Replace At Index
        [Test]
        [TestCaseSource(nameof(ReplaceAtIndexTestCaseData))]
        public void ReplaceAtIndex_Returns_Expected_Result(IEnumerable<int> sequence, int index, int value, IEnumerable<int> expected)
        {
            IEnumerable<int> result = sequence.ReplaceAtIndex(index, value);

            Check.That(result).IsEquivalentTo(expected);
        }

        public static IEnumerable<object[]> ReplaceAtIndexTestCaseData => new object[][]
        {
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                2,
                18742,
                new int[] { 4, 5, 18742, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                0,
                18742,
                new int[] { 18742, 5, 8, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                4,
                18742,
                new int[] { 4, 5, 8, 9, 18742 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                666,
                18742,
                new int[] { 4, 5, 8, 9, 12 },
            },
        };

        [Test]
        public void ReplaceAtIndexes_Throws_Invalid_Operation_Exception_When_Multiple_Data_With_Same_Index()
        {
            IEnumerable<int> sequence = new int[] { 8, 5, 12, 47, 22 };
            IEnumerable<(int index, int newValue)> replaceData = new (int index, int newValue)[]
            {
                (index: 2, newValue: 14),
                (index: 2, newValue: 42),
                (index: 2, newValue: 873),
            };

            Check.ThatCode(() => sequence.ReplaceAtIndexes(replaceData))
                .Throws<InvalidOperationException>();
        }

        [Test]
        [TestCaseSource(nameof(ReplaceAtIndexesTestCaseData))]
        public void ReplaceAtIndexes_Returns_Expected_Result(IEnumerable<int> sequence, IEnumerable<(int index, int newValue)> replaceData, IEnumerable<int> expected)
        {
            IEnumerable<int> result = sequence.ReplaceAtIndexes(replaceData);

            Check.That(result).IsEquivalentTo(expected);
        }

        public static IEnumerable<object[]> ReplaceAtIndexesTestCaseData => new object[][]
        {
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                Array.Empty<(int index, int newValue)>(),
                new int[] { 4, 5, 8, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                new (int index, int newValue)[]
                {
                    (index: 0, newValue: 75),
                    (index: 3, newValue: 57),
                    (index: 4, newValue: 705),
                },
                new int[] { 75, 5, 8, 57, 705 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                new (int index, int newValue)[]
                {
                    (index: 666, newValue: 75),
                    (index: 1231, newValue: 57),
                    (index: -4, newValue: 705),
                },
                new int[] { 4, 5, 8, 9, 12 },
            },
            new object[]
            {
                new int[] { 4, 5, 8, 9, 12 },
                new (int index, int newValue)[]
                {
                    (index: 2, newValue: 75),
                    (index: 1, newValue: 57),
                },
                new int[] { 4, 57, 75, 9, 12 },
            },
        };
        #endregion

        #region Reverse
        [Test]
        [TestCaseSource(nameof(ReverseTestCaseData))]
        public void Reverse_Should_Return_Expected(IEnumerable<int> input, IEnumerable<int> expected)
        {
            IEnumerable<int> result = input.Reverse();

            Check.That(result).IsEquivalentTo(expected);
        }

        public static IEnumerable<object[]> ReverseTestCaseData => new object[][]
        {
            new object[]
            {
                Array.Empty<int>(),
                Array.Empty<int>()
            },
            new object[]
            {
                new int[] { 1, 2, 3, 4 },
                new int[] { 4, 3, 2, 1 },
            },
            new object[]
            {
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 },
            },
        };
        #endregion

        #region Enumerables.Merge
        [Test]
        [TestCaseSource(nameof(EnumerablesMergeTestCaseData))]
        public void Enumerables_merge_Returns_Expected(IEnumerable<IEnumerable<int>> inputs, IEnumerable<int> expected)
        {
            IEnumerable<int> result = Enumerables.Merge(inputs);

            Check.That(result).IsEquivalentTo(expected);
        }

        public static IEnumerable<object[]> EnumerablesMergeTestCaseData => new object[][]
        {
            new object[]
            {
                Array.Empty<int[]>(),
                Array.Empty<int>()
            },
            new object[]
            {
                new int[][]
                {
                    new int[] { 1, 2, 3, 4, 5 },
                },
                new int[] { 1, 2, 3, 4, 5 },
            },
            new object[]
            {
                new int[][]
                {
                    new int[] { 1, 2, 3, 4, 5 },
                    new int[] { 6, 7, 8 },
                    new int[] { 9 },
                    Array.Empty<int>(),
                    new int[] { 10, 11, 12, 13, 14, 15 },
                },
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            }
        };
        #endregion

        #region LastIndex
        [Test]
        [TestCaseSource(nameof(LastIndexTestCaseData))]
        public void LastIndex_Should_Return_Expected(IEnumerable<int> sequence, int expected) => Check.That(sequence.LastIndex()).IsEqualTo(expected);

        [Test]
        public void LastIndex_Does_Not_Interate_On_Collections()
        {
            IEnumerable<string> collectionAsEnumerable = new CollectionThrowingWhenIterated<string>(18);

            Check.ThatCode(() => collectionAsEnumerable.LastIndex())
                .DoesNotThrow<EnumeratedException>();
        }

        public static IEnumerable<object[]> LastIndexTestCaseData => new object[][]
        {
            new object[]
            {
                new int[] { },
                -1
            },
            new object[]
            {
                new int[] { 1 },
                0
            },
            new object[]
            {
                new List<int> { 2, 4, 8, 16, 32, 64 },
                5
            },
            new object[]
            {
                Enumerable.Range(1, 8),
                7
            }
        };
        #endregion

        [Test]
        public void Except_With_Params_Elements_Behaves_As_Expected()
        {
            IEnumerable<int> sequence = Enumerables.Create(1, 2, 3, 4);

            Check.That(sequence.Except(2)).IsEquivalentTo(1, 3, 4);
        }
    }
}