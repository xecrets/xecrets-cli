#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models')

## XfSlip39 Class

A cargo class carrying the information about a secret split into Slip39 shares

```csharp
public sealed class XfSlip39 :
System.IEquatable<Xecrets.Sdk.Models.XfSlip39>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfSlip39

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[XfSlip39](Xecrets.Sdk.Models.XfSlip39.md 'Xecrets.Sdk.Models.XfSlip39')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Properties

<a name='Xecrets.Sdk.Models.XfSlip39.Description'></a>

## XfSlip39.Description Property

A user friendly description in english of the entire SLIP-39 groups and shares

```csharp
public string Description { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfSlip39.Groups'></a>

## XfSlip39.Groups Property

The groups in this SLIP-39 sharing

```csharp
public System.Collections.Generic.List<Xecrets.Sdk.Models.XfSlip39.Group> Groups { get; set; }
```

#### Property Value
[System.Collections.Generic.List&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1 'System.Collections.Generic.List`1')[Group](Xecrets.Sdk.Models.XfSlip39.Group.md 'Xecrets.Sdk.Models.XfSlip39.Group')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1 'System.Collections.Generic.List`1')