using System;
using System.Collections.Generic;

namespace Axvemi.Commons.Attributes;

public class AttributeModifier<T>
{
    public T Value { get; set; }
    public object Source { get; private set; }

    public AttributeModifier(T value, object source = null)
    {
        Value = value;
        Source = source;
    }
}

public abstract class ModifiableAttribute<T>
{
    public class ValueChangedEventArgs : EventArgs
    {
        public T PreviousValue { get; }
        public T ActualValue { get; }

        public ValueChangedEventArgs(T actualValue, T previousValue)
        {
            ActualValue = actualValue;
            PreviousValue = previousValue;
        }
    }

    public event Action<ValueChangedEventArgs> ValueChanged;

    public T ModifiedValue => GetModifiedValue();
    public T Value { get; private set; }

    private readonly List<AttributeModifier<T>> _modifiers = new();

    public ModifiableAttribute()
    {
    }

    public ModifiableAttribute(T baseValue)
    {
        Value = baseValue;
    }

    public virtual void SetValue(T value)
    {
        if (!EqualityComparer<T>.Default.Equals(Value, value))
        {
            T prevModifiedValue = ModifiedValue;
            Value = value;
            if (!EqualityComparer<T>.Default.Equals(ModifiedValue, prevModifiedValue))
            {
                InvokeValueChangedEvent(prevModifiedValue);
            }
        }
    }

    public void AddValue(T value)
    {
        SetValue(Add(Value, value));
    }

    public void AddModifier(AttributeModifier<T> modifier)
    {
        T prevValue = ModifiedValue;
        _modifiers.Add(modifier);
        InvokeValueChangedEvent(prevValue);
    }

    public void RemoveModifier(object source)
    {
        T prevValue = ModifiedValue;
        AttributeModifier<T> attributeModifier = _modifiers.Find(x => x.Source == source);
        _modifiers.Remove(attributeModifier);
        InvokeValueChangedEvent(prevValue);
    }

    protected virtual T GetModifiedValue()
    {
        T value = MakeCopy(Value);
        _modifiers.ForEach(mod => value = Add(value, mod.Value));
        return value;
    }

    protected abstract T Add(T value1, T value2);

    protected virtual T MakeCopy(T value) => value;

    protected void InvokeValueChangedEvent(T prevValue)
    {
        ValueChanged?.Invoke(new ValueChangedEventArgs(ModifiedValue, prevValue));
    }

}
