using DTDLParser;
using DTDLParser.Models;
using System.Text;

namespace DTDLParserSample;

public static partial class DTInfoExtensions
{
    public static string Print(this DTInterfaceInfo root)
    {
        StringBuilder sb = new();
        foreach (var t in root.Telemetries) sb.AppendLine(t.Value.Print());
        foreach(var p in root.Properties) sb.AppendLine(p.Value.Print());
        foreach (var c in root.Commands) sb.AppendLine(c.Value.Print());
        return sb.ToString();
    }

    public static string Print(this DTSchemaInfo s)
    {
        StringBuilder sb = new();
        switch (s.EntityKind)
        {
            case DTEntityKind.Map:
                DTMapInfo map = (DTMapInfo)s;
                sb.Append($"{s.EntityKind}<{ModelParser.GetTermOrUri(map.MapKey.Schema.Id)},{ModelParser.GetTermOrUri(map.MapValue.Schema.Id)}> ");
                foreach(var st in map.MapValue.SupplementalTypes) sb.Append(ModelParser.GetTermOrUri(st));
                sb.Append(' ');
                foreach(var sp in map.MapValue.SupplementalProperties) sb.Append(((DTEnumValueInfo)sp.Value).Name);
                break;
            case DTEntityKind.Array:
                DTArrayInfo arr = (DTArrayInfo)s;
                sb.Append($"{s.EntityKind}<{ModelParser.GetTermOrUri(arr.ElementSchema.Id)}>");
                break;
            case DTEntityKind.Enum:
                DTEnumInfo en = (DTEnumInfo)s;
                sb.Append($"{s.EntityKind} :{ModelParser.GetTermOrUri(en.ValueSchema.Id)} [ ");
                foreach(var e in en.EnumValues) sb.Append($"{e.Name} {e.EnumValue}, ");
                sb.Append(" ]");
                break;
            case DTEntityKind.Object:
                DTObjectInfo o = (DTObjectInfo)s;
                sb.Append($"{s.EntityKind} ");
                foreach(var f in o.Fields) sb.Append(f.Print());
                break;
        }

        return sb.ToString();
    }

    public static string Print(this DTFieldInfo f)
    {
        StringBuilder sb = new();
        sb.Append($"[ {f.Name} : ");
        if (f.Schema.DefinedIn == f.DefinedIn)
        {
            sb.Append(f.Schema.Print());
        }
        else
        {
            sb.Append(ModelParser.GetTermOrUri(f.Schema.Id));
        }
        f.SupplementalTypes.ToList().ForEach(t => sb.Append(" " + ModelParser.GetTermOrUri(t)));
        f.SupplementalProperties.ToList().ForEach(t => sb.Append(" " + ((DTEnumValueInfo)t.Value).Name));
        sb.Append(']');
        return sb.ToString();
    }

    public static string Print(this DTTelemetryInfo t, int pad = 0)
    {
        StringBuilder sb = new();
        sb.Append(" ".PadRight(pad));
        sb.Append($"[T] {t.Name} ");
        if (t.Schema.DefinedIn == t.DefinedIn)
        {
            sb.Append(t.Schema.Print());
        }
        else
        {
            sb.Append(ModelParser.GetTermOrUri(t.Schema.Id));
        }
        t.SupplementalTypes.ToList().ForEach(t => sb.Append(" " + ModelParser.GetTermOrUri(t)));
        if (t.LanguageMajorVersion == 2)
        {
            t.SupplementalProperties.ToList().ForEach(t => sb.Append(" " + ModelParser.GetTermOrUri(((DTUnitInfo)t.Value).Id)));
        }
        else
        {
            t.SupplementalProperties.ToList().ForEach(st =>
            {
                if (st.Key == "dtmi:dtdl:extension:quantitativeTypes:v1:property:unit")
                {
                    sb.Append(" " + ((DTEnumValueInfo)st.Value).Name);
                }
                else
                {
                    sb.Append(" " + st.Value);
                }
            });
        }
        return sb.ToString();
    }

    public static string Print(this DTPropertyInfo p, int pad = 0)
    {
        StringBuilder sb = new();
        sb.Append(" ".PadRight(pad));
        sb.Append($"[P] {p.Name} ");
        if (p.Schema.DefinedIn == p.DefinedIn)
        {
            sb.Append(p.Schema.Print());
        }
        else
        {
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
                else
                {
                    sb.Append(" " + sp.Value);
                }
            };
        }
        return sb.ToString();
    }

    public static string Print(this DTCommandInfo c, int pad = 0)
    {
        StringBuilder sb = new();
        sb.Append(" ".PadRight(pad));
        sb.Append($"[C] {c.Name}");
        if (c.Request != null)
        {
            var req = c.Request;
            if (req.Schema.DefinedIn == req.DefinedIn)
            {
                sb.Append($" req: {req.Schema.Print()} ");
            }
            else
            {
                sb.Append($" req: {ModelParser.GetTermOrUri(req.Schema.Id)} ");
            }
            foreach (var t in req.SupplementalTypes) sb.Append(" " + ModelParser.GetTermOrUri(t));
            foreach (var p in req.SupplementalProperties) sb.Append(" " + ((DTEnumValueInfo)p.Value).Name);
        }
        if (c.Response != null)
        {
            var resp = c.Response;
            if (resp.Schema.DefinedIn == resp.DefinedIn)
            {
                sb.Append($" resp: {resp.Schema.Print()}");
            }
            else
            {
                sb.Append($" resp: {ModelParser.GetTermOrUri(resp.Schema.Id)}");
            }
            foreach (var t in resp.SupplementalTypes) sb.Append(" " + ModelParser.GetTermOrUri(t));
            foreach (var p in resp.SupplementalProperties) sb.Append(" " + ((DTEnumValueInfo)p.Value).Name);
        }
        return sb.ToString();
    }
}