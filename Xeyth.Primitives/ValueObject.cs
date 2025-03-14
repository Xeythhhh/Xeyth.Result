﻿#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Xeyth.Primitives;

/// <summary>Represents the base class all value objects derive from.</summary>
public abstract record ValueObject<T> : IEquatable<T>
{
    public T Value { get; protected set; }

    public virtual bool Equals(ValueObject<T>? other) =>
        other is not null &&
        GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    public bool Equals(T? other) =>
        other is not null &&
        GetAtomicValues().SequenceEqual([other]);

    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (object? obj in GetAtomicValues())
        {
            hashCode.Add(obj);
        }

        return hashCode.ToHashCode();
    }

    /// <summary>Gets the atomic values of the value object.</summary>
    /// <returns>The collection of objects representing the value object values.</returns>
    protected abstract IEnumerable<object?> GetAtomicValues();
}