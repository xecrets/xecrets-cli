#### [Xecrets.Sdk](index.md 'index')

## Xecrets.Sdk.Abstractions Namespace
### Interfaces

<a name='Xecrets.Sdk.Abstractions.IXfApi'></a>

## IXfApi Interface

The main SDK interface to use when calling the command line for operations. Use [XfApiFactory](Xecrets.Sdk.XfApiFactory.md 'Xecrets.Sdk.XfApiFactory') to
create instances of the interface.

```csharp
public interface IXfApi
```
### Methods

<a name='Xecrets.Sdk.Abstractions.IXfApi.CliToolInformationAsync()'></a>

## IXfApi.CliToolInformationAsync() Method

Call the command line and request information as a [CliToolInformation](Xecrets.Sdk.Cli.Models.CliToolInformation.md 'Xecrets.Sdk.Cli.Models.CliToolInformation') instance.

```csharp
System.Threading.Tasks.Task<Xecrets.Sdk.Cli.Models.CliToolInformation> CliToolInformationAsync();
```

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[CliToolInformation](Xecrets.Sdk.Cli.Models.CliToolInformation.md 'Xecrets.Sdk.Cli.Models.CliToolInformation')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The information requested by calling the command line.

<a name='Xecrets.Sdk.Abstractions.IXfApi.CreateKeyPairAsync(string,string)'></a>

## IXfApi.CreateKeyPairAsync(string, string) Method

Create an asymmetric key pair [XfEncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeyPair') as an encrypted blob, using the provided
password.

```csharp
System.Threading.Tasks.Task<Xecrets.Sdk.Models.XfEncryptedKeyPair> CreateKeyPairAsync(string password, string email);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.CreateKeyPairAsync(string,string).password'></a>

`password` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The password to encrypt the key pair with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.CreateKeyPairAsync(string,string).email'></a>

`email` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The email to associate the key pair with.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[XfEncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeyPair')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The [XfEncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeyPair')

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptArmorAsync(Xecrets.Sdk.Models.XfCredentials,string)'></a>

## IXfApi.DecryptArmorAsync(XfCredentials, string) Method

Decrypt an arbitrary string from ASCII armor format.

```csharp
System.Threading.Tasks.Task<(string text,string fileName)> DecryptArmorAsync(Xecrets.Sdk.Models.XfCredentials credentials, string armor);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptArmorAsync(Xecrets.Sdk.Models.XfCredentials,string).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

One or more passwords to decrypt with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptArmorAsync(Xecrets.Sdk.Models.XfCredentials,string).armor'></a>

`armor` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The ASCII armor string to decrypt.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System.ValueTuple')[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System.ValueTuple')[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple 'System.ValueTuple')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The decrypted text, and the original file name embedded in the encrypted data.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptFilesInPlaceAsync(Xecrets.Sdk.Models.XfCredentials,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_)'></a>

## IXfApi.DecryptFilesInPlaceAsync(XfCredentials, IEnumerable<XfFilePair>, Action<XfMessage>) Method

Decrypt files in their location, using the stored original file name.

```csharp
System.Threading.Tasks.Task<Xecrets.Sdk.Models.XfFileResult[]> DecryptFilesInPlaceAsync(Xecrets.Sdk.Models.XfCredentials credentials, System.Collections.Generic.IEnumerable<Xecrets.Sdk.Models.XfFilePair> files, System.Action<Xecrets.Sdk.Models.XfMessage> progress);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptFilesInPlaceAsync(Xecrets.Sdk.Models.XfCredentials,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

One or more passwords to encrypt with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptFilesInPlaceAsync(Xecrets.Sdk.Models.XfCredentials,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_).files'></a>

`files` [System.Collections.Generic.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')[XfFilePair](Xecrets.Sdk.Models.XfFilePair.md 'Xecrets.Sdk.Models.XfFilePair')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')

The list of files to decrypt. Only the [SourceFullName](Xecrets.Sdk.Models.XfFilePair.md#Xecrets.Sdk.Models.XfFilePair.SourceFullName 'Xecrets.Sdk.Models.XfFilePair.SourceFullName') is
            used.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptFilesInPlaceAsync(Xecrets.Sdk.Models.XfCredentials,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_).progress'></a>

`progress` [System.Action&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')[XfMessage](Xecrets.Sdk.Models.XfMessage.md 'Xecrets.Sdk.Models.XfMessage')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')

An [System.Action](https://learn.microsoft.com/en-us/dotnet/api/system.action 'System.Action') delegate called with progress.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[XfFileResult](Xecrets.Sdk.Models.XfFileResult.md 'Xecrets.Sdk.Models.XfFileResult')[[]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System.Array')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
An array of [XfFileResult](Xecrets.Sdk.Models.XfFileResult.md 'Xecrets.Sdk.Models.XfFileResult') instances representing the resulting files.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptKeyPairAsync(string,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfEncryptedKeyPair_)'></a>

## IXfApi.DecryptKeyPairAsync(string, IEnumerable<XfEncryptedKeyPair>) Method

Attempt to decrypt key pairs using the provided password. The first one possible to decrypt in the given
enumerable will be returned.

```csharp
System.Threading.Tasks.Task<Xecrets.Sdk.Models.XfKeyPair?> DecryptKeyPairAsync(string password, System.Collections.Generic.IEnumerable<Xecrets.Sdk.Models.XfEncryptedKeyPair> encryptedKeyPairs);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptKeyPairAsync(string,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfEncryptedKeyPair_).password'></a>

`password` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The password to try to decrypt with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptKeyPairAsync(string,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfEncryptedKeyPair_).encryptedKeyPairs'></a>

`encryptedKeyPairs` [System.Collections.Generic.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')[XfEncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeyPair')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')

A list of key pairs to try to decrypt.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[XfKeyPair](Xecrets.Sdk.Models.XfKeyPair.md 'Xecrets.Sdk.Models.XfKeyPair')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The first key pair possible to decrypt, or null if none could be decrypted.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream)'></a>

## IXfApi.DecryptStreamAsync(XfCredentials, Stream, Stream) Method

Decrypt an arbitrary stream to another stream

```csharp
System.Threading.Tasks.Task<string> DecryptStreamAsync(Xecrets.Sdk.Models.XfCredentials credentials, System.IO.Stream cipherStream, System.IO.Stream clearStream);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

One or more passwords to decrypt with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).cipherStream'></a>

`cipherStream` [System.IO.Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream 'System.IO.Stream')

A stream of data to decrypt.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).clearStream'></a>

`clearStream` [System.IO.Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream 'System.IO.Stream')

A stream to write the clear text to.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
A [System.Threading.Tasks.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System.Threading.Tasks.Task') to wait for and the original file name as a string, or an empty string if the
            credentials were invalid.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_)'></a>

## IXfApi.DecryptToAndKeepFileAsync(XfCredentials, FileInfo, DirectoryInfo, Action<XfMessage>) Method

Decrypt a file using the provided credentials, to a specified directory, returning the resulting original
filename and decrypted file represented by a [System.IO.FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo 'System.IO.FileInfo') instance.

```csharp
System.Threading.Tasks.Task<Xecrets.Sdk.Models.XfFileResult[]> DecryptToAndKeepFileAsync(Xecrets.Sdk.Models.XfCredentials credentials, System.IO.FileInfo file, System.IO.DirectoryInfo directory, System.Action<Xecrets.Sdk.Models.XfMessage> progress);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

One or more passwords to encrypt with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).file'></a>

`file` [System.IO.FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo 'System.IO.FileInfo')

The file to decrypt.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).directory'></a>

`directory` [System.IO.DirectoryInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo 'System.IO.DirectoryInfo')

The location to decrypt to.

<a name='Xecrets.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.Sdk.Models.XfMessage_).progress'></a>

`progress` [System.Action&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')[XfMessage](Xecrets.Sdk.Models.XfMessage.md 'Xecrets.Sdk.Models.XfMessage')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')

An [System.Action](https://learn.microsoft.com/en-us/dotnet/api/system.action 'System.Action') delegate called with progress.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[XfFileResult](Xecrets.Sdk.Models.XfFileResult.md 'Xecrets.Sdk.Models.XfFileResult')[[]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System.Array')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
An array of results, with excactly 1 element.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_)'></a>

## IXfApi.EncryptFilesAsync(XfCredentials, bool, bool, IEnumerable<XfFilePair>, Action<XfMessage>) Method

Encrypt plain text files as encrypted files, keeping or wiping the originals.

```csharp
System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<System.IO.FileInfo>> EncryptFilesAsync(Xecrets.Sdk.Models.XfCredentials credentials, bool overwrite, bool wipe, System.Collections.Generic.IEnumerable<Xecrets.Sdk.Models.XfFilePair> pairs, System.Action<Xecrets.Sdk.Models.XfMessage> progress);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

The credentials to use when encrypting the files.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_).overwrite'></a>

`overwrite` [System.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System.Boolean')

Set to 'false' if overwriting the target should be disallowed, and instead use an
            alternate non-colliding name.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_).wipe'></a>

`wipe` [System.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System.Boolean')

Set to 'true' to also wipe the original plain text.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_).pairs'></a>

`pairs` [System.Collections.Generic.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')[XfFilePair](Xecrets.Sdk.Models.XfFilePair.md 'Xecrets.Sdk.Models.XfFilePair')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')

File name pairs, providing source and target names as well as original name to include
            in the encrypted file.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.Sdk.Models.XfFilePair_,System.Action_Xecrets.Sdk.Models.XfMessage_).progress'></a>

`progress` [System.Action&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')[XfMessage](Xecrets.Sdk.Models.XfMessage.md 'Xecrets.Sdk.Models.XfMessage')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')

An [System.Action](https://learn.microsoft.com/en-us/dotnet/api/system.action 'System.Action') delegate that will be called reporting progress of the
            operation.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[System.Collections.Generic.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')[System.IO.FileInfo](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo 'System.IO.FileInfo')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptKeyPairAsync(Xecrets.Sdk.Models.XfKeyPair)'></a>

## IXfApi.EncryptKeyPairAsync(XfKeyPair) Method

Encrypt a [XfKeyPair](Xecrets.Sdk.Models.XfKeyPair.md 'Xecrets.Sdk.Models.XfKeyPair') into a [XfEncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeyPair') using the provided password in the
[XfKeyPair](Xecrets.Sdk.Models.XfKeyPair.md 'Xecrets.Sdk.Models.XfKeyPair') instance.

```csharp
System.Threading.Tasks.Task<Xecrets.Sdk.Models.XfEncryptedKeyPair> EncryptKeyPairAsync(Xecrets.Sdk.Models.XfKeyPair keyPair);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptKeyPairAsync(Xecrets.Sdk.Models.XfKeyPair).keyPair'></a>

`keyPair` [XfKeyPair](Xecrets.Sdk.Models.XfKeyPair.md 'Xecrets.Sdk.Models.XfKeyPair')

The key pair to encrypt.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[XfEncryptedKeyPair](Xecrets.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.Sdk.Models.XfEncryptedKeyPair')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The encrypted key pair as a blob.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream)'></a>

## IXfApi.EncryptStreamAsync(XfCredentials, string, Stream, Stream) Method

Encrypt an arbitrary stream to another stream

```csharp
System.Threading.Tasks.Task EncryptStreamAsync(Xecrets.Sdk.Models.XfCredentials credentials, string originalFileName, System.IO.Stream clearStream, System.IO.Stream cipherStream);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

One or more passwords to encrypt with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).originalFileName'></a>

`originalFileName` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The original file name to embed into the encrypted file stream

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).clearStream'></a>

`clearStream` [System.IO.Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream 'System.IO.Stream')

A stream of data to encrypt.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptStreamAsync(Xecrets.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).cipherStream'></a>

`cipherStream` [System.IO.Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream 'System.IO.Stream')

The stream to write the encrypted data to.

#### Returns
[System.Threading.Tasks.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System.Threading.Tasks.Task')  
A [System.Threading.Tasks.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System.Threading.Tasks.Task') to wait for.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptTextAsync(Xecrets.Sdk.Models.XfCredentials,string,string)'></a>

## IXfApi.EncryptTextAsync(XfCredentials, string, string) Method

Encrypt an arbitrary string to ASCII armor format.

```csharp
System.Threading.Tasks.Task<string> EncryptTextAsync(Xecrets.Sdk.Models.XfCredentials credentials, string text, string originalFileName);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptTextAsync(Xecrets.Sdk.Models.XfCredentials,string,string).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

One or more passwords to encrypt with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptTextAsync(Xecrets.Sdk.Models.XfCredentials,string,string).text'></a>

`text` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The text to encrypt.

<a name='Xecrets.Sdk.Abstractions.IXfApi.EncryptTextAsync(Xecrets.Sdk.Models.XfCredentials,string,string).originalFileName'></a>

`originalFileName` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The original file name to embed into the encrypted file stream.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The string encrypted, packaged as an ASCII armor string.

<a name='Xecrets.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version)'></a>

## IXfApi.IsSdkCompatibleWith(Version) Method

Determine if the consumer, using this version of the SDK and the provided [cliApiVersion](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version).cliApiVersion 'Xecrets.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version).cliApiVersion')
version are compatible, i.e. if the caller having a command line tool of the given version can use this
version of the SDK.

```csharp
bool IsSdkCompatibleWith(System.Version cliApiVersion);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version).cliApiVersion'></a>

`cliApiVersion` [System.Version](https://learn.microsoft.com/en-us/dotnet/api/system.version 'System.Version')

The version of the command line tool API to check.

#### Returns
[System.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System.Boolean')  
True if this SDK can work with the provided command line tool.

<a name='Xecrets.Sdk.Abstractions.IXfApi.Slip39CombineAsync(System.Collections.Generic.IEnumerable_string_)'></a>

## IXfApi.Slip39CombineAsync(IEnumerable<string>) Method

Try to combine shares and recover the secret.

```csharp
System.Threading.Tasks.Task<Xecrets.Sdk.Models.XfSlip39.ShareSet?> Slip39CombineAsync(System.Collections.Generic.IEnumerable<string> shares);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.Slip39CombineAsync(System.Collections.Generic.IEnumerable_string_).shares'></a>

`shares` [System.Collections.Generic.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[Xecrets.Sdk.Models.XfSlip39.ShareSet](https://learn.microsoft.com/en-us/dotnet/api/xecrets.sdk.models.xfslip39.shareset 'Xecrets.Sdk.Models.XfSlip39.ShareSet')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The shares, grouped and interpreted as well as the secret if possible. Returns null if there's
            something wrong with one or more of the shares.

<a name='Xecrets.Sdk.Abstractions.IXfApi.Slip39SplitAsync(string,int,int)'></a>

## IXfApi.Slip39SplitAsync(string, int, int) Method

Split a secret into shares using the SLIP39 scheme.

```csharp
System.Threading.Tasks.Task<Xecrets.Sdk.Models.XfSlip39.ShareSet> Slip39SplitAsync(string secret, int shares, int threshold);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.Slip39SplitAsync(string,int,int).secret'></a>

`secret` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

The secret to split.

<a name='Xecrets.Sdk.Abstractions.IXfApi.Slip39SplitAsync(string,int,int).shares'></a>

`shares` [System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System.Int32')

The number of shares to split it into.

<a name='Xecrets.Sdk.Abstractions.IXfApi.Slip39SplitAsync(string,int,int).threshold'></a>

`threshold` [System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System.Int32')

The number of shares required to recover the secret.
Must be greater than 1 and less than or equal the number of shares.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[Xecrets.Sdk.Models.XfSlip39.ShareSet](https://learn.microsoft.com/en-us/dotnet/api/xecrets.sdk.models.xfslip39.shareset 'Xecrets.Sdk.Models.XfSlip39.ShareSet')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The shares

<a name='Xecrets.Sdk.Abstractions.IXfApi.UpdatePrivateKeysAsync(Xecrets.Sdk.Models.XfCredentials,string)'></a>

## IXfApi.UpdatePrivateKeysAsync(XfCredentials, string) Method

Load private keys for decryption, returning an updated version with the same keys.

```csharp
System.Threading.Tasks.Task<string> UpdatePrivateKeysAsync(Xecrets.Sdk.Models.XfCredentials credentials, string json);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.UpdatePrivateKeysAsync(Xecrets.Sdk.Models.XfCredentials,string).credentials'></a>

`credentials` [XfCredentials](Xecrets.Sdk.Models.XfCredentials.md 'Xecrets.Sdk.Models.XfCredentials')

One or more passwords to decrypt the private keys with.

<a name='Xecrets.Sdk.Abstractions.IXfApi.UpdatePrivateKeysAsync(Xecrets.Sdk.Models.XfCredentials,string).json'></a>

`json` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

A json serialized instance with private keys.

#### Returns
[System.Threading.Tasks.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System.Threading.Tasks.Task`1')  
The updated encrypted keys json.

<a name='Xecrets.Sdk.Abstractions.IXfApi.WipeFilesAsync(System.Collections.Generic.IEnumerable_string_,System.Action_Xecrets.Sdk.Models.XfMessage_)'></a>

## IXfApi.WipeFilesAsync(IEnumerable<string>, Action<XfMessage>) Method

Wipe files.

```csharp
System.Threading.Tasks.Task WipeFilesAsync(System.Collections.Generic.IEnumerable<string> fullNames, System.Action<Xecrets.Sdk.Models.XfMessage> progress);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApi.WipeFilesAsync(System.Collections.Generic.IEnumerable_string_,System.Action_Xecrets.Sdk.Models.XfMessage_).fullNames'></a>

`fullNames` [System.Collections.Generic.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System.Collections.Generic.IEnumerable`1')

The list of full path names to wipe.

<a name='Xecrets.Sdk.Abstractions.IXfApi.WipeFilesAsync(System.Collections.Generic.IEnumerable_string_,System.Action_Xecrets.Sdk.Models.XfMessage_).progress'></a>

`progress` [System.Action&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')[XfMessage](Xecrets.Sdk.Models.XfMessage.md 'Xecrets.Sdk.Models.XfMessage')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System.Action`1')

An [System.Action](https://learn.microsoft.com/en-us/dotnet/api/system.action 'System.Action') delegate called with progress.

#### Returns
[System.Threading.Tasks.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System.Threading.Tasks.Task')  
A waitable task.

<a name='Xecrets.Sdk.Abstractions.IXfApiFactory'></a>

## IXfApiFactory Interface

A factory for creating an IXfApi instance.

```csharp
public interface IXfApiFactory
```

Derived  
&#8627; [XfApiFactory](Xecrets.Sdk.XfApiFactory.md 'Xecrets.Sdk.XfApiFactory')
### Methods

<a name='Xecrets.Sdk.Abstractions.IXfApiFactory.Create(System.Nullable_bool_,System.Threading.CancellationToken)'></a>

## IXfApiFactory.Create(Nullable<bool>, CancellationToken) Method

Create an [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance.

```csharp
Xecrets.Sdk.Abstractions.IXfApi Create(System.Nullable<bool> debugOverride, System.Threading.CancellationToken ct);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfApiFactory.Create(System.Nullable_bool_,System.Threading.CancellationToken).debugOverride'></a>

`debugOverride` [System.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System.Nullable`1')[System.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System.Nullable`1')

Override the global debug cli flag if non-null.

<a name='Xecrets.Sdk.Abstractions.IXfApiFactory.Create(System.Nullable_bool_,System.Threading.CancellationToken).ct'></a>

`ct` [System.Threading.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System.Threading.CancellationToken')

A [System.Threading.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System.Threading.CancellationToken') to cancel any long running operation.

#### Returns
[IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi')  
An IXfApi instance.

<a name='Xecrets.Sdk.Abstractions.IXfStart'></a>

## IXfStart Interface

Provides start-up information for the application.

```csharp
public interface IXfStart
```
### Methods

<a name='Xecrets.Sdk.Abstractions.IXfStart.StartArgs(string)'></a>

## IXfStart.StartArgs(string) Method

Get the start arguments necessary to start the program identified as [fileNameWithoutExtension](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfStart.StartArgs(string).fileNameWithoutExtension 'Xecrets.Sdk.Abstractions.IXfStart.StartArgs(string).fileNameWithoutExtension'), assuming that if we're started with "dotnet", this program should also be
started with "dotnet" [fileNameWithoutExtension](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfStart.StartArgs(string).fileNameWithoutExtension 'Xecrets.Sdk.Abstractions.IXfStart.StartArgs(string).fileNameWithoutExtension').dll, otherwise it's an executable with
".exe" or without depending on the current operating system.

```csharp
string[] StartArgs(string fileNameWithoutExtension);
```
#### Parameters

<a name='Xecrets.Sdk.Abstractions.IXfStart.StartArgs(string).fileNameWithoutExtension'></a>

`fileNameWithoutExtension` [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

#### Returns
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')[[]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System.Array')

#### Exceptions

[System.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System.InvalidOperationException')