namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// A factory for generating objects that export the <see cref="IDescendantControl"/> interface.
    /// </summary>
    public class DescendantControlFactory
    {
        private string kindEnum;
        private string kindProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DescendantControlFactory"/> class.
        /// </summary>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        /// <param name="kindProperty">The property on the DTDL base obverse class that indicates the kind of DTDL element.</param>
        public DescendantControlFactory(string kindEnum, string kindProperty)
        {
            this.kindEnum = kindEnum;
            this.kindProperty = kindProperty;
        }

        /// <summary>
        /// Create objects that exports the <see cref="IDescendantControl"/> interface.
        /// </summary>
        /// <param name="descendantControlDigest">A <see cref="DescendantControlDigest"/> object that describes a descendant control defined in the metamodel digest.</param>
        /// <returns>List of the objects created.</returns>
        public List<IDescendantControl> Create(DescendantControlDigest descendantControlDigest)
        {
            List<IDescendantControl> descendantControls = new List<IDescendantControl>();

            if (descendantControlDigest.ExcludeType != null)
            {
                descendantControls.Add(new DescendantControlExcludeType(
                    descendantControlDigest.DtdlVersion,
                    descendantControlDigest.RootClass,
                    descendantControlDigest.PropertyNames,
                    descendantControlDigest.IsNarrow,
                    descendantControlDigest.ExcludeType,
                    this.kindEnum,
                    this.kindProperty));
            }

            if (descendantControlDigest.DatatypeProperty != null)
            {
                descendantControls.Add(new DescendantControlDatatypeProperty(
                    descendantControlDigest.DtdlVersion,
                    descendantControlDigest.RootClass,
                    descendantControlDigest.PropertyNames,
                    descendantControlDigest.IsNarrow,
                    descendantControlDigest.DatatypeProperty));
            }

            if (descendantControlDigest.MaxDepth != null)
            {
                if (descendantControlDigest.ImportProperties != null)
                {
                    descendantControls.Add(new DescendantControlImportProperties(
                        descendantControlDigest.DtdlVersion,
                        descendantControlDigest.RootClass,
                        descendantControlDigest.DefiningClass,
                        descendantControlDigest.PropertyNames,
                        descendantControlDigest.IsNarrow,
                        descendantControlDigest.ImportProperties,
                        descendantControlDigest.MaxDepth,
                        descendantControlDigest.AllowSelf));
                }
                else
                {
                    descendantControls.Add(new DescendantControlMaxDepth(
                        descendantControlDigest.DtdlVersion,
                        descendantControlDigest.RootClass,
                        descendantControlDigest.PropertyNames,
                        descendantControlDigest.IsNarrow,
                        descendantControlDigest.MaxDepth,
                        descendantControlDigest.AllowSelf));
                }
            }

            if (descendantControlDigest.MaxCount != null)
            {
                descendantControls.Add(new DescendantControlMaxCount(
                    descendantControlDigest.DtdlVersion,
                    descendantControlDigest.RootClass,
                    descendantControlDigest.PropertyNames,
                    descendantControlDigest.IsNarrow,
                    descendantControlDigest.MaxCount,
                    descendantControlDigest.AllowSelf));
            }

            return descendantControls;
        }
    }
}
