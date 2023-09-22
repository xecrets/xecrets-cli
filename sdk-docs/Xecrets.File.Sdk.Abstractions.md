#### [Xecrets.File.Sdk](index.md 'index')

## Xecrets.File.Sdk.Abstractions Namespace
### Interfaces

<a name='Xecrets.File.Sdk.Abstractions.IXfApi'></a>

## IXfApi Interface

The main SDK interface to use when calling the CLI for operations. Use [XfApiFactory](Xecrets.File.Sdk.XfApiFactory.md 'Xecrets.File.Sdk.XfApiFactory') to create instances of the interface.

```csharp
public interface IXfApi
```
### Methods

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.CliToolInformationAsync()'></a>

## IXfApi.CliToolInformationAsync() Method

Call the CLI and request information as a [CliToolInformation](Xecrets.File.Sdk.Cli.Models.CliToolInformation.md 'Xecrets.File.Sdk.Cli.Models.CliToolInformation') instance.

```csharp
System.Threading.Tasks.Task<Xecrets.File.Sdk.Cli.Models.CliToolInformation> CliToolInformationAsync();
```

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[CliToolInformation](Xecrets.File.Sdk.Cli.Models.CliToolInformation.md 'Xecrets.File.Sdk.Cli.Models.CliToolInformation')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The information requested by calling the CLI.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.CreateKeyPairAsync(string,string)'></a>

## IXfApi.CreateKeyPairAsync(string, string) Method

Create an asymmetric key pair [XfEncryptedKeyPair](Xecrets.File.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.File.Sdk.Models.XfEncryptedKeyPair') as an encrypted blob, using the provided  
password.

```csharp
System.Threading.Tasks.Task<Xecrets.File.Sdk.Models.XfEncryptedKeyPair> CreateKeyPairAsync(string password, string email);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.CreateKeyPairAsync(string,string).password'></a>

`password` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The password to encrypt the key pair with.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.CreateKeyPairAsync(string,string).email'></a>

`email` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The email to associate the key pair with.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[XfEncryptedKeyPair](Xecrets.File.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.File.Sdk.Models.XfEncryptedKeyPair')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The [XfEncryptedKeyPair](Xecrets.File.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.File.Sdk.Models.XfEncryptedKeyPair')

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptFilesInPlaceAsync(Xecrets.File.Sdk.Models.XfCredentials,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_)'></a>

## IXfApi.DecryptFilesInPlaceAsync(XfCredentials, IEnumerable<XfFilePair>, Action<CliMessage>) Method

Decrypt files in their location, using the stored original file name.

```csharp
System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<System.IO.FileInfo>> DecryptFilesInPlaceAsync(Xecrets.File.Sdk.Models.XfCredentials credentials, System.Collections.Generic.IEnumerable<Xecrets.File.Sdk.Models.XfFilePair> files, System.Action<Xecrets.File.Sdk.Cli.CliMessage> progress);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptFilesInPlaceAsync(Xecrets.File.Sdk.Models.XfCredentials,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).credentials'></a>

`credentials` [XfCredentials](Xecrets.File.Sdk.Models.XfCredentials.md 'Xecrets.File.Sdk.Models.XfCredentials')

One or more passwords to encrypt with.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptFilesInPlaceAsync(Xecrets.File.Sdk.Models.XfCredentials,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).files'></a>

`files` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[XfFilePair](Xecrets.File.Sdk.Models.XfFilePair.md 'Xecrets.File.Sdk.Models.XfFilePair')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

The list of files to decrypt. Only the [SourceFullName](Xecrets.File.Sdk.Models.XfFilePair.md#Xecrets.File.Sdk.Models.XfFilePair.SourceFullName 'Xecrets.File.Sdk.Models.XfFilePair.SourceFullName') is  
            used.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptFilesInPlaceAsync(Xecrets.File.Sdk.Models.XfCredentials,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).progress'></a>

`progress` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[CliMessage](Xecrets.File.Sdk.Cli.CliMessage.md 'Xecrets.File.Sdk.Cli.CliMessage')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')

An [System.Action](https://docs.microsoft.com/en-us/dotnet/api/System.Action 'System.Action') delegate called with progress.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.IO.FileInfo](https://docs.microsoft.com/en-us/dotnet/api/System.IO.FileInfo 'System.IO.FileInfo')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
An enumeration of [System.IO.FileInfo](https://docs.microsoft.com/en-us/dotnet/api/System.IO.FileInfo 'System.IO.FileInfo') instances representing the decrypted files.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptKeyPairAsync(string,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfEncryptedKeyPair_)'></a>

## IXfApi.DecryptKeyPairAsync(string, IEnumerable<XfEncryptedKeyPair>) Method

Attempt to decrypt key pairs using the provided password. The first one possible to decrypt in the given  
enumerable will be returned.

```csharp
System.Threading.Tasks.Task<Xecrets.File.Sdk.Models.XfKeyPair?> DecryptKeyPairAsync(string password, System.Collections.Generic.IEnumerable<Xecrets.File.Sdk.Models.XfEncryptedKeyPair> encryptedKeyPairs);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptKeyPairAsync(string,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfEncryptedKeyPair_).password'></a>

`password` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The password to try to decrypt with.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptKeyPairAsync(string,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfEncryptedKeyPair_).encryptedKeyPairs'></a>

`encryptedKeyPairs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[XfEncryptedKeyPair](Xecrets.File.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.File.Sdk.Models.XfEncryptedKeyPair')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

A list of key pairs to try to decrypt.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[XfKeyPair](Xecrets.File.Sdk.Models.XfKeyPair.md 'Xecrets.File.Sdk.Models.XfKeyPair')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The first key pair possible to decrypt, or null if none could be decrypted.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptStream(Xecrets.File.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream)'></a>

## IXfApi.DecryptStream(XfCredentials, Stream, Stream) Method

Decrypt an arbitrary stream to another stream

```csharp
System.Threading.Tasks.Task<string> DecryptStream(Xecrets.File.Sdk.Models.XfCredentials credentials, System.IO.Stream cipherStream, System.IO.Stream clearStream);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptStream(Xecrets.File.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).credentials'></a>

`credentials` [XfCredentials](Xecrets.File.Sdk.Models.XfCredentials.md 'Xecrets.File.Sdk.Models.XfCredentials')

One or more passwords to encrypt with.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptStream(Xecrets.File.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).cipherStream'></a>

`cipherStream` [System.IO.Stream](https://docs.microsoft.com/en-us/dotnet/api/System.IO.Stream 'System.IO.Stream')

A stream of data to decrypt.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptStream(Xecrets.File.Sdk.Models.XfCredentials,System.IO.Stream,System.IO.Stream).clearStream'></a>

`clearStream` [System.IO.Stream](https://docs.microsoft.com/en-us/dotnet/api/System.IO.Stream 'System.IO.Stream')

A stream to write the clear text to.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') to wait for and the original file name as a string.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.File.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.File.Sdk.Cli.CliMessage_)'></a>

## IXfApi.DecryptToAndKeepFileAsync(XfCredentials, FileInfo, DirectoryInfo, Action<CliMessage>) Method

Decrypt a file using the provided credentials, to a specified directory, returning the resulting original  
filename and decrypted file represented by a [System.IO.FileInfo](https://docs.microsoft.com/en-us/dotnet/api/System.IO.FileInfo 'System.IO.FileInfo') instance.

```csharp
System.Threading.Tasks.Task<(System.IO.FileInfo? decrypted,string originalFileName)> DecryptToAndKeepFileAsync(Xecrets.File.Sdk.Models.XfCredentials credentials, System.IO.FileInfo file, System.IO.DirectoryInfo directory, System.Action<Xecrets.File.Sdk.Cli.CliMessage> progress);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.File.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).credentials'></a>

`credentials` [XfCredentials](Xecrets.File.Sdk.Models.XfCredentials.md 'Xecrets.File.Sdk.Models.XfCredentials')

One or more passwords to encrypt with.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.File.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).file'></a>

`file` [System.IO.FileInfo](https://docs.microsoft.com/en-us/dotnet/api/System.IO.FileInfo 'System.IO.FileInfo')

The file to decrypt.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.File.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).directory'></a>

`directory` [System.IO.DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/System.IO.DirectoryInfo 'System.IO.DirectoryInfo')

The location to decrypt to.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.DecryptToAndKeepFileAsync(Xecrets.File.Sdk.Models.XfCredentials,System.IO.FileInfo,System.IO.DirectoryInfo,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).progress'></a>

`progress` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[CliMessage](Xecrets.File.Sdk.Cli.CliMessage.md 'Xecrets.File.Sdk.Cli.CliMessage')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')

An [System.Action](https://docs.microsoft.com/en-us/dotnet/api/System.Action 'System.Action') delegate called with progress.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[System.IO.FileInfo](https://docs.microsoft.com/en-us/dotnet/api/System.IO.FileInfo 'System.IO.FileInfo')[,](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [System.IO.FileInfo](https://docs.microsoft.com/en-us/dotnet/api/System.IO.FileInfo 'System.IO.FileInfo') representing the result, and just the original file name from the encryped  
            filed.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.File.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_)'></a>

## IXfApi.EncryptFilesAsync(XfCredentials, bool, bool, IEnumerable<XfFilePair>, Action<CliMessage>) Method

Encrypt plain text files as encrypted files, keeping or wiping the originals.

```csharp
System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<System.IO.FileInfo>> EncryptFilesAsync(Xecrets.File.Sdk.Models.XfCredentials credentials, bool overwrite, bool wipe, System.Collections.Generic.IEnumerable<Xecrets.File.Sdk.Models.XfFilePair> pairs, System.Action<Xecrets.File.Sdk.Cli.CliMessage> progress);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.File.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).credentials'></a>

`credentials` [XfCredentials](Xecrets.File.Sdk.Models.XfCredentials.md 'Xecrets.File.Sdk.Models.XfCredentials')

The credentials to use when encrypting the files.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.File.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).overwrite'></a>

`overwrite` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

Set to 'false' if overwriting the target should be disallowed, and instead use an alternate non-colliding name.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.File.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).wipe'></a>

`wipe` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

Set to 'true' to also wipe the original plain text.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.File.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).pairs'></a>

`pairs` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[XfFilePair](Xecrets.File.Sdk.Models.XfFilePair.md 'Xecrets.File.Sdk.Models.XfFilePair')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

File name pairs, providing source and target names as well as original name to include in the encrypted file.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptFilesAsync(Xecrets.File.Sdk.Models.XfCredentials,bool,bool,System.Collections.Generic.IEnumerable_Xecrets.File.Sdk.Models.XfFilePair_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).progress'></a>

`progress` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[CliMessage](Xecrets.File.Sdk.Cli.CliMessage.md 'Xecrets.File.Sdk.Cli.CliMessage')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')

An [System.Action](https://docs.microsoft.com/en-us/dotnet/api/System.Action 'System.Action') delegate that will be called reporting progress of the operation.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.IO.FileInfo](https://docs.microsoft.com/en-us/dotnet/api/System.IO.FileInfo 'System.IO.FileInfo')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptKeyPairAsync(Xecrets.File.Sdk.Models.XfKeyPair)'></a>

## IXfApi.EncryptKeyPairAsync(XfKeyPair) Method

Encrypt a [XfKeyPair](Xecrets.File.Sdk.Models.XfKeyPair.md 'Xecrets.File.Sdk.Models.XfKeyPair') into a [XfEncryptedKeyPair](Xecrets.File.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.File.Sdk.Models.XfEncryptedKeyPair') using the provided password in the  
[XfKeyPair](Xecrets.File.Sdk.Models.XfKeyPair.md 'Xecrets.File.Sdk.Models.XfKeyPair') instance.

```csharp
System.Threading.Tasks.Task<Xecrets.File.Sdk.Models.XfEncryptedKeyPair> EncryptKeyPairAsync(Xecrets.File.Sdk.Models.XfKeyPair keyPair);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptKeyPairAsync(Xecrets.File.Sdk.Models.XfKeyPair).keyPair'></a>

`keyPair` [XfKeyPair](Xecrets.File.Sdk.Models.XfKeyPair.md 'Xecrets.File.Sdk.Models.XfKeyPair')

The key pair to encrypt.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[XfEncryptedKeyPair](Xecrets.File.Sdk.Models.XfEncryptedKeyPair.md 'Xecrets.File.Sdk.Models.XfEncryptedKeyPair')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The encrypted key pair as a blob.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptStream(Xecrets.File.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream)'></a>

## IXfApi.EncryptStream(XfCredentials, string, Stream, Stream) Method

Encrypt an arbitrary stream to another stream

```csharp
System.Threading.Tasks.Task EncryptStream(Xecrets.File.Sdk.Models.XfCredentials credentials, string originalFileName, System.IO.Stream clearStream, System.IO.Stream cipherStream);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptStream(Xecrets.File.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).credentials'></a>

`credentials` [XfCredentials](Xecrets.File.Sdk.Models.XfCredentials.md 'Xecrets.File.Sdk.Models.XfCredentials')

One or more passwords to encrypt with.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptStream(Xecrets.File.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).originalFileName'></a>

`originalFileName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The original file name to embed into the encrypted file stream

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptStream(Xecrets.File.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).clearStream'></a>

`clearStream` [System.IO.Stream](https://docs.microsoft.com/en-us/dotnet/api/System.IO.Stream 'System.IO.Stream')

A stream of data to encrypt.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.EncryptStream(Xecrets.File.Sdk.Models.XfCredentials,string,System.IO.Stream,System.IO.Stream).cipherStream'></a>

`cipherStream` [System.IO.Stream](https://docs.microsoft.com/en-us/dotnet/api/System.IO.Stream 'System.IO.Stream')

The stream to write the encrypted data to.

#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') to wait for.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version)'></a>

## IXfApi.IsSdkCompatibleWith(Version) Method

Determine if the consumer, using this version of the SDK and the provided [cliApiVersion](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version).cliApiVersion 'Xecrets.File.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version).cliApiVersion')  
version are compatible, i.e. if the caller having a CLI of the given version can use this version of the  
SDK.

```csharp
bool IsSdkCompatibleWith(System.Version cliApiVersion);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version).cliApiVersion'></a>

`cliApiVersion` [System.Version](https://docs.microsoft.com/en-us/dotnet/api/System.Version 'System.Version')

The version of the CLI API to check.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if this SDK can work with the provided CLI.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.StartMe(string)'></a>

## IXfApi.StartMe(string) Method

Get the start arguments necessary to start the program identified as [fileNameWithoutExtension](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi.StartMe(string).fileNameWithoutExtension 'Xecrets.File.Sdk.Abstractions.IXfApi.StartMe(string).fileNameWithoutExtension'), assuming that if we're started with "dotnet", this program should also be  
started with "dotnet" [fileNameWithoutExtension](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi.StartMe(string).fileNameWithoutExtension 'Xecrets.File.Sdk.Abstractions.IXfApi.StartMe(string).fileNameWithoutExtension').dll, otherwise it's an executable with  
".exe" or without depending on the current operating system.

```csharp
string[] StartMe(string fileNameWithoutExtension);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.StartMe(string).fileNameWithoutExtension'></a>

`fileNameWithoutExtension` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.WipeFilesAsync(System.Collections.Generic.IEnumerable_string_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_)'></a>

## IXfApi.WipeFilesAsync(IEnumerable<string>, Action<CliMessage>) Method

Wipe files.

```csharp
System.Threading.Tasks.Task WipeFilesAsync(System.Collections.Generic.IEnumerable<string> fullNames, System.Action<Xecrets.File.Sdk.Cli.CliMessage> progress);
```
#### Parameters

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.WipeFilesAsync(System.Collections.Generic.IEnumerable_string_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).fullNames'></a>

`fullNames` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

The list of full path names to wipe.

<a name='Xecrets.File.Sdk.Abstractions.IXfApi.WipeFilesAsync(System.Collections.Generic.IEnumerable_string_,System.Action_Xecrets.File.Sdk.Cli.CliMessage_).progress'></a>

`progress` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[CliMessage](Xecrets.File.Sdk.Cli.CliMessage.md 'Xecrets.File.Sdk.Cli.CliMessage')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')

An [System.Action](https://docs.microsoft.com/en-us/dotnet/api/System.Action 'System.Action') delegate called with progress.

#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A waitable task.