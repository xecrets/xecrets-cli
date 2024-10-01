#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models')

## XfFilePair Class

A plain text/cipher text file pair

```csharp
public class XfFilePair
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfFilePair

### Remarks
Create a new instance.
### Constructors

<a name='Xecrets.Sdk.Models.XfFilePair.XfFilePair(string,string)'></a>

## XfFilePair(string, string) Constructor

Create a new instance.

```csharp
public XfFilePair(string sourceFullName, string targetFullName);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfFilePair.XfFilePair(string,string).sourceFullName'></a>

`sourceFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the source, cipher text.

<a name='Xecrets.Sdk.Models.XfFilePair.XfFilePair(string,string).targetFullName'></a>

`targetFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the target, plain text.

<a name='Xecrets.Sdk.Models.XfFilePair.XfFilePair(string,string,string,bool)'></a>

## XfFilePair(string, string, string, bool) Constructor

A plain text/cipher text file pair

```csharp
public XfFilePair(string sourceFullName, string targetFullName, string originalFileName, bool compressBeforeEncrypt);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfFilePair.XfFilePair(string,string,string,bool).sourceFullName'></a>

`sourceFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the source, plain text.

<a name='Xecrets.Sdk.Models.XfFilePair.XfFilePair(string,string,string,bool).targetFullName'></a>

`targetFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the target, cipher text.

<a name='Xecrets.Sdk.Models.XfFilePair.XfFilePair(string,string,string,bool).originalFileName'></a>

`originalFileName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The original file name to use when encrypting.

<a name='Xecrets.Sdk.Models.XfFilePair.XfFilePair(string,string,string,bool).compressBeforeEncrypt'></a>

`compressBeforeEncrypt` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

True if the file should be compressed before encryption.

### Remarks
Create a new instance.
### Properties

<a name='Xecrets.Sdk.Models.XfFilePair.CompressBeforeEncrypt'></a>

## XfFilePair.CompressBeforeEncrypt Property

True if the file should be compressed before encryption.

```csharp
public bool CompressBeforeEncrypt { get; set; }
```

#### Property Value
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Xecrets.Sdk.Models.XfFilePair.OriginalFileName'></a>

## XfFilePair.OriginalFileName Property

The original file name to use when encrypting.

```csharp
public string OriginalFileName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfFilePair.SourceFullName'></a>

## XfFilePair.SourceFullName Property

The full path and name of the source, plain text or cipher text.

```csharp
public string SourceFullName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfFilePair.SourceLength'></a>

## XfFilePair.SourceLength Property

The length in bytes of the source file.

```csharp
public long SourceLength { get; }
```

#### Property Value
[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')

<a name='Xecrets.Sdk.Models.XfFilePair.TargetFullName'></a>

## XfFilePair.TargetFullName Property

The full path and name of the target, plain text or cipher text.

```csharp
public string TargetFullName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')