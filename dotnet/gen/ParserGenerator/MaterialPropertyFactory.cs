namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A factory for generating <see cref="MaterialProperty"/> objects.
    /// </summary>
    public class MaterialPropertyFactory
    {
        private List<int> dtdlVersions;
        private Dictionary<string, Dictionary<string, string>> contexts;
        private string baseClassName;
        private ObjectModelCustomizer objectModelCustomizer;
        private bool isLayeringSupported;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialPropertyFactory"/> class.
        /// </summary>
        /// <param name="dtdlVersions">A list of DTDL major version numbers to create properties for.</param>
        /// <param name="contexts">A dictionary that maps from a context ID to a dictionary of term definitions.</param>
        /// <param name="baseClassName">The name of the C# base class of all DTDL entities.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public MaterialPropertyFactory(List<int> dtdlVersions, Dictionary<string, Dictionary<string, string>> contexts, string baseClassName, ObjectModelCustomizer objectModelCustomizer, bool isLayeringSupported)
        {
            this.dtdlVersions = dtdlVersions;
            this.contexts = contexts;
            this.baseClassName = baseClassName;
            this.objectModelCustomizer = objectModelCustomizer;
            this.isLayeringSupported = isLayeringSupported;
        }

        /// <summary>
        /// Create an object that is a subclass of <see cref="MaterialProperty"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <returns>The object created.</returns>
        public MaterialProperty Create(string propertyName, MaterialPropertyDigest propertyDigest)
        {
            string obversePropertyName = NameFormatter.FormatNameAsProperty(propertyName);
            Dictionary<int, string> propertyNameUris = this.dtdlVersions.Where(v => this.contexts[ParserGeneratorValues.GetDtdlContextIdString(v)].ContainsKey(propertyName)).ToDictionary(v => v, v => this.contexts[ParserGeneratorValues.GetDtdlContextIdString(v)][propertyName]);

            Dictionary<int, List<IPropertyRestriction>> propertyRestrictions = this.GetPropertyRestrictions(propertyName, propertyDigest);

            if (propertyDigest.IsLiteral)
            {
                if (propertyDigest.Datatype != null)
                {
                    if (propertyDigest.Datatype == "langString")
                    {
                        return new LangStringLiteralProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions);
                    }

                    ILiteralType literalType;
                    switch (propertyDigest.Datatype)
                    {
                        case "boolean":
                            literalType = new BooleanLiteralType();
                            break;
                        case "integer":
                            literalType = new IntegerLiteralType();
                            break;
                        case "string":
                            literalType = new StringLiteralType();
                            break;
                        default:
                            throw new Exception("unrecognized type");
                    }

                    if (propertyDigest.IsPlural)
                    {
                        return new PluralTypedLiteralProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions, propertyDigest.Datatype, literalType);
                    }
                    else
                    {
                        return new SingularTypedLiteralProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions, propertyDigest.Datatype, literalType);
                    }
                }
                else
                {
                    if (propertyDigest.IsPlural)
                    {
                        return new PluralUntypedLiteralProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions);
                    }
                    else
                    {
                        return new SingularUntypedLiteralProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions);
                    }
                }
            }
            else if (propertyDigest.Class != null)
            {
                if (propertyDigest.DictionaryKey != null)
                {
                    return new DictionaryProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions);
                }
                else if (propertyDigest.IsPlural)
                {
                    return new PluralObjectProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions, this.isLayeringSupported);
                }
                else
                {
                    return new SingularObjectProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions);
                }
            }
            else
            {
                if (propertyDigest.IsPlural)
                {
                    return new PluralIdentifierProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions, this.baseClassName);
                }
                else
                {
                    return new SingularIdentifierProperty(propertyName, obversePropertyName, propertyNameUris, propertyDigest, this.objectModelCustomizer, propertyRestrictions, this.baseClassName);
                }
            }
        }

        private Dictionary<int, List<IPropertyRestriction>> GetPropertyRestrictions(string propertyName, MaterialPropertyDigest propertyDigest)
        {
            Dictionary<int, List<IPropertyRestriction>> propertyRestrictions = new Dictionary<int, List<IPropertyRestriction>>();

            foreach (KeyValuePair<int, PropertyVersionDigest> kvp in propertyDigest.PropertyVersions)
            {
                if (!propertyRestrictions.TryGetValue(kvp.Key, out List<IPropertyRestriction> versionRestrictions))
                {
                    versionRestrictions = new List<IPropertyRestriction>();
                    propertyRestrictions[kvp.Key] = versionRestrictions;
                }

                if (kvp.Value.Values != null)
                {
                    versionRestrictions.Add(new PropertyRestrictionRequiredValues(propertyName, kvp.Value.ValueUris, kvp.Value.Values));
                }

                if (kvp.Value.UniqueProperties != null)
                {
                    foreach (string uniquePropertyName in kvp.Value.UniqueProperties)
                    {
                        // Dictionary keys are checked for uniqueness on assignment; no need to recheck here.
                        if (uniquePropertyName != propertyDigest.DictionaryKey)
                        {
                            versionRestrictions.Add(new PropertyRestrictionUniqueProperties(propertyName, uniquePropertyName));
                        }
                    }
                }
            }

            return propertyRestrictions;
        }
    }
}
