#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models')

## XfCredentials Class

A cargo class carrying all the various possible credentials used for encryption or decryption. When it's not supported  
or doesn't make sense, only the first will be used.

```csharp
public class XfCredentials
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfCredentials
### Properties

<a name='Xecrets.Sdk.Models.XfCredentials.KeyPairFullNames'></a>

## XfCredentials.KeyPairFullNames Property

An enumeration of full path names to key pair PEM files

```csharp
public System.Collections.Generic.IEnumerable<string> KeyPairFullNames { get; }
```

#### Property Value
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

<a name='Xecrets.Sdk.Models.XfCredentials.Passwords'></a>

## XfCredentials.Passwords Property

An enumeration of passwords

```csharp
public System.Collections.Generic.IEnumerable<string> Passwords { get; }
```

#### Property Value
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

<a name='Xecrets.Sdk.Models.XfCredentials.PublicKeyEmails'></a>

## XfCredentials.PublicKeyEmails Property

An enumeration of e-mail monikers referring to public keys

```csharp
public System.Collections.Generic.IEnumerable<string> PublicKeyEmails { get; }
```

#### Property Value
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

<a name='Xecrets.Sdk.Models.XfCredentials.PublicKeyFullNames'></a>

## XfCredentials.PublicKeyFullNames Property

An enumeration of full path names to public key PEM files

```csharp
public System.Collections.Generic.IEnumerable<string> PublicKeyFullNames { get; }
```

#### Property Value
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')
### Methods

<a name='Xecrets.Sdk.Models.XfCredentials.AddKeyPairFullNames(System.Collections.Generic.IEnumerable_string_)'></a>

## XfCredentials.AddKeyPairFullNames(IEnumerable<string>) Method

Add zero or more full path names to key pairs to add to the collection of credentials.

```csharp
public void AddKeyPairFullNames(System.Collections.Generic.IEnumerable<string> keyPairFullNames);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfCredentials.AddKeyPairFullNames(System.Collections.Generic.IEnumerable_string_).keyPairFullNames'></a>

`keyPairFullNames` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

An enumeration of full path names to files containing key pairs.

<a name='Xecrets.Sdk.Models.XfCredentials.AddPasswords(System.Collections.Generic.IEnumerable_string_)'></a>

## XfCredentials.AddPasswords(IEnumerable<string>) Method

Add zero or more passwords to add to the collection of credentials.

```csharp
public void AddPasswords(System.Collections.Generic.IEnumerable<string> passwords);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfCredentials.AddPasswords(System.Collections.Generic.IEnumerable_string_).passwords'></a>

`passwords` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

An enumeration of passwords to add.

<a name='Xecrets.Sdk.Models.XfCredentials.AddPublicKeyEmails(System.Collections.Generic.IEnumerable_string_)'></a>

## XfCredentials.AddPublicKeyEmails(IEnumerable<string>) Method

Add zero or more e-mail monikers to add to the collection of credentials.

```csharp
public void AddPublicKeyEmails(System.Collections.Generic.IEnumerable<string> publicKeyEmails);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfCredentials.AddPublicKeyEmails(System.Collections.Generic.IEnumerable_string_).publicKeyEmails'></a>

`publicKeyEmails` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

An enumeration of e-mail monikers referring to public keys.

<a name='Xecrets.Sdk.Models.XfCredentials.AddPublicKeyFullNames(System.Collections.Generic.IEnumerable_string_)'></a>

## XfCredentials.AddPublicKeyFullNames(IEnumerable<string>) Method

Add zero or more full path names to public keys to add to the collection of credentials.

```csharp
public void AddPublicKeyFullNames(System.Collections.Generic.IEnumerable<string> publicKeyFullNames);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfCredentials.AddPublicKeyFullNames(System.Collections.Generic.IEnumerable_string_).publicKeyFullNames'></a>

`publicKeyFullNames` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

An enumeration of full path names to files containing public keys.