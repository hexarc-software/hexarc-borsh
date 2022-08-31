namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Supports creating converters for different value tuple types. 
/// </summary>
public sealed class TupleConverterFactory : BorshConverterFactory
{
    private static readonly String s_tupleTypeName = typeof(ValueTuple).FullName!;

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type.FullName is not null && type.FullName.StartsWith(s_tupleTypeName);

    /// <inheritdoc />
    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var arguments = type.GetGenericArguments();
        var converterType = arguments.Length switch
        {
            1 => typeof(TupleConverter<>),
            2 => typeof(TupleConverter<,>),
            3 => typeof(TupleConverter<,,>),
            4 => typeof(TupleConverter<,,,>),
            5 => typeof(TupleConverter<,,,,>),
            6 => typeof(TupleConverter<,,,,,>),
            7 => typeof(TupleConverter<,,,,,,>),
            8 => typeof(TupleConverter<,,,,,,,>),
            _ => throw new NotSupportedException()
        };
        return Activator.CreateInstance(converterType.MakeGenericType(arguments), options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
