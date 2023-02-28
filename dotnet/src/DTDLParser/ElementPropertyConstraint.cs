namespace DTDLParser
{
    /// <summary>
    /// Class describing a constraint on a property of an element.
    /// </summary>
    internal class ElementPropertyConstraint
    {
        /// <summary>
        /// Gets or sets the ID of the element that has a property whose value is constrained.
        /// </summary>
        internal Dtmi ParentId { get; set; }

        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        internal string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the element that has the constraint.
        /// </summary>
        internal Dtmi ElementId { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="ValueConstraint"/> on the value of an element's property.
        /// </summary>
        internal ValueConstraint ValueConstraint { get; set; }
    }
}
