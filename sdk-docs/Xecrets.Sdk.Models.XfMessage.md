#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Models](Xecrets.Sdk.Models.md 'Xecrets.Sdk.Models')

## XfMessage Class

Log output and progress from the command line tool.

```csharp
public class XfMessage
```

Inheritance [System.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System.Object') &#129106; XfMessage
### Properties

<a name='Xecrets.Sdk.Models.XfMessage.Arg1'></a>

## XfMessage.Arg1 Property

The current first argument in effect for this response, typically an 'email', 'file' or 'from' parameter.

```csharp
public string? Arg1 { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.Arg2'></a>

## XfMessage.Arg2 Property

The current second argument in effect for this response, typically a 'to' parameter.

```csharp
public string? Arg2 { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.CliApiVersion'></a>

## XfMessage.CliApiVersion Property

The API version of the command line tool

```csharp
public string? CliApiVersion { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.Display'></a>

## XfMessage.Display Property

The current display parameter in effect for this response

```csharp
public string? Display { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.Id'></a>

## XfMessage.Id Property

The current id value parameter in effect for this response

```csharp
public string? Id { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.Message'></a>

## XfMessage.Message Property

The display message associated with the command response.

```csharp
public string? Message { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.OpCode'></a>

## XfMessage.OpCode Property

The current [Xecrets.Sdk.Models.XfOpCode](https://learn.microsoft.com/en-us/dotnet/api/xecrets.sdk.models.xfopcode 'Xecrets.Sdk.Models.XfOpCode') for this response.

```csharp
public int OpCode { get; set; }
```

#### Property Value
[System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System.Int32')

<a name='Xecrets.Sdk.Models.XfMessage.OpCodeName'></a>

## XfMessage.OpCodeName Property

The name of the [Xecrets.Sdk.Models.XfOpCode](https://learn.microsoft.com/en-us/dotnet/api/xecrets.sdk.models.xfopcode 'Xecrets.Sdk.Models.XfOpCode') for this response.

```csharp
public string? OpCodeName { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.OriginalFileName'></a>

## XfMessage.OriginalFileName Property

The original file name in effect for this response.

```csharp
public string? OriginalFileName { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.Percent'></a>

## XfMessage.Percent Property

The completion percentage in progress of this invocation of the command line tool.

```csharp
public int Percent { get; set; }
```

#### Property Value
[System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System.Int32')

<a name='Xecrets.Sdk.Models.XfMessage.Platform'></a>

## XfMessage.Platform Property

The platform this command line tool is built for and running on.

```csharp
public string? Platform { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.ProgramVersion'></a>

## XfMessage.ProgramVersion Property

The full program version of the command line tool.

```csharp
public string? ProgramVersion { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.Result'></a>

## XfMessage.Result Property

The full path name of the result of the operation of this response.

```csharp
public string? Result { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.Status'></a>

## XfMessage.Status Property

The status of the current operation as an integer.

```csharp
public int Status { get; set; }
```

#### Property Value
[System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System.Int32')

<a name='Xecrets.Sdk.Models.XfMessage.StatusName'></a>

## XfMessage.StatusName Property

The name of the status of the current operation as string.

```csharp
public string? StatusName { get; set; }
```

#### Property Value
[System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System.String')

<a name='Xecrets.Sdk.Models.XfMessage.SubStatus'></a>

## XfMessage.SubStatus Property

The sub status of the current operation as an integer.

```csharp
public int SubStatus { get; set; }
```

#### Property Value
[System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System.Int32')

<a name='Xecrets.Sdk.Models.XfMessage.TotalDone'></a>

## XfMessage.TotalDone Property

Number of bytes of the total that have been processed in progress.

```csharp
public long TotalDone { get; set; }
```

#### Property Value
[System.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System.Int64')

<a name='Xecrets.Sdk.Models.XfMessage.TotalPercent'></a>

## XfMessage.TotalPercent Property

The percentage of total work that has been processed in progress.

```csharp
public int TotalPercent { get; set; }
```

#### Property Value
[System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System.Int32')

<a name='Xecrets.Sdk.Models.XfMessage.TotalWork'></a>

## XfMessage.TotalWork Property

The total work that is to be done by this command line tool invocation.

```csharp
public long TotalWork { get; set; }
```

#### Property Value
[System.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System.Int64')

<a name='Xecrets.Sdk.Models.XfMessage.Utc'></a>

## XfMessage.Utc Property

The date and time of the log message, UTC.

```csharp
public System.DateTime Utc { get; set; }
```

#### Property Value
[System.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System.DateTime')