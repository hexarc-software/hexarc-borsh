namespace Hexarc.Borsh.Serialization.Converters;

public sealed class TupleConverter<T1> : BorshConverter<ValueTuple<T1>>
{
    private readonly BorshConverter<T1> _value1Converter;

    public TupleConverter(BorshSerializerOptions options) =>
        this._value1Converter = options.GetConverter<T1>();

    public override void Write(BorshWriter writer, ValueTuple<T1> value, BorshSerializerOptions options) =>
        this._value1Converter.Write(writer, value.Item1, options);

    public override ValueTuple<T1> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        new(this._value1Converter.Read(ref reader, options));
}

public sealed class TupleConverter<T1, T2> : BorshConverter<ValueTuple<T1, T2>>
{
    private readonly BorshConverter<T1> _value1Converter;
    private readonly BorshConverter<T2> _value2Converter;

    public TupleConverter(BorshSerializerOptions options)
    {
        this._value1Converter = options.GetConverter<T1>();
        this._value2Converter = options.GetConverter<T2>();
    }

    public override void Write(BorshWriter writer, ValueTuple<T1, T2> value, BorshSerializerOptions options)
    {
        this._value1Converter.Write(writer, value.Item1, options);
        this._value2Converter.Write(writer, value.Item2, options);
    }

    public override ValueTuple<T1, T2> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        new(this._value1Converter.Read(ref reader, options),
            this._value2Converter.Read(ref reader, options));
}

public sealed class TupleConverter<T1, T2, T3> : BorshConverter<ValueTuple<T1, T2, T3>>
{
    private readonly BorshConverter<T1> _value1Converter;
    private readonly BorshConverter<T2> _value2Converter;
    private readonly BorshConverter<T3> _value3Converter;

    public TupleConverter(BorshSerializerOptions options)
    {
        this._value1Converter = options.GetConverter<T1>();
        this._value2Converter = options.GetConverter<T2>();
        this._value3Converter = options.GetConverter<T3>();
    }

    public override void Write(BorshWriter writer, ValueTuple<T1, T2, T3> value, BorshSerializerOptions options)
    {
        this._value1Converter.Write(writer, value.Item1, options);
        this._value2Converter.Write(writer, value.Item2, options);
        this._value3Converter.Write(writer, value.Item3, options);
    }

    public override ValueTuple<T1, T2, T3> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        new(this._value1Converter.Read(ref reader, options),
            this._value2Converter.Read(ref reader, options),
            this._value3Converter.Read(ref reader, options));
}

public sealed class TupleConverter<T1, T2, T3, T4> : BorshConverter<ValueTuple<T1, T2, T3, T4>>
{
    private readonly BorshConverter<T1> _value1Converter;
    private readonly BorshConverter<T2> _value2Converter;
    private readonly BorshConverter<T3> _value3Converter;
    private readonly BorshConverter<T4> _value4Converter;

    public TupleConverter(BorshSerializerOptions options)
    {
        this._value1Converter = options.GetConverter<T1>();
        this._value2Converter = options.GetConverter<T2>();
        this._value3Converter = options.GetConverter<T3>();
        this._value4Converter = options.GetConverter<T4>();
    }

    public override void Write(BorshWriter writer, ValueTuple<T1, T2, T3, T4> value, BorshSerializerOptions options)
    {
        this._value1Converter.Write(writer, value.Item1, options);
        this._value2Converter.Write(writer, value.Item2, options);
        this._value3Converter.Write(writer, value.Item3, options);
        this._value4Converter.Write(writer, value.Item4, options);
    }

    public override ValueTuple<T1, T2, T3, T4> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        new(this._value1Converter.Read(ref reader, options),
            this._value2Converter.Read(ref reader, options),
            this._value3Converter.Read(ref reader, options),
            this._value4Converter.Read(ref reader, options));
}

public sealed class TupleConverter<T1, T2, T3, T4, T5> : BorshConverter<ValueTuple<T1, T2, T3, T4, T5>>
{
    private readonly BorshConverter<T1> _value1Converter;
    private readonly BorshConverter<T2> _value2Converter;
    private readonly BorshConverter<T3> _value3Converter;
    private readonly BorshConverter<T4> _value4Converter;
    private readonly BorshConverter<T5> _value5Converter;

    public TupleConverter(BorshSerializerOptions options)
    {
        this._value1Converter = options.GetConverter<T1>();
        this._value2Converter = options.GetConverter<T2>();
        this._value3Converter = options.GetConverter<T3>();
        this._value4Converter = options.GetConverter<T4>();
        this._value5Converter = options.GetConverter<T5>();
    }

    public override void Write(BorshWriter writer, ValueTuple<T1, T2, T3, T4, T5> value, BorshSerializerOptions options)
    {
        this._value1Converter.Write(writer, value.Item1, options);
        this._value2Converter.Write(writer, value.Item2, options);
        this._value3Converter.Write(writer, value.Item3, options);
        this._value4Converter.Write(writer, value.Item4, options);
        this._value5Converter.Write(writer, value.Item5, options);
    }

    public override ValueTuple<T1, T2, T3, T4, T5> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        new(this._value1Converter.Read(ref reader, options),
            this._value2Converter.Read(ref reader, options),
            this._value3Converter.Read(ref reader, options),
            this._value4Converter.Read(ref reader, options),
            this._value5Converter.Read(ref reader, options));
}

public sealed class TupleConverter<T1, T2, T3, T4, T5, T6> : BorshConverter<ValueTuple<T1, T2, T3, T4, T5, T6>>
{
    private readonly BorshConverter<T1> _value1Converter;
    private readonly BorshConverter<T2> _value2Converter;
    private readonly BorshConverter<T3> _value3Converter;
    private readonly BorshConverter<T4> _value4Converter;
    private readonly BorshConverter<T5> _value5Converter;
    private readonly BorshConverter<T6> _value6Converter;

    public TupleConverter(BorshSerializerOptions options)
    {
        this._value1Converter = options.GetConverter<T1>();
        this._value2Converter = options.GetConverter<T2>();
        this._value3Converter = options.GetConverter<T3>();
        this._value4Converter = options.GetConverter<T4>();
        this._value5Converter = options.GetConverter<T5>();
        this._value6Converter = options.GetConverter<T6>();
    }

    public override void Write(BorshWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6> value, BorshSerializerOptions options)
    {
        this._value1Converter.Write(writer, value.Item1, options);
        this._value2Converter.Write(writer, value.Item2, options);
        this._value3Converter.Write(writer, value.Item3, options);
        this._value4Converter.Write(writer, value.Item4, options);
        this._value5Converter.Write(writer, value.Item5, options);
        this._value6Converter.Write(writer, value.Item6, options);
    }

    public override ValueTuple<T1, T2, T3, T4, T5, T6> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        new(this._value1Converter.Read(ref reader, options),
            this._value2Converter.Read(ref reader, options),
            this._value3Converter.Read(ref reader, options),
            this._value4Converter.Read(ref reader, options),
            this._value5Converter.Read(ref reader, options),
            this._value6Converter.Read(ref reader, options));
}

public sealed class TupleConverter<T1, T2, T3, T4, T5, T6, T7> : BorshConverter<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>
{
    private readonly BorshConverter<T1> _value1Converter;
    private readonly BorshConverter<T2> _value2Converter;
    private readonly BorshConverter<T3> _value3Converter;
    private readonly BorshConverter<T4> _value4Converter;
    private readonly BorshConverter<T5> _value5Converter;
    private readonly BorshConverter<T6> _value6Converter;
    private readonly BorshConverter<T7> _value7Converter;

    public TupleConverter(BorshSerializerOptions options)
    {
        this._value1Converter = options.GetConverter<T1>();
        this._value2Converter = options.GetConverter<T2>();
        this._value3Converter = options.GetConverter<T3>();
        this._value4Converter = options.GetConverter<T4>();
        this._value5Converter = options.GetConverter<T5>();
        this._value6Converter = options.GetConverter<T6>();
        this._value7Converter = options.GetConverter<T7>();
    }

    public override void Write(BorshWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7> value, BorshSerializerOptions options)
    {
        this._value1Converter.Write(writer, value.Item1, options);
        this._value2Converter.Write(writer, value.Item2, options);
        this._value3Converter.Write(writer, value.Item3, options);
        this._value4Converter.Write(writer, value.Item4, options);
        this._value5Converter.Write(writer, value.Item5, options);
        this._value6Converter.Write(writer, value.Item6, options);
        this._value7Converter.Write(writer, value.Item7, options);
    }

    public override ValueTuple<T1, T2, T3, T4, T5, T6, T7> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        new(this._value1Converter.Read(ref reader, options),
            this._value2Converter.Read(ref reader, options),
            this._value3Converter.Read(ref reader, options),
            this._value4Converter.Read(ref reader, options),
            this._value5Converter.Read(ref reader, options),
            this._value6Converter.Read(ref reader, options),
            this._value7Converter.Read(ref reader, options));
}

public sealed class TupleConverter<T1, T2, T3, T4, T5, T6, T7, TRest> : BorshConverter<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>
    where TRest: struct
{
    private readonly BorshConverter<T1> _value1Converter;
    private readonly BorshConverter<T2> _value2Converter;
    private readonly BorshConverter<T3> _value3Converter;
    private readonly BorshConverter<T4> _value4Converter;
    private readonly BorshConverter<T5> _value5Converter;
    private readonly BorshConverter<T6> _value6Converter;
    private readonly BorshConverter<T7> _value7Converter;
    private readonly BorshConverter<TRest> _restValueConverter;

    public TupleConverter(BorshSerializerOptions options)
    {
        this._value1Converter = options.GetConverter<T1>();
        this._value2Converter = options.GetConverter<T2>();
        this._value3Converter = options.GetConverter<T3>();
        this._value4Converter = options.GetConverter<T4>();
        this._value5Converter = options.GetConverter<T5>();
        this._value6Converter = options.GetConverter<T6>();
        this._value7Converter = options.GetConverter<T7>();
        this._restValueConverter = options.GetConverter<TRest>();
    }

    public override void Write(BorshWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> value, BorshSerializerOptions options)
    {
        this._value1Converter.Write(writer, value.Item1, options);
        this._value2Converter.Write(writer, value.Item2, options);
        this._value3Converter.Write(writer, value.Item3, options);
        this._value4Converter.Write(writer, value.Item4, options);
        this._value5Converter.Write(writer, value.Item5, options);
        this._value6Converter.Write(writer, value.Item6, options);
        this._value7Converter.Write(writer, value.Item7, options);
        this._restValueConverter.Write(writer, value.Rest, options);
    }

    public override ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        new(this._value1Converter.Read(ref reader, options),
            this._value2Converter.Read(ref reader, options),
            this._value3Converter.Read(ref reader, options),
            this._value4Converter.Read(ref reader, options),
            this._value5Converter.Read(ref reader, options),
            this._value6Converter.Read(ref reader, options),
            this._value7Converter.Read(ref reader, options),
            this._restValueConverter.Read(ref reader, options));
}
