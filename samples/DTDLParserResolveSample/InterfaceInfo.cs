using DTDLParser;
using DTDLParser.Models;

namespace DTDLParserSample;

public class InterfaceInfo
{
    public IReadOnlyDictionary<Dtmi, DTEntityInfo> ObjectModel;
    public readonly DTEntityInfo root;

    public InterfaceInfo(IReadOnlyDictionary<Dtmi, DTEntityInfo> m, Dtmi dtmi)
    {
        ObjectModel = m;
        root = m[dtmi];
    }

    public IEnumerable<DTTelemetryInfo> Telemetries =>
        ((DTInterfaceInfo)root).Contents
            .Where(c => c.Value.EntityKind == DTEntityKind.Telemetry)
            .Select(t => (DTTelemetryInfo)t.Value);

    public IEnumerable<DTPropertyInfo> Properties =>
        ((DTInterfaceInfo)root).Contents
            .Where(c => c.Value.EntityKind == DTEntityKind.Property)
            .Select(p => (DTPropertyInfo)p.Value);

    public IEnumerable<DTCommandInfo> Commands =>
        ((DTInterfaceInfo)root).Contents
            .Where(c => c.Value.EntityKind == DTEntityKind.Command)
            .Select(c => (DTCommandInfo)c.Value);

    public IEnumerable<DTComponentInfo> Components =>
        ((DTInterfaceInfo)root).Contents
            .Where(c => c.Value.EntityKind == DTEntityKind.Component)
            .Select(c => (DTComponentInfo)c.Value);

    public IEnumerable<DTRelationshipInfo> Relationships =>
        ((DTInterfaceInfo)root).Contents
            .Where(c => c.Value.EntityKind == DTEntityKind.Relationship)
            .Select(r => (DTRelationshipInfo)r.Value);

    public void Print()
    {
        Telemetries.ToList().ForEach(t => Console.WriteLine(t.Print()));
        Properties.ToList().ForEach(p => Console.WriteLine(p.Print()));
        Commands.ToList().ForEach(c => Console.WriteLine(c.Print()));
    }
}