namespace DTDLParser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for adding code relating to customizable limit values.
    /// </summary>
    public static class ValueLimiter
    {
        /// <summary>
        /// Add code to instantiate a new dictionary that maps limit specs to limit values.
        /// </summary>
        /// <param name="scope">The <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="limitDict">A dictionary mapping limit specifier to max length value.</param>
        /// <param name="lvalue">Value to which to assign the new dictionary.</param>
        public static void AssignLimitDictionary(CsScope scope, Dictionary<string, int> limitDict, string lvalue)
        {
            CsScope partitionMaxBytesScope = scope.Scope($"{lvalue} = new Dictionary<string, int>", terminate: true);
            foreach (KeyValuePair<string, int> kvp2 in limitDict)
            {
                partitionMaxBytesScope.Line($"{{ \"{kvp2.Key}\", {kvp2.Value} }},");
            }
        }

        /// <summary>
        /// Add code that defines a limit variable set by a limit specifier.
        /// </summary>
        /// <param name="scope">The <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="limitDict">A dictionary mapping limit specifier to max length value.</param>
        /// <param name="limitVar">Name for the max length variable.</param>
        /// <param name="limitSpec">Expression that provides a limit specifier.</param>
        /// <param name="nullable">Indicates whether a null value should be usesd when no limit is specified.</param>
        public static void DefineLimitVariable(CsScope scope, Dictionary<string, int> limitDict, string limitVar, string limitSpec, bool nullable)
        {
            string varType = nullable ? "int?" : "int";
            string defaultVal = nullable ? "null" : "0";

            if (limitDict == null || limitDict.Count == 0)
            {
                scope.Line($"{varType} {limitVar} = {defaultVal};");
            }
            else if (limitDict.Count == 1 && limitDict.ContainsKey(string.Empty))
            {
                scope.Line($"{varType} {limitVar} = {limitDict[string.Empty]};");
            }
            else
            {
                CsSwitchExpr limitExpr = scope.SwitchExpr($"{varType} {limitVar} = {limitSpec}");
                foreach (KeyValuePair<string, int> kvp in limitDict)
                {
                    limitExpr.Arm($"\"{kvp.Key}\"", $"{kvp.Value}");
                }

                limitExpr.Arm("_", defaultVal);
            }
        }
    }
}
