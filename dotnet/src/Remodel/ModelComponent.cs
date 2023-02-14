namespace DTDLParser
{
    using System.Text;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class for encapsulating a JSON container as either a JSON text string or a <c>JContainer</c>.
    /// </summary>
    public class ModelComponent
    {
        private Representation representation;
        private string jsonText;
        private JContainer jsonContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelComponent"/> class.
        /// </summary>
        /// <param name="name">Name for the component, such as a file name.</param>
        /// <param name="encoding">An <c>Encoding</c> indicating how the JSON text is encoded in a file.</param>
        /// <param name="jsonText">A string representing the JSON text with which to initialize the object.</param>
        public ModelComponent(string name, Encoding encoding, string jsonText)
        {
            this.Name = name;
            this.Encoding = encoding;

            this.representation = Representation.Text;
            this.jsonText = jsonText;
            this.jsonContainer = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelComponent"/> class.
        /// </summary>
        /// <param name="name">Name for the component, such as a file name.</param>
        /// <param name="encoding">An <c>Encoding</c> indicating how the JSON text is encoded in a file.</param>
        /// <param name="jsonContainer">A <c>JContainer</c> representing the JSON content with which to initialize the object.</param>
        public ModelComponent(string name, Encoding encoding, JContainer jsonContainer)
        {
            this.Name = name;
            this.Encoding = encoding;

            this.representation = Representation.Container;
            this.jsonText = null;
            this.jsonContainer = jsonContainer;
        }

        private enum Representation
        {
            Text,
            Container,
        }

        /// <summary>
        /// Gets the name of the component, such as a file name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets an <c>Encoding</c> indicating how the JSON text is encoded in a file.
        /// </summary>
        public Encoding Encoding { get; }

        /// <summary>
        /// Gets or sets a string representation of the JSON content.
        /// </summary>
        public string JsonText
        {
            get
            {
                if (this.representation == Representation.Container)
                {
                    this.representation = Representation.Text;
                    this.jsonText = this.jsonContainer.ToString() + "\r\n";
                }

                return this.jsonText;
            }

            set
            {
                this.representation = Representation.Text;
                this.jsonText = value;
            }
        }

        /// <summary>
        /// Gets or sets a <c>JContainer</c> representation of the JSON content.
        /// </summary>
        public JContainer JsonContainer
        {
            get
            {
                if (this.representation == Representation.Text)
                {
                    this.representation = Representation.Container;
                    this.jsonContainer = (JContainer)JToken.Parse(this.jsonText);
                }

                return this.jsonContainer;
            }

            set
            {
                this.representation = Representation.Container;
                this.jsonContainer = value;
            }
        }
    }
}
