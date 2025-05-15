#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models')

## XfFileResult Class

Instatiate a new instance of XfFileResult

```csharp
public class XfFileResult
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfFileResult

### Remarks
If the operation was not successful, the reason for the failure should be implied by the context.
### Constructors

<a name='Xecrets.Sdk.Models.XfFileResult.XfFileResult(string,string,bool)'></a>

## XfFileResult(string, string, bool) Constructor

Instatiate a new instance of XfFileResult

```csharp
public XfFileResult(string? fileName, string? originalFileName, bool success);
```
#### Parameters

<a name='Xecrets.Sdk.Models.XfFileResult.XfFileResult(string,string,bool).fileName'></a>

`fileName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The subject file name.

<a name='Xecrets.Sdk.Models.XfFileResult.XfFileResult(string,string,bool).originalFileName'></a>

`originalFileName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The original file name, when relevant after decryption

<a name='Xecrets.Sdk.Models.XfFileResult.XfFileResult(string,string,bool).success'></a>

`success` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

A [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean') indicating if the operation on the file was successful or not.

### Remarks
If the operation was not successful, the reason for the failure should be implied by the context.
### Properties

<a name='Xecrets.Sdk.Models.XfFileResult.FileName'></a>

## XfFileResult.FileName Property

The subject file name.

```csharp
public string FileName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfFileResult.OriginalFileName'></a>

## XfFileResult.OriginalFileName Property

The original file name, when relevant after decryption

```csharp
public string OriginalFileName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Models.XfFileResult.Success'></a>

## XfFileResult.Success Property

A [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean') indicating if the operation on the file was successful or not.

```csharp
public bool Success { get; }
```

#### Property Value
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Xecrets.Sdk.Models.XfFileResult.Value'></a>

## XfFileResult.Value Property

The file content, if any, as a byte array.

```csharp
public byte[] Value { get; set; }
```

#### Property Value
[System.Byte](https://docs.microsoft.com/en-us/dotnet/api/System.Byte 'System.Byte')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')