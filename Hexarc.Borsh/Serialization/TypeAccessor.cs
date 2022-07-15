using System.Collections.Immutable;
using System.Reflection;

namespace Hexarc.Borsh.Serialization;

internal sealed class TypeAccessor
{
    private readonly IImmutableDictionary<String, PropertyInfo> _propertyInfos;
    
    public TypeAccessor(Type type)
    {
        this._propertyInfos = type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToImmutableDictionary(x => x.Name, x => x);
    }

    public Object? this[Object entity, String property]
    {
        get => this._propertyInfos[property].GetValue(entity);
        set => this._propertyInfos[property].SetValue(entity, value);
    }
}
