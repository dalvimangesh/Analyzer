using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Parsing;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Analyzer.Pipeline
{
    /// <summary>
    /// Analyzer rule for detecting switch statements and generated switch fields.
    /// </summary>
    public class AvoidSwitchStatementsRule : AnalyzerBase
    {
        private string errorMessage;
        private int verdict;
        private readonly string analyzerID;

        /// <summary>
        /// Initializes a new instance of the AvoidSwitchStatementsRule class.
        /// </summary>
        /// <param name="dllFiles">The ParsedDLLFiles object containing the parsed DLL information.</param>
        public AvoidSwitchStatementsRule(ParsedDLLFiles dllFiles) : base(dllFiles)
        {
            errorMessage = "";
            verdict = 1;
            analyzerID = "AvoidSwitchStatements";
        }

        /// <summary>
        /// Checks for switch statements and generated switch fields in the parsed DLL.
        /// </summary>
        private void Check()
        {
            foreach (ParsedClassMonoCecil cls in parsedDLLFiles.classObjListMC)
            {
                foreach (MethodDefinition method in cls.MethodsList)
                {
                    if (!method.HasBody)
                    {
                        continue;
                    }

                    foreach (Instruction instruction in method.Body.Instructions)
                    {
                        // Check for switch statements
                        if (instruction.OpCode == OpCodes.Switch)
                        {
                            errorMessage = "Switch statements are not allowed.";
                            verdict = 0;
                            return;
                        }

                        // Check for generated switch fields
                        if (instruction.OpCode == OpCodes.Ldsfld)
                        {
                            FieldReference field = (FieldReference)instruction.Operand;
                            if (field.Name.Contains("switch") && field.Resolve().IsGeneratedCode())
                            {
                                errorMessage = "Generated switch field detected.";
                                verdict = 0;
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Runs the analysis and returns the result.
        /// </summary>
        /// <returns>An AnalyzerResult object representing the result of the analysis.</returns>
        public override AnalyzerResult Run()
        {
            Check();
            return new AnalyzerResult(analyzerID, verdict, errorMessage);
        }
    }
}
