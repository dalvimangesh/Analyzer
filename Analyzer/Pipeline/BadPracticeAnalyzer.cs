using Analyzer.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Analyzer.Pipeline
{
    internal class BadPracticeAnalyzer : BaseAnalyzer
    {
        public BadPracticeAnalyzer(ParsedDLLFiles dllFiles) : base(dllFiles)
        {
            // Constructor for any necessary setup.
        }

        public List<BadPracticeFinding> AnalyzeBadPractices()
        {
            var findings = new List<BadPracticeFinding>();

            // Implemention of analysis logic

            // Loop through all parsed classes for analysis
            foreach (ParsedClass classObj in parsedDLLFiles.classObjList)
            {
                // TODO: Implement pecific analysis logic here
                // Nshoueed to check if any bad practices are found in this class.
                // If a bad practice is found, create a BadPracticeFinding object and add it to the 'findings' list.

                // Example: Check if the class has any bad practices
                if (HasBadPractice(classObj))
                {
                    // Create a BadPracticeFinding object with the class and add it to findings
                    BadPracticeFinding finding = new BadPracticeFinding(classObj.TypeObj, "Description of the bad practice");
                    findings.Add(finding);
                }
            }

            return findings;
        }

        // TODO: Implement your custom logic to check for bad practices
        private bool HasBadPractice(ParsedClass classObj)
        {
            // Implement a logic here to check if the class has any bad practices.
            // Can check for specific conditions that indicate bad practices and return true if found.
            // Otherwise, return false.

            return false; //
        }
    }
}
