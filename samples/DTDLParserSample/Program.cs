using Azure.IoT.ModelsRepository;
using DTDLParser;

string basePath = Path.Join(System.Reflection.Assembly.GetExecutingAssembly().Location + @"./../../../../");
var dmrClient = new ModelsRepositoryClient(new Uri(basePath));

var parser = new ModelParser(new ParsingOptions()
{
    DtmiResolverAsync = dmrClient.ParserDtmiResolverAsync
});

Console.WriteLine($"Parser version: {parser.GetType().Assembly.FullName}\n");

var example1Dtmi = new Dtmi("dtmi:com:example;1");
var example1Dtdl = File.ReadAllText(Path.Join(basePath, example1Dtmi.ToPath()));
var example1ParseResult = await parser.ParseAsync(example1Dtdl);
var example1 = new InterfaceInfo(example1ParseResult, example1Dtmi);

Console.WriteLine($"Root {example1Dtmi}");
example1.Print();
example1.Components.ToList().ForEach(co =>
{
    Console.WriteLine($"[Co] {co.Name} ({co.Schema.Id})");
    var coInfo = new InterfaceInfo(example1ParseResult, co.Schema.Id);
    coInfo.Print();
});

