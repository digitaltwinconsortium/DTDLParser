namespace DTDLParser
{
    using System.IO;

    /// <summary>
    /// Custom container for holding CLI options.
    /// </summary>
    public class OptionContainer
    {
        /// <summary>Gets or sets the root of directory tree containing DTDL source model.</summary>
        public DirectoryInfo SourceFolder { get; set; }

        /// <summary>Gets or sets the root of directory tree to receive converted model (need not exist).</summary>
        public DirectoryInfo DestFolder { get; set; }

        /// <summary>Gets or sets the roots of subtrees (within source tree) to skip converting.</summary>
        public DirectoryInfo[] SkipFolders { get; set; }

        /// <summary>Gets or sets the type of filling to perform on holes left by skip.</summary>
        public Filler.FillType FillType { get; set; }

        /// <summary>Gets or sets the JSON file containing IRI/term substitutions for contexts, types, and properties.</summary>
        public FileInfo SubsFile { get; set; }

        /// <summary>Gets or sets a JSON file suggesting IRI/term substitutions for contexts, types, and properties.</summary>
        public FileInfo SuggestFile { get; set; }

        /// <summary>Gets or sets the output JSON file to receive summary of changes performed.</summary>
        public FileInfo LogFile { get; set; }

        /// <summary>Gets or sets a value indicating whether to generate new version numbers for user IDs in model.</summary>
        public bool GenerateNewIds { get; set; }

        /// <summary>Gets or sets a value indicating whether validation should be lax, allowing undefined extensions.</summary>
        public bool LaxValidation { get; set; }

        /// <summary>Gets or sets the file pattern to use for enumerating files, e.g., "*.json".</summary>
        public string FilePattern { get; set; }

        /// <summary>Gets or sets the option for which to display documentation instead of performing a remodel.</summary>
        public string OptionToExplain { get; set; }
    }
}
