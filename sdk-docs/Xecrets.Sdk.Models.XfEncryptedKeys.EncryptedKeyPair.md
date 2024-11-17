#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models').[XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys')

## XfEncryptedKeys.EncryptedKeyPair Class

A key pair with a public and an encrypted private key pem. Then encryption is done with a password  
using Xecrets Ez/AxCrypt and then the encrypted file is base64 encoded.

```csharp
public class XfEncryptedKeys.EncryptedKeyPair :
System.IEquatable<Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; EncryptedKeyPair

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[EncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Properties

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair.EncryptedPrivateKey'></a>

## XfEncryptedKeys.EncryptedKeyPair.EncryptedPrivateKey Property

The encrypted private key pem.

```csharp
public string EncryptedPrivateKey { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair.PublicKey'></a>

## XfEncryptedKeys.EncryptedKeyPair.PublicKey Property

The public key pem.

```csharp
public string PublicKey { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')