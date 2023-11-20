#### [Xecrets.File.Sdk](index.md 'index')
### [Xecrets.File.Sdk.Cli.Models](Xecrets.File.Sdk.Cli.Models.md 'Xecrets.File.Sdk.Cli.Models')

## CliToolInformation Class

Information about the command line tool found and used by the SDK

```csharp
public class CliToolInformation
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CliToolInformation

### Remarks
Create a new instance.
### Constructors

<a name='Xecrets.File.Sdk.Cli.Models.CliToolInformation.CliToolInformation(string,string,string)'></a>

## CliToolInformation(string, string, string) Constructor

Information about the command line tool found and used by the SDK

```csharp
public CliToolInformation(string apiVersion, string splash, string fullName);
```
#### Parameters

<a name='Xecrets.File.Sdk.Cli.Models.CliToolInformation.CliToolInformation(string,string,string).apiVersion'></a>

`apiVersion` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

An API version string, for example "1.0" (not the tool version).

<a name='Xecrets.File.Sdk.Cli.Models.CliToolInformation.CliToolInformation(string,string,string).splash'></a>

`splash` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The "splash" text displayed by the tool, will include the tool version.

<a name='Xecrets.File.Sdk.Cli.Models.CliToolInformation.CliToolInformation(string,string,string).fullName'></a>

`fullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path and name of the tool found and used.

### Remarks
Create a new instance.
### Properties

<a name='Xecrets.File.Sdk.Cli.Models.CliToolInformation.CliToolApiVersion'></a>

## CliToolInformation.CliToolApiVersion Property

An API version string, for example "1.0" (not the tool version).

```csharp
public string CliToolApiVersion { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.Models.CliToolInformation.CliToolFullName'></a>

## CliToolInformation.CliToolFullName Property

The full path and name of the tool found and used.

```csharp
public string CliToolFullName { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Xecrets.File.Sdk.Cli.Models.CliToolInformation.CliToolSplash'></a>

## CliToolInformation.CliToolSplash Property

The "splash" text displayed by the tool, will include the tool version.

```csharp
public string CliToolSplash { get; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')