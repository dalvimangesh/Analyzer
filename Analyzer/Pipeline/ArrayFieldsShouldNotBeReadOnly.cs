using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Parsing;
using Mono.Cecil;

namespace Analyzer.Pipeline
{
    /// <summary>
    /// Analyzer rule for detecting readonly array fields in types.
    /// </summary>
    public class ArrayFieldsShouldNotBeReadOnlyRule : AnalyzerBase
    {
        private string errorMessage;
        private int verdict;
        private readonly string analyzerID;

        /// <summary>
        /// Initializes a new instance of the ArrayFieldsShouldNotBeReadOnlyRule class.
        /// </summary>
        /// <param name="dllFiles">The ParsedDLLFiles object containing the parsed DLL information.</param>
        public ArrayFieldsShouldNotBeReadOnlyRule(ParsedDLLFiles dllFiles) : base(dllFiles)
        {
            errorMessage = "";
            verdict = 1;
            analyzerID = "ArrayShouldNotBeReadOnly";
        }

        /// <summary>
        /// Checks for readonly array fields in types.
        /// </summary>
        private void Check()
        {
            foreach (ParsedClassMonoCecil cls in parsedDLLFiles.classObjListMC)
            {
                if (cls.IsInterface || cls.IsEnum || !cls.HasFields || cls.IsDelegate())
                {
                    continue;
                }

                foreach (FieldDefinition field in cls.FieldsList)
                {
                    // Check if the field is public, readonly, and has array type
                    if (field.IsInitOnly && field.IsPublic && field.FieldType.IsArray)
                    {
                        errorMessage = "Array fields should not be readonly.";
                        verdict = 0;
                        return;
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
