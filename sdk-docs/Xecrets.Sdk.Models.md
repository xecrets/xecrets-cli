#### [Xecrets.Sdk](index.md 'index')

## Xecrets.Sdk.Models Namespace

| Classes | |
| :--- | :--- |
| [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials') | A cargo class carrying all the various possible credentials used for encryption or decryption. When it's not supported<br/>or doesn't make sense, only the first will be used. |
| [XfEncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeyPair') | An encrypted key pair blob with some meta data. |
| [XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys') | Extra, possibly imported, encrypted private keys for a user. |
| [XfEncryptedKeys.EncryptedKey](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey') | An encrypted private key with additional metadata. |
| [XfEncryptedKeys.EncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKeyPair') | A key pair with a public and an encrypted private key pem. Then encryption is done with a password<br/>using Xecrets Ez/AxCrypt and then the encrypted file is base64 encoded. |
| [XfEncryptedKeys.EncryptedUserKeys](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys') | A collection of encrypted private keys for a user, where a user is arbitrarily defined as an email address. |
| [XfFilePair](Xecrets.Sdk.Models.XfFilePair.md 'Xecrets.Sdk.Models.XfFilePair') | A plain text/cipher text file pair |
| [XfFileResult](Xecrets.Sdk.Models.XfFileResult.md 'Xecrets.Sdk.Models.XfFileResult') | Instatiate a new instance of XfFileResult |
| [XfKeyPair](Xecrets.Sdk.Models.XfKeyPair.md 'Xecrets.Sdk.Models.XfKeyPair') | A public key pair, including optional encryption password and meta data. |
### Enums

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword'></a>

## XfEncryptedKeys.PrivateKeyPassword Enum

Records wether the private key password is known or unknown.

```csharp
public enum XfEncryptedKeys.PrivateKeyPassword
```
### Fields

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword.Known'></a>

`Known` 1

The password was known when it was last serialized.

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword.Undefined'></a>

`Undefined` 0

Should not be used, means that the password status is not known.

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.PrivateKeyPassword.Unknown'></a>

`Unknown` 2

The password was not known when it was last serialized.