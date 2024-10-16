#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk](Xecrets.Sdk.md 'Xecrets.Sdk')

## XfApiFactory Class

Create an [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance.

```csharp
public class XfApiFactory :
Xecrets.Sdk.Abstractions.IXfApiFactory
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; XfApiFactory

Implements [IXfApiFactory](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApiFactory 'Xecrets.Sdk.Abstractions.IXfApiFactory')
### Constructors

<a name='Xecrets.Sdk.XfApiFactory.XfApiFactory(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler)'></a>

## XfApiFactory(string, bool, string, IScheduler, IScheduler) Constructor

Create an [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance.

```csharp
public XfApiFactory(string license, bool debugCli, string crashLogFullName, System.Reactive.Concurrency.IScheduler taskpoolScheduler, System.Reactive.Concurrency.IScheduler mainthreadScheduler);
```
#### Parameters

<a name='Xecrets.Sdk.XfApiFactory.XfApiFactory(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).license'></a>

`license` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A Xecrets license string to provide for a ready built version of XecretsCli.

<a name='Xecrets.Sdk.XfApiFactory.XfApiFactory(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).debugCli'></a>

`debugCli` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

Set to [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') to cause the command line to stop on a breakpoint and  
            enable attaching a debugger. (Windows only)

<a name='Xecrets.Sdk.XfApiFactory.XfApiFactory(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).crashLogFullName'></a>

`crashLogFullName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The full path where the command line can write a crash log on non-zero return  
            status.

<a name='Xecrets.Sdk.XfApiFactory.XfApiFactory(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).taskpoolScheduler'></a>

`taskpoolScheduler` [System.Reactive.Concurrency.IScheduler](https://docs.microsoft.com/en-us/dotnet/api/System.Reactive.Concurrency.IScheduler 'System.Reactive.Concurrency.IScheduler')

An [System.Reactive.Concurrency.IScheduler](https://docs.microsoft.com/en-us/dotnet/api/System.Reactive.Concurrency.IScheduler 'System.Reactive.Concurrency.IScheduler') instance to schedule work on the  
            taskpool.

<a name='Xecrets.Sdk.XfApiFactory.XfApiFactory(string,bool,string,System.Reactive.Concurrency.IScheduler,System.Reactive.Concurrency.IScheduler).mainthreadScheduler'></a>

`mainthreadScheduler` [System.Reactive.Concurrency.IScheduler](https://docs.microsoft.com/en-us/dotnet/api/System.Reactive.Concurrency.IScheduler 'System.Reactive.Concurrency.IScheduler')

An [System.Reactive.Concurrency.IScheduler](https://docs.microsoft.com/en-us/dotnet/api/System.Reactive.Concurrency.IScheduler 'System.Reactive.Concurrency.IScheduler') instance to schedule work on the main (GUI)  
            thread.
### Methods

<a name='Xecrets.Sdk.XfApiFactory.Create(System.Threading.CancellationToken)'></a>

## XfApiFactory.Create(CancellationToken) Method

Create an [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance.

```csharp
public Xecrets.Sdk.Abstractions.IXfApi Create(System.Threading.CancellationToken ct);
```
#### Parameters

<a name='Xecrets.Sdk.XfApiFactory.Create(System.Threading.CancellationToken).ct'></a>

`ct` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A cancellation token to cancel any long running operation.

Implements [Create(CancellationToken)](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApiFactory.Create(System.Threading.CancellationToken) 'Xecrets.Sdk.Abstractions.IXfApiFactory.Create(System.Threading.CancellationToken)')

#### Returns
[IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi')

<a name='Xecrets.Sdk.XfApiFactory.Safe()'></a>

## XfApiFactory.Safe() Method

Create an [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') instance with safe defaults.

```csharp
public static Xecrets.Sdk.Abstractions.IXfApi Safe();
```

#### Returns
[IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi')  
An instance of [IXfApi](Xecrets.Sdk.Abstractions.md#Xecrets.Sdk.Abstractions.IXfApi 'Xecrets.Sdk.Abstractions.IXfApi') ready to use.

### Remarks
Should normally not be used when using Axantum ready built command line tool binaries, as no license is  
provided. Another option to provide the license is to place it in a text file next to the executable.