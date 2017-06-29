using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public class ComparisonExceptionRule : StopSignExceptionRule
    {
        public virtual IList<string> Words { get;set; }

        public override bool IsMatch(AnalysisProcessContext processContext)
        {
            bool isMatch = false;
            string word = processContext.Word;
            char sign = processContext.Sign;

            string wordFound = Words?.FirstOrDefault(_shortcut => word.StartsWith(_shortcut));

            if (!string.IsNullOrEmpty(wordFound))
            {
                isMatch = true;
                AfterWordFound(processContext, wordFound);
            }

            return isMatch;
        }

        protected virtual void AfterWordFound(AnalysisProcessContext processContext, string matchedWord)
        {
            string word = processContext.Word;

            if (!word.Equals(matchedWord))
            {
                int matchedWordCount = matchedWord.Count();
                processContext.Output.SeperateWordAtIndex = matchedWordCount - processContext.StopSignIndexIntoWord - 1;
            }
        }

        public ComparisonExceptionRule()
        {
        }

        public ComparisonExceptionRule(IList<string> words)
        {
            Words = words;
        }
    }
}
