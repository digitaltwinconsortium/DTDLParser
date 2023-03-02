using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading;
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
    /// <param name="indent">Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.</param>
    /// <returns>A string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object.</returns>
    [JSExport]
    public static string ParseToJson(string[] jsonTexts, bool indent = false)
    {
        return modelParser.ParseToJson(jsonTexts, indent);
    }

    /// <summary>
    /// Parse an array of JSON text strings as DTDL models and return the result as a JSON object model.
    /// </summary>
    /// <param name="jsonText">The JSON text string to parse as DTDL models.</param>
    /// <param name="indent">Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.</param>
    /// <returns>A string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object.</returns>
    [JSExport]
    public static string ParseToJson(string jsonText, bool indent = false)
    {
        return modelParser.ParseToJson(StringToEnumerable(jsonText), indent);
    }

    /// <summary>
    /// Asynchronously parse an array of JSON text strings as DTDL models and return the result as a JSON object model.
    /// </summary>
    /// <param name="jsonTexts">The JSON text strings to parse as DTDL models.</param>
    /// <param name="indent">Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.</param>
    /// <returns>A <c>Task</c> object whose <c>Result</c> property is a string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object.</returns>
    [JSExport]
    public static async Task<string> ParseToJsonAsync(string[] jsonTexts, bool indent = false)
    {
        return await modelParser.ParseToJsonAsync(GetJsonTexts(jsonTexts), indent);
    }

    /// <summary>
    /// Asynchronously parse an array of JSON text strings as DTDL models and return the result as a JSON object model.
    /// </summary>
    /// <param name="jsonText">The JSON text string to parse as DTDL models.</param>
    /// <param name="indent">Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.</param>
    /// <returns>A <c>Task</c> object whose <c>Result</c> property is a string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object.</returns>
    [JSExport]
    public static async Task<string> ParseToJsonAsync(string jsonText, bool indent = false)
    {
        return await modelParser.ParseToJsonAsync(StringToAsyncEnumerable(jsonText), indent);
    }

    private static IEnumerable<string> StringToEnumerable(string jsonText)
    {
        yield return jsonText;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private static async IAsyncEnumerable<string> StringToAsyncEnumerable(string jsonText)
    {
        yield return jsonText;
    }

    private static async IAsyncEnumerable<string> GetJsonTexts(string[] jsonTexts)
    {
        foreach (string jsonText in jsonTexts)
        {
            yield return jsonText;
        }
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously    }
}
