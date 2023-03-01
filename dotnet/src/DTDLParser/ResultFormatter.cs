namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The type of phrase to generate when expanding an auto-generated DTMI.
    /// </summary>
    internal enum ExpansionType
    {
        /// <summary>A nominative phrase</summary>
        Nominative,

        /// <summary>A possessive phrase</summary>
        Possessive,

        /// <summary>A relative phrase</summary>
        Relative,

        /// <summary>An elective phrase</summary>
        Elective,
    }

    /// <summary>
    /// Class for formatting results returned from formatted select queries.
    /// </summary>
    internal class ResultFormatter
    {
        private static readonly Dictionary<ExpansionType, string> ExpansionSpecs = new Dictionary<ExpansionType, string>()
        {
            { ExpansionType.Nominative, ":n" },
            { ExpansionType.Possessive, ":p" },
            { ExpansionType.Relative, ":r" },
            { ExpansionType.Elective, ":e" },
        };

        private string formatString;
        private StringBuilder formattedResult;
        private string finalResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultFormatter"/> class.
        /// </summary>
        /// <param name="formatString">The format string for the message.</param>
        internal ResultFormatter(string formatString)
        {
            this.formatString = formatString;
            this.formattedResult = new StringBuilder(formatString);
            this.finalResult = null;
        }

        /// <summary>
        /// Get a string representation of the result.
        /// </summary>
        /// <returns>The formatted result as a string.</returns>
        public override string ToString()
        {
            if (this.finalResult == null)
            {
                this.finalResult = this.formattedResult.ToString();
            }

            return this.finalResult;
        }

        /// <summary>
        /// Install a value in the formatter.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <param name="value">The value.</param>
        internal void Install(string key, string value)
        {
            this.finalResult = null;

            string undecoratedKey = "{" + key + "}";
            if (this.formatString.Contains(undecoratedKey))
            {
                this.formattedResult.Replace(undecoratedKey, value);
                return;
            }

            foreach (KeyValuePair<ExpansionType, string> expansionSpec in ExpansionSpecs)
            {
                string decoratedKey = "{" + key + expansionSpec.Value + "}";
                if (this.formatString.Contains(decoratedKey))
                {
                    this.formattedResult.Replace(decoratedKey, this.ExpandValue(value, expansionSpec.Key));
                    return;
                }
            }
        }

        private string ExpandValue(string value, ExpansionType expansionType)
        {
            if (value.StartsWith(ParserValues.BlankNodePrefix))
            {
                return this.GetBasicExpansion(value, expansionType, string.Empty);
            }

            if (!value.StartsWith(ParserValues.DtmiPrefix))
            {
                return this.GetBasicExpansion(value, expansionType, " '" + value + "'");
            }

            Dtmi dtmi;
            try
            {
                dtmi = new Dtmi(value);
            }
            catch (ParsingException)
            {
                return this.GetBasicExpansion(value, expansionType, " '" + value + "'");
            }

            if (!dtmi.Labels.Last().StartsWith("_"))
            {
                string fragmentlessValue = dtmi.Fragmentless.ToString();
                return this.GetBasicExpansion(fragmentlessValue, expansionType, " " + fragmentlessValue);
            }

            switch (expansionType)
            {
                case ExpansionType.Nominative:
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    this.BuildNominativePhrase(dtmi, dtmi.Labels.Length, ref stringBuilder);
                    return stringBuilder.ToString();
                }

                case ExpansionType.Possessive:
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (dtmi.Labels.Last().StartsWith("__"))
                    {
                        this.BuildNominativePhrase(dtmi, dtmi.Labels.Length - 2, ref stringBuilder);
                        stringBuilder.AppendFormat(" has '{0}' value with name '{1}' whose", dtmi.Labels[dtmi.Labels.Length - 2].Substring(1), dtmi.Labels[dtmi.Labels.Length - 1].Substring(2));
                    }
                    else
                    {
                        this.BuildNominativePhrase(dtmi, dtmi.Labels.Length - 1, ref stringBuilder);
                        stringBuilder.AppendFormat(" has '{0}' value whose", dtmi.Labels[dtmi.Labels.Length - 1].Substring(1));
                    }

                    return stringBuilder.ToString();
                }

                case ExpansionType.Relative:
                    return dtmi.Labels.Last().StartsWith("__") ? " with name '" + dtmi.Labels.Last().Substring(2) + "'" : string.Empty;
                case ExpansionType.Elective:
                    return string.Empty;
                default:
                    throw new Exception("ExpansionType invalid");
            }
        }

        private string GetBasicExpansion(string value, ExpansionType expansionType, string defaultValue)
        {
            switch (expansionType)
            {
                case ExpansionType.Nominative:
                    return value;
                case ExpansionType.Possessive:
                    return value + "'s";
                default:
                    return defaultValue;
            }
        }

        private void BuildNominativePhrase(Dtmi dtmi, int labelCount, ref StringBuilder stringBuilder)
        {
            if (dtmi.Labels[labelCount - 1].StartsWith("__"))
            {
                this.BuildNominativePhrase(dtmi, labelCount - 2, ref stringBuilder);
                stringBuilder.AppendFormat(" has '{0}' value with name '{1}' which", dtmi.Labels[labelCount - 2].Substring(1), dtmi.Labels[labelCount - 1].Substring(2));
            }
            else if (dtmi.Labels[labelCount - 1].StartsWith("_"))
            {
                this.BuildNominativePhrase(dtmi, labelCount - 1, ref stringBuilder);
                stringBuilder.AppendFormat(" has '{0}' value which", dtmi.Labels[labelCount - 1].Substring(1));
            }
            else
            {
                stringBuilder.Append(dtmi.Scheme);

                for (int i = 0; i < labelCount; ++i)
                {
                    stringBuilder.Append($":{dtmi.Labels[i]}");
                }

                if (dtmi.MajorVersion > 0)
                {
                    stringBuilder.Append($";{dtmi.MajorVersion}");

                    if (dtmi.MinorVersion > 0)
                    {
                        stringBuilder.Append($".{dtmi.MinorVersion}");
                    }
                }
            }
        }
    }
}
