/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using DTDLParser.Models;

    /// <summary>
    /// The <c>DtmiResolverAsync</c> delegate enables a client of the parser to set a callback that will be invoked when the parser encounters a context reference or a dependent reference to a DTMI for which it has no definition.
    /// This callback will be called during execution of the <see cref="ModelParser.ParseAsync(IAsyncEnumerable{string}, DtdlParseLocator, CancellationToken)"/> API.
    /// The resolver accepts a collection of DTMIs as an argument, so a batch of identifiers can be resolved together; this reduces the count of network round trips when resolution requires remote retrieval.
    /// If the resolver is unable to obtain a definition for the DTMIs, it should return null for the enumeration.
    /// </summary>
    /// <param name="dtmis">A collection of DTMIs to resolve.</param>
    /// <param name="cancellationToken">A <c>CancellationToken</c> to cancel the resolution prematurely.</param>
    /// <returns>An enumeration of JSON strings that collectively include definitions for all elements with the given DTMIs. The order is not required to match the order of the DTMIs passed to the resolver.</returns>
    public delegate IAsyncEnumerable<string> DtmiResolverAsync(IReadOnlyCollection<Dtmi> dtmis, CancellationToken cancellationToken = default);
}
