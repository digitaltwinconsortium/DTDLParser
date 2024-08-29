namespace DTDLParser
{
    /// <summary>
    /// Class for adding code to material classes that are marked as partitions.
    /// </summary>
    public static class MaterialClassPresenter
    {
        /// <summary>
        /// Generate code for the constructor of the material class.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="classIsRootable">True if the material class is a rootable.</param>
        public static void GenerateConstructorCode(CsScope scope, bool classIsRootable)
        {
            if (classIsRootable)
            {
                scope.Break();
                scope.Line("this.sourceTexts = new Dictionary<string, string>();");
            }
        }

        /// <summary>
        /// Generate appropriate members for the material class.
        /// </summary>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="classIsPartition">True if the material class is a partition type.</param>
        /// <param name="classIsRootable">True if the material class is a rootable type.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public static void AddMembers(CsClass obverseClass, string typeName, bool classIsPartition, bool classIsRootable, bool classIsBase, bool isLayeringSupported)
        {
            AddRecordSourceAsAppropriateMethod(obverseClass, typeName, classIsPartition, classIsRootable, classIsBase);
            AddGetJsonLdTextMethod(obverseClass, classIsRootable, isLayeringSupported);
            AddGetJsonLdMethod(obverseClass, classIsRootable, isLayeringSupported);
            AddFields(obverseClass, classIsRootable);
        }

        private static void AddRecordSourceAsAppropriateMethod(CsClass obverseClass, string typeName, bool classIsPartition, bool classIsRootable, bool classIsBase)
        {
            CsMethod method = obverseClass.Method(Access.Internal, classIsBase ? Novelty.Virtual : Novelty.Override, "void", "RecordSourceAsAppropriate");
            method.Summary("Record JSON-LD source as appropriate and check length of text against limit.");
            method.Param("string", "layer", "The name of the layer whose source to record.");
            method.Param("JsonLdElement", "elt", "A <c>JsonLdElement</c> containing the JSON-LD source of the element.");
            method.Param("AggregateContext", "aggregateContext", "An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Param("bool", "atRoot", "True if the element is at the document root.");
            method.Param("bool", "globalized", "True if the element has been globalized.");

            CsIf ifShouldRecord;
            if (classIsPartition)
            {
                method.Body.Line($"bool addPartition = !atRoot && this.{ParserGeneratorValues.IdentifierName}.Tail == string.Empty;");
                ifShouldRecord = method.Body.If("(atRoot || addPartition) && !globalized");
                ifShouldRecord.Line("string jsonText = elt.GetOutlinedJsonText(addPartition ? aggregateContext.GetJsonLdText() : null);");
            }
            else if (classIsRootable)
            {
                ifShouldRecord = method.Body.If("atRoot && !globalized");
                ifShouldRecord.Line("string jsonText = elt.GetOutlinedJsonText(null);");
            }
            else
            {
                return;
            }

            string partitionDescription = classIsPartition ? typeName : "top-level element";

            ifShouldRecord.If($"PartitionTypeCollection.PartitionMaxBytes.TryGetValue(this.{ParserGeneratorValues.DtdlVersionPropertyName}, out Dictionary<string, int> specMaxBytes) && specMaxBytes.TryGetValue(aggregateContext.LimitSpecifier, out int maxBytes) && jsonText.Length > maxBytes")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"partitionTooLarge\",")
                    .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                    .Line($"partition: \"{partitionDescription}\",")
                    .Line($"observedCount: jsonText.Length,")
                    .Line($"expectedCount: maxBytes,")
                    .Line("element: elt);");
            ifShouldRecord.Line($"this.sourceTexts[layer] = jsonText;");
        }

        private static void AddGetJsonLdTextMethod(CsClass obverseClass, bool classIsRootable, bool isLayeringSupported)
        {
            if (classIsRootable)
            {
                CsMethod method = obverseClass.Method(Access.Public, Novelty.Normal, ParserGeneratorValues.ObverseTypeString, ParserGeneratorValues.GetJsonLdTextMethodName);
                method.InheritDoc();

                if (isLayeringSupported)
                {
                    method.Param("string", "layer", null, "\"\"");
                }
                else
                {
                    method.Body.Line("string layer = string.Empty;");
                }

                method.Body.Line("return this.sourceTexts.TryGetValue(layer, out string sourceText) ? sourceText : string.Empty;");
            }
        }

        private static void AddGetJsonLdMethod(CsClass obverseClass, bool classIsRootable, bool isLayeringSupported)
        {
            if (classIsRootable)
            {
                CsMethod method = obverseClass.Method(Access.Public, Novelty.Normal, "JsonElement", ParserGeneratorValues.GetJsonLdMethodName);
                method.InheritDoc();

                if (isLayeringSupported)
                {
                    method.Param("string", "layer", null, "\"\"");
                }
                else
                {
                    method.Body.Line("string layer = string.Empty;");
                }

                CsIf ifTryGet = method.Body.If("this.sourceTexts.TryGetValue(layer, out string sourceText)");
                ifTryGet.Using("JsonDocument sourceDoc = JsonDocument.Parse(sourceText)")
                    .Line("return sourceDoc.RootElement.Clone();");
                ifTryGet.Else()
                    .Line("return new JsonElement();");
            }
        }

        private static void AddFields(CsClass obverseClass, bool classIsRootable)
        {
            if (classIsRootable)
            {
                obverseClass.Field(Access.Private, "Dictionary<string, string>", "sourceTexts");
            }
        }
    }
}
