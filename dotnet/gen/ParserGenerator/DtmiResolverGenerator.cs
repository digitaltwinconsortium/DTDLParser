namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>DtmiResolver</c>, <c>DtmiResolverAsync</c>, <c>DtmiPartialResolver</c>, and <c>DtmiPartialResolverAsync</c> delegates.
    /// </summary>
    public class DtmiResolverGenerator : ITypeGenerator
    {
        private readonly bool isLayeringSupported;

        /// <summary>
        /// Initializes a new instance of the <see cref="DtmiResolverGenerator"/> class.
        /// </summary>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public DtmiResolverGenerator(bool isLayeringSupported)
        {
            this.isLayeringSupported = isLayeringSupported;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            this.GenerateDtmiResolver(parserLibrary, isAsync: true);
            this.GenerateDtmiResolver(parserLibrary, isAsync: false);
            this.GenerateDtmiPartialResolver(parserLibrary, isAsync: true);
            this.GenerateDtmiPartialResolver(parserLibrary, isAsync: false);
        }

        private void GenerateDtmiResolver(CsLibrary parserLibrary, bool isAsync)
        {
            string delegateName = this.GetFullName("DtmiResolver", isAsync);
            string usedByMethodName = this.GetFullName("Parse", isAsync);
            string eltDesignator = this.isLayeringSupported ? "all layers of the" : "all";
            string enumTypeName = this.GetFullName("I{0}Enumerable", isAsync);

            string tokenArg = isAsync ? ", CancellationToken" : string.Empty;
            CsDelegate resolverDelegate = parserLibrary.Delegate(Access.Public, Novelty.Normal, $"{enumTypeName}<string>", delegateName);
            resolverDelegate.Summary($"The <c>{delegateName}</c> delegate enables a client of the parser to set a callback that will be invoked when the parser encounters a context reference or a dependent reference to a DTMI for which it has no definition.");
            resolverDelegate.Summary($"This callback will be called during execution of the <see cref=\"ModelParser.{usedByMethodName}({enumTypeName}{{string}}, DtdlParseLocator{tokenArg})\"/> API.");
            resolverDelegate.Summary("The resolver accepts a collection of DTMIs as an argument, so a batch of identifiers can be resolved together; this reduces the count of network round trips when resolution requires remote retrieval.");
            resolverDelegate.Summary("If the resolver is unable to obtain a definition for the DTMIs, it should return null for the enumeration.");
            resolverDelegate.Param("IReadOnlyCollection<Dtmi>", "dtmis", "A collection of DTMIs to resolve.");

            if (isAsync)
            {
                resolverDelegate.Param("CancellationToken", "cancellationToken", "A <c>CancellationToken</c> to cancel the resolution prematurely.", "default");
            }

            resolverDelegate.Returns($"An enumeration of JSON strings that collectively include definitions for {eltDesignator} elements with the given DTMIs. The order is not required to match the order of the DTMIs passed to the resolver.");
        }

        private void GenerateDtmiPartialResolver(CsLibrary parserLibrary, bool isAsync)
        {
            if (this.isLayeringSupported)
            {
                string delegateName = this.GetFullName("DtmiPartialResolver", isAsync);
                string usedByMethodName = this.GetFullName("Parse", isAsync);
                string enumTypeName = this.GetFullName("I{0}Enumerable", isAsync);

                CsDelegate resolverDelegate = parserLibrary.Delegate(Access.Public, Novelty.Normal, $"{enumTypeName}<string>", delegateName);
                resolverDelegate.Summary($"The <c>{delegateName}</c> delegate enables a client of the parser to set a callback that will be invoked to retrieve layer definitions for elements that may be only partially defined in the model.");
                resolverDelegate.Summary($"When the DTDL passed to the <c>{usedByMethodName}</c> method contains an element whose type allows a partial definition and which has a user identifier, the parser calls the registered <c>DtmiPartialResolver</c> to obtain any additional layer definitions for the element.");
                resolverDelegate.Summary("The partial resolver accepts a collection of <seealso cref=\"DtmiLayerInfo\"/> structs as an argument, each of which indicates not only the DTMI of the element to define but also a collection of layers that should not receive definitions because they are already known for the element.");
                resolverDelegate.Summary("If there are no additional layer definitions, the resolver should return an empty collection.");
                resolverDelegate.Param("IReadOnlyCollection<DtmiLayerInfo>", "dtmiLayers", "A collection of <seealso cref=\"DtmiLayerInfo\"/> structs characterizing the elements to partially resolve.");

                if (isAsync)
                {
                    resolverDelegate.Param("CancellationToken", "cancellationToken", "A <c>CancellationToken</c> to cancel the resolution prematurely.", "default");
                }

                resolverDelegate.Returns("An enumeration of JSON strings that collectively include definitions for any layers of the elements that are not already known.");
                resolverDelegate.Remarks($"If there is no registered <c>DtmiPartialResolver</c>, the parser will assume that the DTDL passed to the <c>{usedByMethodName}</c> method includes all layer definitions for any identifier that has at least one layer definition.");
                resolverDelegate.Remarks($"For example, if models passed to {usedByMethodName}() include a reference to Foo and a definition for Foo#X, the parser will assume (if there is no partial resolver) that Foo has only a layer \"X\" definition and no base definition.");
                resolverDelegate.Remarks($"On the other hand, if the models passed to {usedByMethodName}() include a reference to Bar and no definition for Bar in any layer, the parser will call the DtmiResolver just as it does now.");
            }
        }

        private string GetFullName(string baseName, bool isAsync)
        {
            string methodDecorator = isAsync ? "Async" : string.Empty;
            return baseName.Contains("{0}") ? string.Format(baseName, methodDecorator) : $"{baseName}{methodDecorator}";
        }

        private string GetReturnType(string baseType, bool isAsync)
        {
            if (baseType == "void")
            {
                return isAsync ? $"Task" : baseType;
            }
            else
            {
                return isAsync ? $"Task<{baseType}>" : baseType;
            }
        }
    }
}
