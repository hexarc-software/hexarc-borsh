using System.Reflection;

namespace Hexarc.Borsh.Serialization.Converters;

public sealed class UnionConverterFactory : BorshConverterFactory
{
    private readonly Type _unionConverter = typeof(UnionConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsInterface
            ? type.GetCustomAttributes<BorshUnionAttribute>(false).Any()
            : type.GetCustomAttribute<BorshObjectAttribute>() is not null &&
              type.GetCustomAttributes<BorshUnionAttribute>(false).Any();

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        return Activator.CreateInstance(this._unionConverter.MakeGenericType(type), options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
