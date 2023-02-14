namespace DTDLParser
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Static class that checks conditions on CLI arguments.
    /// </summary>
    public class Checker
    {
        /// <summary>
        /// Check CLI arguments for completeness, correctness, and consistency.
        /// </summary>
        /// <param name="optionContainer">A <see cref="OptionContainer"/> that holds the CLI options.</param>
        /// <returns>True if all conditions satisfied.</returns>
        public static bool CheckArguments(OptionContainer optionContainer)
        {
            if (optionContainer.SourceFolder == null || optionContainer.DestFolder == null)
            {
                DisplayOneLineError("Options '--source' and '--dest' are both required.");
                return false;
            }

            if (optionContainer.FillType != Filler.FillType.None && !optionContainer.SkipFolders.Any())
            {
                DisplayOneLineError("Option '--fill' not permitted without option '--skip'.", "fill");
                return false;
            }

            foreach (DirectoryInfo skipFolder in optionContainer.SkipFolders)
            {
                if (!skipFolder.FullName.StartsWith(optionContainer.SourceFolder.FullName))
                {
                    DisplayOneLineError($"Skip path {skipFolder.FullName} is not under source path {optionContainer.SourceFolder.FullName}", "skip");
                    return false;
                }
            }

            for (int ix1 = 0; ix1 < optionContainer.SkipFolders.Length; ++ix1)
            {
                DirectoryInfo skipFolder1 = optionContainer.SkipFolders[ix1];
                for (int ix2 = ix1 + 1; ix2 < optionContainer.SkipFolders.Length; ++ix2)
                {
                    DirectoryInfo skipFolder2 = optionContainer.SkipFolders[ix2];

                    if (skipFolder1.FullName == skipFolder2.FullName)
                    {
                        DisplayOneLineError($"Skip path duplicated in arguments: {skipFolder1.FullName}", "skip");
                        return false;
                    }

                    if (skipFolder1.FullName.StartsWith(skipFolder2.FullName) || skipFolder2.FullName.StartsWith(skipFolder1.FullName))
                    {
                        DisplayOneLineError($"Skip paths are not independent: {skipFolder1.FullName} and {skipFolder2.FullName}", "skip");
                        return false;
                    }
                }
            }

            if (!optionContainer.SourceFolder.Exists)
            {
                DisplayOneLineError($"Source directory does not exist: {optionContainer.SourceFolder.FullName}", "source");
                return false;
            }

            if (optionContainer.DestFolder.Exists && (optionContainer.DestFolder.EnumerateDirectories().Any() || optionContainer.DestFolder.EnumerateFiles().Any()))
            {
                DisplayOneLineError($"Destination directory not empty: {optionContainer.DestFolder.FullName}", "dest");
                return false;
            }

            foreach (DirectoryInfo skipFolder in optionContainer.SkipFolders)
            {
                if (!skipFolder.Exists)
                {
                    DisplayOneLineError($"Skip directory does not exist: {skipFolder.FullName}", "skip");
                    return false;
                }
            }

            if (optionContainer.SubsFile != null && !optionContainer.SubsFile.Exists)
            {
                DisplayOneLineError($"Subs file does not exist: {optionContainer.SubsFile.FullName}", "subs");
                return false;
            }

            if (optionContainer.SuggestFile != null && optionContainer.SuggestFile.Exists)
            {
                DisplayOneLineError($"Suggest file already exists: {optionContainer.SuggestFile.FullName}", "suggest");
                return false;
            }

            if (optionContainer.LogFile != null && optionContainer.LogFile.Exists)
            {
                DisplayOneLineError($"Log file already exists: {optionContainer.LogFile.FullName}", "log");
                return false;
            }

            return true;
        }

        private static void DisplayOneLineError(string errorMsg, string specificOption = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMsg);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("For more information, type:");

            if (specificOption != null)
            {
                Console.WriteLine($"  Remodel --explain {specificOption}");
            }
            else
            {
                Console.WriteLine("  Remodel --help");
            }
        }
    }
}
