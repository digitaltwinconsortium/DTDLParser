namespace DTDLParser
{
    using System;

    /// <summary>
    /// Class for displaying status updates during the execution of a CLI tool.
    /// </summary>
    public static class Announcer
    {
        private static bool inMediasRes;

        static Announcer()
        {
            inMediasRes = false;
        }

        /// <summary>
        /// Display a heading for the current stage of the tool's operation.
        /// </summary>
        /// <param name="description">A textual description of the stage's operation.</param>
        public static void Heading(string description)
        {
            Fin();
            Console.Write(description);
            inMediasRes = true;
        }

        /// <summary>
        /// Display a subheading for the current stage of the tool's operation.
        /// </summary>
        /// <param name="description">A textual description of the stage's operation.</param>
        public static void Subheading(string description)
        {
            Fin();
            Console.Write($"  {description}");
            inMediasRes = true;
        }

        /// <summary>
        /// Display an indication that the tool has made an increment of progress.
        /// </summary>
        public static void Tick()
        {
            if (inMediasRes)
            {
                Console.Write(".");
            }
        }

        /// <summary>
        /// Conclude the tool's operation.
        /// </summary>
        public static void Fin()
        {
            if (inMediasRes)
            {
                Console.WriteLine();
                inMediasRes = false;
            }
        }
    }
}
