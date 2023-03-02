# DTDLParserResolveSample

This sample loads an interface located in a folder structure following the [DMR Convention](https://github.com/Azure/iot-plugandplay-models-tools/wiki/Resolution-Convention), and enumerates the DTDL elements.

The `dtmi:com:example;1` interface uses an additional interface `dtmi:com:example:base;1`. 

## Resolving extenal references

To resolve external dependencies, this sample uses the [Azure.IoT.ModelsRepository](https://www.nuget.org/packages/Azure.IoT.ModelsRepository) package  to resolve interfaces from a local folder. The parser is configured with the `ParserDtmiResolverAsync` provided in the `ModelsRepositoryClientExtensions`:

```cs
public static async IAsyncEnumerable<string> ParserDtmiResolverAsync(
    this ModelsRepositoryClient client, IReadOnlyCollection<Dtmi> dtmis,
    [EnumeratorCancellation] CancellationToken ct = default)
{
    foreach (var dtmi in dtmis.Select(s => s.AbsoluteUri))
    {
        var result = await client.GetModelAsync(dtmi, ModelDependencyResolution.Disabled, ct);
        yield return result.Content[dtmi];
    }
}
``` 

And used from the `ModelResolver` class:

```cs
internal class ModelResolver {
    public static async Task<DTInterfaceInfo> LoadModelAsync(string dtmi, string dmrBasePath) {
        var dmrClient = new ModelsRepositoryClient(new Uri(dmrBasePath));
        var parser = new ModelParser(new ParsingOptions() {
            DtmiResolverAsync = dmrClient.ParserDtmiResolverAsync
        });
        Console.WriteLine($"Parser version: {parser.GetType().Assembly.FullName}\n Resolving from: {new DirectoryInfo(dmrBasePath).FullName}");
        var id = new Dtmi(dtmi);
        var dtdl = await dmrClient.GetModelAsync(dtmi, ModelDependencyResolution.Disabled);
        var result = await parser.ParseAsync(dtdl.Content[dtmi]);
        return (DTInterfaceInfo)result[id];
    }
}
```


## Print DTDL Object Model

The `DTInfoExtension` provides `Print` methods for each `DT*Info` type:

```cs
var example1 = await ModelResolver.LoadModelAsync("dtmi:com:example;1", basePath);

Console.WriteLine(example1.Print(true));
foreach (var co in example1.Components) 
    Console.WriteLine(
        $"[Co] {co.Value.Name} ({co.Value.Schema.Id}) \n" +
          $"{co.Value.Schema.Print()}\n");
```