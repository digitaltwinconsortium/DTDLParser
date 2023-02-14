namespace DTDLParser
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Generator for a C# library of code files.
    /// </summary>
    public class CsLibrary
    {
        private const string FileExt = ".g.cs";

        private readonly string outputDirectory;
        private readonly string libNamespace;
        private readonly string copyrightHolder;

        private List<string> systemNamespaces;
        private List<string> otherNamespaces;
        private List<string> subNamespaces;
        private List<ICsFile> csFiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsLibrary"/> class.
        /// </summary>
        /// <param name="outputDirectory">Path to directory in which to create generated code files.</param>
        /// <param name="libNamespace">Namespace for the library.</param>
        /// <param name="copyrightHolder">Optional copyright holder for XML comments; if not provided, no XML comment header will be written.</param>
        public CsLibrary(string outputDirectory, string libNamespace, string copyrightHolder = null)
        {
            this.outputDirectory = outputDirectory;
            this.libNamespace = libNamespace;
            this.copyrightHolder = copyrightHolder;

            this.systemNamespaces = new List<string>();
            this.otherNamespaces = new List<string>();
            this.subNamespaces = new List<string>();
            this.csFiles = new List<ICsFile>();
        }

        /// <summary>
        /// Add a "using" declaration to all code files.
        /// </summary>
        /// <param name="usingNamespace">The namespace for the "using" declaration.</param>
        public void Using(string usingNamespace)
        {
            if (usingNamespace == "System" || usingNamespace.StartsWith("System."))
            {
                this.systemNamespaces.Add(usingNamespace);
            }
            else
            {
                this.otherNamespaces.Add(usingNamespace);
            }
        }

        /// <summary>
        /// Add a sub-namespace to the library.
        /// </summary>
        /// <param name="subNamespace">Additional component to be added to the library namespace.</param>
        public void SubNamespace(string subNamespace)
        {
            this.subNamespaces.Add(subNamespace);
        }

        /// <summary>
        /// Add a C# interface to the library.
        /// </summary>
        /// <param name="access">Access level of interface.</param>
        /// <param name="typeName">The name of the interface being declared.</param>
        /// <param name="exports">Interfaces exported by this interface.</param>
        /// <returns>The <see cref="CsInterface"/> object added.</returns>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsInterface Interface(Access access, string typeName, string exports = null, string subNamespace = null)
        {
            CsInterface csInterface = new CsInterface(access, typeName, exports, subNamespace);
            this.csFiles.Add(csInterface);
            return csInterface;
        }

        /// <summary>
        /// Add a C# class to the library.
        /// </summary>
        /// <param name="access">Access level of class.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="typeName">The name of the class being declared.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="completeness">Partial or Full.</param>
        /// <param name="exports">Base class and/or interfaces exported by the class being declared.</param>
        /// <returns>The <see cref="CsClass"/> object added.</returns>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsClass Class(Access access, Novelty novelty, string typeName, Multiplicity multiplicity = Multiplicity.Instance, Completeness completeness = Completeness.Full, string exports = null, string subNamespace = null)
        {
            CsClass csClass = new CsClass(access, novelty, typeName, multiplicity, completeness, exports, subNamespace);
            this.csFiles.Add(csClass);
            return csClass;
        }

        /// <summary>
        /// Add a C# struct to the library.
        /// </summary>
        /// <param name="access">Access level of struct.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="typeName">The name of the struct being declared.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="completeness">Partial or Full.</param>
        /// <param name="exports">Base struct and/or interfaces exported by the struct being declared.</param>
        /// <returns>The <see cref="CsStruct"/> object added.</returns>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsStruct Struct(Access access, Novelty novelty, string typeName, Multiplicity multiplicity = Multiplicity.Instance, Completeness completeness = Completeness.Full, string exports = null, string subNamespace = null)
        {
            CsStruct csStruct = new CsStruct(access, novelty, typeName, multiplicity, completeness, exports, subNamespace);
            this.csFiles.Add(csStruct);
            return csStruct;
        }

        /// <summary>
        /// Add a C# enum to the library.
        /// </summary>
        /// <param name="access">Access level of enum.</param>
        /// <param name="typeName">The name of the enum being declared.</param>
        /// <param name="sorted">True if the enum values should be sorted by name.</param>
        /// <returns>The <see cref="CsEnum"/> object added.</returns>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsEnum Enum(Access access, string typeName, bool sorted = false, string subNamespace = null)
        {
            CsEnum csEnum = new CsEnum(access, typeName, sorted, subNamespace);
            this.csFiles.Add(csEnum);
            return csEnum;
        }

        /// <summary>
        /// Add a C# delegate to the library.
        /// </summary>
        /// <param name="access">Access level of delegate.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="type">Type of delegate.</param>
        /// <param name="name">Name of delegate.</param>
        /// <param name="subNamespace">A sub-namespace in which to declare the delefate.</param>
        /// <returns>The <see cref="CsDelegate"/> object added.</returns>
        public CsDelegate Delegate(Access access, Novelty novelty, string type, string name, string subNamespace = null)
        {
            CsDelegate csDelegate = new CsDelegate(access, novelty, type, name, subNamespace);
            this.csFiles.Add(csDelegate);
            return csDelegate;
        }

        /// <summary>
        /// Generate code files for the library.
        /// </summary>
        /// <returns>An array of generated filenames.</returns>
        public string[] Generate()
        {
            if (!Directory.Exists(this.outputDirectory))
            {
                Directory.CreateDirectory(this.outputDirectory);
            }

            List<string> filePaths = new List<string>();

            foreach (ICsFile csFile in this.csFiles)
            {
                string fileName = csFile.TypeName + FileExt;
                string filePath = Path.Combine(this.outputDirectory, fileName);
                CodeWriter codeWriter = new CodeWriter(filePath);

                if (this.copyrightHolder != null)
                {
                    codeWriter.WriteLine($"// <copyright file=\"{fileName}\" company=\"{this.copyrightHolder}\">");
                    codeWriter.WriteLine($"// Copyright (c) {this.copyrightHolder}. All rights reserved.");
                    codeWriter.WriteLine("// </copyright>");
                    codeWriter.Break();
                }

                codeWriter.WriteLine("/* This is an auto-generated file.  Do not modify. */");
                codeWriter.Break();

                codeWriter.WriteLine($"namespace {this.GetNamespace(csFile.SubNamespace)}");
                codeWriter.OpenScope();

                foreach (string usingNamespace in this.systemNamespaces)
                {
                    codeWriter.WriteLine($"using {usingNamespace};");
                }

                foreach (string usingNamespace in this.otherNamespaces)
                {
                    codeWriter.WriteLine($"using {usingNamespace};");
                }

                if (csFile.SubNamespace != null)
                {
                    codeWriter.WriteLine($"using {this.libNamespace};");
                }

                foreach (string subNamespace in this.subNamespaces)
                {
                    if (subNamespace != csFile.SubNamespace)
                    {
                        codeWriter.WriteLine($"using {this.GetNamespace(subNamespace)};");
                    }
                }

                codeWriter.Break();

                csFile.GenerateCode(codeWriter);

                codeWriter.CloseScope();

                codeWriter.Close();

                filePaths.Add(filePath);
            }

            return filePaths.ToArray();
        }

        private string GetNamespace(string subNamespace)
        {
            return subNamespace != null ? $"{this.libNamespace}.{subNamespace}" : this.libNamespace;
        }
    }
}
