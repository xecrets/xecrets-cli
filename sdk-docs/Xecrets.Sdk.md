#### [Xecrets.Sdk](index.md 'index')

## Xecrets.Sdk Namespace

| Classes | |
| :--- | :--- |
| [XfApiFactory](Xecrets.Sdk.XfApiFactory.md 'Xecrets.Sdk.XfApiFactory') | Create an [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance. |
| [XfException](Xecrets.Sdk.XfException.md 'Xecrets.Sdk.XfException') | The exception type thrown when something goes wrong calling the command line tool. In addition to the base class<br/>[System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception') there is an [ExitCode](Xecrets.Sdk.XfException.md#Xecrets.Sdk.XfException.ExitCode 'Xecrets.Sdk.XfException.ExitCode') property containing the actual exit code from the<br/>tool. Exit codes are [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32') but they are mapped to enum [Xecrets.Cli.Public.XfStatusCode](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.Cli.Public.XfStatusCode 'Xecrets.Cli.Public.XfStatusCode') here. To ensure<br/>that the mapping is correct, call [IsSdkCompatibleWith(Version)](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version) 'Xecrets.Sdk.Abstractions.IXfApi.IsSdkCompatibleWith(System.Version)') ./> |
| [XfExtensions](Xecrets.Sdk.XfExtensions.md 'Xecrets.Sdk.XfExtensions') | Useful extension methods for file names |
| [XfSdkVersion](Xecrets.Sdk.XfSdkVersion.md 'Xecrets.Sdk.XfSdkVersion') | Helper to determine if the current SDK version is compatible with a given command line tool API version. |
