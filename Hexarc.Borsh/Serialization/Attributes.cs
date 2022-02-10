namespace Hexarc.Borsh.Serialization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class BorshObjectAttribute : Attribute {}

[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshIgnoreAttribute : Attribute { }
