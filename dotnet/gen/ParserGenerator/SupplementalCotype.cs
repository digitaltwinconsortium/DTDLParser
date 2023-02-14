namespace DTDLParser
{
    /// <summary>
    /// Represents a cotype constraint on supplemental type.
    /// </summary>
    public class SupplementalCotype
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalCotype"/> class.
        /// </summary>
        /// <param name="cotype">The type name (DTDL term) of the cotype.</param>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        public SupplementalCotype(string cotype, string kindEnum)
        {
            this.KindValue = $"{kindEnum}.{NameFormatter.FormatNameAsEnumValue(cotype)}";
        }

        /// <summary>
        /// Gets a string representation of the kind value of the cotype.
        /// </summary>
        public string KindValue { get; }
    }
}
