namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>Helpers</c> partial class.
    /// </summary>
    public class HelpersGenerator : ITypeGenerator
    {
        private readonly string baseClassName;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpersGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        public HelpersGenerator(string baseName)
        {
            this.baseClassName = NameFormatter.FormatNameAsClass(baseName);
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass helpersClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "Helpers", Multiplicity.Static, Completeness.Partial);
            helpersClass.Summary("A static class that holds various helper functions.");

            this.GenerateGetObjectIdMethod(helpersClass);

            this.GenerateAreListsIdEqualMethod(helpersClass);

            this.GenerateAreListsIdOrLiteralEqualMethod(helpersClass);

            this.GenerateAreDictionariesIdEqualMethod(helpersClass);

            this.GenerateAreDictionariesIdOrLiteralEqualMethod(helpersClass);

            this.GenerateAreListsDeepEqualMethod(helpersClass);

            this.GenerateAreListsDeepOrLiteralEqualMethod(helpersClass);

            this.GenerateAreDictionariesDeepEqualMethod(helpersClass);

            this.GenerateAreDictionariesDeepOrLiteralEqualMethod(helpersClass);

            this.GenerateGetDictionaryIdHashCodeMethod(helpersClass);

            this.GenerateGetDictionaryIdOrLiteralHashCodeMethod(helpersClass);

            this.GenerateGetListIdHashCodeMethod(helpersClass);

            this.GenerateGetListIdOrLiteralHashCodeMethod(helpersClass);
        }

        private void GenerateGetObjectIdMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.IdentifierType, "GetObjectId", Multiplicity.Static);
            method.Summary($"Gets the identifier of an object assumed to be a {this.baseClassName}.");

            method.Param("object", "obj", "The object.");
            method.Returns("The identifier of the object.");

            method.Body.Line($"return (({this.baseClassName})obj).Id;");
        }

        private void GenerateAreListsIdEqualMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "AreListsIdEqual", Multiplicity.Static);
            method.Summary("Checks the equality of two lists by comparing their identifier values.");

            method.TypeParam("T", "The object type of the list values.");
            method.Param("IReadOnlyList<T>", "list1", "One of the lists to compare for equality.");
            method.Param("IReadOnlyList<T>", "list2", "The list to compare against.");
            method.Returns("True if the lists are equal.");

            method.Preamble($"where T : {this.baseClassName}");

            method.Body
                .Line("IEnumerator<T> enum1 = list1.GetEnumerator();")
                .Line("IEnumerator<T> enum2 = list2.GetEnumerator();")
                .Break();

            CsWhile whileMoveNext = method.Body.While("enum1.MoveNext()");

            whileMoveNext.If("!enum2.MoveNext()")
                .Line("return false;");
            whileMoveNext.If("enum1.Current.Id != enum2.Current.Id")
                .Line("return false;");

            method.Body.Line("return !enum2.MoveNext();");
        }

        private void GenerateAreListsIdOrLiteralEqualMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "AreListsIdOrLiteralEqual", Multiplicity.Static);
            method.Summary("Checks the equality of two lists by comparing their identifier values or literal values.");

            method.Param("IReadOnlyList<object>", "list1", "One of the lists to compare for equality.");
            method.Param("IReadOnlyList<object>", "list2", "The list to compare against.");
            method.Returns("True if the lists are equal.");

            method.Body
                .Line("IEnumerator<object> enum1 = list1.GetEnumerator();")
                .Line("IEnumerator<object> enum2 = list2.GetEnumerator();")
                .Break();

            CsWhile whileMoveNext = method.Body.While("enum1.MoveNext()");

            whileMoveNext.If("!enum2.MoveNext()")
                .Line("return false;");

            CsIf ifBase = whileMoveNext.If($"enum1.Current is {this.baseClassName} val1 && enum2.Current is {this.baseClassName} val2");

            ifBase.If("val1.Id != val2.Id")
                .Line("return false;");

            ifBase.ElseIf($"enum1.Current is Uri id1 && enum2.Current is Uri id2")
                .If("id1 != id2")
                    .Line("return false;");

            ifBase.ElseIf($"enum1.Current is List<Uri> uris1 && enum2.Current is List<Uri> uris2")
                .If("!uris1.SequenceEqual(uris2)")
                    .Line("return false;");

            ifBase.ElseIf("((IComparable)enum1.Current).CompareTo((IComparable)enum2.Current) != 0")
                .Line("return false;");

            method.Body.Line("return !enum2.MoveNext();");
        }

        private void GenerateAreDictionariesIdEqualMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "AreDictionariesIdEqual", Multiplicity.Static);
            method.Summary("Checks the equality of two dictionaries by comparing their identifier values.");

            method.TypeParam("T", "The object type of the dictionary values.");
            method.Param("IReadOnlyDictionary<string, T>", "dict1", "One of the dictionaries to compare for equality.");
            method.Param("IReadOnlyDictionary<string, T>", "dict2", "The dictionary to compare against.");
            method.Returns("True if the dictionaries are equal.");

            method.Preamble($"where T : {this.baseClassName}");

            method.Body.If("dict1.Count != dict2.Count")
                .Line("return false;");

            method.Body.ForEach("KeyValuePair<string, T> kvp in dict1")
                .If("!dict2.TryGetValue(kvp.Key, out T value) || kvp.Value.Id != value.Id")
                    .Line("return false;");

            method.Body.Line("return true;");
        }

        private void GenerateAreDictionariesIdOrLiteralEqualMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "AreDictionariesIdOrLiteralEqual", Multiplicity.Static);
            method.Summary("Checks the equality of two dictionaries by comparing their identifier values or literal values.");

            method.Param("IReadOnlyDictionary<string, object>", "dict1", "One of the dictionaries to compare for equality.");
            method.Param("IReadOnlyDictionary<string, object>", "dict2", "The dictionary to compare against.");
            method.Returns("True if the dictionaries are equal.");

            method.Body.If("dict1.Count != dict2.Count")
                .Line("return false;");

            CsForEach forEachDict1Pair = method.Body.ForEach("KeyValuePair<string, object> kvp in dict1");

            forEachDict1Pair.If("!dict2.TryGetValue(kvp.Key, out object value)")
                .Line("return false;");

            CsIf ifBase = forEachDict1Pair.If($"kvp.Value is {this.baseClassName} val1 && value is {this.baseClassName} val2");

            ifBase.If("val1.Id != val2.Id")
                .Line("return false;");

            ifBase.ElseIf($"kvp.Value is Uri id1 && value is Uri id2")
                .If("id1 != id2")
                    .Line("return false;");

            ifBase.ElseIf($"kvp.Value is List<Uri> uris1 && value is List<Uri> uris2")
                .If("!uris1.SequenceEqual(uris2)")
                    .Line("return false;");

            ifBase.ElseIf("kvp.Value is JsonElement elt1 && value is JsonElement elt2")
                .If("!AreJsonElementsEqual(elt1, elt2)")
                    .Line("return false;");

            ifBase.ElseIf("kvp.Value is List<object> objList1 && value is List<object> objList2")
                .If("!AreListsIdOrLiteralEqual(objList1, objList2)")
                    .Line("return false;");

            ifBase.ElseIf("((IComparable)kvp.Value).CompareTo((IComparable)value) != 0")
                .Line("return false;");

            method.Body.Line("return true;");
        }

        private void GenerateAreListsDeepEqualMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "AreListsDeepEqual", Multiplicity.Static);
            method.Summary("Checks the equality of two lists by deeply comparing their values.");

            method.TypeParam("T", "The object type of the list values.");
            method.Param("IReadOnlyList<T>", "list1", "One of the lists to compare for equality.");
            method.Param("IReadOnlyList<T>", "list2", "The list to compare against.");
            method.Returns("True if the lists are deeply equal.");

            method.Preamble($"where T : {this.baseClassName}");

            method.Body
                .Line("IEnumerator<T> enum1 = list1.GetEnumerator();")
                .Line("IEnumerator<T> enum2 = list2.GetEnumerator();")
                .Break();

            CsWhile whileMoveNext = method.Body.While("enum1.MoveNext()");

            whileMoveNext.If("!enum2.MoveNext()")
                .Line("return false;");
            whileMoveNext.If("!enum1.Current.DeepEquals(enum2.Current)")
                .Line("return false;");

            method.Body.Line("return !enum2.MoveNext();");
        }

        private void GenerateAreListsDeepOrLiteralEqualMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "AreListsDeepOrLiteralEqual", Multiplicity.Static);
            method.Summary("Checks the equality of two lists by deeply comparing their values or comparing their literal values.");

            method.Param("IReadOnlyList<object>", "list1", "One of the lists to compare for equality.");
            method.Param("IReadOnlyList<object>", "list2", "The list to compare against.");
            method.Returns("True if the lists are deeply equal.");

            method.Body
                .Line("IEnumerator<object> enum1 = list1.GetEnumerator();")
                .Line("IEnumerator<object> enum2 = list2.GetEnumerator();")
                .Break();

            CsWhile whileMoveNext = method.Body.While("enum1.MoveNext()");

            whileMoveNext.If("!enum2.MoveNext()")
                .Line("return false;");

            CsIf ifBase = whileMoveNext.If($"enum1.Current is {this.baseClassName} val1 && enum2.Current is {this.baseClassName} val2");

            ifBase.If("!val1.DeepEquals(val2)")
                .Line("return false;");

            ifBase.ElseIf($"enum1.Current is Uri id1 && enum2.Current is Uri id2")
                .If("id1 != id2")
                    .Line("return false;");

            ifBase.ElseIf($"enum1.Current is List<Uri> uris1 && enum2.Current is List<Uri> uris2")
                .If("!uris1.SequenceEqual(uris2)")
                    .Line("return false;");

            ifBase.ElseIf("((IComparable)enum1.Current).CompareTo((IComparable)enum2.Current) != 0")
                .Line("return false;");

            method.Body.Line("return !enum2.MoveNext();");
        }

        private void GenerateAreDictionariesDeepEqualMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "AreDictionariesDeepEqual", Multiplicity.Static);
            method.Summary("Checks the equality of two dictionaries by deeply comparing their values.");

            method.TypeParam("T", "The object type of the dictionary values.");
            method.Param("IReadOnlyDictionary<string, T>", "dict1", "One of the dictionaries to compare for equality.");
            method.Param("IReadOnlyDictionary<string, T>", "dict2", "The dictionary to compare against.");
            method.Returns("True if the dictionaries are deeply equal.");

            method.Preamble($"where T : {this.baseClassName}");

            method.Body.If("dict1.Count != dict2.Count")
                .Line("return false;");

            method.Body.ForEach("KeyValuePair<string, T> kvp in dict1")
                .If("!dict2.TryGetValue(kvp.Key, out T value) || !kvp.Value.DeepEquals(value)")
                    .Line("return false;");

            method.Body.Line("return true;");
        }

        private void GenerateAreDictionariesDeepOrLiteralEqualMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "AreDictionariesDeepOrLiteralEqual", Multiplicity.Static);
            method.Summary("Checks the equality of two dictionaries by deeply comparing their values or comparing their literal values.");

            method.Param("IReadOnlyDictionary<string, object>", "dict1", "One of the dictionaries to compare for equality.");
            method.Param("IReadOnlyDictionary<string, object>", "dict2", "The dictionary to compare against.");
            method.Returns("True if the dictionaries are deeply equal.");

            method.Body.If("dict1.Count != dict2.Count")
                .Line("return false;");

            CsForEach forEachDict1Pair = method.Body.ForEach("KeyValuePair<string, object> kvp in dict1");

            forEachDict1Pair.If("!dict2.TryGetValue(kvp.Key, out object value)")
                .Line("return false;");

            CsIf ifBase = forEachDict1Pair.If($"kvp.Value is {this.baseClassName} val1 && value is {this.baseClassName} val2");

            ifBase.If("!val1.DeepEquals(val2)")
                .Line("return false;");

            ifBase.ElseIf($"kvp.Value is Uri id1 && value is Uri id2")
                .If("id1 != id2")
                    .Line("return false;");

            ifBase.ElseIf($"kvp.Value is List<Uri> uris1 && value is List<Uri> uris2")
                .If("!uris1.SequenceEqual(uris2)")
                    .Line("return false;");

            ifBase.ElseIf("((IComparable)kvp.Value).CompareTo((IComparable)value) != 0")
                .Line("return false;");

            method.Body.Line("return true;");
        }

        private void GenerateGetDictionaryIdHashCodeMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, "int", "GetDictionaryIdHashCode", Multiplicity.Static);
            method.Summary("Computes a hash code of a dictionary based on its identifier values.");

            method.TypeParam("T", "The object type of the dictionary values.");
            method.Param("IReadOnlyDictionary<string, T>", "dict", "The dictionary to hash.");
            method.Returns("The hash value of the dictionary.");

            method.Preamble($"where T : {this.baseClassName}");

            method.Body.Line("int hashCode = 0;");

            method.Body.ForEach("KeyValuePair<string, T> kvp in dict")
                .Scope("unchecked")
                    .Line($"hashCode += (kvp.Key.GetHashCode() * 131) * kvp.Value.{ParserGeneratorValues.IdentifierName}.GetHashCode();");

            method.Body.Line("return hashCode;");
        }

        private void GenerateGetDictionaryIdOrLiteralHashCodeMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, "int", "GetDictionaryIdOrLiteralHashCode", Multiplicity.Static);
            method.Summary("Computes a hash code of a dictionary based on its identifier values or literal values.");

            method.Param("IReadOnlyDictionary<string, object>", "dict", "The dictionary to hash.");
            method.Returns("The hash value of the dictionary.");

            method.Body.Line("int hashCode = 0;");

            CsForEach forEachKey = method.Body.ForEach("KeyValuePair<string, object> kvp in dict");
            forEachKey.Line("int valueHashCode;");
            CsIf ifUris = forEachKey.If("kvp.Value is List<Uri> uris");
            ifUris
                .Line("valueHashCode = 0;")
                .ForEach("Uri uri in uris")
                    .Scope("unchecked")
                        .Line("valueHashCode = (valueHashCode * 131) + uri.GetHashCode();");

            ifUris.ElseIf("kvp.Value is JsonElement elt")
                .Line("valueHashCode = GetJsonElementHashCode(elt);");

            ifUris.ElseIf("kvp.Value is List<object> objList")
                .Line("valueHashCode = GetListIdOrLiteralHashCode(objList);");

            ifUris.Else()
                .Line($"valueHashCode = ((kvp.Value as {this.baseClassName})?.{ParserGeneratorValues.IdentifierName} ?? kvp.Value).GetHashCode();");

            forEachKey
                .Scope("unchecked")
                    .Line("hashCode += (kvp.Key.GetHashCode() * 131) + valueHashCode;");

            method.Body.Line("return hashCode;");
        }

        private void GenerateGetListIdHashCodeMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, "int", "GetListIdHashCode", Multiplicity.Static);
            method.Summary("Computes a hash code of a list based on its identifier values.");

            method.TypeParam("T", "The object type of the list values.");
            method.Param("IReadOnlyList<T>", "list", "The list to hash.");
            method.Returns("The hash value of the list.");

            method.Preamble($"where T : {this.baseClassName}");

            method.Body.Line("int hashCode = 0;");

            method.Body.ForEach("T element in list")
                .Scope("unchecked")
                    .Line("hashCode = (hashCode * 131) + element.Id.GetHashCode();");

            method.Body.Line("return hashCode;");
        }

        private void GenerateGetListIdOrLiteralHashCodeMethod(CsClass helpersClass)
        {
            CsMethod method = helpersClass.Method(Access.Internal, Novelty.Normal, "int", "GetListIdOrLiteralHashCode", Multiplicity.Static);
            method.Summary("Computes a hash code of a list based on its identifier values or literal values.");

            method.Param("IReadOnlyList<object>", "list", "The list to hash.");
            method.Returns("The hash value of the list.");

            method.Body.Line("int hashCode = 0;");

            method.Body.ForEach("object element in list")
                .Scope("unchecked")
                    .Line($"hashCode = (hashCode * 131) + ((element as {this.baseClassName})?.Id ?? element).GetHashCode();");

            method.Body.Line("return hashCode;");
        }
    }
}
