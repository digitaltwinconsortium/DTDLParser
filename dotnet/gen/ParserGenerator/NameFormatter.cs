namespace DTDLParser
{
    /// <summary>
    /// Static class that formats obverse names as various C# element names.
    /// </summary>
    public static class NameFormatter
    {
        private const string EnumPrefix = "DT";
        private const string EnumSuffix = "Kind";

        private static ObjectModelCustomizer objectModelCustomizer;

        /// <summary>
        /// Set an <see cref="ObjectModelCustomizer"/> to establish appropriate names for the classes in the object model.
        /// </summary>
        /// <param name="customizer">The <see cref="ObjectModelCustomizer"/> to use.</param>
        public static void SetObjectModelCustomizer(ObjectModelCustomizer customizer)
        {
            objectModelCustomizer = customizer;
        }

        /// <summary>
        /// Format an obverse name as a class name.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for an obverse class.</returns>
        public static string FormatNameAsClass(string name)
        {
            return objectModelCustomizer.GetClassName("CS", name);
        }

        /// <summary>
        /// Format an obverse name as a parameter name.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for a parameter.</returns>
        public static string FormatNameAsParameter(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        /// <summary>
        /// Format an obverse name as a variable name.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for a variable.</returns>
        public static string FormatNameAsVariable(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        /// <summary>
        /// Format an obverse name as a property name.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for a parameter of an obverse class.</returns>
        public static string FormatNameAsProperty(string name)
        {
            return objectModelCustomizer.GetPropertyName("CS", name);
        }

        /// <summary>
        /// Format an obverse name as an Enum name.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for an obverse Enum.</returns>
        public static string FormatNameAsEnum(string name)
        {
            return EnumPrefix + char.ToUpperInvariant(name[0]) + name.Substring(1) + EnumSuffix;
        }

        /// <summary>
        /// Format an obverse name as an Enum parameter.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for an Enum parameter.</returns>
        public static string FormatNameAsEnumParameter(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1) + EnumSuffix;
        }

        /// <summary>
        /// Format an obverse name as an Enum property.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for an Enum property in an obverse class.</returns>
        public static string FormatNameAsEnumProperty(string name)
        {
            return char.ToUpperInvariant(name[0]) + name.Substring(1) + EnumSuffix;
        }

        /// <summary>
        /// Format an obverse name as an Enum value.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for an Enum value.</returns>
        public static string FormatNameAsEnumValue(string name)
        {
            return char.ToUpperInvariant(name[0]) + name.Substring(1);
        }

        /// <summary>
        /// Format an obverse name as a field name.
        /// </summary>
        /// <param name="name">The obverse name.</param>
        /// <returns>The name for a field of an obverse class.</returns>
        public static string FormatNameAsField(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}
