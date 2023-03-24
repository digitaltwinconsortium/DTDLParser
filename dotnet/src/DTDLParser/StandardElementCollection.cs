namespace DTDLParser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A collection of values of standard elements from the DTDL metamodel.
    /// </summary>
    internal partial class StandardElementCollection
    {
        private static readonly Model EndogenousStandardModel;
        private static readonly Dictionary<Dtmi, HashSet<Dtmi>> EndogenousElementReferences;
        private static readonly Dictionary<Dtmi, Dtmi> EndogenousAliases;

        private Model exogenousStandardModel;
        private Dictionary<Dtmi, HashSet<Dtmi>> exogenousElementReferences;

        static StandardElementCollection()
        {
            EndogenousStandardModel = new Model();
            EndogenousElementReferences = new Dictionary<Dtmi, HashSet<Dtmi>>();

            var objectPropertyInfoList = new List<ParsedObjectPropertyInfo>();
            var endogenousAggregateContext = new AggregateContext(new ContextCollection(), new SupplementalTypeCollection(), allowUndefinedExtensions: false, suppressDefinitionMerging: true);
            var parsingErrorCollection = new ParsingErrorCollection();

            ParseResourceIntoEndogenousStandardModel(Assembly.GetExecutingAssembly().GetManifestResourceNames().First(), objectPropertyInfoList, endogenousAggregateContext, parsingErrorCollection);

            UpdateElementReferences(EndogenousElementReferences, objectPropertyInfoList);

            EndogenousStandardModel.SetObjectProperties(objectPropertyInfoList, parsingErrorCollection, standardElementCollection: null, preserveElementAliases: true);

            EndogenousAliases = new Dictionary<Dtmi, Dtmi>();
            PopulateEndogenousAliases();

            parsingErrorCollection.ThrowIfAny();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardElementCollection"/> class.
        /// </summary>
        internal StandardElementCollection()
        {
            this.exogenousStandardModel = new Model();
            this.exogenousElementReferences = new Dictionary<Dtmi, HashSet<Dtmi>>();
        }

        /// <summary>
        /// Add model-level elements from a DTDL extension to the collection.
        /// </summary>
        /// <param name="extensionId">The identifier of the extension.</param>
        /// <param name="modelGraphElements">A list of <see cref="JsonLdElement"/> of DTDL model elements.</param>
        /// <param name="supplementalTypeCollection">A <see cref="SupplementalTypeCollection"/> containing information about supplemental types from the extension.</param>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="preserveElementAliases">True if the method should avoid resolving any aliases before adding the element and referenced elements.</param>
        internal void AddExtensionModel(Dtmi extensionId, List<JsonLdElement> modelGraphElements, SupplementalTypeCollection supplementalTypeCollection, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, bool preserveElementAliases)
        {
            if (modelGraphElements == null)
            {
                return;
            }

            var objectPropertyInfoList = new List<ParsedObjectPropertyInfo>();
            var elementPropertyConstraints = new List<ElementPropertyConstraint>();

            foreach (JsonLdElement modelGraphElement in modelGraphElements)
            {
                ModelParser.ParseElement(this.exogenousStandardModel, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, modelGraphElement, globalize: true, allowReservedIds: true, tolerateSolecisms: false);
            }

            foreach (ParsedObjectPropertyInfo objectPropertyInfo in objectPropertyInfoList)
            {
                if (!this.exogenousStandardModel.Dict.ContainsKey(objectPropertyInfo.ReferencedElementId))
                {
                    this.TryAddElementToModel(this.exogenousStandardModel, objectPropertyInfo.ReferencedElementId, preserveElementAliases);
                }
            }

            UpdateElementReferences(this.exogenousElementReferences, objectPropertyInfoList);

            this.exogenousStandardModel.SetObjectProperties(objectPropertyInfoList, parsingErrorCollection, standardElementCollection: this, preserveElementAliases: preserveElementAliases);

            parsingErrorCollection.ThrowIfAny();

            this.exogenousStandardModel.CheckElementPropertyConstraints(elementPropertyConstraints, supplementalTypeCollection, standardElementCollection: this, parsingErrorCollection, preserveElementAliases: preserveElementAliases);
            this.exogenousStandardModel.ApplyTransformations(parsingErrorCollection);
            this.exogenousStandardModel.CheckRestrictionsAndSiblingConstraints(elementPropertyConstraints, supplementalTypeCollection, parsingErrorCollection);

            parsingErrorCollection.ThrowIfAny();
        }

        /// <summary>
        /// Resolve an element identifier through any aliases until a non-aliased element identifier is reached.
        /// </summary>
        /// <param name="elementId">The element identifier to resolve.</param>
        /// <returns>The reselved element identifier.</returns>
        internal Dtmi ResolveAnyAliases(Dtmi elementId)
        {
            return EndogenousAliases.TryGetValue(elementId, out Dtmi resolvedElementId) ? resolvedElementId : elementId;
        }

        /// <summary>
        /// Add an element from the collection to the model; also add any transitively referenced elements in the collection.
        /// </summary>
        /// <param name="model">The model to add the element to.</param>
        /// <param name="elementId">The identifier of the element to add.</param>
        /// <param name="preserveElementAliases">True if the method should avoid resolving any aliases before adding the element and referenced elements.</param>
        /// <returns>True if the element is present in the collection.</returns>
        internal bool TryAddElementToModel(Model model, Dtmi elementId, bool preserveElementAliases)
        {
            Dtmi resolvedElementId;
            if (preserveElementAliases || !EndogenousAliases.TryGetValue(elementId, out resolvedElementId))
            {
                resolvedElementId = elementId;
            }

            return this.TryAddElementFromStandardModelToModel(EndogenousStandardModel, EndogenousElementReferences, model, resolvedElementId, preserveElementAliases)
                || this.TryAddElementFromStandardModelToModel(this.exogenousStandardModel, this.exogenousElementReferences, model, resolvedElementId, preserveElementAliases);
        }

        private static void UpdateElementReferences(Dictionary<Dtmi, HashSet<Dtmi>> elementReferences, List<ParsedObjectPropertyInfo> objectPropertyInfoList)
        {
            foreach (ParsedObjectPropertyInfo objectPropertyInfo in objectPropertyInfoList)
            {
                if (!elementReferences.ContainsKey(objectPropertyInfo.ElementId))
                {
                    elementReferences[objectPropertyInfo.ElementId] = new HashSet<Dtmi>();
                }

                elementReferences[objectPropertyInfo.ElementId].Add(objectPropertyInfo.ReferencedElementId);
            }
        }

        private static void ParseResourceIntoEndogenousStandardModel(string resourceName, List<ParsedObjectPropertyInfo> objectPropertyInfoList, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            StreamReader resourceReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName));

            JsonLdValueCollection valueCollection = JsonLdReader.ReadForParse(resourceReader.ReadToEnd(), 0, null, parsingErrorCollection);

            foreach (JsonLdValue jsonLdValue in valueCollection.Values)
            {
                ModelParser.ParseElement(EndogenousStandardModel, objectPropertyInfoList, null, aggregateContext.GetChildContext(jsonLdValue.ElementValue, parsingErrorCollection), parsingErrorCollection, jsonLdValue.ElementValue, globalize: true, allowReservedIds: true, tolerateSolecisms: false);
            }

            parsingErrorCollection.ThrowIfAny();
        }

        private bool TryAddElementFromStandardModelToModel(Model standardModel, Dictionary<Dtmi, HashSet<Dtmi>> elementReferences, Model model, Dtmi elementId, bool preserveElementAliases)
        {
            if (!standardModel.Dict.TryGetValue(elementId, out var element))
            {
                return false;
            }

            model.Dict[elementId] = element;

            if (elementReferences.TryGetValue(elementId, out HashSet<Dtmi> referencedElementIds))
            {
                foreach (Dtmi referencedElementId in referencedElementIds)
                {
                    if (!model.Dict.ContainsKey(referencedElementId))
                    {
                        this.TryAddElementToModel(model, referencedElementId, preserveElementAliases);
                    }
                }
            }

            return true;
        }
    }
}
