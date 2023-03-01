namespace DTDLParser
{
    /// <summary>
    /// Interface for establishing a type constraint on a property.
    /// </summary>
    internal interface IPropertyInstanceBinder
    {
        /// <summary>
        /// Add a binding on a property whose value must be an instance of another property.
        /// </summary>
        /// <param name="propertyName">Property whose value defines the instance validation criteria.</param>
        /// <param name="instancePropertyName">Property whose value must be an instance of <paramref name="propertyName"/>.</param>
        void AddInstanceProperty(string propertyName, string instancePropertyName);
    }
}
