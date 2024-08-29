namespace MyParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using DTDLParser;
    using DTDLParser.Models;

    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("TestParse modelFile");
                return;
            }

            string modelFile = args[0];

            var modelParser = new ModelParser();
            string modelText = File.OpenText(modelFile).ReadToEnd();

            DtdlParseLocator parseLocator = (int parseIndex, int parseLine, out string sourceName, out int sourceLine) =>
            {
                sourceName = modelFile;
                sourceLine = parseLine;
                return true;
            };

            IReadOnlyDictionary<Dtmi, DTEntityInfo> modelDict;
            try
            {
                modelDict = modelParser.Parse(modelText, parseLocator);
            }
            catch (ParsingException pex)
            {
                foreach (ParsingError perr in pex.Errors)
                {
                    Console.WriteLine($"{perr.ValidationID} -- {perr.Message}");
                }

                return;
            }

            Console.WriteLine($"Model parsed successfully, {modelDict.Count} elements");

            foreach (KeyValuePair<Dtmi, DTEntityInfo> kvp in modelDict)
            {
                if (kvp.Value is IRootable rootableElement)
                {
                    string jsonText = rootableElement.GetJsonLdText();
                    if (jsonText != string.Empty)
                    {
                        Console.WriteLine($"{kvp.Key}:\n{jsonText}\n");
                    }
                }
            }
        }
    }
}
