namespace DTDLParser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Class for adding code to express material classes as JSON objects.
    /// </summary>
    public class MaterialClassJsonizer
    {
        private const string Indentation = "  ";

        private readonly Dictionary<string, MaterialClassDigest> materialClasses;
        private readonly StreamWriter streamWriter;
        private readonly IndentedTextWriter indentedTextWriter;
        private readonly string kindProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialClassJsonizer"/> class.
        /// </summary>
        /// <param name="filePath">Full path of file to generate.</param>
        /// <param name="materialClasses">A a dictionary that maps from class name to a <see cref="MaterialClassDigest"/> object providing details about the named material class.</param>
        /// <param name="baseTypeName">The name of the base type of all entities.</param>
        public MaterialClassJsonizer(string filePath, Dictionary<string, MaterialClassDigest> materialClasses, string baseTypeName)
        {
            this.materialClasses = materialClasses;

            this.streamWriter = new StreamWriter(filePath);
            this.indentedTextWriter = new IndentedTextWriter(this.streamWriter, Indentation);
            this.indentedTextWriter.Indent = 0;
            this.kindProperty = NameFormatter.FormatNameAsEnumProperty(baseTypeName);

            string idVar = $"{Lowercase(baseTypeName)}Id";
            string eltType = $"{Uppercase(baseTypeName)}Type";

            this.indentedTextWriter.WriteLine($"export type DtdlObjectModel = {{ [{idVar}: string]: {eltType} }};");
        }

        /// <summary>
        /// Generate appropriate members for the material class.
        /// </summary>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        /// <param name="materialProperties">A <c>List</c> of <c>MaterialProperty</c> objects belonging to the type being generated.</param>
        public static void AddMembers(CsClass obverseClass, string typeName, bool classIsBase, List<MaterialProperty> materialProperties)
        {
            CsMethod method = obverseClass.Method(Access.Internal, classIsBase ? Novelty.Virtual : Novelty.Override, "void", "WriteToJson");
            method.Summary("Write a JSON representation of the DTDL element represented by an object of this class.");
            method.Param("Utf8JsonWriter", "jsonWriter", "A <c>Utf8JsonWriter</c> object with which to write the JSON representation.");
            method.Param("bool", "includeClassId", $"True if the mothed should add a {ParserGeneratorValues.ClassIdName} property to the JSON object.");

            if (classIsBase)
            {
                method.Body.Line($"jsonWriter.WritePropertyName(\"{ParserGeneratorValues.SupplementalTypesPropertyName}\");");
                method.Body.Line($"jsonWriter.WriteStartArray();");
                method.Body.ForEach($"{ParserGeneratorValues.IdentifierType} supplementalType in {ParserGeneratorValues.SupplementalTypesPropertyName}")
                    .Line("jsonWriter.WriteStringValue(supplementalType.ToString());");
                method.Body.Line($"jsonWriter.WriteEndArray();");
                method.Body.Break();

                method.Body.Line($"jsonWriter.WritePropertyName(\"{ParserGeneratorValues.SupplementalPropertiesPropertyName}\");");
                method.Body.Line($"jsonWriter.WriteStartObject();");
                method.Body.ForEach($"KeyValuePair<string, object> supplementalProperty in {ParserGeneratorValues.SupplementalPropertiesPropertyName}")
                    .Line("jsonWriter.WritePropertyName(supplementalProperty.Key);")
                    .Line("Helpers.WriteToJson(jsonWriter, supplementalProperty.Value);");
                method.Body.Line($"jsonWriter.WriteEndObject();");
                method.Body.Break();

                method.Body.Line($"jsonWriter.WritePropertyName(\"{ParserGeneratorValues.UndefinedTypesPropertyName}\");");
                method.Body.Line($"jsonWriter.WriteStartArray();");
                method.Body.ForEach($"string undefinedType in {ParserGeneratorValues.UndefinedTypesPropertyName}")
                    .Line("jsonWriter.WriteStringValue(undefinedType);");
                method.Body.Line($"jsonWriter.WriteEndArray();");
                method.Body.Break();

                method.Body.Line($"jsonWriter.WritePropertyName(\"{ParserGeneratorValues.UndefinedPropertiesPropertyName}\");");
                method.Body.Line($"jsonWriter.WriteStartObject();");
                method.Body.ForEach($"KeyValuePair<string, JsonElement> undefinedProperty in {ParserGeneratorValues.UndefinedPropertiesPropertyName}")
                    .Line("jsonWriter.WritePropertyName(undefinedProperty.Key);")
                    .Line("undefinedProperty.Value.WriteTo(jsonWriter);");
                method.Body.Line($"jsonWriter.WriteEndObject();");
                method.Body.Break();
            }
            else
            {
                method.Body.Line("base.WriteToJson(jsonWriter, includeClassId: false);");
                method.Body.Break();
            }

            method.Body.If("includeClassId")
                .Line($"jsonWriter.WriteString(\"{ParserGeneratorValues.ClassIdName}\", $\"dtmi:dtdl:class:{typeName};{{this.LanguageMajorVersion}}\");");

            foreach (MaterialProperty materialProperty in materialProperties)
            {
                materialProperty.AddJsonWritingCode(method.Body);
                method.Body.Break();
            }
        }

        /// <summary>
        /// Close the file and flush writes.
        /// </summary>
        public void Close()
        {
            this.indentedTextWriter.Close();
            this.streamWriter.Close();
        }

        /// <summary>
        /// Add TypeScript type info to the file.
        /// </summary>
        /// <param name="typeName">Name of the type being generated.</param>
        /// <param name="parentTypeName">Name of the parent type of this type, or null if no parent.</param>
        /// <param name="classDigest">A <see cref="MaterialClassDigest"/> object containing digested information about the material class.</param>
        /// <param name="properties">A list of <see cref="MaterialProperty"/> objects for the properties of the material class.</param>
        public void AddTypeInfo(string typeName, string parentTypeName, MaterialClassDigest classDigest, List<MaterialProperty> properties)
        {
            string typeType = $"{Uppercase(typeName)}Type";
            string infoType = $"{Uppercase(typeName)}Info";
            string parentInfoType = parentTypeName != null ? $"{Uppercase(parentTypeName)}Info" : null;

            string extendsClause = parentInfoType != null ? $" extends {parentInfoType}" : string.Empty;

            this.indentedTextWriter.WriteLine();
            this.indentedTextWriter.WriteLine($"export interface {infoType}{extendsClause} {{");
            this.indentedTextWriter.Indent++;

            HashSet<string> concreteSubclasses = new HashSet<string>();
            AddDictValuesToHashSet(concreteSubclasses, classDigest.ConcreteSubclasses);
            AddDictValuesToHashSet(concreteSubclasses, classDigest.ExtensibleMaterialSubclasses);
            this.indentedTextWriter.WriteLine($"{this.kindProperty}: {string.Join(" | ", concreteSubclasses.Select(s => $"'{s}'"))};");

            if (parentTypeName == null)
            {
                this.AddBaseTypeProperties();
            }

            foreach (MaterialProperty materialProperty in properties)
            {
                if (materialProperty.ObversePropertyName != this.kindProperty)
                {
                    materialProperty.AddTypeScriptType(this.indentedTextWriter);
                }
            }

            this.indentedTextWriter.Indent--;
            this.indentedTextWriter.WriteLine("}");

            string subTypeTypes = string.Concat(this.materialClasses.Where(p => p.Value.ParentClass == typeName).Select(p => $" | {Uppercase(p.Key)}Type"));
            this.indentedTextWriter.WriteLine();
            this.indentedTextWriter.WriteLine($"export type {typeType} = {infoType}{subTypeTypes};");
        }

        private static void AddDictValuesToHashSet(HashSet<string> set, Dictionary<int, List<string>> dict)
        {
            foreach (KeyValuePair<int, List<string>> kvp in dict)
            {
                foreach (string elt in kvp.Value)
                {
                    set.Add(elt);
                }
            }
        }

        private static string Lowercase(string value)
        {
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }

        private static string Uppercase(string value)
        {
            return char.ToUpperInvariant(value[0]) + value.Substring(1);
        }

        private void AddBaseTypeProperties()
        {
            this.indentedTextWriter.WriteLine($"{ParserGeneratorValues.SupplementalTypesPropertyName}: string[];");
            this.indentedTextWriter.WriteLine($"{ParserGeneratorValues.SupplementalPropertiesPropertyName}: {{ [property: string]: any }};");
            this.indentedTextWriter.WriteLine($"{ParserGeneratorValues.UndefinedTypesPropertyName}: string[];");
            this.indentedTextWriter.WriteLine($"{ParserGeneratorValues.UndefinedPropertiesPropertyName}: {{ [property: string]: any }};");
            this.indentedTextWriter.WriteLine($"{ParserGeneratorValues.ClassIdName}: string;");
        }
    }
}
