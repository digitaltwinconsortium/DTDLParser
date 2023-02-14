namespace DTDLParser
{
    /// <summary>
    /// Class for adding code to material classes that are marked as partitions.
    /// </summary>
    public static class MaterialClassPartitioner
    {
        /// <summary>
        /// Generate code for the constructor of the material class.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="classIsPartition">True if the material class is a partition.</param>
        public static void GenerateConstructorCode(CsScope scope, bool classIsPartition)
        {
            scope.Break();
            scope.Line($"this.{ParserGeneratorValues.IsPartitionPropertyName} = {ParserGeneratorValues.GetBooleanLiteral(classIsPartition)};");
        }

        /// <summary>
        /// Generate appropriate members for the material class.
        /// </summary>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="classIsPartition">True if the material class is a partition.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        public static void AddMembers(CsClass obverseClass, string typeName, bool classIsPartition, bool classIsBase)
        {
            CsProperty isPartitionProperty = obverseClass.Property(Access.Internal, classIsBase ? Novelty.Virtual : Novelty.Override, ParserGeneratorValues.ObverseTypeBoolean, ParserGeneratorValues.IsPartitionPropertyName);
            isPartitionProperty.Summary("Gets a value indicating whether this class is a partition point for the object model.");
            isPartitionProperty.Get();
        }
    }
}
