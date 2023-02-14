namespace DTDLParser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Abstracts a JSON file that receives a summary of changes performed.
    /// </summary>
    public class Summarizer
    {
        private FileInfo logFile;
        private HashSet<string> canonicalizedModels;
        private HashSet<string> concretizedModels;
        private HashSet<string> typeReplacedModels;
        private HashSet<string> typeRemovedModels;
        private HashSet<string> propertyReplacedModels;
        private HashSet<string> propertyRemovedModels;
        private HashSet<string> keywordRemovedModels;
        private Dictionary<string, HashSet<string>> partnerContextsReorderedModels;
        private Dictionary<string, HashSet<string>> featureContextsAddedModels;
        private HashSet<string> undefinedExtensionContextAddedModels;
        private HashSet<string> contextReplacedModels;
        private HashSet<string> contextRemovedModels;
        private Dictionary<string, string> userIdChangeMap;
        private List<string> skippedRoots;
        private Filler.FillType fillType;

        /// <summary>
        /// Initializes a new instance of the <see cref="Summarizer"/> class.
        /// </summary>
        /// <param name="logFile">A <c>FileInfo</c> object representing the JSON file to which to write an operation summary.</param>
        public Summarizer(FileInfo logFile)
        {
            this.logFile = logFile;

            this.canonicalizedModels = new HashSet<string>();
            this.concretizedModels = new HashSet<string>();
            this.typeReplacedModels = new HashSet<string>();
            this.typeRemovedModels = new HashSet<string>();
            this.propertyReplacedModels = new HashSet<string>();
            this.propertyRemovedModels = new HashSet<string>();
            this.keywordRemovedModels = new HashSet<string>();
            this.partnerContextsReorderedModels = new Dictionary<string, HashSet<string>>();
            this.featureContextsAddedModels = new Dictionary<string, HashSet<string>>();
            this.undefinedExtensionContextAddedModels = new HashSet<string>();
            this.contextReplacedModels = new HashSet<string>();
            this.contextRemovedModels = new HashSet<string>();
            this.userIdChangeMap = new Dictionary<string, string>();
            this.skippedRoots = new List<string>();
            this.fillType = Filler.FillType.None;
        }

        /// <summary>
        /// Gets a value indicating whether a summary of operations will be logged.
        /// </summary>
        public bool IsSummarizing { get => this.logFile != null; }

        /// <summary>
        /// Write the summary of operations to the log file.
        /// </summary>
        public void WriteLogFile()
        {
            if (!this.IsSummarizing)
            {
                return;
            }

            Announcer.Heading("Writing summary file");

            JObject logObj = this.logFile != null ? new JObject() : null;

            logObj["canonicalization"] = new JArray(this.canonicalizedModels.ToList());

            logObj["concretization"] = new JArray(this.concretizedModels.ToList());

            logObj["contextReplacement"] = new JArray(this.contextReplacedModels.ToList());
            logObj["contextRemoval"] = new JArray(this.contextRemovedModels.ToList());

            logObj["typeReplacement"] = new JArray(this.typeReplacedModels.ToList());
            logObj["typeRemoval"] = new JArray(this.typeRemovedModels.ToList());

            logObj["propertyReplacement"] = new JArray(this.propertyReplacedModels.ToList());
            logObj["propertyRemoval"] = new JArray(this.propertyRemovedModels.ToList());

            logObj["keywordRemoval"] = new JArray(this.keywordRemovedModels.ToList());

            logObj["partnerContextsReordering"] = new JObject(this.partnerContextsReorderedModels.Select(e => new JProperty(e.Key, new JArray(e.Value.ToList()))));

            logObj["featureContextsAddition"] = new JObject(this.featureContextsAddedModels.Select(e => new JProperty(e.Key, new JArray(e.Value.ToList()))));

            logObj["undefinedExtensionContextAddition"] = new JArray(this.undefinedExtensionContextAddedModels.ToList());

            logObj["userIdChanges"] = new JObject(this.userIdChangeMap.Select(e => new JProperty(e.Key, e.Value)));

            if (this.skippedRoots.Any())
            {
                switch (this.fillType)
                {
                    case Filler.FillType.None:
                        logObj["skippedSubtrees"] = new JArray(this.skippedRoots);
                        break;
                    case Filler.FillType.Move:
                        logObj["movedSubtrees"] = new JArray(this.skippedRoots);
                        break;
                    case Filler.FillType.Copy:
                        logObj["copiedSubtrees"] = new JArray(this.skippedRoots);
                        break;
#if NET6_0_OR_GREATER
                    case Filler.FillType.Link:
                        logObj["linkedSubtrees"] = new JArray(this.skippedRoots);
                        break;
#endif
                }
            }

            FileStream writeStream = this.logFile.OpenWrite();
            writeStream.SetLength(0);
            StreamWriter streamWriter = new StreamWriter(writeStream);
            streamWriter.WriteLine(logObj.ToString());
            streamWriter.Close();
        }

        /// <summary>
        /// Report that at least one class, property, or element names in a model has been canonicalized.
        /// </summary>
        /// <param name="modelName">Name of the model in which the canonicalization has happened.</param>
        public void ModelCanonicalized(string modelName)
        {
            if (this.IsSummarizing)
            {
                this.canonicalizedModels.Add(modelName);
            }
        }

        /// <summary>
        /// Report that at least one abstract classes has been replaced with an appropriate concrete subclasses in a model.
        /// </summary>
        /// <param name="modelName">Name of the model in which the concretization has happened.</param>
        public void ModelConcretized(string modelName)
        {
            if (this.IsSummarizing)
            {
                this.concretizedModels.Add(modelName);
            }
        }

        /// <summary>
        /// Report that at least one invalid type has been replaced with a substitute in a model.
        /// </summary>
        /// <param name="modelName">Name of the model in which the replacement has happened.</param>
        public void ModelTypeReplaced(string modelName)
        {
            if (this.IsSummarizing)
            {
                this.typeReplacedModels.Add(modelName);
            }
        }

        /// <summary>
        /// Report that at least one invalid type has been removed from a model.
        /// </summary>
        /// <param name="modelName">Name of the model in which the removal has happened.</param>
        public void ModelTypeRemoved(string modelName)
        {
            if (this.IsSummarizing)
            {
                this.typeRemovedModels.Add(modelName);
            }
        }

        /// <summary>
        /// Report that at least one invalid property has been replaced with a substitute in a model.
        /// </summary>
        /// <param name="modelName">Name of the model in which the replacement has happened.</param>
        public void ModelPropertyReplaced(string modelName)
        {
            if (this.IsSummarizing)
            {
                this.propertyReplacedModels.Add(modelName);
            }
        }

        /// <summary>
        /// Report that at least one invalid property has been removed from a model.
        /// </summary>
        /// <param name="modelName">Name of the model in which the removal has happened.</param>
        public void ModelPropertyRemoved(string modelName)
        {
            if (this.IsSummarizing)
            {
                this.propertyRemovedModels.Add(modelName);
            }
        }

        /// <summary>
        /// Report that at least one improper keyword has been removed from a model.
        /// </summary>
        /// <param name="modelName">Name of the model in which the removal has happened.</param>
        public void ModelKeywordRemoved(string modelName)
        {
            if (this.IsSummarizing)
            {
                this.keywordRemovedModels.Add(modelName);
            }
        }

        /// <summary>
        /// Report on the context changes that have happened in a model.
        /// </summary>
        /// <param name="modelName">Name of the model in which the changes have happened.</param>
        /// <param name="partnerContextsReordered">A list of partner contexts that have been reordered to satisfy stricter requirements of the current version of DTDL.</param>
        /// <param name="featureContextsAdded">A list of feature contexts added to support the use of a type that has moved from core or standard to a feature extension.</param>
        /// <param name="undefinedExtensionContextAdded">True if a context specifier has been added to permit apocryphal types and properties in a model.</param>
        /// <param name="contextReplaced">True if a context specifier in the model has been replaced by a value from the subsitutions file.</param>
        /// <param name="contextRemoved">True if a context specifier in the model has been removed as specified by the subsitutions file.</param>
        public void ContextChanged(
            string modelName,
            List<string> partnerContextsReordered,
            List<string> featureContextsAdded,
            bool undefinedExtensionContextAdded,
            bool contextReplaced,
            bool contextRemoved)
        {
            if (this.IsSummarizing)
            {
                foreach (string partnerContext in partnerContextsReordered)
                {
                    if (!this.partnerContextsReorderedModels.TryGetValue(partnerContext, out HashSet<string> modelNames))
                    {
                        modelNames = new HashSet<string>();
                        this.partnerContextsReorderedModels[partnerContext] = modelNames;
                    }

                    modelNames.Add(modelName);
                }

                foreach (string featureContext in featureContextsAdded)
                {
                    if (!this.featureContextsAddedModels.TryGetValue(featureContext, out HashSet<string> modelNames))
                    {
                        modelNames = new HashSet<string>();
                        this.featureContextsAddedModels[featureContext] = modelNames;
                    }

                    modelNames.Add(modelName);
                }

                if (undefinedExtensionContextAdded)
                {
                    this.undefinedExtensionContextAddedModels.Add(modelName);
                }

                if (contextReplaced)
                {
                    this.contextReplacedModels.Add(modelName);
                }

                if (contextRemoved)
                {
                    this.contextRemovedModels.Add(modelName);
                }
            }
        }

        /// <summary>
        /// Report that a given user ID has been changed to a different ID as part of the model upgrade process.
        /// </summary>
        /// <param name="oldId">The ID in the source model.</param>
        /// <param name="newId">The new ID in the modified model.</param>
        public void UserIdChanged(string oldId, string newId)
        {
            if (this.IsSummarizing)
            {
                this.userIdChangeMap[oldId] = newId;
            }
        }

        /// <summary>
        /// Report that a subtree in the source hierarchy was skipped from conversion.
        /// </summary>
        /// <param name="submodelRoot">Path of the root of the subtree that was skipped.</param>
        public void SubtreeSkipped(string submodelRoot)
        {
            this.skippedRoots.Add(submodelRoot);
        }

        /// <summary>
        /// Record that the holes left by skipping have been filled.
        /// </summary>
        /// <param name="fillType">The type of filling: none, move, copy, or link.</param>
        public void HolesFilled(Filler.FillType fillType)
        {
            this.fillType = fillType;
        }
    }
}
