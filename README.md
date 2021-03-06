# Hexarc Borsh
[![License](http://img.shields.io/:license-mit-blue.svg)](http://badges.mit-license.org) 
[![NuGet](https://img.shields.io/nuget/v/Hexarc.Borsh.svg)](https://www.nuget.org/packages/Hexarc.Borsh)
[![Downloads](http://img.shields.io/nuget/dt/Hexarc.Borsh.svg)](https://www.nuget.org/packages/Hexarc.Borsh)

Hexarc.Borsh is .NET implementation of the [Binary Object Representation Serializer for Hashing](https://borsh.io/) format.

## Getting started

Install the package with the `NuGet` CLI:
```sh
dotnet add package Hexarc.Borsh
```

Reference the `Hexarc.Borsh` namespace in your code:
```cs
using Hexarc.Borsh;
```

Serialize and deserialize .NET objects via the `BorshSerializer` class:
```cs
[BorshObject]
public class Point
{
    [BorshPropertyOrder(0)]
    public Int32 X { get; init; }
    
    [BorshPropertyOrder(1)]
    public Int32 Y { get; init; }
    
    [BorshPropertyOrder(2)]
    public Int32 Z { get; init; }
}

var point = new Point() { X = 5, Y = 10, Z = 20 };

var raw = BorshSerializer.Serialize(point);
var restored = BorshSerializer.Deserialize<Point>(raw);
```

## Features
These types can be serialized by default:
* `Byte`, `SByte`, `Boolean`, `Int16`, `UInt16`, `Int32`, `UInt32`, `Int64`, `UInt64`
* `Single`, `Double`, `Half`
* `Nullable<T>`
* `Enum`
* `String`
* `DateTime`
* `ValueTuple`
* Arrays as `T[]`
* `List<T>`
* `HashSet<T>`
* `Dictionary<TKey, TValue>`
* `Hexarc.Borsh.Option<T>`
* POCO like user defined classes

### Object serialization
Serializable types must be annotated with the `BorshObject` attribute. 
The `BorshIgnore` attribute can be used to exclude properties from serialization.
```cs
[BorshObject]
public class Point
{
    [BorshPropertyOrder(0)]
    public Int32 X { get; init; }
    
    [BorshPropertyOrder(1)]
    public Int32 Y { get; init; }
    
    [BorshPropertyOrder(2)]
    public Int32 Z { get; init; }
    
    // This property will be exluded from serialization.
    [BorshIgnore]
    public String? Memo { get; init; }
}

var raw = BorshSerializer.Serialize(new Point { X = 1, Y = 2, Z = 3 });
```

Records serialization:
```cs
[BorshObject]
public sealed record Rect(
    [property: BorshPropertyOrder(0)] Int32 Width,
    [property: BorshPropertyOrder(1)] Int32 Height
);

var rect = new Rect(10, 20);
var raw = BorshSerializer.Serialize(rect);
```

### Nullable reference type serialization
Another important notice that Borsh is mostly designed to support the Rust
type system. So `null` reference values are not supported in .NET implementation.
Please use the special `Hexarc.Borsh.Serialization.BorshOptionalAttribute` attribute or 
`Hexarc.Borsh.Option<T>` type.

Property annotation example:
```cs
[BorshObject]
public class PersonDetails
{
    [BorshPropertyOrder(0)]
    [BorshOptional]
    public String? FirstName { get; init; }

    [BorshPropertyOrder(1)]
    [BorshOptional]
    public String? LastName { get; init; }
}
```
In case you need to serialize a top level nullable reference type object:
```cs
String? input = Console.ReadLine();

var raw = BorshSerializer.Serialize(Option<String>.Create(input));
var restored = BorshSerializer.Deserialize<Option<String>>(raw);
```

### Union Serialization
The `BorshUnion` attribute allows to serialize union types:
```cs
[BorshUnion(0, typeof(Circle))]
[BorshUnion(1, typeof(Square))]
[BorshObject]
public abstract class Figure {}

[BorshObject]
public sealed class Circle : Figure
{
    [BorshPropertyOrder(0)]
    public Int32 Radius { get; init; }
}

[BorshObject]
public sealed class Square : Figure
{
    [BorshPropertyOrder(0)]
    public Int32 SideSize { get; init; }
}

Figure square = new Square { SideSize = 1 };
var raw = BorshSerializer.Serialize(square);
```

## Acknowledgments
Built with JetBrains tools for [Open Source](https://jb.gg/OpenSourceSupport) projects.

![JetBrains Logo (Main) logo](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg)

## License
MIT ?? [Max Koverdyaev](https://github.com/shadeglare)
