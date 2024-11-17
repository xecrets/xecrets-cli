#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Cli](Xecrets.Sdk.Cli.md 'Xecrets.Sdk.Cli')

## CliMessage Class

Log output and progress from the command line tool.

```csharp
public class CliMessage
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CliMessage
### Properties

<a name='Xecrets.Sdk.Cli.CliMessage.Arg1'></a>

## CliMessage.Arg1 Property

The current first argument in effect for this response, typically an 'email', 'file' or 'from' parameter.

```csharp
public string? Arg1 { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.Arg2'></a>

## CliMessage.Arg2 Property

The current second argument in effect for this response, typically a 'to' parameter.

```csharp
public string? Arg2 { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.CliApiVersion'></a>

## CliMessage.CliApiVersion Property

The API version of the command line tool

```csharp
public string? CliApiVersion { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.Display'></a>

## CliMessage.Display Property

The current display parameter in effect for this response

```csharp
public string? Display { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.Id'></a>

## CliMessage.Id Property

The current id value parameter in effect for this response

```csharp
public string? Id { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.Message'></a>

## CliMessage.Message Property

The display message associated with the command response.

```csharp
public string? Message { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.OpCode'></a>

## CliMessage.OpCode Property

The current [Xecrets.Sdk.Cli.XfOpCode](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.Sdk.Cli.XfOpCode 'Xecrets.Sdk.Cli.XfOpCode') for this response.

```csharp
public int OpCode { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Xecrets.Sdk.Cli.CliMessage.OpCodeName'></a>

## CliMessage.OpCodeName Property

The name of the [Xecrets.Sdk.Cli.XfOpCode](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.Sdk.Cli.XfOpCode 'Xecrets.Sdk.Cli.XfOpCode') for this response.

```csharp
public string? OpCodeName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.OriginalFileName'></a>

## CliMessage.OriginalFileName Property

The original file name in effect for this response.

```csharp
public string? OriginalFileName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.Percent'></a>

## CliMessage.Percent Property

The completion percentage in progress of this invocation of the command line tool.

```csharp
public int Percent { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Xecrets.Sdk.Cli.CliMessage.Platform'></a>

## CliMessage.Platform Property

The platform this command line tool is built for and running on.

```csharp
public string? Platform { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.ProgramVersion'></a>

## CliMessage.ProgramVersion Property

The full program version of the command line tool.

```csharp
public string? ProgramVersion { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.Result'></a>

## CliMessage.Result Property

The full path name of the result of the operation of this response.

```csharp
public string? Result { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.Status'></a>

## CliMessage.Status Property

The status of the current operation as an integer.

```csharp
public int Status { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Xecrets.Sdk.Cli.CliMessage.StatusName'></a>

## CliMessage.StatusName Property

The name of the status of the current operation as string.

```csharp
public string? StatusName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.Sdk.Cli.CliMessage.TotalDone'></a>

## CliMessage.TotalDone Property

Number of bytes of the total that have been processed in progress.

```csharp
public long TotalDone { get; set; }
```

#### Property Value
[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')

<a name='Xecrets.Sdk.Cli.CliMessage.TotalPercent'></a>

## CliMessage.TotalPercent Property

The percentage of total work that has been processed in progress.

```csharp
public int TotalPercent { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Xecrets.Sdk.Cli.CliMessage.TotalWork'></a>

## CliMessage.TotalWork Property

The total work that is to be done by this command line tool invocation.

```csharp
public long TotalWork { get; set; }
```

#### Property Value
[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')

<a name='Xecrets.Sdk.Cli.CliMessage.Utc'></a>

## CliMessage.Utc Property

The date and time of the log message, UTC.

```csharp
public System.DateTime Utc { get; set; }
```

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')