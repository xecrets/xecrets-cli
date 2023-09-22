#### [Xecrets.File.Sdk](index.md 'index')
### [Xecrets.File.Sdk](Xecrets.File.Sdk.md 'Xecrets.File.Sdk')

## XfException Class

The exception type thrown when something goes wrong calling the CLI tool. In addition to the base class [System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception') there is an [ExitCode](Xecrets.File.Sdk.XfException.md#Xecrets.File.Sdk.XfException.ExitCode 'Xecrets.File.Sdk.XfException.ExitCode') property containing the actual exit code from the tool.  
Exit codes are [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32') but they are mapped to enum [Xecrets.File.Cli.Public.XfStatusCode](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.File.Cli.Public.XfStatusCode 'Xecrets.File.Cli.Public.XfStatusCode') here. To ensure that the  
mapping is correct, call [IsSdkCompatibleWith(Version)](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version) 'Xecrets.File.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version)') ./>

```csharp
public class XfException : System.Exception
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception') &#129106; XfException
### Constructors

<a name='Xecrets.File.Sdk.XfException.XfException(int,string)'></a>

## XfException(int, string) Constructor

Initializes a new instance of the [XfException](Xecrets.File.Sdk.XfException.md 'Xecrets.File.Sdk.XfException') class with a specified message and exit code.

```csharp
public XfException(int exitCode, string message);
```
#### Parameters

<a name='Xecrets.File.Sdk.XfException.XfException(int,string).exitCode'></a>

`exitCode` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

The CLI exit code.

<a name='Xecrets.File.Sdk.XfException.XfException(int,string).message'></a>

`message` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The message.

<a name='Xecrets.File.Sdk.XfException.XfException(int,string,System.Exception)'></a>

## XfException(int, string, Exception) Constructor

Initializes a new instance of the [XfException](Xecrets.File.Sdk.XfException.md 'Xecrets.File.Sdk.XfException') class with a specified message, exit code and inner exception.

```csharp
public XfException(int exitCode, string message, System.Exception ex);
```
#### Parameters

<a name='Xecrets.File.Sdk.XfException.XfException(int,string,System.Exception).exitCode'></a>

`exitCode` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

The CLI exit code.

<a name='Xecrets.File.Sdk.XfException.XfException(int,string,System.Exception).message'></a>

`message` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The message.

<a name='Xecrets.File.Sdk.XfException.XfException(int,string,System.Exception).ex'></a>

`ex` [System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception')

The inner exception.

<a name='Xecrets.File.Sdk.XfException.XfException(string)'></a>

## XfException(string) Constructor

Initializes a new instance of the [XfException](Xecrets.File.Sdk.XfException.md 'Xecrets.File.Sdk.XfException') class with a specified message.

```csharp
public XfException(string message);
```
#### Parameters

<a name='Xecrets.File.Sdk.XfException.XfException(string).message'></a>

`message` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The message.

<a name='Xecrets.File.Sdk.XfException.XfException(string,System.Exception)'></a>

## XfException(string, Exception) Constructor

Initializes a new instance of the [XfException](Xecrets.File.Sdk.XfException.md 'Xecrets.File.Sdk.XfException') class with a specified message and inner exception.

```csharp
public XfException(string message, System.Exception ex);
```
#### Parameters

<a name='Xecrets.File.Sdk.XfException.XfException(string,System.Exception).message'></a>

`message` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The message.

<a name='Xecrets.File.Sdk.XfException.XfException(string,System.Exception).ex'></a>

`ex` [System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception')

The inner exception.
### Properties

<a name='Xecrets.File.Sdk.XfException.ExitCode'></a>

## XfException.ExitCode Property

The exit code from the CLI.

```csharp
public int ExitCode { get; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')
### Methods

<a name='Xecrets.File.Sdk.XfException.ToString()'></a>

## XfException.ToString() Method

Creates and returns a string representation of the current exception.

```csharp
public override string ToString();
```

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')