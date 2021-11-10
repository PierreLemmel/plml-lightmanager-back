namespace LightManager.Core.Tests.Helpers;

public class UtilsShould
{
    [Test]
    public void Swap_Swaps_Variable_Values()
    {
        const float value1 = 14.0f;
        const float value2 = 18_000.0f;

        float var1 = value1;
        float var2 = value2;

        Utils.Swap(ref var1, ref var2);

        Check.That(var1).IsEqualTo(value2);
        Check.That(var2).IsEqualTo(value1);
    }
}