#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk](Xecrets.Sdk.md 'Xecrets.Sdk')

## XfExtensions Class

Useful extension methods for file names

```csharp
public static class XfExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfExtensions
### Methods

<a name='Xecrets.Sdk.XfExtensions.AddEncryptedExtension(thisstring)'></a>

## XfExtensions.AddEncryptedExtension(this string) Method

Add the extension for an encrypted file

```csharp
public static string AddEncryptedExtension(this string file);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.AddEncryptedExtension(thisstring).file'></a>

`file` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A file name, presumably without extension

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The file parameter with the extension for encrypted files concatented.

<a name='Xecrets.Sdk.XfExtensions.AddKeyPairFullName(thisXecrets.Sdk.Models.XfCredentials,string)'></a>

## XfExtensions.AddKeyPairFullName(this XfCredentials, string) Method

Add a key pair residing in a file to a credentials collection.

```csharp
public static void AddKeyPairFullName(this Xecrets.Sdk.Models.XfCredentials xfCredentials, string keyPairFullName);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.AddKeyPairFullName(thisXecrets.Sdk.Models.XfCredentials,string).xfCredentials'></a>

`xfCredentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

The [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials') to use.

<a name='Xecrets.Sdk.XfExtensions.AddKeyPairFullName(thisXecrets.Sdk.Models.XfCredentials,string).keyPairFullName'></a>

`keyPairFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The key pair full path and name.

<a name='Xecrets.Sdk.XfExtensions.AddPassword(thisXecrets.Sdk.Models.XfCredentials,string)'></a>

## XfExtensions.AddPassword(this XfCredentials, string) Method

Add a password to a collection of credentials.

```csharp
public static void AddPassword(this Xecrets.Sdk.Models.XfCredentials xfCredentials, string password);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.AddPassword(thisXecrets.Sdk.Models.XfCredentials,string).xfCredentials'></a>

`xfCredentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

The [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials') to use.

<a name='Xecrets.Sdk.XfExtensions.AddPassword(thisXecrets.Sdk.Models.XfCredentials,string).password'></a>

`password` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The password to add to the collection.

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string)'></a>

## XfExtensions.DecryptFileAsync(this IXfApi, XfCredentials, string) Method

Attempt to decrypt a file

```csharp
public static System.Threading.Tasks.Task<byte[]?> DecryptFileAsync(this Xecrets.Sdk.Abstractions.IXfApi xfApi, Xecrets.Sdk.Models.XfCredentials xfCredentials, string fileFullName);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string).xfApi'></a>

`xfApi` [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi')

The [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance to use.

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string).xfCredentials'></a>

`xfCredentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

The [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials') to use.

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string).fileFullName'></a>

`fileFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The destination full path and name.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Byte](https://docs.microsoft.com/en-us/dotnet/api/System.Byte 'System.Byte')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')

<a name='Xecrets.Sdk.XfExtensions.Encrypted(thisSystem.Collections.Generic.IEnumerable_string_)'></a>

## XfExtensions.Encrypted(this IEnumerable<string>) Method

Select the files that appear to be encrypted, according to their extension.

```csharp
public static string[] Encrypted(this System.Collections.Generic.IEnumerable<string> files);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.Encrypted(thisSystem.Collections.Generic.IEnumerable_string_).files'></a>

`files` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

File names to filter

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
An array of the files that match the pattern for encrypted files.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,byte[],string)'></a>

## XfExtensions.EncryptFileAsync(this IXfApi, XfCredentials, string, byte[], string) Method

Encrypt an in-memory blob as a file

```csharp
public static System.Threading.Tasks.Task EncryptFileAsync(this Xecrets.Sdk.Abstractions.IXfApi xfApi, Xecrets.Sdk.Models.XfCredentials xfCredentials, string originalFileName, byte[] clearBytes, string fileFullName);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,byte[],string).xfApi'></a>

`xfApi` [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi')

The [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance to use.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,byte[],string).xfCredentials'></a>

`xfCredentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

The [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials') to use.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,byte[],string).originalFileName'></a>

`originalFileName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The original file name to embed in the encrypted file.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,byte[],string).clearBytes'></a>

`clearBytes` [System.Byte](https://docs.microsoft.com/en-us/dotnet/api/System.Byte 'System.Byte')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

The data blob to encrypt.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,byte[],string).fileFullName'></a>

`fileFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The destination full path and name.

#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')

<a name='Xecrets.Sdk.XfExtensions.IsEncrypted(thisstring)'></a>

## XfExtensions.IsEncrypted(this string) Method

A predicate determining of the name of a file has the suggested encrypted extension, i.e. .axx

```csharp
public static bool IsEncrypted(this string file);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.IsEncrypted(thisstring).file'></a>

`file` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A possibly full path and name of a file.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the name ends with the encrypted extension.

<a name='Xecrets.Sdk.XfExtensions.NotEncrypted(thisSystem.Collections.Generic.IEnumerable_string_)'></a>

## XfExtensions.NotEncrypted(this IEnumerable<string>) Method

Select the files that appear not to be encrypted, according to their extension.

```csharp
public static System.Collections.Generic.IEnumerable<string> NotEncrypted(this System.Collections.Generic.IEnumerable<string> files);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.NotEncrypted(thisSystem.Collections.Generic.IEnumerable_string_).files'></a>

`files` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

File names to filter

#### Returns
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')  
An enumeration of the files that do not match the pattern for encrypted files.

<a name='Xecrets.Sdk.XfExtensions.ToEncryptedName(thisstring,string)'></a>

## XfExtensions.ToEncryptedName(this string, string) Method

Build an encrypted file name from the original file name, i.e. according to the pattern: filename.ext =>
filename-ext.axx

```csharp
public static string ToEncryptedName(this string fullName, string destinationFileFullFolder);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.ToEncryptedName(thisstring,string).fullName'></a>

`fullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of a plain text file, possibly with an extension.

<a name='Xecrets.Sdk.XfExtensions.ToEncryptedName(thisstring,string).destinationFileFullFolder'></a>

`destinationFileFullFolder` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path to an optional destination folder. Set to empty string if same as source.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The suggested name for it when encrypted.