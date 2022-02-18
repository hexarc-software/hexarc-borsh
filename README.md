# Hexarc Borsh
[![License](http://img.shields.io/:license-mit-blue.svg)](http://badges.mit-license.org) 
[![NuGet](https://img.shields.io/nuget/v/Hexarc.Borsh.svg)](https://www.nuget.org/packages/Hexarc.Borsh)
[![Downloads](http://img.shields.io/nuget/dt/Hexarc.Borsh.svg)](https://www.nuget.org/packages/Hexarc.Borsh)

Hexarc.Borsh is .NET implementation of the [Binary Object Representation Serializer for Hashing](https://borsh.io/) format.

Current status: 🚧 Under development 🚧

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
public class Point
{
    public Int32 X { get; init; }
    public Int32 Y { get; init; }
    public Int32 Z { get; init; }
}

var point = new Point() { X = 5, Y = 10, Z = 20 };

var raw = BorshSerializer.Serialize(point);
var restored = BorshSerializer.Deserialize<Point>(raw);
```

## Features
Limited count of the .NET types are currently supported:
* `Byte`, `SByte`, `Boolean`, `Int16`, `UInt16`, `Int32`, `UInt32`, `Int64`, `UInt64`
* `Single`, `Double`
* `Nullable<T>`
* `Enum`
* `String`
* Arrays as `T[]`
* `List<T>`
* `HashSet<T>`
* `Dictionary<TKey, TValue>`
* `Hexarc.Borsh.Option<T>`
* POCO like user defined classes

All other types are not supported at the moment but already planned.

Another important notice that Borsh is mostly designed to support the Rust
type system. So `null` reference values are not supported in .NET implementation.
Please use the special `Hexarc.Borsh.BorshOptional` attribute or 
`Hexarc.Borsh.Option<T>` type.

Property annotation example:
```cs
public class PersonDetails
{
    [BorshOptional]
    public String? FirstName { get; init; }
    
    [BorshOptional]
    public String? LastName { get; init; }
}
```
In case you need to serialize a top level nullable object:
```cs
String? input = Console.ReadLine();

var raw = BorshSerializer.Serialize(Option<String>.Create(input));
var restored = BorshSerializer.Deserialize<Option<String>>(raw);
```

## Acknowledgments
Built with JetBrains tools for [Open Source](https://jb.gg/OpenSourceSupport) projects.

![JetBrains Logo (Main) logo](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg)

## License
MIT © [Max Koverdyaev](https://github.com/shadeglare)
