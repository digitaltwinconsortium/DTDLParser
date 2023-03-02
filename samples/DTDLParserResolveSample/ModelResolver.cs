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
        Console.WriteLine($"Parser version: {parser.GetType().Assembly.FullName}\n");
        var id = new Dtmi(dtmi);
        var dtdl = File.ReadAllText(Path.Join(dmrBasePath, id.ToPath()));
        var result = await parser.ParseAsync(dtdl);
        return (DTInterfaceInfo)result[id];
    }
}
