namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# code statement with an initial line of code followed by brace-enclosed lines.
    /// </summary>
    public class CsPropertyBody
    {
        private CsGet get;
        private CsSet set;
        private CsInit init;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsPropertyBody"/> class.
        /// </summary>
        public CsPropertyBody()
        {
            this.get = null;
            this.set = null;
            this.init = null;
        }

        /// <summary>
        /// Add a get accessor to the property body.
        /// </summary>
        /// <returns>The <see cref="CsGet"/> object added.</returns>
        public CsGet Get()
        {
            this.get = new CsGet();
            return this.get;
        }

        /// <summary>
        /// Add a set accessor to the property body.
        /// </summary>
        /// <returns>The <see cref="CsSet"/> object added.</returns>
        public CsSet Set()
        {
            this.set = new CsSet();
            return this.set;
        }

        /// <summary>
        /// Add an init accessor to the property body.
        /// </summary>
        /// <returns>The <see cref="CsInit"/> object added.</returns>
        public CsInit Init()
        {
            this.init = new CsInit();
            return this.init;
        }

        /// <summary>
        /// Generate code for the property body.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for generating the property body code.</param>
        public void GenerateCode(CodeWriter codeWriter)
        {
            codeWriter.OpenScope();

            if (this.get != null)
            {
                this.get.GenerateCode(codeWriter);
            }

            if (this.set != null)
            {
                this.set.GenerateCode(codeWriter);
            }

            if (this.init != null)
            {
                this.init.GenerateCode(codeWriter);
            }

            codeWriter.CloseScope();
        }
    }
}
