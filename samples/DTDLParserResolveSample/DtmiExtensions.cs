using DTDLParser;

namespace DTDLParserSample
{
    public static class DtmiExtensions
    {
        public static string ToPath(this Dtmi dtmi) => $"{dtmi.ToString().ToLowerInvariant().Replace(":", "/").Replace(";", "-")}.json";
    }
}