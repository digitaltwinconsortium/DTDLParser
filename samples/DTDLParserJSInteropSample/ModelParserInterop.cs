using DTDLParser;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace DtdlParserJSInterop;

[SupportedOSPlatform("browser")]
public partial class ModelParserInterop
{
    public static void Main() => Console.WriteLine("dotnet loaded");

    [JSExport]
    public static string ParserVersion() => typeof(ModelParser).Assembly.FullName;

    [JSExport]
    public static string Parse(string dtdl) => new ModelParser().ParseToJson(new string[] { dtdl });
}