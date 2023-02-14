namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Class for adding parsing code to material classes.
    /// </summary>
    public static class MaterialClassEqualizer
    {
        /// <summary>
        /// Generate appropriate members for the material class.
        /// </summary>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="className">The C# name of the material class.</param>
        /// <param name="kindProperty">The property on the DTDL base obverse class that indicates the kind of DTDL element.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        /// <param name="properties">A list of <see cref="MaterialProperty"/> objects for the properties of the material class.</param>
        /// <param name="parentMap">A dictionary that maps from class type to parent class type.</param>
        public static void AddMembers(CsClass obverseClass, string className, string kindProperty, bool classIsBase, bool classIsAugmentable, List<MaterialProperty> properties, Dictionary<string, string> parentMap)
        {
            GenerateEqualsMethod(obverseClass, className, classIsBase, classIsAugmentable, properties);
            GenerateDeepEqualsMethod(obverseClass, className, classIsBase, classIsAugmentable, properties);
            GenerateBaseEqualsMethod(obverseClass, className, kindProperty, parentMap);
            GenerateBaseDeepEqualsMethod(obverseClass, className, kindProperty, parentMap);
            GenerateObjectEqualsMethod(obverseClass, className);
            GenerateEqualsOperator(obverseClass, className);
            GenerateNotEqualsOperator(obverseClass, className);
            GenerateGetHashCodeMethod(obverseClass, classIsBase, classIsAugmentable, properties);
        }

        private static void GenerateEqualsMethod(CsClass obverseClass, string className, bool classIsBase, bool classIsAugmentable, List<MaterialProperty> properties)
        {
            CsMethod equalsMethod = obverseClass.Method(Access.Public, Novelty.Virtual, ParserGeneratorValues.ObverseTypeBoolean, "Equals");
            equalsMethod.Summary($"Compares to another <c>{className}</c> object.");
            equalsMethod.Param(className, "other", $"The other <c>{className}</c> object to compare to.");
            equalsMethod.Returns("True if equal.");

            CsMultiLine returnLine = equalsMethod.Body.MultiLine($"return {(classIsBase ? "!ReferenceEquals(null, other)" : "base.Equals(other)")}");

            CsSorted sorted = returnLine.Sorted();
            foreach (MaterialProperty materialProperty in properties)
            {
                materialProperty.AddEqualsLine(sorted);
            }

            MaterialClassAugmentor.AddEqualsLines(sorted, classIsAugmentable);

            returnLine.Line(";");
        }

        private static void GenerateDeepEqualsMethod(CsClass obverseClass, string className, bool classIsBase, bool classIsAugmentable, List<MaterialProperty> properties)
        {
            CsMethod equalsMethod = obverseClass.Method(Access.Public, Novelty.Virtual, ParserGeneratorValues.ObverseTypeBoolean, "DeepEquals");
            equalsMethod.Summary($"Compares to another <c>{className}</c> object, recursing through the entire subtree of object properties.");
            equalsMethod.Param(className, "other", $"The other <c>{className}</c> object to compare to.");
            equalsMethod.Returns("True if equal.");

            CsMultiLine returnLine = equalsMethod.Body.MultiLine($"return {(classIsBase ? "!ReferenceEquals(null, other)" : "base.DeepEquals(other)")}");

            CsSorted sorted = returnLine.Sorted();
            foreach (MaterialProperty materialProperty in properties)
            {
                materialProperty.AddDeepEqualsLine(sorted);
            }

            MaterialClassAugmentor.AddDeepEqualsLines(sorted, classIsAugmentable);

            returnLine.Line(";");
        }

        private static void GenerateBaseEqualsMethod(CsClass obverseClass, string className, string kindProperty, Dictionary<string, string> parentMap)
        {
            string parentClassName = className;
            while (parentMap.TryGetValue(parentClassName, out parentClassName))
            {
                CsMethod equalsMethod = obverseClass.Method(Access.Public, Novelty.Override, ParserGeneratorValues.ObverseTypeBoolean, "Equals");
                equalsMethod.InheritDoc();
                equalsMethod.Param(parentClassName, "other");

                equalsMethod.Body.Line($"return this.{kindProperty} == other?.{kindProperty} && this.Equals(({className})other);");
            }
        }

        private static void GenerateBaseDeepEqualsMethod(CsClass obverseClass, string className, string kindProperty, Dictionary<string, string> parentMap)
        {
            string parentClassName = className;
            while (parentMap.TryGetValue(parentClassName, out parentClassName))
            {
                CsMethod equalsMethod = obverseClass.Method(Access.Public, Novelty.Override, ParserGeneratorValues.ObverseTypeBoolean, "DeepEquals");
                equalsMethod.InheritDoc();
                equalsMethod.Param(parentClassName, "other");

                equalsMethod.Body.Line($"return this.{kindProperty} == other?.{kindProperty} && this.DeepEquals(({className})other);");
            }
        }

        private static void GenerateObjectEqualsMethod(CsClass obverseClass, string className)
        {
            CsMethod equalsMethod = obverseClass.Method(Access.Public, Novelty.Override, ParserGeneratorValues.ObverseTypeBoolean, "Equals");
            equalsMethod.Attribute("EditorBrowsable(EditorBrowsableState.Never)");
            equalsMethod.InheritDoc();
            equalsMethod.Param("object", "otherObj");

            equalsMethod.Body.Line($"return otherObj is {className} other && this.Equals(other);");
        }

        private static void GenerateEqualsOperator(CsClass obverseClass, string className)
        {
            CsMethod equalsOperator = obverseClass.Method(Access.Public, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "operator ==", Multiplicity.Static);
            equalsOperator.Summary($"Determines whether two <c>{className}</c> objects are equal.");
            equalsOperator.Param(className, "x", $"One <c>{className}</c> object to compare.");
            equalsOperator.Param(className, "y", $"Another <c>{className}</c> object to compare to the first.");
            equalsOperator.Returns("True if equal.");

            equalsOperator.Body.If("ReferenceEquals(null, x)")
                .Line("return ReferenceEquals(null, y);");
            equalsOperator.Body.Line("return x.Equals(y);");
        }

        private static void GenerateNotEqualsOperator(CsClass obverseClass, string className)
        {
            CsMethod notEqualsOperator = obverseClass.Method(Access.Public, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "operator !=", Multiplicity.Static);
            notEqualsOperator.Summary($"Determines whether two <c>{className}</c> objects are not equal.");
            notEqualsOperator.Param(className, "x", $"One <c>{className}</c> object to compare.");
            notEqualsOperator.Param(className, "y", $"Another <c>{className}</c> object to compare to the first.");
            notEqualsOperator.Returns("True if not equal.");

            notEqualsOperator.Body.If("ReferenceEquals(null, x)")
                .Line("return !ReferenceEquals(null, y);");
            notEqualsOperator.Body.Line("return !x.Equals(y);");
        }

        private static void GenerateGetHashCodeMethod(CsClass obverseClass, bool classIsBase, bool classIsAugmentable, List<MaterialProperty> properties)
        {
            CsMethod method = obverseClass.Method(Access.Public, Novelty.Override, ParserGeneratorValues.ObverseTypeInteger, "GetHashCode");
            method.Attribute("EditorBrowsable(EditorBrowsableState.Never)");
            method.InheritDoc();

            method.Body.Line($"int hashCode = {(classIsBase ? "0" : "base.GetHashCode()")};");
            method.Body.Break();

            CsScope uncheckedScope = method.Body.Scope("unchecked");
            CsSorted sorted = uncheckedScope.Sorted();
            foreach (MaterialProperty materialProperty in properties)
            {
                materialProperty.AddHashLine(sorted);
            }

            MaterialClassAugmentor.AddHashLines(sorted, classIsAugmentable);

            method.Body.Line("return hashCode;");
        }
    }
}
