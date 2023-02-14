namespace DTDLParser
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Enumeration of sample file names.
    /// </summary>
    public class FileEnumeration : IEnumerable<string>
    {
        private string samplesConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEnumeration"/> class.
        /// </summary>
        /// <param name="samplesConfig">A string specifying how the sample files should be selected.</param>
        public FileEnumeration(string samplesConfig)
        {
            this.samplesConfig = samplesConfig;
        }

        /// <inheritdoc/>
        public IEnumerator<string> GetEnumerator()
        {
            int sepPos = this.samplesConfig.IndexOf(':');
            string designator = this.samplesConfig.Substring(0, sepPos);
            string designation = this.samplesConfig.Substring(sepPos + 1);

            if (designator == "pattern")
            {
                string sampleFolder = Path.GetDirectoryName(designation);
                string samplePattern = Path.GetFileName(designation);

                return Directory.EnumerateFiles(sampleFolder, samplePattern, SearchOption.TopDirectoryOnly).GetEnumerator();
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
