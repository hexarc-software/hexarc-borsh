namespace Hexarc.Borsh;

public abstract class Option<T> where T : class
{
    internal Option() {}

    public static Option<T> Create(T? value) =>
        value is null ? new None<T>() : new Some<T>(value);

    public static Option<T> Some(T value) => Create(value);

    public static Option<T> None() => Create(default);
}

public sealed class Some<T> : Option<T> where T : class
{
    public T Value { get; }

    internal Some(T value) =>
        this.Value = value;
}

public sealed class None<T> : Option<T> where T : class
{
    internal None() {}
}
