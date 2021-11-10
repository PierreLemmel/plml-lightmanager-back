namespace LightManager.Core.Tests.Helpers;

public class ArraysShould
{
    #region Merge
    [Test]
    public void Merge_Returns_Empty_Array_When_Input_Is_Empty()
    {
        IEnumerable<int[]> empty = Enumerable.Empty<int[]>();

        int[] result = Arrays.Merge(empty);
        Check.That(result).IsEmpty();
    }

    [Test]
    [TestCaseSource(nameof(MergeTestCaseData))]
    public void Merge_Returns_Expected_Result(IEnumerable<int[]> arrays, int[] expected)
    {
        int[] result = Arrays.Merge(arrays);
        Check.That(result).ContainsExactly(expected);
    }

    public static IEnumerable<object[]> MergeTestCaseData => new object[][]
    {
            new object[]
            {
                new List<int[]>() { new int[] { 1, 2, 3, 4, 5 } },
                new int[] { 1, 2, 3, 4, 5 }
            },
            new object[]
            {
                new List<int[]>()
                {
                    new int[] { 1, 2, 3, 4, 5 },
                    new int[] { 6, 7 }
                },
                new int[] { 1, 2, 3, 4, 5, 6, 7 }
            },
            new object[]
            {
                new List<int[]>()
                {
                    new int[] { 1, 2, 3, 4, 5 },
                    new int[] { 6, 7 },
                    new int[] { 1, 2, 3 },
                },
                new int[] { 1, 2, 3, 4, 5, 6, 7, 1, 2, 3 }
            },
            new object[]
            {
                new List<int[]>()
                {
                    new int[] { },
                    new int[] { 1, 2, 3, 4, 5 },
                    new int[] { 6, 7 },
                    new int[] { 1, 2, 3 },
                    new int[] { }
                },
                new int[] { 1, 2, 3, 4, 5, 6, 7, 1, 2, 3 }
            },
            new object[]
            {
                new List<int[]>()
                {
                    new int[] { },
                    new int[] { 1, 2, 3, 4, 5 },
                    new int[] { 6, 7 },
                    new int[] { 1, 2, 3 },
                    new int[] { },
                    new int[] { 1, 2, 3 }
                },
                new int[] { 1, 2, 3, 4, 5, 6, 7, 1, 2, 3, 1, 2, 3 }
            },
    };
    #endregion

    #region ShiftLeft
    [Test]
    [TestCaseSource(nameof(ShiftLeftTestCaseData))]
    public void ShiftLeft_Returns_Expected_Result(int[] input, int[] expected)
    {
        input.ShiftLeft();

        Check.That(input)
            .IsEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ShiftLeftTestCaseData => new object[][]
    {
            new object[]
            {
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
                new int[] { 2, 3, 4, 5, 6, 7, 8, 1 }
            },
            new object[]
            {
                new int[] { 18 },
                new int[] { 18 },
            },
            new object[]
            {
                new int[] { },
                new int[] { },
            }
    };
    #endregion

    #region ShiftRight
    [Test]
    [TestCaseSource(nameof(ShiftRightTestCaseData))]
    public void ShiftRight_Returns_Expected_Result(int[] input, int[] expected)
    {
        input.ShiftRight();

        Check.That(input)
            .IsEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ShiftRightTestCaseData => new object[][]
    {
            new object[]
            {
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
                new int[] { 8, 1, 2, 3, 4, 5, 6, 7 }
            },
            new object[]
            {
                new int[] { 18 },
                new int[] { 18 },
            },
            new object[]
            {
                new int[] { },
                new int[] { },
            }
    };
    #endregion

    #region Repeated
    [Test]
    [TestCaseSource(nameof(RepeatedTestCaseData))]
    public void Repeated_Returns_Expected_Result(int[] input, int count, int[] expected)
    {
        int[] result = input.Repeated(count);

        Check.That(result)
            .IsEquivalentTo(expected);
    }

    public static IEnumerable<object[]> RepeatedTestCaseData => new object[][]
    {
            new object[]
            {
                new int[] { },
                5,
                new int[] { }
            },
            new object[]
            {
                new int[] { 1, 2, 3, 4 },
                1,
                new int[] { 1, 2, 3, 4 },
            },
            new object[]
            {
                new int[] { 1, 2, 3, 4 },
                0,
                new int[] { },
            },
            new object[]
            {
                new int[] { 1, 2, 3, 4 },
                5,
                new int[] { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4 },
            },
    };
    #endregion

    #region Reverse
    [Test]
    [TestCaseSource(nameof(ReverseTestCaseData))]
    public void Reverse_Should_Return_Expected(int[] input, int[] expected)
    {
        input.Reverse();

        Check.That(input).IsEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ReverseTestCaseData => new object[][]
    {
            new object[]
            {
                new int[] {},
                new int[] {}
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
}