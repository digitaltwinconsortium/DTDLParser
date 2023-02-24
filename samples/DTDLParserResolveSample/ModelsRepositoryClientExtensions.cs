
using Azure.IoT.ModelsRepository;
using DTDLParser;
using System.Runtime.CompilerServices;

namespace DTDLParserSample;

internal static class ModelsRepositoryClientExtensions
{
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
}
