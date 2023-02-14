/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;
    using DTDLParser.Models;

    /// <summary>
    /// A class for testing the <see cref="ModelParser"/> using JSON test cases.
    /// </summary>
    internal class TestModel
    {
        private bool useAsyncApi;

        private DtdlParseLocator dtdlLocator;

        private IEnumerable<string> jsonTexts;

        private IReadOnlyDictionary<Dtmi, DTEntityInfo> result;

        private ModelParser modelParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestModel"/> class.
        /// </summary>
        /// <param name="modelParser">The <c>ModelParser</c> to use for parsing the test model.</param>
        /// <param name="jsonTexts">The JSON text strings to parse.</param>
        /// <param name="dtdlLocator">A <c>DtdlLocator</c> for locating errors in the JSON sources.</param>
        /// <param name="useAsyncApi">Use the asynchronous parse/resolve API.</param>
        public TestModel(ModelParser modelParser, IEnumerable<string> jsonTexts, DtdlParseLocator dtdlLocator, bool useAsyncApi)
        {
            this.modelParser = modelParser;
            this.jsonTexts = jsonTexts;
            this.dtdlLocator = dtdlLocator;
            this.useAsyncApi = useAsyncApi;
        }

        /// <summary>
        /// Gets the size of the parsed model.
        /// </summary>
        public int Size
        {
            get
            {
                return this.result.Count;
            }
        }

        /// <summary>
        /// Get a string representation of the object, which for DTDL elements is a string representation of the element's ID.
        /// </summary>
        /// <param name="obj">The object to represent.</param>
        /// <returns>The string representation.</returns>
        public static string GetObjectAsString(object obj)
        {
            return (obj as DTEntityInfo)?.Id.ToString() ?? obj?.ToString();
        }

        /// <summary>
        /// Indicate whether the given element ID is present in the model.
        /// </summary>
        /// <param name="elementId">The identifier to check for.</param>
        /// <returns>True if present.</returns>
        public bool IsElementInModel(Dtmi elementId)
        {
            return this.result.ContainsKey(elementId);
        }

        /// <summary>
        /// Indicate whether another model has equivalent content to this model.
        /// </summary>
        /// <param name="other">The other model to compare.</param>
        /// <returns>True if the two models are equivalent.</returns>
        public bool IsModelEquivalent(TestModel other)
        {
            foreach (KeyValuePair<Dtmi, DTEntityInfo> kvp in other.result)
            {
                if (!this.result.ContainsKey(kvp.Key) || !this.result[kvp.Key].Equals(kvp.Value) || this.result[kvp.Key].GetHashCode() != kvp.Value.GetHashCode())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get the <see cref="DTEntityInfo"/> indicated by the given element ID.
        /// </summary>
        /// <param name="elementId">The identifer of the element to retrieve.</param>
        /// <returns>The element from the model.</returns>
        public DTEntityInfo GetElement(Dtmi elementId)
        {
            return this.result[elementId];
        }

        /// <summary>
        /// Get a list of DTDL texts corresponding to this object model.
        /// </summary>
        /// <returns>A <c>List</c> of strings of DTDL text.</returns>
        public List<string> GetDtdl()
        {
            List<string> dtdlTexts = new List<string>();
            foreach (DTEntityInfo element in this.result.Values)
            {
                if (element is IRootable rootableElement)
                {
                    string jsonText = rootableElement.GetJsonLdText();
                    if (jsonText != string.Empty)
                    {
                        dtdlTexts.Add(jsonText);
                    }
                }
            }

            return dtdlTexts;
        }

        /// <summary>
        /// Perform the parsing.
        /// </summary>
        public void DoParse()
        {
            if (this.useAsyncApi)
            {
                Task<IReadOnlyDictionary<Dtmi, DTEntityInfo>> parseTask = this.modelParser.ParseAsync(this.EnumerableStringToAsync(this.jsonTexts), this.dtdlLocator);
                try
                {
                    parseTask.Wait();
                    this.result = parseTask.Result;
                }
                catch (AggregateException ae)
                {
                    if (ae.InnerExceptions.Count() == 1)
                    {
                        throw ae.InnerExceptions.First();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                this.result = this.modelParser.Parse(this.jsonTexts, this.dtdlLocator);
            }
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async IAsyncEnumerable<string> EnumerableStringToAsync(IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                yield return value;
            }
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
