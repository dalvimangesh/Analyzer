using Analyzer.Parsing;
using Mono.Cecil.Cil;
using Mono.Cecil;

namespace Analyzer.Pipeline
{
    /// <summary>
    /// Analyzer rule for detecting readonly array fields in classes.
    /// </summary>
    public class ArrayFieldsShouldNotBeReadOnlyRule : AnalyzerBase
    {
        private string errorMessage;
        private int verdict;
        private readonly string analyzerID;

        public ArrayFieldsShouldNotBeReadOnlyRule(ParsedDLLFiles dllFiles) : base(dllFiles)
        {
            errorMessage = "";
            verdict = 1;
            analyzerID = "ArrayFieldsShouldNotBeReadOnly";
        }

        /// <summary>
        /// Runs the analysis to check for readonly array fields in classes.
        /// </summary>
        /// <returns>An <see cref="AnalyzerResult"/> based on the analysis.</returns>
        public override AnalyzerResult Run()
        {
            CheckForReadOnlyArrayFields();
            return new AnalyzerResult(analyzerID, verdict, errorMessage);
        }

        /// <summary>
        /// Checks each class for readonly array fields.
        /// </summary>
        private void CheckForReadOnlyArrayFields()
        {
            foreach (ParsedClassMonoCecil cls in parsedDLLFiles.classObjListMC)
            {
                foreach (FieldDefinition field in cls.FieldsList)
                {
                    // Check if the field is an array and is marked as readonly
                    if (field.IsInitOnly && field.IsPublic && field.FieldType.IsArray)
                    {
                        
                        verdict = 0;
                        errorMessage = $"Readonly array field found in class {field.DeclaringType.FullName}, field {field.Name}.";
                        return; // You can return early if a readonly array field is found.
                    }
                }
            }
        }
    }
}
