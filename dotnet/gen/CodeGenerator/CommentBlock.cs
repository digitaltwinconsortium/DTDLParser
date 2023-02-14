namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for generating a comment block from separately added comment lines.
    /// </summary>
    public class CommentBlock
    {
        private List<string> commentLines;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentBlock"/> class.
        /// </summary>
        public CommentBlock()
        {
            this.commentLines = new List<string>();
        }

        /// <summary>
        /// Add a comment line.
        /// </summary>
        /// <param name="commentText">The comment text to add.</param>
        public void AddComment(string commentText)
        {
            this.commentLines.Add(commentText);
        }

        /// <summary>
        /// Generate a comment block from the accumulated comments.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> to which to add the comment block.</param>
        public void GenerateBlock(CsScope scope)
        {
            int maxCommentLength = this.commentLines.Max(c => c.Length);
            foreach (string commentLine in this.commentLines)
            {
                scope.Line($"/* {commentLine.PadRight(maxCommentLength)} */");
            }

            scope.Break();
            this.commentLines.Clear();
        }
    }
}
