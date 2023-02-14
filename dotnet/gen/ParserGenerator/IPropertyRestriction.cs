namespace DTDLParser
{
    /// <summary>
    /// Represents a restriction on the properties of a class that is materialized in the parser object model.
    /// </summary>
    public interface IPropertyRestriction
    {
        /// <summary>
        /// Add code to the CheckRestrictions method in the material class that has this restriction.
        /// </summary>
        /// <param name="checkRestrictionsMethodBody">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="materialProperty">The <see cref="MaterialProperty"/> that has the restriction.</param>
        void AddRestriction(CsScope checkRestrictionsMethodBody, string typeName, MaterialProperty materialProperty);
    }
}
