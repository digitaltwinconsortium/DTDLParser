namespace DTDLParser
{
    /// <summary>
    /// Generator for C# finalizer.
    /// </summary>
    public class CsFinalizer : CsMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsFinalizer"/> class.
        /// </summary>
        /// <param name="name">Name of class or struct.</param>
        public CsFinalizer(string name)
            : base(hasBody: true, Access.Implicit, Novelty.Normal, null, $"~{name}")
        {
        }
    }
}
