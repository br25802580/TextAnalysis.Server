using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    /// <summary>
    /// Rule for comparing an input word with any of reserved words list.
    /// if a match found- the input word is Exceptional and not the end of the sentence.
    /// </summary>
    public class ComparisonExceptionRule : StopSignExceptionRule
    {
        #region Properties

        /// <summary>
        /// Words to compare current input word with these. 
        /// </summary>
        public virtual IList<string> ReservedWords { get; set; }

        #endregion Properties

        #region VirtualMethods

        /// <summary>
        /// Find whether current input word is matched to this exception rule.
        /// Check if current word start with any of certain words.
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <returns></returns>
        public override bool IsMatch(AnalysisProcessContext processContext)
        {
            bool isMatch = false;
            string word = processContext.Word;
            char sign = processContext.Sign;

            string wordFound = ReservedWords?.FirstOrDefault(_shortcut => word.StartsWith(_shortcut));

            if (!string.IsNullOrEmpty(wordFound))
            {
                isMatch = true;
                AddSeparatorSpaceIfNecessary(processContext, wordFound);
            }

            return isMatch;
        }

        /// <summary>
        /// Find whether to add a space after the word location which has been found
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <param name="matchedWord"></param>
        protected virtual void AddSeparatorSpaceIfNecessary(AnalysisProcessContext processContext, string matchedWord)
        {
            string word = processContext.Word;

            if (!word.Equals(matchedWord))
            {
                int matchedWordCount = matchedWord.Count();
                processContext.Output.AddSpaceAtIndex = matchedWordCount - processContext.StopSignIndexIntoWord - 1;
            }
        }

        #endregion VirtualMethods

        #region Ctor

        public ComparisonExceptionRule()
        {
        }

        public ComparisonExceptionRule(IList<string> words)
        {
            ReservedWords = words;
        }

        #endregion Ctor
    }
}
