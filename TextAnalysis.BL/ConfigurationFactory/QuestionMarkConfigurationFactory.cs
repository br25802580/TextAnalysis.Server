using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis.Model;

namespace TextAnalysis.BL
{
    /// <summary>
    /// Factory which creates QuestionMarkConfiguration
    /// </summary>
    class QuestionMarkConfigurationFactory : SignConfigurationFactory
    {
        #region Properties

        public override char Sign
        {
            get
            {
                return Consts.QUESTION_MARK_SIGN;
            }
        }

        #endregion Properties

        #region Override Methods

        protected override IList<StopSignExceptionRule> GetSentenceStartExceptions()
        {
            var exceptions = new List<StopSignExceptionRule>();
            var regexList = new List<string>();
            var regexException = new RegexExceptionRule(regexList);

            // regexList.Add("");
            //regexList.Add("");

            exceptions.Add(regexException);

            return exceptions;
        }

        protected override IList<StopSignExceptionRule> GetSentenceMidExceptions()
        {
            return base.GetSentenceMidExceptions();
        }

        protected override IList<StopSignExceptionRule> GetSentenceAnywhereExceptions()
        {
            var exceptions = new List<StopSignExceptionRule>();
            var regexList = new List<string>();
            var regexException = new RegexExceptionRule(regexList);

            //url
            regexList.Add(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$");

            exceptions.Add(regexException);

            return exceptions;
        }

        #endregion Override Methods

    }
}
