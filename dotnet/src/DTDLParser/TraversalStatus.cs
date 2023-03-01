namespace DTDLParser
{
    /// <summary>
    /// Indicates the status of traversing a descendant hierarchy.
    /// </summary>
    internal enum TraversalStatus
    {
        /// <summary>No traversal at this point in the hierarchy has begun.</summary>
        NotStarted,

        /// <summary>A traversal at this point in the hierarchy is in progress.</summary>
        InProgress,

        /// <summary>The hierarchy hereunder has been fully traversed.</summary>
        Complete,
    }
}
