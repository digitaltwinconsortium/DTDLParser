namespace DTDLParser
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Digital Twin Modeling Identifier.
    /// </summary>
    public class Dtmi : Uri, IComparable<Dtmi>
    {
        private static readonly Regex DtmiRegex;

        private static readonly Uri InvalidDtmiValidationId;

        private static readonly char[] Separators = { ':', ';' };

        private bool versionInfoExtracted;
        private int majorVersion;
        private int minorVersion;
        private double completeVersion;
        private string versionless;

        private string[] labels;
        private bool? isReserved;
        private string tail;
        private Dtmi fragmentless;

        static Dtmi()
        {
            DtmiRegex = new Regex(@"^dtmi:(?:_+[A-Za-z0-9]|[A-Za-z])(?:[A-Za-z0-9_]*[A-Za-z0-9])?(?::(?:_+[A-Za-z0-9]|[A-Za-z])(?:[A-Za-z0-9_]*[A-Za-z0-9])?)*(?:;[1-9][0-9]{0,8}(?:\.[1-9][0-9]{0,5})?)?(?:#(?:(?:_+[A-Za-z0-9]|[A-Za-z])(?:[A-Za-z0-9_]*[A-Za-z0-9])?)?)?$", RegexOptions.Compiled);

            InvalidDtmiValidationId = new Uri("dtmi:dtdl:parsingError:invalidDtmi");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dtmi"/> class from a Uri instance.
        /// </summary>
        /// <param name="uri">The URI to copy from.</param>
        /// <param name="skipValidation">Skips the validation check.</param>
        public Dtmi(Uri uri, bool skipValidation = false)
            : this(uri.OriginalString, skipValidation || (uri is Dtmi))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dtmi"/> class from a string.
        /// </summary>
        /// <param name="value">String value of the DTMI.</param>
        /// <param name="skipValidation">Skips the validation check.</param>
        public Dtmi(string value, bool skipValidation = false)
            : base(value)
        {
            if (!skipValidation && !DtmiRegex.IsMatch(value))
            {
                throw this.GetInvalidIdException(value);
            }

            this.versionInfoExtracted = false;
            this.labels = null;
            this.isReserved = null;
            this.tail = null;
            this.fragmentless = null;
        }

        /// <summary>
        /// Gets the major version of the DTMI as an integer.
        /// </summary>
        public int MajorVersion
        {
            get
            {
                this.ExtractVersionInfoIfNeeded();
                return this.majorVersion;
            }
        }

        /// <summary>
        /// Gets the minor version of the DTMI as an integer.
        /// </summary>
        public int MinorVersion
        {
            get
            {
                this.ExtractVersionInfoIfNeeded();
                return this.minorVersion;
            }
        }

        /// <summary>
        /// Gets the major and minor version of the DTMI as a double.
        /// The major version is in the integral portion and the minor version is in the fractional portion.
        /// The minor version is left-zero-padded so that sort order is maintained.
        /// Ex: version 2.22 is returned as 2.000022.
        /// </summary>
        public double CompleteVersion
        {
            get
            {
                this.ExtractVersionInfoIfNeeded();
                return this.completeVersion;
            }
        }

        /// <summary>
        /// Gets the portion of the DTMI that preceeds the version number.
        /// </summary>
        public string Versionless
        {
            get
            {
                this.ExtractVersionInfoIfNeeded();
                return this.versionless;
            }
        }

        /// <summary>
        /// Gets the sequence of labels in the path portion of the DTMI.
        /// </summary>
        public string[] Labels
        {
            get
            {
                if (this.labels == null)
                {
                    this.labels = this.AbsolutePath.Split(Separators);
                    if (this.AbsolutePath.Contains(";"))
                    {
                        Array.Resize(ref this.labels, this.labels.Length - 1);
                    }
                }

                return this.labels;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the DTMI is reserved for auto-generation.
        /// </summary>
        public bool IsReserved
        {
            get
            {
                if (this.isReserved == null)
                {
                    this.isReserved = this.AbsoluteUri.Contains(":_") || this.Fragment.StartsWith("#_");
                }

                return (bool)this.isReserved;
            }
        }

        /// <summary>
        /// Gets the name of the identifier's tail, which is the empty string if there is no fragment.
        /// </summary>
        /// <remarks>
        /// The tail is the same as the fragment without the leading #.
        /// The case of no fragment and the case of an empty fragment both yield an empty string for the tail.
        /// </remarks>
        public string Tail
        {
            get
            {
                if (this.tail == null)
                {
                    this.tail = this.Fragment == string.Empty ? string.Empty : this.Fragment.Substring(1);
                }

                return this.tail;
            }
        }

        /// <summary>
        /// Gets the identifier with any fragment removed.
        /// </summary>
        public Dtmi Fragmentless
        {
            get
            {
                if (this.fragmentless == null)
                {
                    int hashIx = this.AbsoluteUri.IndexOf('#');
                    this.fragmentless = hashIx >= 0 ? new Dtmi(this.AbsoluteUri.Substring(0, hashIx), skipValidation: true) : this;
                }

                return this.fragmentless;
            }
        }

        /// <summary>
        /// Try to create a new instance of the <see cref="Dtmi"/> class from a string.
        /// </summary>
        /// <param name="value">String value of the DTMI.</param>
        /// <param name="dtmi">Out parameter to receive the newly created <see cref="Dtmi"/>.</param>
        /// <returns>True if the string represents a valid DTMI; false if it does not.</returns>
        public static bool TryCreateDtmi(string value, out Dtmi dtmi)
        {
            dtmi = DtmiRegex.IsMatch(value) ? new Dtmi(value, skipValidation: true) : null;
            return dtmi != null;
        }

        /// <summary>
        /// Compares the current instance with another <c>Dtmi</c> and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">The <c>Dtmi</c> to compare against.</param>
        /// <returns>Negative if this instance precedes <paramref name="other"/>, positive if this instance follows <paramref name="other"/>, zero if position is the same.</returns>
        public int CompareTo(Dtmi other)
        {
            return this.AbsoluteUri.CompareTo(other.AbsoluteUri);
        }

        private void ExtractVersionInfoIfNeeded()
        {
            if (this.versionInfoExtracted)
            {
                return;
            }

            if (this.Scheme != ParserValues.DtmiScheme)
            {
                throw this.GetInvalidIdException(this.AbsoluteUri.ToString());
            }

            this.versionInfoExtracted = true;

            int sepIx = this.AbsoluteUri.IndexOf(';');
            if (sepIx < 0)
            {
                this.majorVersion = 0;
                this.minorVersion = 0;
                this.completeVersion = 0.0;
                this.versionless = this.AbsoluteUri;
            }
            else
            {
                int dotIx = this.AbsoluteUri.IndexOf('.');
                int hashIx = this.AbsoluteUri.IndexOf('#');
                int verEndIx = hashIx < 0 ? this.AbsoluteUri.Length : hashIx;
                int majorEndIx = dotIx < 0 ? verEndIx : dotIx;
                this.majorVersion = int.Parse(this.AbsoluteUri.Substring(sepIx + 1, majorEndIx - sepIx - 1));
                this.minorVersion = dotIx < 0 ? 0 : int.Parse(this.AbsoluteUri.Substring(dotIx + 1, verEndIx - dotIx - 1));
                this.completeVersion = (double)this.majorVersion + ((double)this.minorVersion * 0.000001);
                this.versionless = this.AbsoluteUri.Substring(0, sepIx) + this.Fragment;
            }
        }

        private ParsingException GetInvalidIdException(string value)
        {
            return new ParsingException(new ParsingError[]
            {
                new ParsingError()
                {
                    ValidationID = InvalidDtmiValidationId,
                    Cause = $"{value} is not a legal DTMI.",
                    Action = "Replace the identifier with a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                    PrimaryID = this,
                },
            });
        }
    }
}
