
using DTDLParserResolveSample;

var example1 = await ModelResolver.LoadModelAsync("dtmi:azureiot:PaaD:IoTPhone;1", "https://iotmodels.github.io/dmr/");
Console.WriteLine(example1.Print(true));
foreach (var co in example1.Components) 
    Console.WriteLine($"[Co] {co.Value.Name} ({co.Value.Schema.Id}) \n {co.Value.Schema.Print()}\n");
