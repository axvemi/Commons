using System;

namespace Axvemi.Commons.Attributes;
public class ModifiableAttributeNumber<T>: ModifiableAttribute<T> where T: struct, IConvertible
{
    public ModifiableAttributeNumber(T baseValue) : base(baseValue)
    {
    }

    protected override T Add(T value1, T value2)
    {
        double result = Convert.ToDouble(value1) + Convert.ToDouble(value2);
        return (T)Convert.ChangeType(result, typeof(T));
    }
}

