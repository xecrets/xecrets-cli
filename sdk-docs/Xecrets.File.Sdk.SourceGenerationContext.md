#### [Xecrets.File.Sdk](index.md 'index')
### [Xecrets.File.Sdk](Xecrets.File.Sdk.md 'Xecrets.File.Sdk')

## SourceGenerationContext Class

```csharp
internal class SourceGenerationContext : System.Text.Json.Serialization.JsonSerializerContext,
System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.Text.Json.Serialization.JsonSerializerContext](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonSerializerContext 'System.Text.Json.Serialization.JsonSerializerContext') &#129106; SourceGenerationContext

Implements [System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver 'System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver')
### Constructors

<a name='Xecrets.File.Sdk.SourceGenerationContext.SourceGenerationContext()'></a>

## SourceGenerationContext() Constructor

Initializes a new instance of the [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') class.

```csharp
public SourceGenerationContext();
```
### Properties

<a name='Xecrets.File.Sdk.SourceGenerationContext.CliIdentity'></a>

## SourceGenerationContext.CliIdentity Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<Xecrets.File.Sdk.Cli.Models.CliIdentity> CliIdentity { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[Xecrets.File.Sdk.Cli.Models.CliIdentity](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.File.Sdk.Cli.Models.CliIdentity 'Xecrets.File.Sdk.Cli.Models.CliIdentity')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')

<a name='Xecrets.File.Sdk.SourceGenerationContext.CliMessage'></a>

## SourceGenerationContext.CliMessage Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<Xecrets.File.Sdk.Cli.CliMessage> CliMessage { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[CliMessage](Xecrets.File.Sdk.Cli.CliMessage.md 'Xecrets.File.Sdk.Cli.CliMessage')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')

<a name='Xecrets.File.Sdk.SourceGenerationContext.DateTime'></a>

## SourceGenerationContext.DateTime Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<System.DateTime> DateTime { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')

<a name='Xecrets.File.Sdk.SourceGenerationContext.Default'></a>

## SourceGenerationContext.Default Property

The default [System.Text.Json.Serialization.JsonSerializerContext](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonSerializerContext 'System.Text.Json.Serialization.JsonSerializerContext') associated with a default [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions') instance.

```csharp
public static Xecrets.File.Sdk.SourceGenerationContext Default { get; }
```

#### Property Value
[SourceGenerationContext](Xecrets.File.Sdk.SourceGenerationContext.md 'Xecrets.File.Sdk.SourceGenerationContext')

<a name='Xecrets.File.Sdk.SourceGenerationContext.GeneratedSerializerOptions'></a>

## SourceGenerationContext.GeneratedSerializerOptions Property

The source-generated options associated with this context.

```csharp
protected override System.Text.Json.JsonSerializerOptions? GeneratedSerializerOptions { get; }
```

#### Property Value
[System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

<a name='Xecrets.File.Sdk.SourceGenerationContext.Int32'></a>

## SourceGenerationContext.Int32 Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<int> Int32 { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')

<a name='Xecrets.File.Sdk.SourceGenerationContext.Int64'></a>

## SourceGenerationContext.Int64 Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<long> Int64 { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')

<a name='Xecrets.File.Sdk.SourceGenerationContext.Key'></a>

## SourceGenerationContext.Key Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<Xecrets.File.Sdk.Cli.Models.CliIdentity.KeyPair.Key> Key { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[Xecrets.File.Sdk.Cli.Models.CliIdentity.KeyPair.Key](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.File.Sdk.Cli.Models.CliIdentity.KeyPair.Key 'Xecrets.File.Sdk.Cli.Models.CliIdentity.KeyPair.Key')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')

<a name='Xecrets.File.Sdk.SourceGenerationContext.KeyPair'></a>

## SourceGenerationContext.KeyPair Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<Xecrets.File.Sdk.Cli.Models.CliIdentity.KeyPair> KeyPair { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[Xecrets.File.Sdk.Cli.Models.CliIdentity.KeyPair](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.File.Sdk.Cli.Models.CliIdentity.KeyPair 'Xecrets.File.Sdk.Cli.Models.CliIdentity.KeyPair')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')

<a name='Xecrets.File.Sdk.SourceGenerationContext.String'></a>

## SourceGenerationContext.String Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<string> String { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')

<a name='Xecrets.File.Sdk.SourceGenerationContext.UserEmail'></a>

## SourceGenerationContext.UserEmail Property

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public System.Text.Json.Serialization.Metadata.JsonTypeInfo<Xecrets.File.Sdk.Cli.Models.CliIdentity.UserEmail> UserEmail { get; }
```

#### Property Value
[System.Text.Json.Serialization.Metadata.JsonTypeInfo&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')[Xecrets.File.Sdk.Cli.Models.CliIdentity.UserEmail](https://docs.microsoft.com/en-us/dotnet/api/Xecrets.File.Sdk.Cli.Models.CliIdentity.UserEmail 'Xecrets.File.Sdk.Cli.Models.CliIdentity.UserEmail')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.Metadata.JsonTypeInfo-1 'System.Text.Json.Serialization.Metadata.JsonTypeInfo`1')