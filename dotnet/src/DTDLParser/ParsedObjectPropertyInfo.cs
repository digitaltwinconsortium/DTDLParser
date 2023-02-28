namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Class containing information about an object property being parsed.
    /// </summary>
    internal partial class ParsedObjectPropertyInfo
    {
        /// <summary>
        /// Gets or sets the ID of the element that has the property.
        /// </summary>
        internal Dtmi ElementId { get; set; }

        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        internal string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the layer of the property value.
        /// </summary>
        internal string Layer { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="JsonLdProperty"/> object representing the property by which the element refers to the property.
        /// </summary>
        internal JsonLdProperty JsonLdProperty { get; set; }

        /// <summary>
        /// Gets or sets the original string value used in this property reference.
        /// </summary>
        internal string ReferencedIdString { get; set; }

        /// <summary>
        /// Gets or sets the <c>DTMI</c> of the object referenced by this property.
        /// </summary>
        internal Dtmi ReferencedElementId { get; set; }

        /// <summary>
        /// Gets or sets the string value of the property that holds the dictionary key.
        /// </summary>
        internal string KeyProperty { get; set; }

        /// <summary>
        /// Gets or sets the allowed versions for the property's class.
        /// </summary>
        internal HashSet<int> AllowedVersions { get; set; }

        /// <summary>
        /// Gets or sets the identifier of a parent element in which the value of this property must be defined.
        /// </summary>
        internal Dtmi ChildOf { get; set; }

        /// <summary>
        /// Gets or sets a format string for the error cause when a bad type is given in a model.
        /// </summary>
        internal string BadTypeCauseFormat { get; set; }

        /// <summary>
        /// Gets or sets a format string for the error cause when a bad type is given in a model, with source location information.
        /// </summary>
        internal string BadTypeLocatedCauseFormat { get; set; }

        /// <summary>
        /// Gets or sets a format string for the error resolution action when a bad type is given in a model.
        /// </summary>
        internal string BadTypeActionFormat { get; set; }
    }
}
