namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a control (either restriction or transformation) on the descendants of a class that is materialized in the parser object model.
    /// </summary>
    public interface IDescendantControl
    {
        /// <summary>
        /// Indicates whether this control applies to the given <paramref name="typeName"/>.
        /// </summary>
        /// <param name="typeName">The name of the type to check.</param>
        /// <returns>True if applicable.</returns>
        bool AppliesToType(string typeName);

        /// <summary>
        /// Generate appropriate members for the material class that has this control.
        /// </summary>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        /// <param name="classIsAbstract">True if the material class is abstract.</param>
        /// <param name="materialProperties">A list of the <see cref="MaterialProperty"/> objects associated with the material class.</param>
        void AddMembers(CsClass obverseClass, string typeName, bool classIsBase, bool classIsAbstract, List<MaterialProperty> materialProperties);

        /// <summary>
        /// Add code to the CheckRestrictions method in the material class that has this control.
        /// </summary>
        /// <param name="checkRestrictionsMethodBody">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="dtdlVersion">The version of DTDL whose restriction should be added.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        void AddRestriction(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName);

        /// <summary>
        /// Add code to the ApplyTransformations method in the material class that has this control.
        /// </summary>
        /// <param name="applyTransformationsMethodBody">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="dtdlVersion">The version of DTDL whose transformation should be added.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="materialProperties">A list of the <see cref="MaterialProperty"/> objects associated with the material class.</param>
        void AddTransformation(CsScope applyTransformationsMethodBody, int dtdlVersion, string typeName, List<MaterialProperty> materialProperties);
    }
}
