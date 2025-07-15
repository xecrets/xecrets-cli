#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models')

## XfEncryptedKeyPair Class

An encrypted key pair blob with some meta data.

```csharp
public class XfEncryptedKeyPair
```

Inheritance [System.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System.Object') &#129106; XfEncryptedKeyPair

### Remarks
Create a new instance.
### Constructors

<a name='Xecrets.Sdk.Models.XfEncryptedKeyPair.XfEncryptedKeyPair(string,string,byte[])'></a>

## XfEncryptedKeyPair(string, string, byte[]) Constructor

An encrypted key pair blob with some meta data.

```csharp
public XfEncryptedKeyPair(string fullName, string email, byte[] data);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfEncryptedKeyPair.XfEncryptedKeyPair(string,string,byte[]).fullName'></a>

`fullName` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The full path and name to the encrypted [Data](Xecrets.Sdk.Models.XfEncryptedKeyPair.md#Xecrets.Sdk.Models.XfEncryptedKeyPair.Data 'Xecrets.Sdk.Models.XfEncryptedKeyPair.Data') blob, or an empty
            string./>

<a name='Xecrets.Sdk.Models.XfEncryptedKeyPair.XfEncryptedKeyPair(string,string,byte[]).email'></a>

`email` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The email moniker that this key pair is associated with.

<a name='Xecrets.Sdk.Models.XfEncryptedKeyPair.XfEncryptedKeyPair(string,string,byte[]).data'></a>

`data` [System.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System.Byte')[[]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System.Array')

The encrypted key pair blob.

### Remarks
Create a new instance.
### Properties

<a name='Xecrets.Sdk.Models.XfEncryptedKeyPair.Data'></a>

## XfEncryptedKeyPair.Data Property

The encrypted key pair blob.

```csharp
public byte[] Data { get; }
```

#### Property Value
[System.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System.Byte')[[]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System.Array')

<a name='Xecrets.Sdk.Models.XfEncryptedKeyPair.Email'></a>

## XfEncryptedKeyPair.Email Property

The email moniker that this key pair is associated with.

```csharp
public string Email { get; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfEncryptedKeyPair.FullName'></a>

## XfEncryptedKeyPair.FullName Property

The full path and name to the encrypted [Data](Xecrets.Sdk.Models.XfEncryptedKeyPair.md#Xecrets.Sdk.Models.XfEncryptedKeyPair.Data 'Xecrets.Sdk.Models.XfEncryptedKeyPair.Data') blob, or an empty string./>

```csharp
public string FullName { get; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')