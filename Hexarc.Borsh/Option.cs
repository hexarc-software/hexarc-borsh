namespace Hexarc.Borsh;

public abstract class Option<T> where T : class
{
    internal Option() {}

    public static Option<T> Create(T? value) =>
        value is null ? new None<T>() : new Some<T>(value);
}

public sealed class Some<T> : Option<T> where T : class
{
    public T Value { get; }

    public Some(T? value) =>
        this.Value = value ?? throw new ArgumentException("Argument cannot be null", nameof(value));
}

public sealed class None<T> : Option<T> where T : class { }
