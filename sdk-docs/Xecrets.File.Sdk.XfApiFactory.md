#### [Xecrets.File.Sdk](index.md 'index')
### [Xecrets.File.Sdk](Xecrets.File.Sdk.md 'Xecrets.File.Sdk')

## XfApiFactory Class

Static factory methods to create an [IXfApi](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi 'Xecrets.File.Sdk.Abstractions.IXfApi') implementation.

```csharp
public static class XfApiFactory
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfApiFactory
### Methods

<a name='Xecrets.File.Sdk.XfApiFactory.Create()'></a>

## XfApiFactory.Create() Method

Create an [IXfApi](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi 'Xecrets.File.Sdk.Abstractions.IXfApi') instance with defaults.

```csharp
public static Xecrets.File.Sdk.Abstractions.IXfApi Create();
```

#### Returns
[IXfApi](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi 'Xecrets.File.Sdk.Abstractions.IXfApi')  
An instance of [IXfApi](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi 'Xecrets.File.Sdk.Abstractions.IXfApi') ready to use.

### Remarks
Should normally not be used when using Axantum ready built CLI tools, as no license is provided. Another  
option to provide the license is to place it in a text file next to the executable.

<a name='Xecrets.File.Sdk.XfApiFactory.Create(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler)'></a>

## XfApiFactory.Create(string, bool, string, IScheduler, IScheduler) Method

Create an [IXfApi](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi 'Xecrets.File.Sdk.Abstractions.IXfApi') instance with all parameters explicitly defined.

```csharp
public static Xecrets.File.Sdk.Abstractions.IXfApi Create(string license, bool debugCli, string crashLogFullName, System.Reactive.Concurrency.IScheduler taskpoolScheduler, System.Reactive.Concurrency.IScheduler mainthreadScheduler);
```
#### Parameters

<a name='Xecrets.File.Sdk.XfApiFactory.Create(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).license'></a>

`license` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A Xecrets File license string to provide for a ready built version of  
            XecretsFileCli.

<a name='Xecrets.File.Sdk.XfApiFactory.Create(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).debugCli'></a>

`debugCli` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

Set to [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') to cause the CLI to stop on a breakpoint and enable  
            attaching a debugger. (Windows only)

<a name='Xecrets.File.Sdk.XfApiFactory.Create(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).crashLogFullName'></a>

`crashLogFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path where the CLI can write a crash log on non-zero return status.

<a name='Xecrets.File.Sdk.XfApiFactory.Create(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).taskpoolScheduler'></a>

`taskpoolScheduler` [System.Reactive.Concurrency.IScheduler](https://docs.microsoft.com/en-us/dotnet/api/System.Reactive.Concurrency.IScheduler 'System.Reactive.Concurrency.IScheduler')

An [System.Reactive.Concurrency.IScheduler](https://docs.microsoft.com/en-us/dotnet/api/System.Reactive.Concurrency.IScheduler 'System.Reactive.Concurrency.IScheduler') instance to schedule work on the  
            taskpool.

<a name='Xecrets.File.Sdk.XfApiFactory.Create(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).mainthreadScheduler'></a>

`mainthreadScheduler` [System.Reactive.Concurrency.IScheduler](https://docs.microsoft.com/en-us/dotnet/api/System.Reactive.Concurrency.IScheduler 'System.Reactive.Concurrency.IScheduler')

An [System.Reactive.Concurrency.IScheduler](https://docs.microsoft.com/en-us/dotnet/api/System.Reactive.Concurrency.IScheduler 'System.Reactive.Concurrency.IScheduler') instance to schedule work on the main (GUI)  
            thread.

#### Returns
[IXfApi](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi 'Xecrets.File.Sdk.Abstractions.IXfApi')  
An instance of [IXfApi](Xecrets.File.Sdk.Abstractions.md#Xecrets.File.Sdk.Abstractions.IXfApi 'Xecrets.File.Sdk.Abstractions.IXfApi') ready to use.