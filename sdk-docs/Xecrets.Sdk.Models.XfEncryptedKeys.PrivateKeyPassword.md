#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models').[XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys')

## XfEncryptedKeys.PrivateKeyPassword Enum

Records wether the private key password is known or unknown.

```csharp
public enum XfEncryptedKeys.PrivateKeyPassword
```
### Fields

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword.Undefined'></a>

`Undefined` 0

Should not be used, means that the password status is not known.

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword.Known'></a>

`Known` 1

The password was known when it was last serialized.

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword.Unknown'></a>

`Unknown` 2

The password was not known when it was last serialized.