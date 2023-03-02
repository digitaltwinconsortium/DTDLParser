using Azure.IoT.ModelsRepository;
using DTDLParser;
using DTDLParser.Models;

namespace DTDLParserResolveSample;
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
