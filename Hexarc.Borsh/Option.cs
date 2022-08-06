namespace Hexarc.Borsh;

/// <summary>
/// Represents the optional type that can be used on special
/// occurrences to wrap .NET nullable reference types.
/// </summary>
/// <typeparam name="T">The type to wrap.</typeparam>
public abstract class Option<T> where T : class
{
    internal Option() {}

    /// <summary>
    /// Create an instance of the <see cref="Option{T}"/> type
    /// from a given nullable reference type value.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <returns>
    /// Return the <see cref="Option{T}"/> instance that holds
    /// the given either as <see cref="Some{T}" /> or <see cref="None{T}"/>
    /// </returns>
    public static Option<T> Create(T? value) =>
        value is null ? new None<T>() : new Some<T>(value);

    /// <summary>
    /// Creates a <see cref="Some{T}"/> instance from a given value.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <returns>
    /// The <see cref="Some{T}"/> instance that holds the given value.
    /// </returns>
    public static Option<T> Some(T value) => Create(value);

    /// <summary>
    /// Creates a <see cref="None{T}"/> instance.
    /// </summary>
    /// <returns>
    /// The <see cref="None{T}"/> instance.
    /// </returns>
    public static Option<T> None() => Create(default);
}

/// <summary>
/// The <see cref="Option{T}"/> case that represents a non-empty value. 
/// </summary>
/// <typeparam name="T">The type to wrap.</typeparam>
public sealed class Some<T> : Option<T> where T : class
{
    /// <summary>
    /// Gets the wrapped value.
    /// </summary>
    public T Value { get; }

    internal Some(T value) =>
        this.Value = value;
}

/// <summary>
/// The <see cref="Option{T}"/> case that represents an empty value.
/// </summary>
/// <typeparam name="T">The type to wrap.</typeparam>
public sealed class None<T> : Option<T> where T : class
{
    internal None() {}
}
