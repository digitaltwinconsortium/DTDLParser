namespace DTDLParser
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Enumeration of tutorial file names.
    /// </summary>
    public class FileEnumeration : IEnumerable<string>
    {
        private string tutorialsConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEnumeration"/> class.
        /// </summary>
        /// <param name="tutorialsConfig">A string specifying how the tutorial files should be selected.</param>
        public FileEnumeration(string tutorialsConfig)
        {
            this.tutorialsConfig = tutorialsConfig;
        }

        /// <inheritdoc/>
        public IEnumerator<string> GetEnumerator()
        {
            int sepPos = this.tutorialsConfig.IndexOf(':');
            string designator = this.tutorialsConfig.Substring(0, sepPos);
            string designation = this.tutorialsConfig.Substring(sepPos + 1);

            if (designator == "pattern")
            {
                string tutorialFolder = Path.GetDirectoryName(designation);
                string tutorialPattern = Path.GetFileName(designation);

                return Directory.EnumerateFiles(tutorialFolder, tutorialPattern, SearchOption.TopDirectoryOnly).GetEnumerator();
            }
            else if (designator == "manifest")
            {
                return new FileEnumerator(designation);
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
