namespace DTDLParser
{
    /// <summary>
    /// Access level for C# declarations.
    /// </summary>
    public enum Access
    {
        /// <summary>No explicit access level, used for finalizers and static constructors.</summary>
        Implicit,

        /// <summary>public.</summary>
        Public,

        /// <summary>internal.</summary>
        Internal,

        /// <summary>protected internal.</summary>
        ProtectedInternal,

        /// <summary>protected.</summary>
        Protected,

        /// <summary>private protected.</summary>
        PrivateProtected,

        /// <summary>private.</summary>
        Private,
    }
}
