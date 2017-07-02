using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    /// <summary>
    /// Rule for comparing an input word with any of regex list.
    /// if a match found- the input word is Exceptional and not the end of the sentence.
    /// </summary>
    public class RegexExceptionRule : StopSignExceptionRule
    {
        #region Properties

        public IList<string> RegexList { get; set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Find whether current input word is matched to this exception rule.
        /// Check if current word is matched to any of certain regexes.
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <returns></returns>
        public override bool IsMatch(AnalysisProcessContext processContext)
        {
            bool isMatched = false;
            string inputWord = processContext.Word;
            char sign = processContext.Sign;

            for (int i = 0; i < RegexList.Count && !isMatched; i++)
            {
                var regex = new Regex(RegexList[i]);

                isMatched = regex.IsMatch(inputWord);

                if (!isMatched)
                {
                    //Check if word without end stop signs is matched (eg. Check the word "Hi!!" as "Hi".)
                    isMatched = regex.IsMatch(processContext.WordWithEndTrim);

                    if (isMatched)
                    {
                        processContext.Output.WordCompleteSentenceAtIndex = inputWord.Count();
                    }
                    else
                    {
                        isMatched = IsPartOfWordMatched(processContext, inputWord, regex);
                    }
                }
            }

            return isMatched;
        }

        /// <summary>
        /// Try seperrate word by available stop signs, 
        /// and check if part of the word is matched to a specific regex
        /// </summary>
        /// <param name="processContext"></param>
        /// <param name="inputWord"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        private bool IsPartOfWordMatched(AnalysisProcessContext processContext, string inputWord, Regex regex)
        {
            int endSignIndex = inputWord.Count();
            string newWord = string.Empty;
            bool isMatched = false;

            do
            {
                endSignIndex = inputWord.LastIndexOfAny(processContext.AnalysisConfiguration.Signs.ToArray(), endSignIndex - 1);

                if (endSignIndex > -1)
                {
                    newWord = inputWord.Substring(0, endSignIndex);
                    isMatched = regex.IsMatch(newWord);
                }
            }
            while (!isMatched && endSignIndex > 0);

            if (isMatched)
                processContext.Output.WordCompleteSentenceAtIndex = newWord.Count();

            return isMatched;
        }

        #endregion Public Methods

        #region Ctor

        public RegexExceptionRule()
        {
        }

        public RegexExceptionRule(IList<string> regexList)
        {
            RegexList = regexList;
        }

        #endregion Ctor
    }
}
