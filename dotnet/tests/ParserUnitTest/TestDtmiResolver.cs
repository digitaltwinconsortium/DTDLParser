namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
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
            this.resolutionArray = null;
            this.useAsyncApi = useAsyncApi;
            this.expectedResolutionCount = 0;
            this.actualResolutionCount = 0;

            if (testCaseObject.ContainsKey("resolution"))
            {
                this.resolutionArray = (JArray)testCaseObject["resolution"];
                this.expectedResolutionCount = this.resolutionArray.Count;
            }
        }

        /// <summary>
        /// Check that resolution has happened correctly.
        /// </summary>
        public void Check(bool isModelValid)
        {
            if (isModelValid)
            {
                Assert.AreEqual(this.expectedResolutionCount, this.actualResolutionCount);
            }
            else
            {
                Assert.IsTrue(this.actualResolutionCount <= this.expectedResolutionCount);
            }
        }

        /// <summary>
        /// Get an appropriate initialization value for the parser's <c>DtmiResolver</c> property.
        /// </summary>
        /// <returns>A <see cref="DtmiResolver"/> or null, as appropriate.</returns>
        public DtmiResolver GetResolver()
        {
            return this.resolutionArray != null && !this.useAsyncApi ? this.Resolve : (DtmiResolver)null;
        }

        /// <summary>
        /// Get an appropriate initialization value for the parser's <c>DtmiResolverAsync</c> property.
        /// </summary>
        /// <returns>A <see cref="DtmiResolverAsync"/> or null, as appropriate.</returns>
        public DtmiResolverAsync GetResolverAsync()
        {
            return this.resolutionArray != null && this.useAsyncApi ? this.ResolveAsync : (DtmiResolverAsync)null;
        }

        private IEnumerable<string> Resolve(IReadOnlyCollection<Dtmi> dtmis)
        {
            Assert.IsTrue(this.actualResolutionCount < this.expectedResolutionCount);
            JObject resolutionObject = (JObject)this.resolutionArray[this.actualResolutionCount];

            List<string> expectedIdentifiers = ((JArray)resolutionObject["request"]).Select(t => ((JValue)t).Value<string>()).ToList();
            expectedIdentifiers.Sort();

            List<string> actualIdentifiers = dtmis.Select(d => d.ToString()).ToList();
            actualIdentifiers.Sort();

            Assert.IsTrue(expectedIdentifiers.SequenceEqual(actualIdentifiers));

            ++this.actualResolutionCount;

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
            IEnumerable<string> values = this.Resolve(dtmis);
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
