namespace DTDLParser
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a propertyDigest.IsPlural typed literal property on a class that is materialized in the parser object model.
    /// </summary>
    public class PluralTypedLiteralProperty : TypedLiteralProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluralTypedLiteralProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        /// <param name="datatype">A string representation of the datatype of the literal property.</param>
        /// <param name="literalType">An object that exports the <see cref="ILiteralType"/> interface to support declaring and parsing a specific literal type.</param>
        public PluralTypedLiteralProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, string datatype, ILiteralType literalType)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions, datatype, literalType)
        {
        }

        /// <inheritdoc/>
        public override PropertyRepresentation Representation
        {
            get => PropertyRepresentation.List;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType
        {
            get => $"List<{this.LiteralType.GetSingularType(isOptional: false)}>";
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"this.{this.ObversePropertyName} = new {this.ConcretePropertyType}();");
            }
        }

        /// <inheritdoc/>
        public override void AddEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& this.{this.ObversePropertyName}.SequenceEqual(other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& this.{this.ObversePropertyName}.SequenceEqual(other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"hashCode = (hashCode * 131) + Helpers.GetListHashCode(this.{this.ObversePropertyName});");
            }
        }

        /// <inheritdoc/>
        public override void AddJsonWritingCode(CsScope scope)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                throw new Exception("write-to-JSON logic for propertyDigest.IsPlural typed literals not written yet");
            }
        }

        /// <inheritdoc/>
        public override void AddTypeScriptType(IndentedTextWriter indentedTextWriter)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                throw new Exception("TypeScript type logic for propertyDigest.IsPlural typed literals not written yet");
            }
        }

        /// <inheritdoc/>
        public override CsScope Iterate(CsScope outerScope, ref string varName)
        {
            return outerScope.ForEach($"{this.LiteralType.GetSingularType(isOptional: false)} {varName} in this.{this.ObversePropertyName}");
        }

        /// <inheritdoc/>
        public override CsScope One(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}.First()";

            return outerScope.If($"this.{this.ObversePropertyName}.Any()");
        }

        /// <inheritdoc/>
        public override CsScope CheckPresence(CsScope outerScope)
        {
            return outerScope.If($"this.{this.ObversePropertyName}.Any()");
        }

        /// <inheritdoc/>
        protected override void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
            throw new Exception("parsing logic for propertyDigest.IsPlural typed literals not written yet");
        }
    }
}
