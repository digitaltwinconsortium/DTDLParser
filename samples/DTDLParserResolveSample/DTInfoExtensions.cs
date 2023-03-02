using DTDLParser;
using DTDLParser.Models;
using System.Text;

namespace DTDLParserResolveSample;

public static partial class DTInfoExtensions {
    public static string Print(this DTInterfaceInfo dtInterface, bool isRoot = false) {
        StringBuilder sb = new();
        if (isRoot) sb.Append($"Root {dtInterface.Id}\n");
        foreach (var t in dtInterface.Telemetries) sb.AppendLine(t.Value.Print());
        foreach (var p in dtInterface.Properties) sb.AppendLine(p.Value.Print());
        foreach (var c in dtInterface.Commands) sb.AppendLine(c.Value.Print());
        return sb.ToString();
    }

    public static string Print(this DTSchemaInfo s) {
        StringBuilder sb = new();
        switch (s.EntityKind) {
            case DTEntityKind.Map:
                DTMapInfo map = (DTMapInfo)s;
                sb.Append($"{s.EntityKind}<{ModelParser.GetTermOrUri(map.MapKey.Schema.Id)},{ModelParser.GetTermOrUri(map.MapValue.Schema.Id)}> ");
                foreach (var st in map.MapValue.SupplementalTypes) sb.Append(ModelParser.GetTermOrUri(st));
                sb.Append(' ');
                foreach (var sp in map.MapValue.SupplementalProperties) sb.Append(((DTEnumValueInfo)sp.Value).Name);
                break;
            case DTEntityKind.Array:
                DTArrayInfo arr = (DTArrayInfo)s;
                sb.Append($"{s.EntityKind}<{ModelParser.GetTermOrUri(arr.ElementSchema.Id)}>");
                break;
            case DTEntityKind.Enum:
                DTEnumInfo en = (DTEnumInfo)s;
                sb.Append($"{s.EntityKind} :{ModelParser.GetTermOrUri(en.ValueSchema.Id)} [ ");
                foreach (var e in en.EnumValues) sb.Append($"{e.Name} {e.EnumValue}, ");
                sb.Append(" ]");
                break;
            case DTEntityKind.Object:
                DTObjectInfo o = (DTObjectInfo)s;
                sb.Append($"{s.EntityKind} ");
                foreach (var f in o.Fields) sb.Append(f.Print());
                break;
        }

        return sb.ToString();
    }

    public static string Print(this DTFieldInfo f) {
        StringBuilder sb = new();
        sb.Append($"[ {f.Name} : ");
        if (f.Schema.DefinedIn == f.DefinedIn) {
            sb.Append(f.Schema.Print());
        }
        else {
            sb.Append(ModelParser.GetTermOrUri(f.Schema.Id));
        }
        foreach (var t in f.SupplementalTypes) sb.Append(" " + ModelParser.GetTermOrUri(t));
        foreach (var p in f.SupplementalProperties) sb.Append(" " + ((DTEnumValueInfo)p.Value).Name);
        sb.Append(']');
        return sb.ToString();
    }

    public static string Print(this DTTelemetryInfo t) {
        StringBuilder sb = new();
        sb.Append($" [T] {t.Name} ");
        if (t.Schema.DefinedIn == t.DefinedIn) {
            sb.Append(t.Schema.Print());
        }
        else {
            sb.Append(ModelParser.GetTermOrUri(t.Schema.Id));
        }
        foreach (var st in t.SupplementalTypes) sb.Append(" " + ModelParser.GetTermOrUri(st));
        
        if (t.LanguageMajorVersion == 2) {
            foreach (var sp in t.SupplementalProperties) sb.Append(" " +  ModelParser.GetTermOrUri(((DTUnitInfo)sp.Value).Id));
        }
        else {
            foreach (var sp in t.SupplementalProperties) {
                if (sp.Key == "dtmi:dtdl:extension:quantitativeTypes:v1:property:unit") {
                    sb.Append(" " + ((DTEnumValueInfo)sp.Value).Name);
                }
                else {
                    sb.Append(" " + sp.Value);
                }
            }
        }
        return sb.ToString();
    }

    public static string Print(this DTPropertyInfo p) {
        StringBuilder sb = new();
        sb.Append($" [P] {p.Name} ");
        if (p.Schema.DefinedIn == p.DefinedIn) {
            sb.Append(p.Schema.Print());
        }
        else {
            sb.Append(ModelParser.GetTermOrUri(p.Schema.Id));
        }

        foreach(var t in p.SupplementalTypes) sb.Append(" " + ModelParser.GetTermOrUri(t));
        if (p.LanguageMajorVersion == 2)
        {
            foreach(var pp in p.SupplementalProperties) sb.Append(" " + ModelParser.GetTermOrUri(((DTUnitInfo)pp.Value).Id));
        }
        else
        {
            foreach (var sp in p.SupplementalProperties)
            {
                if (sp.Key == "dtmi:dtdl:extension:quantitativeTypes:v1:property:unit")
                {
                    sb.Append(" " + ((DTEnumValueInfo)sp.Value).Name);
                }
                else {
                    sb.Append(" " + sp.Value);
                }
            };
        }
        return sb.ToString();
    }

    public static string Print(this DTCommandInfo c) {
        StringBuilder sb = new();
        sb.Append($" [C] {c.Name}");
        if (c.Request != null) {
            var req = c.Request;
            if (req.Schema.DefinedIn == req.DefinedIn) {
                sb.Append($" req: {req.Schema.Print()} ");
            }
            else {
                sb.Append($" req: {ModelParser.GetTermOrUri(req.Schema.Id)} ");
            }
            foreach (var t in req.SupplementalTypes) sb.Append(" " + ModelParser.GetTermOrUri(t));
            foreach (var p in req.SupplementalProperties) sb.Append(" " + ((DTEnumValueInfo)p.Value).Name);
        }
        if (c.Response != null) {
            var resp = c.Response;
            if (resp.Schema.DefinedIn == resp.DefinedIn) {
                sb.Append($" resp: {resp.Schema.Print()}");
            }
            else {
                sb.Append($" resp: {ModelParser.GetTermOrUri(resp.Schema.Id)}");
            }
            foreach (var t in resp.SupplementalTypes) sb.Append(" " + ModelParser.GetTermOrUri(t));
            foreach (var p in resp.SupplementalProperties) sb.Append(" " + ((DTEnumValueInfo)p.Value).Name);
        }
        return sb.ToString();
    }
}