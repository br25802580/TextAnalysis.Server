using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public class RegexExceptionRule : StopSignExceptionRule
    {
        public IList<string> RegexList { get; set; }

        public override bool IsMatch(AnalysisProcessContext processContext)
        {
            bool isMatch = false;
            string word = processContext.Word;
            char sign = processContext.Sign;

            for (int i = 0; i < RegexList.Count &&!isMatch; i++)
            {
                var regex = new Regex(RegexList[i]);

                 isMatch = regex.IsMatch(word);

                if (!isMatch)
                {
                    isMatch = regex.IsMatch(processContext.WordWithEndTrim);

                    if (isMatch)
                    {
                        int stopSignIndexGap = word.Count() - processContext.StopSignIndexIntoWord;
                        processContext.Output.StopSignIndexGap = stopSignIndexGap;
                    }
                    else {
                        int endSignIndex= word.Count();
                        string newWord=string.Empty;

                        do
                        {
                            endSignIndex = word.LastIndexOfAny(processContext.AnalysisConfiguration.Signs.ToArray(), endSignIndex-1);

                            if (endSignIndex > -1)
                                {
                                newWord = word.Substring(0, endSignIndex);
                                isMatch = regex.IsMatch(newWord);
                            }
                        }
                        while (!isMatch && endSignIndex > 0);

                        if (isMatch)
                        {
                            int stopSignIndexGap = newWord.Count() - processContext.StopSignIndexIntoWord;
                            processContext.Output.StopSignIndexGap = stopSignIndexGap;
                        }
                    }
                }
            }

            return isMatch;
        }

        public RegexExceptionRule()
        {
        }

        public RegexExceptionRule(IList<string> regexList)
        {
            RegexList = regexList;
        }
    }
}
