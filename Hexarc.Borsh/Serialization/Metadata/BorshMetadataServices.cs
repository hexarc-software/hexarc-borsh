using Hexarc.Borsh.Serialization.Converters;

namespace Hexarc.Borsh.Serialization.Metadata;

/// <summary>
/// The BORSH metadata services for .NET primitives.
/// </summary>
public static class BorshMetadataServices
{
    /// <summary>
    /// Gets the BORSH converter for the <see cref="Boolean"/> type.
    /// </summary>
    public static BorshConverter<Boolean> BooleanConverter => _borshConverter ??= new BooleanConverter();
    private static BorshConverter<Boolean>? _borshConverter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="Byte"/> type.
    /// </summary>
    public static BorshConverter<Byte> ByteConverter => _byteConverter ??= new ByteConverter();
    private static BorshConverter<Byte>? _byteConverter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="SByte"/> type.
    /// </summary>
    [CLSCompliant(false)]
    public static BorshConverter<SByte> SByteConverter => _sbyteConverter ??= new SByteConverter();
    private static BorshConverter<SByte>? _sbyteConverter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="Int16"/> type.
    /// </summary>
    public static BorshConverter<Int16> Int16Converter => _int16Converter ??= new Int16Converter();
    private static BorshConverter<Int16>? _int16Converter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="Int32"/> type.
    /// </summary>
    public static BorshConverter<Int32> Int32Converter => _int32Converter ??= new Int32Converter();
    private static BorshConverter<Int32>? _int32Converter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="Int64"/> type.
    /// </summary>
    public static BorshConverter<Int64> Int64Converter => _int64Converter ??= new Int64Converter();
    private static BorshConverter<Int64>? _int64Converter;
    
    /// <summary>
    /// Gets the BORSH converter for the <see cref="Int128"/> type.
    /// </summary>
    public static BorshConverter<Int128> Int128Converter => _int128Converter ??= new Int128Converter();
    private static BorshConverter<Int128>? _int128Converter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="UInt16"/> type.
    /// </summary>
    [CLSCompliant(false)]
    public static BorshConverter<UInt16> UInt16Converter => _uint16Converter ??= new UInt16Converter();
    private static BorshConverter<UInt16>? _uint16Converter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="UInt32"/> type.
    /// </summary>
    [CLSCompliant(false)]
    public static BorshConverter<UInt32> UInt32Converter => _uint32Converter ??= new UInt32Converter();
    private static BorshConverter<UInt32>? _uint32Converter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="UInt64"/> type.
    /// </summary>
    [CLSCompliant(false)]
    public static BorshConverter<UInt64> UInt64Converter => _uint64Converter ??= new UInt64Converter();
    private static BorshConverter<UInt64>? _uint64Converter;
    
    /// <summary>
    /// Gets the BORSH converter for the <see cref="UInt128"/> type.
    /// </summary>
    [CLSCompliant(false)]
    public static BorshConverter<UInt128> UInt128Converter => _uint128Converter ??= new UInt128Converter();
    private static BorshConverter<UInt128>? _uint128Converter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="Half"/> type.
    /// </summary>
    public static BorshConverter<Half> HalfConverter => _halfConverter ??= new HalfConverter();
    private static BorshConverter<Half>? _halfConverter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="Single"/> type.
    /// </summary>
    public static BorshConverter<Single> SingleConverter => _singleConverter ??= new SingleConverter();
    private static BorshConverter<Single>? _singleConverter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="Double"/> type.
    /// </summary>
    public static BorshConverter<Double> DoubleConverter => _doubleConverter ??= new DoubleConverter();
    private static BorshConverter<Double>? _doubleConverter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="String"/> type.
    /// </summary>
    public static BorshConverter<String> StringConverter => _stringConverter ??= new StringConverter();
    private static BorshConverter<String>? _stringConverter;

    /// <summary>
    /// Gets the BORSH converter for the <see cref="DateTime"/> type.
    /// </summary>
    public static BorshConverter<DateTime> DateTimeConverter => _dateTimeConverter ??= new DateTimeConverter();
    private static BorshConverter<DateTime>? _dateTimeConverter;
}
