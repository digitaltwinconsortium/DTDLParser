namespace ParserUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DTDLParser;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A test implementation of a <see cref="DtmiResolver"/ and a <see cref="DtmiResolverAsync"/>>.
    /// </summary>
    internal class TestDtmiResolver : IResolutionChecker
    {
        private JArray resolutionArray;
        bool useAsyncApi;
        private int expectedResolutionCount;
        private int actualResolutionCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDtmiResolver"/> class.
        /// </summary>
        /// <param name="testCaseObject">A <c>JObject</c> containing information about the test case.</param>
        /// <param name="useAsyncApi">Use the asynchronous parse/resolve API.</param>
        public TestDtmiResolver(JObject testCaseObject, bool useAsyncApi)
        {
            resolutionArray = null;
            this.useAsyncApi = useAsyncApi;
            expectedResolutionCount = 0;
            actualResolutionCount = 0;

            if (testCaseObject.ContainsKey("resolution"))
            {
                resolutionArray = (JArray)testCaseObject["resolution"];
                expectedResolutionCount = resolutionArray.Count;
            }
        }

        /// <summary>
        /// Check that resolution has happened correctly.
        /// </summary>
        public void Check(bool isModelValid)
        {
            if (isModelValid)
            {
                Assert.AreEqual(expectedResolutionCount, actualResolutionCount);
            }
            else
            {
                Assert.IsTrue(actualResolutionCount <= expectedResolutionCount);
            }
        }

        /// <summary>
        /// Get an appropriate initialization value for the parser's <c>DtmiResolver</c> property.
        /// </summary>
        /// <returns>A <see cref="DtmiResolver"/> or null, as appropriate.</returns>
        public DtmiResolver GetResolver()
        {
            return resolutionArray != null && !useAsyncApi ? Resolve : null;
        }

        /// <summary>
        /// Get an appropriate initialization value for the parser's <c>DtmiResolverAsync</c> property.
        /// </summary>
        /// <returns>A <see cref="DtmiResolverAsync"/> or null, as appropriate.</returns>
        public DtmiResolverAsync GetResolverAsync()
        {
            return resolutionArray != null && useAsyncApi ? ResolveAsync : null;
        }

        private IEnumerable<string> Resolve(IReadOnlyCollection<Dtmi> dtmis)
        {
            Assert.IsTrue(actualResolutionCount < expectedResolutionCount);
            JObject resolutionObject = (JObject)resolutionArray[actualResolutionCount];

            List<string> expectedIdentifiers = ((JArray)resolutionObject["request"]).Select(t => ((JValue)t).Value<string>()).ToList();
            expectedIdentifiers.Sort();

            List<string> actualIdentifiers = dtmis.Select(d => d.ToString()).ToList();
            actualIdentifiers.Sort();

            Assert.IsTrue(expectedIdentifiers.SequenceEqual(actualIdentifiers));

            ++actualResolutionCount;

            if (resolutionObject["response"].Type != JTokenType.Null)
            {
                return ((JArray)resolutionObject["response"]).Select(t => t.ToString());
            }

            return null;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning disable CS8425 // 'CancellationToken' is not decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
        private async IAsyncEnumerable<string> ResolveAsync(IReadOnlyCollection<Dtmi> dtmis, CancellationToken _)
        {
            IEnumerable<string> values = Resolve(dtmis);
            if (values != null)
            {
                foreach (string value in values)
                {
                    yield return value;
                }
            }
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CS8425 // 'CancellationToken' is not decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
    }
}
