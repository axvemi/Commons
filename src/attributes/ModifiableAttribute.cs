using System.Collections.Generic;
using System.Linq;

namespace Axvemi.Commons;

public class AttributeModifier<T>
{
    public T Value;
    public object Source;

    public AttributeModifier(T value, object source = null)
    {
        Value = value;
        Source = source;
    }
}

public abstract class ModifiableAttribute<T>
{
    //public event EventHandler<EventArgs> ValueChanged;

    /// <summary>
    /// Value without the modifiers applied
    /// </summary>
    public T RawValue;
    /// <summary>
    /// Value with the modifiers applied
    /// </summary>
    public T Value => GetModifiedValue();

    public List<AttributeModifier<T>> Modifiers { get; } = new();

    public ModifiableAttribute(T value)
    {
        RawValue = value;
    }

    public void AddModifier(AttributeModifier<T> modifier)
    {
        Modifiers.Add(modifier);
    }

    public void RemoveModifier(object source)
    {
        var modifier = Modifiers.FirstOrDefault(m => m.Source == source);
        Modifiers.Remove(modifier);
    }

    protected T GetModifiedValue()
    {
        T value = MakeCopy(RawValue);
        Modifiers.ForEach(m => value = Add(value, m.Value));
        return value;
    }

    protected abstract T Add(T value1, T value2);

    protected virtual T MakeCopy(T value) => value;
}