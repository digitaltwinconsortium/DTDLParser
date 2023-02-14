namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Class for conditionally generating a series of alternatives in an if/elseif/.../elseif/else structure.
    /// </summary>
    public class ConditionalAlternativeSeries
    {
        private AlternativeSeries alternativeSeries;
        private CommentBlock commentBlock;
        private List<ConditionalComment> conditionalComments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalAlternativeSeries"/> class.
        /// </summary>
        /// <param name="outerScope">Scope in which to add the conditional scope.</param>
        public ConditionalAlternativeSeries(CsScope outerScope)
        {
            this.alternativeSeries = new AlternativeSeries(outerScope);
            this.commentBlock = new CommentBlock();
            this.conditionalComments = new List<ConditionalComment>();
        }

        /// <summary>
        /// Try to get a <see cref="CsScope"/> for a condition in the series.
        /// </summary>
        /// <param name="metaCondition">True if a scope should be allocated for the condition.</param>
        /// <param name="condition">Text for the parenthesized portion of an if or else-if statement.</param>
        /// <param name="altComment">A comment string to add to the code if <paramref name="metaCondition"/> evaluates to false.</param>
        /// <param name="scope">Out-parameter that receives a <see cref="CsScope"/> if <paramref name="metaCondition"/> evaluates to true.</param>
        /// <returns>True if <paramref name="metaCondition"/> evaluates to true.</returns>
        public bool TryGetScope(bool metaCondition, string condition, string altComment, out CsScope scope)
        {
            if (metaCondition)
            {
                foreach (ConditionalComment conditionalComment in this.conditionalComments)
                {
                    this.commentBlock.AddComment(conditionalComment.Comment);
                    this.commentBlock.GenerateBlock(this.alternativeSeries.GetScope(conditionalComment.Condition));
                }

                this.conditionalComments.Clear();
                scope = this.alternativeSeries.GetScope(condition);
                return true;
            }
            else if (condition == null)
            {
                foreach (ConditionalComment conditionalComment in this.conditionalComments)
                {
                    this.commentBlock.AddComment(conditionalComment.Comment);
                }

                this.commentBlock.AddComment(altComment);

                this.commentBlock.GenerateBlock(this.alternativeSeries.GetScope(null));
                scope = null;
                return false;
            }
            else
            {
                this.conditionalComments.Add(new ConditionalComment() { Condition = condition, Comment = altComment });
                scope = null;
                return false;
            }
        }

        private struct ConditionalComment
        {
            public string Condition;
            public string Comment;
        }
    }
}
