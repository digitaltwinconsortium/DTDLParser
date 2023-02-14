namespace DTDLParser
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Static class that provides explanatory documentation on various command-line options.
    /// </summary>
    public static class Explainer
    {
        /// <summary>
        /// Display documentation on the indicated option.
        /// </summary>
        /// <param name="optionToExplain">String value representing a command-line option to explain.</param>
        public static void Explain(string optionToExplain)
        {
            switch (optionToExplain)
            {
                case "source":
                    ExplainSource();
                    break;
                case "dest":
                    ExplainDest();
                    break;
                case "skip":
                    ExplainSkip();
                    break;
                case "fill":
                    ExplainFill();
                    break;
                case "subs":
                    ExplainSubs();
                    break;
                case "suggest":
                    ExplainSuggest();
                    break;
                case "log":
                    ExplainLog();
                    break;
                case "new-ids":
                    ExplainNewIds();
                    break;
                case "lax":
                    ExplainLax();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Option '{optionToExplain}' not recognized.");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("Explainable options are: source, dest, skip, fill, subs, suggest, log, new-ids, lax.");
                    break;
            }
        }

        private static void ExplainSource()
        {
            Console.WriteLine();
            Console.WriteLine("The --source option specifies the root of a directory tree that contains a DTDL source model to convert.");
            Console.WriteLine("All files in the tree with a .json extension will be read and parsed as DTDL.");
            Console.WriteLine("Collectively, all source files must yield a single DTDL model that is complete and valid.");
            Console.WriteLine($"Context specifiers in the source model may indicate any versions of DTDL prior to v{Remodeler.TargetDtdlVersion}.");
            Console.WriteLine();
            Console.WriteLine("Example:");
            ColorLine(ConsoleColor.Green, "  Remodel ", "--source models\\Appliances", $" --dest models\\AppliancesV{Remodeler.TargetDtdlVersion}");
            Console.WriteLine();
            Console.WriteLine("There is no means to exclude a JSON file in the source tree from inclusion in the source model.");
            Console.WriteLine($"However, portions of the source tree can be excluded from conversion to DTDL v{Remodeler.TargetDtdlVersion} by means of the --skip option.");
            Console.WriteLine("For assistance on the use of --skip, type:");
            Console.WriteLine("  Remodel --explain skip");
        }

        private static void ExplainDest()
        {
            Console.WriteLine();
            Console.WriteLine("The --dest option specifies the root of a directory tree that will receive the converted DTDL model.");
            Console.WriteLine("Neither the specified root directory nor any of its path components is required to exist beforehand.");
            Console.WriteLine("However, if the specified destination directory exists, it is required to be empty.");
            Console.WriteLine();
            Console.WriteLine("If the conversion succeeds, a directory hierarchy paralleling the source hierarchy will be created at the destination root.");
            Console.WriteLine("The destination hierarchy will contain a converted file for each file in the source tree with a .json extension, except as excluded via --skip.");
            Console.WriteLine("Prior to creating the new hierarchy, the converted model will be parsed to ensure that it is complete and valid.");
            Console.WriteLine();
            Console.WriteLine("Example:");
            ColorLine(ConsoleColor.Green, "  Remodel --source models\\Appliances ", $"--dest models\\AppliancesV{Remodeler.TargetDtdlVersion}");
            Console.WriteLine();
            Console.WriteLine("When files are excluded from conversion via --skip, the resulting holes in the destination tree can be filled in by means of the --fill option.");
            Console.WriteLine("For assistance on the use of --fill, type:");
            Console.WriteLine("  Remodel --explain fill");
        }

        private static void ExplainSkip()
        {
            Console.WriteLine();
            Console.WriteLine("The --skip option specifies the roots of one or more subtrees within the source tree.");
            Console.WriteLine("The root of each subtree must exist in the source tree, and the subtrees must not overlap each other.");
            Console.WriteLine($"The files within the subtrees are not converted to DTDL v{Remodeler.TargetDtdlVersion}, unlike other source files outside the subtrees.");
            Console.WriteLine("The subtrees are included in the source model when it is validated, and they are ephemerally combined with the converted model when it is validated.");
            Console.WriteLine();
            Console.WriteLine("Example:");
            ColorLine(ConsoleColor.Green, $"  Remodel --source models\\Appliances --dest models\\AppliancesV{Remodeler.TargetDtdlVersion} ", "--skip models\\Appliances\\External");
            Console.WriteLine();
            Console.WriteLine("When files are excluded from conversion via --skip, the destination tree contains no files corresponding to files in the skipped source subtrees.");
            Console.WriteLine("If desired, these holes in the destination tree can be filled in by means of the --fill option.");
            Console.WriteLine("For assistance on the use of --fill, type:");
            Console.WriteLine("  Remodel --explain fill");
        }

        private static void ExplainFill()
        {
            Console.WriteLine();
            Console.WriteLine("The --fill option specifies a mechanism to fill holes in the destination tree caused by excluding source subtrees via the --skip option.");
            Console.WriteLine("This option may be specified only when the --skip option is also specified.");
            Console.WriteLine("When the --skip option is used, not all files in the source tree will have corresponding files in the destination tree.");
            Console.WriteLine("The --fill option provides various alternatives for representing these skipped files in the destination tree.");
#if NET6_0_OR_GREATER
            Console.WriteLine("The permissible values for this option are Copy, Link, Move, and None (which is the default).");
#else
            Console.WriteLine("The permissible values for this option are Copy, Move, and None (which is the default).");
#endif
            Console.WriteLine();
            Console.WriteLine("When Copy is specified, the skipped subtrees in the source tree are copied to the corresponding locations in the destination tree.");
            Console.WriteLine("Specifically, any files matching the --pattern value (default *.json) are copied, as are the directory paths that lead to these files.");
#if NET6_0_OR_GREATER
            Console.WriteLine();
            Console.WriteLine("When Link is specified, the root of each skipped subtree in the source tree becomes the target of a symbolic link.");
            Console.WriteLine("Symbolic links are created at the corresponding locations in the destination tree.");
            Console.WriteLine("In Windows, creating symbolic links is not always supported for all users, so this option might fail to create the symlinks.");
            Console.WriteLine("If this happens, you can try deleting the destination tree and reattempting the conversion from an elevated command prompt.");
#endif
            Console.WriteLine();
            Console.WriteLine("When Move is specified, the skipped subtrees in the source tree are moved to the corresponding locations in the destination tree.");
            Console.WriteLine("Conseqently, the source tree may no longer have a complete model.");
            Console.WriteLine();
            Console.WriteLine("Example:");
            ColorLine(ConsoleColor.Green, $"  Remodel --source models\\Appliances --dest models\\AppliancesV{Remodeler.TargetDtdlVersion} --skip models\\Appliances\\External ", "--fill Copy");
        }

        private static void ExplainSubs()
        {
            Console.WriteLine();
            Console.WriteLine("The --subs option specifies the path of a JSON file containing IRI/term substitutions for contexts, types, and properties.");
            Console.WriteLine("The substitutions file must contain a single JSON object with four properties: \"contextAddition\", \"contextSubstitutions\", \"typeSubstitutions\", \"propertySubstitutions\".");
            Console.WriteLine();
            Console.WriteLine("The \"contextAddition\" property takes a JSON string value.");
            Console.WriteLine("If a model contains an undefined type that is permitted only when an unknown context is present, the value of this property is added to the model as an additional context specifier.");
            Console.WriteLine("Example:");
            ColorLine(ConsoleColor.Green, "  ", "\"contextAddition\"", $": \"{Suggester.UndefinedExtensionContextSuggestion}\",");
            Console.WriteLine();
            Console.WriteLine("The \"contextSubstitutions\" property takes a JSON object value; each property in the object defines a substitution rule:");
            Console.WriteLine("  If the property value is a JSON string, any context specifier in the model that matches the property key is replaced with the property value.");
            Console.WriteLine("  If the property value is JSON null, any context specifier that matches the property key is removed from the model.");
            Console.WriteLine("Example of one replacement rule and one removal rule:");
            ColorLine(ConsoleColor.Green, "  ", "\"contextSubstitutions\"", ": {");
            Console.WriteLine("    \"http://foo.com/bar\": \"dtmi:com:foo:bar;1\",");
            Console.WriteLine("    \"http://foo.com/baz\": null");
            Console.WriteLine("  },");
            Console.WriteLine();
            Console.WriteLine("The \"typeSubstitutions\" property takes a JSON object value; each property in the object defines a substitution rule:");
            Console.WriteLine("  If the property value is a JSON string, any \"@type\" value in the model that matches the property key is replaced with the property value.");
            Console.WriteLine("  If the property value is JSON null, any \"@type\" value that matches the property key is removed from the model.");
            Console.WriteLine("Example of one replacement rule and one removal rule:");
            ColorLine(ConsoleColor.Green, "  ", "\"typeSubstitutions\"", ": {");
            Console.WriteLine("    \"//foobar\": \"foobar\",");
            Console.WriteLine("    \"//foo/bar\": null");
            Console.WriteLine("  },");
            Console.WriteLine();
            Console.WriteLine("The \"propertySubstitutions\" property takes a JSON object value; each property in the object defines a substitution rule:");
            Console.WriteLine("  If the property value is a JSON string, any property name in the model that matches the property key is replaced with the property value.");
            Console.WriteLine("  If the property value is JSON null, any property whose name matches the property key is removed from the model.");
            Console.WriteLine("Example of one replacement rule and one removal rule:");
            ColorLine(ConsoleColor.Green, "  ", "\"propertySubstitutions\"", ": {");
            Console.WriteLine("    \"http://foo.com/Foo\": \"Foo\",");
            Console.WriteLine("    \"http://foo.com/Bar\": null");
            Console.WriteLine("  }");
            Console.WriteLine();
            Console.WriteLine("If the JSON properties above are members of a JSON object in a file named substitutions.json, the substitutions can be applied as in the following example:");
            ColorLine(ConsoleColor.Green, $"  Remodel --source models\\Appliances --dest models\\AppliancesV{Remodeler.TargetDtdlVersion} ", "--subs substitutions.json");
        }

        private static void ExplainSuggest()
        {
            Console.WriteLine();
            Console.WriteLine("The --suggest option specifies an output JSON file to receive suggested IRI/term substitutions for contexts, types, and properties.");
            Console.WriteLine("The specified file must not exist prior to executing the Remodel command.");
            Console.WriteLine("The format of this file matches the format expected by the --subs option, so the generated file can be passed directly into a subsequent invocation of Remodel.");
            Console.WriteLine("Furthermore, if the --subs option and the --suggest option are both specified, the substitution rules from the --subs file will be incorporated into the generated suggestions file.");
            Console.WriteLine();
            Console.WriteLine("The file's \"contextAddition\" property will have a JSON string value.");
            Console.WriteLine("This value is copied directly from the \"contextAddition\" property in the subsitutions file if the --subs option is specified.");
            Console.WriteLine($"Oherwise, the value is \"{Suggester.UndefinedExtensionContextSuggestion}\".");
            Console.WriteLine();
            Console.WriteLine("The file's \"contextSubstitutions\" property will have a JSON object value.");
            Console.WriteLine("Each property in the object defines a substitution rule whose key is a disallowed context specifier used in the model.");
            Console.WriteLine("Disallowed context specifiers are identified by any of the following parsing errors:");
            DisplayUris(Suggester.DisallowedContextSpecifierValidationIds);
            Console.WriteLine("A disallowed context specifier is a terminal error, so it may be necessary to iterate on execution of the Remodel tool after each context substitution rule is added.");
            Console.WriteLine();
            Console.WriteLine("The file's \"typeSubstitutions\" property will have a JSON object value.");
            Console.WriteLine("Each property in the object defines a substitution rule whose key is a disallowed type value used in the model.");
            Console.WriteLine("Disallowed type values are identified by any of the following parsing errors:");
            DisplayUris(Suggester.DisallowedTypeValueValidationIds);
            Console.WriteLine("A disallowed type value will block parsing of element properties, so it may be necessary to iterate on execution of the Remodel tool after type substitution rules are added.");
            Console.WriteLine();
            Console.WriteLine("The file's \"propertySubstitutions\" property will have a JSON object value.");
            Console.WriteLine("Each property in the object defines a substitution rule whose key is a disallowed property name used in the model.");
            Console.WriteLine("Disallowed property names are identified by any of the following parsing errors:");
            DisplayUris(Suggester.DisallowedPropertyNameValidationIds);
            Console.WriteLine();
            Console.WriteLine("Examples:");
            ColorLine(ConsoleColor.Green, $"  Remodel --source models\\Appliances --dest models\\AppliancesV{Remodeler.TargetDtdlVersion} ", "--suggest newSubs.json");
            ColorLine(ConsoleColor.Green, $"  Remodel --source models\\Appliances --dest models\\AppliancesV{Remodeler.TargetDtdlVersion} --subs substitutions.json ", "--suggest newSubs.json");
            Console.WriteLine();
            Console.WriteLine("For more detail on the content, format, and usage of a substitutions file, type:");
            Console.WriteLine("  Remodel --explain subs");
        }

        private static void ExplainLog()
        {
            Console.WriteLine();
            Console.WriteLine("The --log option specifies an output JSON file to receive a summary of changes performed by the upgrade.");
            Console.WriteLine("The log file must not exist prior to executing the Remodel command.");
            Console.WriteLine("After execution, the log file will contain a single JSON object whose property values summarize the changes.");
            Console.WriteLine("Following are descriptions of the values for each property of this JSON object:");
            Console.WriteLine();
            Console.WriteLine("The value of the \"canonicalization\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates the name of a source model file for which at least one class, property, or element name was canonicalized.");
            Console.WriteLine("For example, a property name of \"dtmi:dtdl:property:schema;2\" is canonicalized as \"schema\".");
            Console.WriteLine();
            Console.WriteLine("The value of the \"concretization\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates the name of a source model file for which at least one abstract class was replaced with an appropriate concrete subclass.");
            Console.WriteLine("For example, the CommandPayload class is concrete in DTDL v2 but abstract in DTDL v3.");
            Console.WriteLine("So, each instance of \"CommandPayload\" is replaced by one of its concrete subclasses \"CommandRequest\" or \"CommandResponse\" as appropriate.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"contextReplacement\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file for which at least one context specifier was replaced according to a \"contextSubstitutions\" rule from the substitutions file.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"contextRemoval\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file for which at least one context specifier was removed according to a \"contextSubstitutions\" rule from the substitutions file.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"typeReplacement\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file for which at least one type was replaced according to a \"typeSubstitutions\" rule from the substitutions file.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"typeRemoval\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file for which at least one type was removed according to a \"typeSubstitutions\" rule from the substitutions file.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"propertyReplacement\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file for which at least one property name was replaced according to a \"propertySubstitutions\" rule from the substitutions file.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"propertyRemoval\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file for which at least one property was removed according to a \"propertySubstitutions\" rule from the substitutions file.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"keywordRemoval\" property is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file for which at least one improper JSON-LD keyword was removed.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"partnerContextsReordering\" property is a JSON object.");
            Console.WriteLine("Each property in the object has a name that inticates an IoT partner for which an extension context is defined, such as \"iotcentral\".");
            Console.WriteLine("Each property in the object has a value that is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file for which the partner's context specifier was relocated to follow the DTDL context specifier.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"featureContextsAddition\" property is a JSON object.");
            Console.WriteLine("Each property in the object has a name that inticates a feature extension context, such as \"dtmi:dtdl:extension:quantitativeTypes;1\".");
            Console.WriteLine("Each property in the object has a value that is a JSON array of string values.");
            Console.WriteLine("Each value in the array indicates a source model file to which the feature extension context specifier was added.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"undefinedExtensionContextAddition\" property is a JSON array of string values.");
            Console.WriteLine("Each value indicates a source model file for which the value of the \"contextAddition\" property from the substitutions file was added to the model as an additional context specifier.");
            Console.WriteLine();
            Console.WriteLine("The value of the \"userIdChanges\" property is a JSON object.");
            Console.WriteLine("Each property in the object indicates a user ID in the model that was replaced with a new user ID.");
            Console.WriteLine("For example, the property \"dtmi:ex:Foo;1\": \"dtmi:ex:Foo;2\" indicateds that user ID \"dtmi:ex:Foo;1\" has been replaced with \"dtmi:ex:Foo;2\".");
            Console.WriteLine();
            Console.WriteLine("If the --skip option is used to skip conversion of one or more subtrees in the source tree, there will be an additional property whose value is a JSON array of subtree roots.");
            Console.WriteLine("The name of this property depends on whether the --fill option is used and what its value is.");
            Console.WriteLine("If the --fill option is not used, the property name is \"skippedSubtrees\".");
            Console.WriteLine("If the --fill option is used with a value of Copy, the property name is \"copiedSubtrees\".");
#if NET6_0_OR_GREATER
            Console.WriteLine("If the --fill option is used with a value of Link, the property name is \"linkedSubtrees\".");
#endif
            Console.WriteLine("If the --fill option is used with a value of Move, the property name is \"movedSubtrees\".");
            Console.WriteLine();
            Console.WriteLine("Example:");
            ColorLine(ConsoleColor.Green, $"  Remodel --source models\\Appliances --dest models\\AppliancesV{Remodeler.TargetDtdlVersion} ", "--log log.json");
            Console.WriteLine();
            Console.WriteLine("For more detail on the content, format, and usage of a substitutions file, type:");
            Console.WriteLine("  Remodel --explain subs");
        }

        private static void ExplainNewIds()
        {
            Console.WriteLine();
            Console.WriteLine("The --new-ids option specifies that all user IDs in the source model should be updated with new version numbers in the converted model.");
            Console.WriteLine();
            Console.WriteLine("DTDL mandates that every identifier should have at most one definition.");
            Console.WriteLine("When a model is converted to a new version of DTDL, the definitions in the model consequently change.");
            Console.WriteLine("To preserve the uniqueness of each identifier, the --new-ids option causes each element in the converted model to receive a new ID.");
            Console.WriteLine();
            Console.WriteLine("The ID for each converted element is the same as the ID for the corresponding source element, except its version number is incremented.");
            Console.WriteLine("Each identifier's version number is usually incremented by 1.");
            Console.WriteLine("However, if a model contains multiple IDs that differ only by version number, the versions will be increased to keep them unique within the model.");
            Console.WriteLine("For example, if a model contains IDs \"dtmi:ex:foo;3\", \"dtmi:ex:foo;4\", and \"dtmi:ex:foo;6\", the new IDs will be \"dtmi:ex:foo;7\", \"dtmi:ex:foo;8\", and \"dtmi:ex:foo;10\".");
            Console.WriteLine();
            Console.WriteLine("Example:");
            ColorLine(ConsoleColor.Green, $"  Remodel --source models\\Appliances --dest models\\AppliancesV{Remodeler.TargetDtdlVersion} ", "--new-ids");
        }

        private static void ExplainLax()
        {
            Console.WriteLine();
            Console.WriteLine("The --lax option specifies that model validation should be lax, such that undefined extensions are allowed.");
            Console.WriteLine();
            Console.WriteLine("A model may contain one or more context specifiers in addition to than the DTDL context specifier.");
            Console.WriteLine("Each of these additional context specifiers must be a valid DTMI with a version suffix.");
            Console.WriteLine("For the model to be understood as valid, each additional context specifier must refer to a recognized language extension.");
            Console.WriteLine("When a context specifier is not recognized, a service is permitted to decide whether to reject or ignore the additional context.");
            Console.WriteLine();
            Console.WriteLine("When the --lax option is specified, the Remodel tool ignores any unknown context specifiers in models.");
            Console.WriteLine("This is appropriate when intending to submit the model to a service that allows and ignores context references to unknown language extensions.");
            Console.WriteLine();
            Console.WriteLine("When the --lax option is not specified, the Remodel tool rejects models that have unknown context specifiers.");
            Console.WriteLine("This is appropriate when intending to submit the model to a service that rejects models with contexts that refer to unknown language extensions.");
            Console.WriteLine();
            Console.WriteLine("Example:");
            ColorLine(ConsoleColor.Green, $"  Remodel --source models\\Appliances --dest models\\AppliancesV{Remodeler.TargetDtdlVersion} ", "--lax");
        }

        private static void ColorLine(ConsoleColor color, string prologue, string highlight, string epilogue = "")
        {
            Console.Write(prologue);
            Console.ForegroundColor = color;
            Console.Write(highlight);
            Console.ResetColor();
            Console.WriteLine(epilogue);
        }

        private static void DisplayUris(IEnumerable<Uri> uris)
        {
            foreach (Uri uri in uris)
            {
                Console.WriteLine($"  \"{uri.AbsoluteUri}\"");
            }
        }
    }
}
