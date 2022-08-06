using System.Reflection;

namespace Hexarc.Borsh.Serialization.Converters;

public sealed class UnionConverterFactory : BorshConverterFactory
{
    private static readonly Type s_unionConverter = typeof(UnionConverter<>);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type.IsInterface
            ? type.GetCustomAttributes<BorshUnionAttribute>(false).Any()
            : type.GetCustomAttribute<BorshObjectAttribute>() is not null &&
              type.GetCustomAttributes<BorshUnionAttribute>(false).Any();

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        return Activator.CreateInstance(s_unionConverter.MakeGenericType(type), options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
