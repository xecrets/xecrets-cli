#### [Xecrets.File.Sdk](index.md 'index')
### [Xecrets.File.Sdk.Models](Xecrets.File.Sdk.Models.md 'Xecrets.File.Sdk.Models')

## XfFilePair Class

A plain text/cipher text file pair

```csharp
public class XfFilePair
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfFilePair
### Constructors

<a name='Xecrets.File.Sdk.Models.XfFilePair.XfFilePair(string,string)'></a>

## XfFilePair(string, string) Constructor

Create a new instance.

```csharp
public XfFilePair(string sourceFullName, string targetFullName);
```
#### Parameters

<a name='Xecrets.File.Sdk.Models.XfFilePair.XfFilePair(string,string).sourceFullName'></a>

`sourceFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the source, cipher text.

<a name='Xecrets.File.Sdk.Models.XfFilePair.XfFilePair(string,string).targetFullName'></a>

`targetFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the target, plain text.

<a name='Xecrets.File.Sdk.Models.XfFilePair.XfFilePair(string,string,string)'></a>

## XfFilePair(string, string, string) Constructor

Create a new instance.

```csharp
public XfFilePair(string sourceFullName, string targetFullName, string originalFileName);
```
#### Parameters

<a name='Xecrets.File.Sdk.Models.XfFilePair.XfFilePair(string,string,string).sourceFullName'></a>

`sourceFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the source, plain text.

<a name='Xecrets.File.Sdk.Models.XfFilePair.XfFilePair(string,string,string).targetFullName'></a>

`targetFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the target, cipher text.

<a name='Xecrets.File.Sdk.Models.XfFilePair.XfFilePair(string,string,string).originalFileName'></a>

`originalFileName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The original file name to use when encrypting.
### Properties

<a name='Xecrets.File.Sdk.Models.XfFilePair.OriginalFileName'></a>

## XfFilePair.OriginalFileName Property

The original file name to use when encrypting.

```csharp
public string OriginalFileName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Models.XfFilePair.SourceFullName'></a>

## XfFilePair.SourceFullName Property

The full path and name of the source, plain text or cipher text.

```csharp
public string SourceFullName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Models.XfFilePair.TargetFullName'></a>

## XfFilePair.TargetFullName Property

The full path and name of the target, plain text or cipher text.

```csharp
public string TargetFullName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')