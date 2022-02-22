using Hexarc.Borsh.Serialization.Converters;

namespace Hexarc.Borsh.Serialization.Metadata;

public static class BorshMetadataServices
{
    public static BorshConverter<Boolean> BooleanConverter => _borshConverter ??= new BooleanConverter();
    private static BorshConverter<Boolean>? _borshConverter;

    public static BorshConverter<Byte> ByteConverter => _byteConverter ??= new ByteConverter();
    private static BorshConverter<Byte>? _byteConverter;

    public static BorshConverter<SByte> SByteConverter => _sbyteConverter ??= new SByteConverter();
    private static BorshConverter<SByte>? _sbyteConverter;

    public static BorshConverter<Int16> Int16Converter => _int16Converter ??= new Int16Converter();
    private static BorshConverter<Int16>? _int16Converter;

    public static BorshConverter<Int32> Int32Converter => _int32Converter ??= new Int32Converter();
    private static BorshConverter<Int32>? _int32Converter;

    public static BorshConverter<Int64> Int64Converter => _int64Converter ??= new Int64Converter();
    private static BorshConverter<Int64>? _int64Converter;

    public static BorshConverter<UInt16> UInt16Converter => _uint16Converter ??= new UInt16Converter();
    private static BorshConverter<UInt16>? _uint16Converter;

    public static BorshConverter<UInt32> UInt32Converter => _uint32Converter ??= new UInt32Converter();
    private static BorshConverter<UInt32>? _uint32Converter;

    public static BorshConverter<UInt64> UInt64Converter => _uint64Converter ??= new UInt64Converter();
    private static BorshConverter<UInt64>? _uint64Converter;

    public static BorshConverter<Half> HalfConverter => _halfConverter ??= new HalfConverter();
    private static BorshConverter<Half>? _halfConverter;

    public static BorshConverter<Single> SingleConverter => _singleConverter ??= new SingleConverter();
    private static BorshConverter<Single>? _singleConverter;

    public static BorshConverter<Double> DoubleConverter => _doubleConverter ??= new DoubleConverter();
    private static BorshConverter<Double>? _doubleConverter;

    public static BorshConverter<String> StringConverter => _stringConverter ??= new StringConverter();
    private static BorshConverter<String>? _stringConverter;

    public static BorshConverter<DateTime> DateTimeConverter => _dateTimeConverter ??= new DateTimeConverter();
    private static BorshConverter<DateTime>? _dateTimeConverter;
}
