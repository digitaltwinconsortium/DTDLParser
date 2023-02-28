namespace ParserUnitTest
{
    /// <summary>
    /// Interface for checking whether resolution of DTMIs happened correctly.
    /// </summary>
    internal interface IResolutionChecker
    {
        /// <summary>
        /// Check that resolution has happened correctly.
        /// </summary>
        void Check(bool isModelValid);
    }
}
