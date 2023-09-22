#### [Xecrets.File.Sdk](index.md 'index')
### [Xecrets.File.Sdk.Cli](Xecrets.File.Sdk.Cli.md 'Xecrets.File.Sdk.Cli')

## CliMessage Class

Log output and progress from the CLI tool.

```csharp
public class CliMessage
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CliMessage
### Properties

<a name='Xecrets.File.Sdk.Cli.CliMessage.CliApiVersion'></a>

## CliMessage.CliApiVersion Property

The API version of the CLI tool

```csharp
public string? CliApiVersion { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Email'></a>

## CliMessage.Email Property

The current email parameter in effect for this response

```csharp
public string? Email { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.File'></a>

## CliMessage.File Property

The current file parameter in effect for this response

```csharp
public string? File { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.From'></a>

## CliMessage.From Property

The current file from parameter in effect for this response

```csharp
public string? From { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Id'></a>

## CliMessage.Id Property

The current id value parameter in effect for this response

```csharp
public string? Id { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Item'></a>

## CliMessage.Item Property

The current item parameter in effect for this response

```csharp
public string? Item { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Message'></a>

## CliMessage.Message Property

The display message associated with the command response.

```csharp
public string? Message { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.OpCode'></a>

## CliMessage.OpCode Property

The current [Xecrets.File.Cli.Public.XfOpCode](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.File.Cli.Public.XfOpCode 'Xecrets.File.Cli.Public.XfOpCode') for this response.

```csharp
public int OpCode { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Xecrets.File.Sdk.Cli.CliMessage.OpCodeName'></a>

## CliMessage.OpCodeName Property

The name of the [Xecrets.File.Cli.Public.XfOpCode](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.File.Cli.Public.XfOpCode 'Xecrets.File.Cli.Public.XfOpCode') for this response.

```csharp
public string? OpCodeName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.OriginalFileName'></a>

## CliMessage.OriginalFileName Property

The original file name in effect for this response.

```csharp
public string? OriginalFileName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Percent'></a>

## CliMessage.Percent Property

The completion percentage in progress of this invocation of the CLI tool.

```csharp
public int Percent { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Platform'></a>

## CliMessage.Platform Property

The platform this CLI tool is built for and running on.

```csharp
public string? Platform { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.ProgramVersion'></a>

## CliMessage.ProgramVersion Property

The full program version of the CLI tool.

```csharp
public string? ProgramVersion { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Result'></a>

## CliMessage.Result Property

The full path name of the result of the operation of this response.

```csharp
public string? Result { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Status'></a>

## CliMessage.Status Property

The status of the current operation as an integer.

```csharp
public int Status { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Xecrets.File.Sdk.Cli.CliMessage.StatusName'></a>

## CliMessage.StatusName Property

The name of the status of the current operation as string.

```csharp
public string? StatusName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.To'></a>

## CliMessage.To Property

The full path name of the to parameter of the current operation.

```csharp
public string? To { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.CliMessage.TotalDone'></a>

## CliMessage.TotalDone Property

Number of bytes of the total that have been processed in progress.

```csharp
public long TotalDone { get; set; }
```

#### Property Value
[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')

<a name='Xecrets.File.Sdk.Cli.CliMessage.TotalPercent'></a>

## CliMessage.TotalPercent Property

The percentage of total work that has been processed in progress.

```csharp
public int TotalPercent { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Xecrets.File.Sdk.Cli.CliMessage.TotalWork'></a>

## CliMessage.TotalWork Property

The total work that is to be done by this CLI tool invocation.

```csharp
public long TotalWork { get; set; }
```

#### Property Value
[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')

<a name='Xecrets.File.Sdk.Cli.CliMessage.Utc'></a>

## CliMessage.Utc Property

The date and time of the log message, UTC.

```csharp
public System.DateTime Utc { get; set; }
```

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')