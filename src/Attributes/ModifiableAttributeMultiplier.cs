
namespace Axvemi.Commons.Attributes;

public class ModifiableAttributeMultiplier : ModifiableAttributeNumber<float>
{
    public ModifiableAttributeMultiplier(float baseValue) : base(baseValue)
    {
    }

    protected override float Add(float value1, float value2)
    {
        return value1 * value2;
    }
}
