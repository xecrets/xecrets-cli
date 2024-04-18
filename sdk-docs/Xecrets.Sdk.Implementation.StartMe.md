#### [Xecrets.Sdk](index.md 'index')
### [Xecrets.Sdk.Implementation](Xecrets.Sdk.Implementation.md 'Xecrets.Sdk.Implementation')

## StartMe Class

```csharp
internal static class StartMe
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; StartMe
### Methods

<a name='Xecrets.Sdk.Implementation.StartMe.StartArgs()'></a>

## StartMe.StartArgs() Method

Get the arguments necessary to start this process, typically the path to a .exe as the first and  
only argument, or "dotnet" as the first argument and the path to a .NET DLL as the second.

```csharp
public static string[] StartArgs();
```

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='Xecrets.Sdk.Implementation.StartMe.StartArgsFor(string)'></a>

## StartMe.StartArgsFor(string) Method

Get the start arguments necessary to start the program identified as [fileNameWithoutExtension](Xecrets.Sdk.Implementation.StartMe.md#Xecrets.Sdk.Implementation.StartMe.StartArgsFor(string).fileNameWithoutExtension 'Xecrets.Sdk.Implementation.StartMe.StartArgsFor(string).fileNameWithoutExtension'), assuming  
that if we're started with "dotnet", this program should also be started with "dotnet" fileName.dll, otherwise it's an  
executable with ".exe" or without depending on the current operating system.

```csharp
public static string[] StartArgsFor(string fileNameWithoutExtension);
```
#### Parameters

<a name='Xecrets.Sdk.Implementation.StartMe.StartArgsFor(string).fileNameWithoutExtension'></a>

`fileNameWithoutExtension` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')