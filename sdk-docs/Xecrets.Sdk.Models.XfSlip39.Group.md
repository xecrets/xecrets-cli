#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models').[XfSlip39](Xecrets.Sdk.Models.XfSlip39.md 'Xecrets.Sdk.Models.XfSlip39')

## XfSlip39.Group Class

A cargo class carrying the information about a group of shares

```csharp
public sealed class XfSlip39.Group :
System.IEquatable<Xecrets.Sdk.Models.XfSlip39.Group>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; Group

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[Group](Xecrets.Sdk.Models.XfSlip39.Group.md 'Xecrets.Sdk.Models.XfSlip39.Group')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Properties

<a name='Xecrets.Sdk.Models.XfSlip39.Group.Description'></a>

## XfSlip39.Group.Description Property

A user friendly description in english of the group

```csharp
public string Description { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfSlip39.Group.Shares'></a>

## XfSlip39.Group.Shares Property

The shares in this group

```csharp
public System.Collections.Generic.List<Xecrets.Sdk.Models.XfSlip39.Group.Share> Shares { get; set; }
```

#### Property Value
[System.Collections.Generic.List&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1 'System.Collections.Generic.List`1')[Share](Xecrets.Sdk.Models.XfSlip39.Group.Share.md 'Xecrets.Sdk.Models.XfSlip39.Group.Share')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1 'System.Collections.Generic.List`1')