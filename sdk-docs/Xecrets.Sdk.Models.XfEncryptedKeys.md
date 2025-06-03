#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models')

## XfEncryptedKeys Class

Extra, possibly imported, encrypted private keys for a user.

```csharp
public record XfEncryptedKeys : System.IEquatable<Xecrets.Sdk.Models.XfEncryptedKeys>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfEncryptedKeys

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')

### Remarks
This type is used for interoperability with AxCrypt, so the JSON property names and structure cannot be changed
without breaking compatibility with AxCrypt. It's intended to be used for importing of private keys, typically
from AxCrypt, but could also be from another instance or installation of Xecrets Ez.
### Properties

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.Empty'></a>

## XfEncryptedKeys.Empty Property

An empty instance.

```csharp
public static Xecrets.Sdk.Models.XfEncryptedKeys Empty { get; }
```

#### Property Value
[XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys')

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.UserKeys'></a>

## XfEncryptedKeys.UserKeys Property

A list of encrypted private keys for a physical user, possibly with multiple email addresses, possibly with
multiple keys for each email address.

```csharp
public System.Collections.Generic.IList<Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys> UserKeys { get; init; }
```

#### Property Value
[System.Collections.Generic.IList&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IList-1 'System.Collections.Generic.IList`1')[EncryptedUserKeys](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IList-1 'System.Collections.Generic.IList`1')
### Methods

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.Deserialize(string)'></a>

## XfEncryptedKeys.Deserialize(string) Method

Deserialize a JSON string to an instance.

```csharp
public static Xecrets.Sdk.Models.XfEncryptedKeys Deserialize(string json);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.Deserialize(string).json'></a>

`json` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The json to deserialize

#### Returns
[XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys')  
The deserialized instance or an empty instance.

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.Serialize(Xecrets.Sdk.Models.XfEncryptedKeys)'></a>

## XfEncryptedKeys.Serialize(XfEncryptedKeys) Method

Serialize this instance to a JSON string.

```csharp
public static string Serialize(Xecrets.Sdk.Models.XfEncryptedKeys xfEncryptedKeys);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.Serialize(Xecrets.Sdk.Models.XfEncryptedKeys).xfEncryptedKeys'></a>

`xfEncryptedKeys` [XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys')

The instance to serialize.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
A JSON string