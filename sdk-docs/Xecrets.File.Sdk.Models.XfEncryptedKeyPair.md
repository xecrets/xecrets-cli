#### [Xecrets.File.Sdk](index.md 'index')
### [Xecrets.File.Sdk.Models](Xecrets.File.Sdk.Models.md 'Xecrets.File.Sdk.Models')

## XfEncryptedKeyPair Class

An encrypted key pair blob with some meta data.

```csharp
public class XfEncryptedKeyPair
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfEncryptedKeyPair

### Remarks
Create a new instance.
### Constructors

<a name='Xecrets.File.Sdk.Models.XfEncryptedKeyPair.XfEncryptedKeyPair(string,string,byte[])'></a>

## XfEncryptedKeyPair(string, string, byte[]) Constructor

An encrypted key pair blob with some meta data.

```csharp
public XfEncryptedKeyPair(string fullName, string email, byte[] data);
```
#### Parameters

<a name='Xecrets.File.Sdk.Models.XfEncryptedKeyPair.XfEncryptedKeyPair(string,string,byte[]).fullName'></a>

`fullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name to the encrypted [Data](Xecrets.File.Sdk.Models.XfEncryptedKeyPair.md#Xecrets.File.Sdk.Models.XfEncryptedKeyPair.Data 'Xecrets.File.Sdk.Models.XfEncryptedKeyPair.Data') blob, or an empty  
            string./>

<a name='Xecrets.File.Sdk.Models.XfEncryptedKeyPair.XfEncryptedKeyPair(string,string,byte[]).email'></a>

`email` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The email moniker that this key pair is associated with.

<a name='Xecrets.File.Sdk.Models.XfEncryptedKeyPair.XfEncryptedKeyPair(string,string,byte[]).data'></a>

`data` [System.Byte](https://docs.microsoft.com/en-us/dotnet/api/System.Byte 'System.Byte')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

The encrypted key pair blob.

### Remarks
Create a new instance.
### Properties

<a name='Xecrets.File.Sdk.Models.XfEncryptedKeyPair.Data'></a>

## XfEncryptedKeyPair.Data Property

The encrypted key pair blob.

```csharp
public byte[] Data { get; }
```

#### Property Value
[System.Byte](https://docs.microsoft.com/en-us/dotnet/api/System.Byte 'System.Byte')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='Xecrets.File.Sdk.Models.XfEncryptedKeyPair.Email'></a>

## XfEncryptedKeyPair.Email Property

The email moniker that this key pair is associated with.

```csharp
public string Email { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Models.XfEncryptedKeyPair.FullName'></a>

## XfEncryptedKeyPair.FullName Property

The full path and name to the encrypted [Data](Xecrets.File.Sdk.Models.XfEncryptedKeyPair.md#Xecrets.File.Sdk.Models.XfEncryptedKeyPair.Data 'Xecrets.File.Sdk.Models.XfEncryptedKeyPair.Data') blob, or an empty string./>

```csharp
public string FullName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')