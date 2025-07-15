#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models').[XfEncryptedKeys](Xecrets.Sdk.Models.XfEncryptedKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys')

## XfEncryptedKeys.EncryptedUserKeys Class

A collection of encrypted private keys for a user, where a user is arbitrarily defined as an email address.

```csharp
public record XfEncryptedKeys.EncryptedUserKeys : System.IEquatable<Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys>
```

Inheritance [System.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System.Object') &#129106; EncryptedUserKeys

Implements [System.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System.IEquatable`1')[EncryptedUserKeys](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System.IEquatable`1')

### Remarks
Normally all the keys belong to the same physical user, but due to email changes etc the keys may be
associated with different email addresses.
### Properties

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys.Email'></a>

## XfEncryptedKeys.EncryptedUserKeys.Email Property

The email address identifier for this list of keys.

```csharp
public string Email { get; init; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedUserKeys.EncryptedKeys'></a>

## XfEncryptedKeys.EncryptedUserKeys.EncryptedKeys Property

A list of encrypted keys for this email.

```csharp
public System.Collections.Generic.IList<Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey> EncryptedKeys { get; init; }
```

#### Property Value
[System.Collections.Generic.IList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1 'System.Collections.Generic.IList`1')[EncryptedKey](Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey.md 'Xecrets.Sdk.Models.XfEncryptedKeys.EncryptedKey')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1 'System.Collections.Generic.IList`1')