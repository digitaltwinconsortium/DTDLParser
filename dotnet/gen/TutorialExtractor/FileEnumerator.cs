namespace DTDLParser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Enumerator of tutorial file names.
    /// </summary>
    public class FileEnumerator : IEnumerator<string>
    {
        private const string LeftSentinel = "](";
        private const string RightSentinel = ")";

        private StreamReader manifestReader;
        private string folder;
        private string manifestLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEnumerator"/> class.
        /// </summary>
        /// <param name="manifestFilename">Name of the file containing a manifest of file names to enumerate.</param>
        public FileEnumerator(string manifestFilename)
        {
            FileStream manifestStream = new FileStream(manifestFilename, FileMode.Open, FileAccess.Read);
            this.manifestReader = new StreamReader(manifestStream);
            this.folder = Path.GetDirectoryName(manifestFilename);
            this.manifestLine = null;
        }

        /// <inheritdoc/>
        public string Current
        {
            get
            {
                int startPos = this.manifestLine.IndexOf(LeftSentinel) + LeftSentinel.Length;
                int endPos = this.manifestLine.IndexOf(RightSentinel);
                return Path.Combine(this.folder, this.manifestLine.Substring(startPos, endPos - startPos));
            }
        }

        /// <inheritdoc/>
        object IEnumerator.Current { get => this.Current; }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            do
            {
                this.manifestLine = this.manifestReader.ReadLine();
            }
            while (this.manifestLine != null && !this.manifestLine.Contains(LeftSentinel));

            return this.manifestLine != null;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
