using System.Text.Json;

namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverterFactory : BorshConverter
{
    public abstract BorshConverter? CreateConverter(Type type, JsonSerializerOptions options);
}
