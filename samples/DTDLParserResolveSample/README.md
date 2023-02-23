# DTDLParserResolveSample

This sample loads an interface located in a folder structure following the [DMR Convention](https://github.com/Azure/iot-plugandplay-models-tools/wiki/Resolution-Convention), and enumerates the DTDL elements.

The `dtmi:com:example;1` interface uses an additional interface `dtmi:com:example:base;1`. 

## Resolving extenal references

To resolve external dependencies, this sample uses the [Azure.IoT.ModelsRepository](https://www.nuget.org/packages/Azure.IoT.ModelsRepository) configured to read from a local folder.

```cs
string basePath = Path.Join(System.Reflection.Assembly.GetExecutingAssembly().Location + @"./../../../../");
var dmrClient = new ModelsRepositoryClient(new Uri(basePath));
```

The parser is configured with the `ParserDtmiResolverAsync` provided in the `ModelsRepositoryClientExtensions`

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

```cs
var parser = new ModelParser(new ParsingOptions()
{
    DtmiResolverAsync = dmrClient.ParserDtmiResolverAsync
});
```

## DTDL Object Model

To enumerate the interface contents, the class `InterfaceInfo` provides an object model to navigate the parser results

```cs
var example1Dtmi = new Dtmi("dtmi:com:example;1");
var example1Dtdl = File.ReadAllText(Path.Join(basePath, example1Dtmi.ToPath()));
var example1ParseResult = await parser.ParseAsync(example1Dtdl);
var example1 = new InterfaceInfo(example1ParseResult, example1Dtmi);
example1.Properties.ToList().ForEach(p => Console.WriteLine(p.Name));
```

## Print DT*Info 

The `DTInfoExtensions` class provides a `Print` method to output to the console relevant properties for each `DT*Info` class.

