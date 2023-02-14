namespace DTDLParser
{
    /// <summary>
    /// Interface for checking whether an obverse object has a given type.
    /// </summary>
    internal interface ITypeChecker
    {
        /// <summary>
        /// Determine whether the object has the indicated type.
        /// </summary>
        /// <param name="typeId">The DTMI of the type to check for.</param>
        /// <returns>True if the object has the type.</returns>
        bool DoesHaveType(Dtmi typeId);
    }
}
