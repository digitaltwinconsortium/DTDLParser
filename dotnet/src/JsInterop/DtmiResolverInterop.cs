using System.Runtime.InteropServices.JavaScript;

/// <summary>
/// Partial class to be implemented by JavaScript code to resolve DTMI strings to their definitions.
/// </summary>
public partial class DtmiResolverInterop
{
    [JSImport("resolve", "DtmiResolverInterop")]
    internal static partial string[] Resolve(string[] dtmis);
}
