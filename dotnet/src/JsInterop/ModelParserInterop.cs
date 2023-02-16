using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using DTDLParser;

/// <summary>
/// The static <c>ModelParserInterop</c> class parses models written in the DTDL language.
/// This class can be used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
/// </summary>
public static partial class ModelParserInterop
{
    private static ModelParser modelParser = null;

    static ModelParserInterop()
    {
        modelParser = new ModelParser();
    }

    /// <summary>
    /// Empty main method to overcome a limitation with .NET 7 JSInterop functionality.
    /// </summary>
    public static void Main()
    {
    }

    /// <summary>
    /// Parse an array of JSON text strings as DTDL models and return the result as a JSON object model.
    /// </summary>
    /// <param name="jsonTexts">The JSON text strings to parse as DTDL models.</param>
    /// <returns>A string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object.</returns>
    [JSExport]
    public static string ParseToJson(string[] jsonTexts)
    {
        return modelParser.ParseToJson(jsonTexts);
    }

    /// <summary>
    /// Asynchronously parse an array of JSON text strings as DTDL models and return the result as a JSON object model.
    /// </summary>
    /// <param name="jsonTexts">The JSON text strings to parse as DTDL models.</param>
    /// <returns>A <c>Task</c> object whose <c>Result</c> property is a string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object.</returns>
    [JSExport]
    public static async Task<string> ParseToJsonAsync(string[] jsonTexts)
    {
        return await modelParser.ParseToJsonAsync(GetJsonTexts(jsonTexts));
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private static async IAsyncEnumerable<string> GetJsonTexts(string[] jsonTexts)
    {
        foreach (string jsonText in jsonTexts)
        {
            yield return jsonText;
        }
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously    }
}
