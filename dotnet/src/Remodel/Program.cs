namespace DTDLParser
{
    using System;
    using System.CommandLine;
    using System.IO;

    /// <summary>
    /// Static class to hold the application's <see cref="Main(string[])"/> method and supporting methods.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">A list of string arguments to the program.</param>
        public static void Main(string[] args)
        {
            var sourceOption = new Option<DirectoryInfo>(
                name: "--source",
                description: "Root of directory tree containing DTDL source model")
                { ArgumentHelpName = "DIRPATH" };

            var destOption = new Option<DirectoryInfo>(
                name: "--dest",
                description: $"Root of directory tree to receive DTDL v{Remodeler.TargetDtdlVersion} model (need not exist, but must be empty)")
                { ArgumentHelpName = "DIRPATH" };

            var skipOption = new Option<DirectoryInfo[]>(
                name: "--skip",
                description: "Roots of subtrees (within source tree) to skip converting")
                { ArgumentHelpName = "DIRPATH ...", AllowMultipleArgumentsPerToken = true };

            var fillOption = new Option<Filler.FillType>(
                name: "--fill",
                description: "Fill holes in destination tree left by --skip option",
                getDefaultValue: () => Filler.FillType.None);

            var subsOption = new Option<FileInfo>(
                name: "--subs",
                description: "JSON file containing IRI/term substitutions for contexts, types, and properties")
                { ArgumentHelpName = "FILEPATH" };

            var suggestOption = new Option<FileInfo>(
                name: "--suggest",
                description: "Output JSON file containing suggested IRI/term substitutions (must not exist)")
                { ArgumentHelpName = "FILEPATH" };

            var logOption = new Option<FileInfo>(
                name: "--log",
                description: "Output JSON file to receive summary of changes performed (must not exist)")
                { ArgumentHelpName = "FILEPATH" };

            var newIdsOption = new Option<bool>(
                name: "--new-ids",
                description: "Generate new version numbers for user IDs in model");

            var laxOption = new Option<bool>(
                name: "--lax",
                description: "Lax validation, allowing undefined extensions");

            var patternOption = new Option<string>(
                name: "--pattern",
                description: "Filename search pattern for identifying DTDL model files",
                getDefaultValue: () => "*.json");

            var explainOption = new Option<string>(
                name: "--explain",
                description: "Explain a given option")
                { ArgumentHelpName = "source|dest|skip|fill|subs|suggest|log|new-ids|lax" };

            var rootCommand = new RootCommand($"Upgrade a DTDL model to v{Remodeler.TargetDtdlVersion}")
            {
                sourceOption,
                destOption,
                skipOption,
                fillOption,
                subsOption,
                suggestOption,
                logOption,
                newIdsOption,
                laxOption,
                patternOption,
                explainOption,
            };

            ArgBinder argBinder = new ArgBinder(sourceOption, destOption, skipOption, fillOption, subsOption, suggestOption, logOption, newIdsOption, laxOption, patternOption, explainOption);

            rootCommand.SetHandler(
                (OptionContainer container) => { Remodel(container); },
                argBinder);

            rootCommand.Invoke(args);
        }

        /// <summary>
        /// Execute the behavior of the application, which is to upgrade a model to the latest version of DTDL.
        /// </summary>
        /// <param name="optionContainer">A <see cref="OptionContainer"/> that holds the CLI options.</param>
        internal static void Remodel(OptionContainer optionContainer)
        {
            try
            {
                if (optionContainer.OptionToExplain != null)
                {
                    Explainer.Explain(optionContainer.OptionToExplain);
                    return;
                }

                if (!Checker.CheckArguments(optionContainer))
                {
                    return;
                }

                Substitutions substitutions = optionContainer.SubsFile != null ? new Substitutions(optionContainer.SubsFile) : null;
                if (substitutions != null && !substitutions.TryValidate())
                {
                    return;
                }

                Suggester suggester = optionContainer.SuggestFile != null ? new Suggester(optionContainer.SuggestFile, substitutions) : null;

                Summarizer summarizer = new Summarizer(optionContainer.LogFile);

                Remodeler remodeler = new Remodeler(optionContainer.SourceFolder, optionContainer.SkipFolders, substitutions, suggester, summarizer, optionContainer.FilePattern);

                if (!remodeler.TryValidateSourceModel(allowUndefinedExtensions: optionContainer.LaxValidation))
                {
                    return;
                }

                remodeler.CanonicalizeNames();

                remodeler.ReplaceAbstractWithConcreteSubclasses();

                remodeler.ReplaceOrRemoveInvalidTypes();

                remodeler.ReplaceOrRemoveInvalidProperties();

                remodeler.RemoveImproperKeywords();

                remodeler.UpgradeContexts();

                if (optionContainer.GenerateNewIds)
                {
                    remodeler.GenerateNewVersionNumbersForUserIds();
                }

                if (!remodeler.TryValidateModifiedModel(allowUndefinedExtensions: optionContainer.LaxValidation))
                {
                    Console.WriteLine();

                    if (remodeler.TrySuggestSubstitutions())
                    {
                        Console.WriteLine($"A suggested substitutions file has been generated as {optionContainer.SuggestFile.Name}.");

                        if (substitutions != null)
                        {
                            Console.WriteLine($"This generated file includes (and possibly modifies) substitutions from the file {optionContainer.SubsFile.Name}");
                            Console.WriteLine("Try re-running the Remodel command using the new substitutions file instead of the previous one.");
                        }
                        else
                        {
                            Console.WriteLine($"Try re-running the Remodel command adding the option: --subs {optionContainer.SuggestFile.FullName}");
                        }

                        Console.WriteLine($"You may wish to modify property values in the {optionContainer.SuggestFile.Name} file before running the command.");
                        Console.WriteLine("For further assistance, type:");
                        Console.WriteLine("  Remodel --explain subs");
                    }
                    else
                    {
                        if (suggester == null)
                        {
                            Console.WriteLine("You may need to specify substitute values for one or more context specifiers, undefined co-types, or undefined property names.");
                            Console.WriteLine("For assistance on specifying substitute values via a substitutions file, type:");
                            Console.WriteLine("  Remodel --explain subs");
                            Console.WriteLine();
                            Console.WriteLine("The Remodel tool may be able to suggest values for a substitutions file to address issues in the model.");
                            Console.WriteLine("For assistance, type:");
                            Console.WriteLine("  Remodel --explain suggest");
                        }
                        else
                        {
                            Console.WriteLine("The Remodel tool was unable to determine appropriate substitute values needed for model validity.");
                            Console.WriteLine("You may need to experiment and manually determine modifications either to the substitutions file or to the model directly.");
                        }
                    }

                    return;
                }

                remodeler.WriteDestinationFiles(optionContainer.DestFolder);

                Filler filler = new Filler(optionContainer.SourceFolder, optionContainer.DestFolder, optionContainer.SkipFolders, summarizer, optionContainer.FilePattern);

                filler.FillHoles(optionContainer.FillType);

                summarizer.WriteLogFile();

                Announcer.Fin();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"FATAL ERROR: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
