#### [Xecrets.File.Sdk](index.md 'index')
### [Xecrets.File.Sdk.Models](Xecrets.File.Sdk.Models.md 'Xecrets.File.Sdk.Models')

## XfKeyPair Class

A public key pair, including optional encryption password and meta data.

```csharp
public class XfKeyPair
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfKeyPair
### Constructors

<a name='Xecrets.File.Sdk.Models.XfKeyPair.XfKeyPair()'></a>

## XfKeyPair() Constructor

Instantiate a key pair with empty default values

```csharp
public XfKeyPair();
```

<a name='Xecrets.File.Sdk.Models.XfKeyPair.XfKeyPair(Xecrets.File.Sdk.Models.XfKeyPair)'></a>

## XfKeyPair(XfKeyPair) Constructor

Instantiate a key pair with properties copied from another instance.

```csharp
public XfKeyPair(Xecrets.File.Sdk.Models.XfKeyPair keyPair);
```
#### Parameters

<a name='Xecrets.File.Sdk.Models.XfKeyPair.XfKeyPair(Xecrets.File.Sdk.Models.XfKeyPair).keyPair'></a>

`keyPair` [XfKeyPair](Xecrets.File.Sdk.Models.XfKeyPair.md 'Xecrets.File.Sdk.Models.XfKeyPair')

The key pair to copy
### Properties

<a name='Xecrets.File.Sdk.Models.XfKeyPair.Created'></a>

## XfKeyPair.Created Property

Meta data specifying when it was created (UTC)

```csharp
public System.DateTime Created { get; set; }
```

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')

<a name='Xecrets.File.Sdk.Models.XfKeyPair.Email'></a>

## XfKeyPair.Email Property

The email moniker used to identify the key pair

```csharp
public string Email { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Models.XfKeyPair.Empty'></a>

## XfKeyPair.Empty Property

An empty key pair

```csharp
public static Xecrets.File.Sdk.Models.XfKeyPair Empty { get; }
```

#### Property Value
[XfKeyPair](Xecrets.File.Sdk.Models.XfKeyPair.md 'Xecrets.File.Sdk.Models.XfKeyPair')

<a name='Xecrets.File.Sdk.Models.XfKeyPair.FullName'></a>

## XfKeyPair.FullName Property

The full path and name to the encrypted blob, or an empty string./>

```csharp
public string FullName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Models.XfKeyPair.Password'></a>

## XfKeyPair.Password Property

The password to use when encrypting the key pair

```csharp
public string Password { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Models.XfKeyPair.PrivateKey'></a>

## XfKeyPair.PrivateKey Property

The private key PEM

```csharp
public string PrivateKey { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Models.XfKeyPair.PublicKey'></a>

## XfKeyPair.PublicKey Property

The public key PEM

```csharp
public string PublicKey { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')