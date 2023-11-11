using Analyzer.Parsing;
using Mono.Cecil.Cil;
using Mono.Cecil;
using System;

namespace Analyzer.Pipeline
{
    /// <summary>
    /// Analyzer rule for detecting switch statements in methods.
    /// </summary>
    public class AvoidSwitchStatementsAnalyzer : AnalyzerBase
    {
        private string errorMessage;
        private int verdict;
        private readonly string analyzerID;

        public AvoidSwitchStatementsAnalyzer(ParsedDLLFiles dllFiles) : base(dllFiles)
        {
            errorMessage = "";
            verdict = 1;
            analyzerID = "104";
        }

        /// <summary>
        /// Runs the analysis to check for the presence of switch statements in methods.
        /// </summary>
        /// <returns>An <see cref="AnalyzerResult"/> based on the analysis.</returns>
        public override AnalyzerResult Run()
        {
            CheckForSwitchStatements();
            return new AnalyzerResult(analyzerID, verdict, errorMessage);
        }

        /// <summary>
        /// Checks each method for the presence of switch statements.
        /// </summary>
        private void CheckForSwitchStatements()
        {
            foreach (ParsedClassMonoCecil cls in parsedDLLFiles.classObjListMC)
            {
                foreach (MethodDefinition method in cls.MethodsList)
                {
                    if (method.HasBody)
                    {
                        foreach (Instruction instruction in method.Body.Instructions)
                        {
                            if (instruction.OpCode == OpCodes.Switch)
                            {
                                // Modify the verdict and errorMessage as needed for reporting.
                                verdict = 0;
                                errorMessage = $"Switch statement found in method {method.FullName}.";
                                return; // You can return early if a switch statement is found.
                            }
                        }
                    }
                }
            }
        }
    }
}
