namespace DTDLParser
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Class that fills holes in directory hierarchies via moving, copying, or linking.
    /// </summary>
    public class Filler
    {
        private DirectoryInfo sourceFolder;
        private DirectoryInfo destFolder;
        private DirectoryInfo[] skipFolders;
        private Summarizer summarizer;
        private string filePattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="Filler"/> class.
        /// </summary>
        /// <param name="sourceFolder">Root of directory tree containing DTDL source model.</param>
        /// <param name="destFolder">Root of directory tree to receive converted model (need not exist).</param>
        /// <param name="skipFolders">Roots of subtrees (within source tree) to skip converting.</param>
        /// <param name="summarizer">A <see cref="Summarizer"/> for recording operations performed.</param>
        /// <param name="filePattern">File pattern to use for enumerating files, e.g., "*.json".</param>
        public Filler(DirectoryInfo sourceFolder, DirectoryInfo destFolder, DirectoryInfo[] skipFolders, Summarizer summarizer, string filePattern)
        {
            this.sourceFolder = sourceFolder;
            this.destFolder = destFolder;
            this.skipFolders = skipFolders;
            this.summarizer = summarizer;
            this.filePattern = filePattern;

            foreach (DirectoryInfo skipFolder in skipFolders)
            {
                string submodelRoot = skipFolder.FullName.Substring(sourceFolder.FullName.Length + 1);
                summarizer.SubtreeSkipped(submodelRoot);
            }
        }

        /// <summary>
        /// The type of filling to perform.
        /// </summary>
        public enum FillType
        {
            /// <summary>Do not fill directory holes.</summary>
            None,

            /// <summary>Fill directory holes by moving subtrees from the source tree.</summary>
            Move,

            /// <summary>Fill directory holes by copying subtrees from the source tree.</summary>
            Copy,

#if NET6_0_OR_GREATER
            /// <summary>Fill directory holes by creating symbolic links to subdirectories in the source tree.</summary>
            Link,
#endif
        }

        /// <summary>
        /// Fill holes in directory hierarchy via moving, copying, or linking.
        /// </summary>
        /// <param name="fillType">The type of filling to perform.</param>
        public void FillHoles(FillType fillType)
        {
            if (fillType == FillType.None)
            {
                return;
            }

            Announcer.Heading("Filling holes in destination tree");

            foreach (DirectoryInfo skipFolder in this.skipFolders)
            {
                DirectoryInfo fillFolder = new DirectoryInfo(skipFolder.FullName.Replace(this.sourceFolder.FullName, this.destFolder.FullName));

                switch (fillType)
                {
                    case FillType.Move:
                        Announcer.Subheading($"Moving subtree {skipFolder.FullName} to {fillFolder.FullName}");
                        this.MoveSubtree(skipFolder, fillFolder);
                        break;
                    case FillType.Copy:
                        Announcer.Subheading($"Copying files matching pattern \"{this.filePattern}\" from {skipFolder.FullName} to {fillFolder.FullName}");
                        this.CopySubtree(skipFolder, fillFolder);
                        break;
#if NET6_0_OR_GREATER
                    case FillType.Link:
                        Announcer.Subheading($"Creating link {fillFolder.FullName} to subtree {skipFolder.FullName}");
                        this.LinkSubtree(skipFolder, fillFolder);
                        break;
#endif
                }
            }

            this.summarizer.HolesFilled(fillType);
        }

        private void MoveSubtree(DirectoryInfo skipFolder, DirectoryInfo fillFolder)
        {
            fillFolder.Parent.Create();
            skipFolder.MoveTo(fillFolder.FullName);
        }

        private void CopySubtree(DirectoryInfo skipFolder, DirectoryInfo fillFolder)
        {
            if (skipFolder.EnumerateFiles(this.filePattern).Any())
            {
                fillFolder.Create();

                foreach (FileInfo fileInfo in skipFolder.GetFiles(this.filePattern))
                {
                    fileInfo.CopyTo(Path.Combine(fillFolder.FullName, fileInfo.Name));
                }
            }

            foreach (DirectoryInfo subFolder in skipFolder.GetDirectories())
            {
                this.CopySubtree(subFolder, new DirectoryInfo(Path.Combine(fillFolder.FullName, subFolder.Name)));
            }
        }

#if NET6_0_OR_GREATER
        private void LinkSubtree(DirectoryInfo skipFolder, DirectoryInfo fillFolder)
        {
            fillFolder.Parent.Create();

            if (Directory.CreateSymbolicLink(fillFolder.FullName, skipFolder.FullName)?.LinkTarget == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    Could not create symbolic link to {skipFolder.FullName}");
                Console.ResetColor();
            }
        }
#endif
    }
}
