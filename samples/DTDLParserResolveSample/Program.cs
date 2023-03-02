using DTDLParserResolveSample;

string basePath = Path.Join(System.Reflection.Assembly.GetExecutingAssembly().Location + @"./../../../../");

var example1 = await ModelResolver.LoadModelAsync("dtmi:com:example;1", basePath);

Console.WriteLine(example1.Print(true));
foreach (var co in example1.Components) 
    Console.WriteLine(
        $"[Co] {co.Value.Name} ({co.Value.Schema.Id}) \n" +
          $"{co.Value.Schema.Print()}\n");

