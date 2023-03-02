using DTDLParser;

namespace DTDLParserResolveSample;
public static class DtmiExtensions
{
    public static string ToPath(this Dtmi dtmi) => $"{dtmi.ToString().ToLowerInvariant().Replace(":", "/").Replace(";", "-")}.json";
}