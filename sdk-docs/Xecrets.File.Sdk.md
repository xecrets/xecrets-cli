#### [Xecrets.File.Sdk](index.md 'index')

## Xecrets.File.Sdk Namespace

| Classes | |
| :--- | :--- |
| [SourceGenerationContext](Xecrets.File.Sdk.SourceGenerationContext.md 'Xecrets.File.Sdk.SourceGenerationContext') | |
| [XfApiFactory](Xecrets.File.Sdk.XfApiFactory.md 'Xecrets.File.Sdk.XfApiFactory') | Static factory methods to create an [IXfApi](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi 'Xecrets.File.Sdk.Abstractions.IXfApi') implementation. |
| [XfException](Xecrets.File.Sdk.XfException.md 'Xecrets.File.Sdk.XfException') | The exception type thrown when something goes wrong calling the command line tool. In addition to the base class<br/>[System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception') there is an [ExitCode](Xecrets.File.Sdk.XfException.md#Xecrets.File.Sdk.XfException.ExitCode 'Xecrets.File.Sdk.XfException.ExitCode') property containing the actual exit code from the<br/>tool. Exit codes are [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32') but they are mapped to enum [Xecrets.File.Cli.Public.XfStatusCode](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.File.Cli.Public.XfStatusCode 'Xecrets.File.Cli.Public.XfStatusCode') here. To ensure<br/>that the mapping is correct, call [IsSdkCompatibleWith(Version)](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version) 'Xecrets.File.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version)') ./> |
| [XfExtensions](Xecrets.File.Sdk.XfExtensions.md 'Xecrets.File.Sdk.XfExtensions') | Useful extension methods for file names |
| [XfSdkVersion](Xecrets.File.Sdk.XfSdkVersion.md 'Xecrets.File.Sdk.XfSdkVersion') | Helper to determine if the current SDK version is compatible with a given command line tool API version. |
