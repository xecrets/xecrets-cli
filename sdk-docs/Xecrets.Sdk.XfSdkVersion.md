#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk](Xecrets.Sdk.md 'Xecrets.Sdk')

## XfSdkVersion Class

Helper to determine if the current SDK version is compatible with a given command line tool API version.

```csharp
public static class XfSdkVersion
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfSdkVersion
### Properties

<a name='Xecrets.Sdk.XfSdkVersion.SdkVersion'></a>

## XfSdkVersion.SdkVersion Property

The API version is to be updated when the command line options are updated in an incompatible way. The minor
version is increased if changes do not change the meaning or syntax of any pre-existing options, i.e. purely new
additional capabilities. The major version is increased if changes in any way changes an existing option so it
may not work as expected by an older consumer. The consumer should thus only accept an export version that has
the same Major version, and a minor version greather than or equal to it's known version.

```csharp
public static System.Version SdkVersion { get; }
```

#### Property Value
[System.Version](https://docs.microsoft.com/en-us/dotnet/api/System.Version 'System.Version')

### Remarks
Changes includes not only syntax and semantics, but also actual numerical values assigned to <seealso cref="T:Xecrets.Sdk.Models.XfOpCode"/>, <seealso cref="T:Xecrets.Sdk.Models.XfStatusCode"> and </seealso>and <seealso cref="T:Xecrets.Sdk.Models.XfCliApi"/> as well as changes
or removals of json property names in
<seealso cref="T:Xecrets.Sdk.Models.XfMessage"/> .
### Methods

<a name='Xecrets.Sdk.XfSdkVersion.IsSdkCompatibleWith(System.Version)'></a>

## XfSdkVersion.IsSdkCompatibleWith(Version) Method

A predicate determining if this SDK is compatible with a given command line tool API version.

```csharp
public static bool IsSdkCompatibleWith(System.Version cliApiVersion);
```
#### Parameters

<a name='Xecrets.Sdk.XfSdkVersion.IsSdkCompatibleWith(System.Version).cliApiVersion'></a>

`cliApiVersion` [System.Version](https://docs.microsoft.com/en-us/dotnet/api/System.Version 'System.Version')

The command line tool API version

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if this SDK is compatible with the command line tool, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') otherwise.