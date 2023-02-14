namespace DTDLParser
{
    using System.CommandLine;
    using System.CommandLine.Binding;
    using System.IO;

    /// <summary>
    /// Custom arguemnt binder for CLI.
    /// </summary>
    public class ArgBinder : BinderBase<OptionContainer>
    {
        private readonly Option<DirectoryInfo> sourceFolder;
        private readonly Option<DirectoryInfo> destFolder;
        private readonly Option<DirectoryInfo[]> skipFolders;
        private readonly Option<Filler.FillType> fillType;
        private readonly Option<FileInfo> subsFile;
        private readonly Option<FileInfo> suggestFile;
        private readonly Option<FileInfo> logFile;
        private readonly Option<bool> generateNewIds;
        private readonly Option<bool> laxValidation;
        private readonly Option<string> filePattern;
        private readonly Option<string> optionToExplain;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgBinder"/> class.
        /// </summary>
        /// <param name="sourceFolder">Root of directory tree containing DTDL source model.</param>
        /// <param name="destFolder">Root of directory tree to receive converted model (need not exist).</param>
        /// <param name="skipFolders">Roots of subtrees (within source tree) to skip converting.</param>
        /// <param name="fillType">The type of filling to perform on holes left by <paramref name="skipFolders"/>.</param>
        /// <param name="subsFile">JSON file containing IRI/term substitutions for contexts, types, and properties.</param>
        /// <param name="suggestFile">Output JSON file containing suggested IRI/term substitutions for contexts, types, and properties.</param>
        /// <param name="logFile">Output JSON file to receive summary of changes performed.</param>
        /// <param name="generateNewIds">Generate new version numbers for user IDs in model.</param>
        /// <param name="laxValidation">Lax validation, allowing undefined extensions.</param>
        /// <param name="filePattern">File pattern to use for enumerating files, e.g., "*.json".</param>
        /// <param name="optionToExplain">Display documentation on this option instead of performing a remodel.</param>
        public ArgBinder(
            Option<DirectoryInfo> sourceFolder,
            Option<DirectoryInfo> destFolder,
            Option<DirectoryInfo[]> skipFolders,
            Option<Filler.FillType> fillType,
            Option<FileInfo> subsFile,
            Option<FileInfo> suggestFile,
            Option<FileInfo> logFile,
            Option<bool> generateNewIds,
            Option<bool> laxValidation,
            Option<string> filePattern,
            Option<string> optionToExplain)
        {
            this.sourceFolder = sourceFolder;
            this.destFolder = destFolder;
            this.skipFolders = skipFolders;
            this.fillType = fillType;
            this.subsFile = subsFile;
            this.suggestFile = suggestFile;
            this.logFile = logFile;
            this.generateNewIds = generateNewIds;
            this.laxValidation = laxValidation;
            this.filePattern = filePattern;
            this.optionToExplain = optionToExplain;
        }

        /// <inheritdoc/>
        protected override OptionContainer GetBoundValue(BindingContext bindingContext) =>
            new OptionContainer()
            {
                SourceFolder = bindingContext.ParseResult.GetValueForOption(this.sourceFolder),
                DestFolder = bindingContext.ParseResult.GetValueForOption(this.destFolder),
                SkipFolders = bindingContext.ParseResult.GetValueForOption(this.skipFolders),
                FillType = bindingContext.ParseResult.GetValueForOption(this.fillType),
                SubsFile = bindingContext.ParseResult.GetValueForOption(this.subsFile),
                SuggestFile = bindingContext.ParseResult.GetValueForOption(this.suggestFile),
                LogFile = bindingContext.ParseResult.GetValueForOption(this.logFile),
                GenerateNewIds = bindingContext.ParseResult.GetValueForOption(this.generateNewIds),
                LaxValidation = bindingContext.ParseResult.GetValueForOption(this.laxValidation),
                FilePattern = bindingContext.ParseResult.GetValueForOption(this.filePattern),
                OptionToExplain = bindingContext.ParseResult.GetValueForOption(this.optionToExplain),
            };
    }
}
