
namespace Axvemi.Commons;

public class ModifiableAttributeMultiplier : ModifiableAttribute<float>
{
    public ModifiableAttributeMultiplier(float baseValue) : base(baseValue)
    {
    }

    protected override float Add(float value1, float value2) => value1 * value2;
}
