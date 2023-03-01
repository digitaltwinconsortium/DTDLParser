namespace DTDLParser
{
    /// <summary>
    /// Interface for establishing constraints.
    /// </summary>
    internal interface IConstrainer
    {
        /// <summary>
        /// Add a constraint on the value of the indicated property.
        /// </summary>
        /// <param name="propertyName">Property whose value is to be constrained.</param>
        /// <param name="valueConstraint">A <see cref="ValueConstraint"/> to apply to the property named by <paramref name="propertyName"/>.</param>
        void AddPropertyValueConstraint(string propertyName, ValueConstraint valueConstraint);

        /// <summary>
        /// Add a constraint on siblings.
        /// </summary>
        /// <param name="siblingConstraint">A <see cref="SiblingConstraint"/> to apply.</param>
        void AddSiblingConstraint(SiblingConstraint siblingConstraint);
    }
}
