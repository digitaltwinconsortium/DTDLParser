namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The <c>DtdlExtension</c> class encapsulates the components of a DTDL extension JSON object.
    /// </summary>
    internal class DtdlExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtdlExtension"/> class.
        /// </summary>
        /// <param name="extensionElt">A <see cref="JsonLdElement"/> object representing the extension definition.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        internal DtdlExtension(JsonLdElement extensionElt, ParsingErrorCollection parsingErrorCollection)
        {
            this.ExtensionId = this.GetExtensionId(extensionElt, parsingErrorCollection);
            this.Context = this.GetContext(extensionElt, parsingErrorCollection);
            this.CheckType(extensionElt, parsingErrorCollection);

            foreach (JsonLdProperty jsonLdProp in extensionElt.Properties)
            {
                switch (jsonLdProp.Name)
                {
                    case "metamodel":
                        this.Metamodels = this.GetMetamodels(jsonLdProp.Values, parsingErrorCollection);
                        break;
                    case "model":
                        this.Models = this.GetModels(jsonLdProp.Values, parsingErrorCollection);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the identifier for the extension, which is also the extension context specifier.
        /// </summary>
        internal Dtmi ExtensionId { get; }

        /// <summary>
        /// Gets an array of <see cref="JsonLdContextComponent"/> representing the context components specified by the extension's '@context' property.
        /// </summary>
        internal JsonLdContextComponent[] Context { get; }

        /// <summary>
        /// Gets a list of <see cref="JsonLdElement"/> representing the metamodel definitions in the extension.
        /// </summary>
        internal List<JsonLdElement> Metamodels { get; }

        /// <summary>
        /// Gets a list of <see cref="JsonLdElement"/> representing the model definitions in the extension.
        /// </summary>
        internal List<JsonLdElement> Models { get; }

        private Dtmi GetExtensionId(JsonLdElement extensionElt, ParsingErrorCollection parsingErrorCollection)
        {
            if (extensionElt.Id == null)
            {
                parsingErrorCollection.Quit("missingExtensionId", element: extensionElt);
            }

            if (extensionElt.Id == string.Empty)
            {
                parsingErrorCollection.Quit("invalidExtensionIdElement", element: extensionElt);
            }

            if (!Dtmi.TryCreateDtmi(extensionElt.Id, out Dtmi extensionId) || extensionId.Fragment != string.Empty)
            {
                parsingErrorCollection.Quit(
                    "invalidExtensionSpecifier",
                    element: extensionElt,
                    identifier: extensionElt.Id);
            }

            if (extensionId.MajorVersion == 0)
            {
                parsingErrorCollection.Quit(
                    "missingExtensionVersion",
                    element: extensionElt,
                    identifier: extensionId.ToString());
            }

            return extensionId;
        }

        private JsonLdContextComponent[] GetContext(JsonLdElement extensionElt, ParsingErrorCollection parsingErrorCollection)
        {
            if (extensionElt.Context == null)
            {
                parsingErrorCollection.Quit(
                    "missingExtensionContext",
                    element: extensionElt,
                    contextId: this.ExtensionId);
            }

            if (!extensionElt.Context.Any())
            {
                parsingErrorCollection.Quit(
                    "emptyExtensionContext",
                    element: extensionElt,
                    contextId: this.ExtensionId);
            }

            return extensionElt.Context;
        }

        private void CheckType(JsonLdElement extensionElt, ParsingErrorCollection parsingErrorCollection)
        {
            if (extensionElt.Types == null)
            {
                parsingErrorCollection.Quit(
                    "missingExtensionType",
                    element: extensionElt,
                    contextId: this.ExtensionId);
            }

            if (!extensionElt.Types.Contains(ParserValues.LanguageExtensionType))
            {
                parsingErrorCollection.Quit(
                    "incorrectExtensionType",
                    element: extensionElt,
                    contextId: this.ExtensionId);
            }
        }

        private List<JsonLdElement> GetMetamodels(JsonLdValueCollection metamodelValueCollection, ParsingErrorCollection parsingErrorCollection)
        {
            if (metamodelValueCollection.Count == 0)
            {
                parsingErrorCollection.Quit(
                    "metamodelNoValue",
                    incidentValues: metamodelValueCollection,
                    contextId: this.ExtensionId);
            }

            if (metamodelValueCollection.Count > 1)
            {
                parsingErrorCollection.Quit(
                    "metamodelMultipleValues",
                    incidentValues: metamodelValueCollection,
                    contextId: this.ExtensionId);
            }

            JsonLdValue metamodelValue = metamodelValueCollection.Values.First();

            if (metamodelValue.ValueType != JsonLdValueType.Element)
            {
                parsingErrorCollection.Quit(
                    "metamodelNotObject",
                    incidentValue: metamodelValue,
                    contextId: this.ExtensionId);
            }

            if (metamodelValue.ElementValue.Graph == null)
            {
                parsingErrorCollection.Quit(
                    "metamodelNoGraph",
                    incidentValue: metamodelValue,
                    contextId: this.ExtensionId);
            }

            return metamodelValue.ElementValue.Graph;
        }

        private List<JsonLdElement> GetModels(JsonLdValueCollection modelValueCollection, ParsingErrorCollection parsingErrorCollection)
        {
            if (modelValueCollection.Count == 0)
            {
                parsingErrorCollection.Quit(
                    "modelNoValue",
                    incidentValues: modelValueCollection,
                    contextId: this.ExtensionId);
                return null;
            }

            if (modelValueCollection.Count > 1)
            {
                parsingErrorCollection.Quit(
                    "modelMultipleValues",
                    incidentValues: modelValueCollection,
                    contextId: this.ExtensionId);
                return null;
            }

            JsonLdValue modelValue = modelValueCollection.Values.First();

            if (modelValue.ValueType != JsonLdValueType.Element)
            {
                parsingErrorCollection.Quit(
                    "modelNotObject",
                    incidentValue: modelValue,
                    contextId: this.ExtensionId);
            }

            if (modelValue.ElementValue.Graph == null)
            {
                parsingErrorCollection.Quit(
                    "modelNoGraph",
                    incidentValue: modelValue,
                    contextId: this.ExtensionId);
            }

            return modelValue.ElementValue.Graph;
        }
    }
}
