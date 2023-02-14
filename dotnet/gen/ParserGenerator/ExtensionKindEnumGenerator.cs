namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>enum</c>.
    /// </summary>
    public class ExtensionKindEnumGenerator : ITypeGenerator
    {
        private readonly List<string> extensionKinds;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionKindEnumGenerator"/> class.
        /// </summary>
        /// <param name="extensionKinds">A list of strings that indicate the extension points defined in the metamodel digest.</param>
        public ExtensionKindEnumGenerator(List<string> extensionKinds)
        {
            this.extensionKinds = extensionKinds;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsEnum csEnum = parserLibrary.Enum(Access.Public, NameFormatter.FormatNameAsEnum("Extension"), sorted: false);
            csEnum.Summary($"Indicates the kind of extension.");
            csEnum.Remarks("DTDL has a limited number of types that can be subtyped by DTDL language extensions.");
            csEnum.Remarks("Values of this enum are returned by the <see cref=\"DTSupplementalTypeInfo.ExtensionKind\"/> property to indicate the extensible DTDL type that is an ancestor of the supplemental type.");

            foreach (string extensionKind in this.extensionKinds)
            {
                csEnum.Value(NameFormatter.FormatNameAsEnumValue(extensionKind), $"The kind of extension is {extensionKind}.");
            }
        }
    }
}
