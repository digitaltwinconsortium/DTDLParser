namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Code generator for <c>enum</c> that enumerates the kinds of concrete classes that are subclasses of the DTDL base class.
    /// </summary>
    public class BaseKindEnumGenerator : ITypeGenerator
    {
        private readonly string baseName;
        private readonly string typeName;
        private readonly List<string> elementNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseKindEnumGenerator"/> class.
        /// </summary>
        /// <param name="materialClasses">A a dictionary that maps from class name to a <see cref="MaterialClassDigest"/> object providing details about the named material class.</param>
        /// <param name="baseName">The base name for the parser's object model.</param>
        public BaseKindEnumGenerator(Dictionary<string, MaterialClassDigest> materialClasses, string baseName)
        {
            this.baseName = baseName;
            this.typeName = NameFormatter.FormatNameAsEnum(baseName);
            this.elementNames = new List<string>() { ParserGeneratorValues.ReferenceObverseName };
            this.elementNames.AddRange(materialClasses.Where(p => !p.Value.IsAbstract).Select(p => p.Key).ToList());
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsEnum csEnum = parserLibrary.Enum(Access.Public, this.typeName, sorted: true);
            csEnum.Summary($"Indicates the kind of {this.baseName}, meaning the concrete DTDL type assigned to the corresponding element in the model.");

            foreach (string elementName in this.elementNames)
            {
                csEnum.Value(NameFormatter.FormatNameAsEnumValue(elementName), $"The kind of the {this.baseName} is {elementName}.");
            }
        }
    }
}
