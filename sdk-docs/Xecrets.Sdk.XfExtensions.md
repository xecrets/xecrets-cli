#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk](Xecrets.Sdk.md 'Xecrets.Sdk')

## XfExtensions Class

Useful extension methods for credentials

```csharp
public static class XfExtensions
```

Inheritance [System.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System.Object') → XfExtensions
### Methods

<a name='Xecrets.Sdk.XfExtensions.AddKeyPairFullName(thisXecrets.Sdk.Models.XfCredentials,string)'></a>

## XfExtensions.AddKeyPairFullName(this XfCredentials, string) Method

Add a key pair residing in a file to a credentials' collection.

```csharp
public static void AddKeyPairFullName(this Xecrets.Sdk.Models.XfCredentials xfCredentials, string keyPairFullName);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.AddKeyPairFullName(thisXecrets.Sdk.Models.XfCredentials,string).xfCredentials'></a>

`xfCredentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

The [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials') to use.

<a name='Xecrets.Sdk.XfExtensions.AddKeyPairFullName(thisXecrets.Sdk.Models.XfCredentials,string).keyPairFullName'></a>

`keyPairFullName` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

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

`password` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

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

`fileFullName` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The destination full path and name.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[System.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System.Byte')[[]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System.Array')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream)'></a>

## XfExtensions.DecryptFileAsync(this IXfApi, XfCredentials, Stream, Stream) Method

Decrypt a cipher stream to a clear stream.

```csharp
public static System.Threading.Tasks.Task<string> DecryptFileAsync(this Xecrets.Sdk.Abstractions.IXfApi xfApi, Xecrets.Sdk.Models.XfCredentials xfCredentials, System.IO.Stream cipherStream, System.IO.Stream clearStream);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).xfApi'></a>

`xfApi` [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi')

The [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance to use.

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).xfCredentials'></a>

`xfCredentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

The [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials') to use.

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).cipherStream'></a>

`cipherStream` [System.IO.Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream 'System.IO.Stream')

The stream containing the encrypted data.

<a name='Xecrets.Sdk.XfExtensions.DecryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).clearStream'></a>

`clearStream` [System.IO.Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream 'System.IO.Stream')

The stream to which the decrypted data is written.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The original file name, or an empty string when the credentials are invalid.

<a name='Xecrets.Sdk.XfExtensions.DecryptForOpenAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_)'></a>

## XfExtensions.DecryptForOpenAsync(this IXfApi, XfCredentials, FileInfo, DirectoryInfo, Action<XfMessage>) Method

Decrypt a file using the provided credentials, to a specified directory, returning the resulting original
filename and decrypted file represented by a [System.IO.FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo 'System.IO.FileInfo') instance.

```csharp
public static System.Threading.Tasks.Task<Xecrets.Sdk.Models.XfFileResult[]> DecryptForOpenAsync(this Xecrets.Sdk.Abstractions.IXfApi xfApi, Xecrets.Sdk.Models.XfCredentials credentials, System.IO.FileInfo file, System.IO.DirectoryInfo directory, System.Action<Xecrets.Sdk.Models.XfMessage> progress);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.DecryptForOpenAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).xfApi'></a>

`xfApi` [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi')

The [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance to use.

<a name='Xecrets.Sdk.XfExtensions.DecryptForOpenAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

One or more passwords to decrypt with.

<a name='Xecrets.Sdk.XfExtensions.DecryptForOpenAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).file'></a>

`file` [System.IO.FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo 'System.IO.FileInfo')

The file to decrypt.

<a name='Xecrets.Sdk.XfExtensions.DecryptForOpenAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).directory'></a>

`directory` [System.IO.DirectoryInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo 'System.IO.DirectoryInfo')

The location to decrypt to.

<a name='Xecrets.Sdk.XfExtensions.DecryptForOpenAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).progress'></a>

`progress` [System.Action&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')[XfMessage](Xecrets.Sdk.Models.XfMessage.md 'Xecrets.Sdk.Models.XfMessage')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')

An [System.Action](https://learn.microsoft.com/en-us/dotnet/api/system.action 'System.Action') delegate called with progress.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[XfFileResult](Xecrets.Sdk.Models.XfFileResult.md 'Xecrets.Sdk.Models.XfFileResult')[[]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System.Array')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
An array of results, with exactly 1 element.

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

`originalFileName` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The original file name to embed in the encrypted file.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,byte[],string).clearBytes'></a>

`clearBytes` [System.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte 'System.Byte')[[]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System.Array')

The data blob to encrypt.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,byte[],string).fileFullName'></a>

`fileFullName` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The destination full path and name.

#### Returns
[System.Threading.Tasks.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System.Threading.Tasks.Task')

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream)'></a>

## XfExtensions.EncryptFileAsync(this IXfApi, XfCredentials, string, Stream, Stream) Method

Encrypt a clear stream to a cipher stream.

```csharp
public static System.Threading.Tasks.Task EncryptFileAsync(this Xecrets.Sdk.Abstractions.IXfApi xfApi, Xecrets.Sdk.Models.XfCredentials xfCredentials, string originalFileName, System.IO.Stream clearStream, System.IO.Stream cipherStream);
```
#### Parameters

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).xfApi'></a>

`xfApi` [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi')

The [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance to use.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).xfCredentials'></a>

`xfCredentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

The [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials') to use.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).originalFileName'></a>

`originalFileName` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The original file name to embed in the encrypted stream.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).clearStream'></a>

`clearStream` [System.IO.Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream 'System.IO.Stream')

The stream containing the data to encrypt.

<a name='Xecrets.Sdk.XfExtensions.EncryptFileAsync(thisXecrets.Sdk.Abstractions.IXfApi,Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).cipherStream'></a>

`cipherStream` [System.IO.Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream 'System.IO.Stream')

The stream to which the encrypted data is written.

#### Returns
[System.Threading.Tasks.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System.Threading.Tasks.Task')  
A task representing the asynchronous operation.