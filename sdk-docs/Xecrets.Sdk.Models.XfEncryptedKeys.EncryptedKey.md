#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models').[XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys')

## XfEncryptedKeys.EncryptedKey Class

An encrypted private key with additional metadata.

```csharp
public record XfEncryptedKeys.EncryptedKey : System.IEquatable<Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; EncryptedKey

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[EncryptedKey](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Properties

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey.Email'></a>

## XfEncryptedKeys.EncryptedKey.Email Property

The email address of the user this key belongs to.

```csharp
public string Email { get; init; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey.EncryptedKeyPair'></a>

## XfEncryptedKeys.EncryptedKey.EncryptedKeyPair Property

The key pair, with the private key encrypted.

```csharp
public Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair EncryptedKeyPair { get; init; }
```

#### Property Value
[EncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair')

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey.PrivateKeyStatus'></a>

## XfEncryptedKeys.EncryptedKey.PrivateKeyStatus Property

Wether the private key password was known or unknown when last serialized.

```csharp
public Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword PrivateKeyStatus { get; init; }
```

#### Property Value
[PrivateKeyPassword](Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword.md 'Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword')

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey.Thumbprint'></a>

## XfEncryptedKeys.EncryptedKey.Thumbprint Property

The thumbprint of the key, used to identify it if required.

```csharp
public string Thumbprint { get; init; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey.Timestamp'></a>

## XfEncryptedKeys.EncryptedKey.Timestamp Property

The timestamp when the key was created.

```csharp
public System.DateTime Timestamp { get; init; }
```

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')