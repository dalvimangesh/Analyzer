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

        // AnalyzeBadPractices method to check for bad practices
        public int AnalyzeBadPractices()
        {
            // Create a flag to track if any bad practice is found
            bool badPracticeFound = false;

            // Iterate through the parsed classes
            foreach (ParsedClass classObj in parsedDLLFiles.classObjList)
            {
                // Add logic to analyze each class for bad practices here.

                // If you find a bad practice, set the flag to true.
                if (ContainsBadPractice(classObj))
                {
                    badPracticeFound = true;
                }
            }

            // If at least one bad practice is found, return 0. Otherwise, return 1.
            return badPracticeFound ? 0 : 1;
        }

        // Method to check if a class contains a bad practice
        private bool ContainsBadPractice(ParsedClass classObj)
        {
            // Implement logic to check for bad practices in the class.

            return true; // Replace with your logic for bad practice detection.
        }

        // GetScore method, returns the result of AnalyzeBadPractices
        public int GetScore()
        {
            // In this case, you return the result of AnalyzeBadPractices directly.
            return AnalyzeBadPractices();
        }
    }
}
