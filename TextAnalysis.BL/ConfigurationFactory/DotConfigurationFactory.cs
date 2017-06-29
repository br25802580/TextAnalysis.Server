using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis.Model;

namespace TextAnalysis.BL
{

    /// <summary>
    /// Factory which creates DotConfiguration
    /// </summary>
    class DotConfigurationFactory : SignConfigurationFactory
    {
        #region Properties

        public override char Sign
        {
            get
            {
                return Consts.DOT_SIGN;
            }
        }

        #endregion Properties

        #region Override Methods

        protected override IList<StopSignExceptionRule> GetSentenceStartExceptions()
        {
            var exceptions = new List<StopSignExceptionRule>();
            var regexList = DataStore.GetDotRegexListForStartSentence();
            StopSignExceptionRule regexListException = new RegexExceptionRule(regexList);
            exceptions.Add(regexListException);

            return exceptions;
        }

        protected override IList<StopSignExceptionRule> GetSentenceAnywhereExceptions()
        {
            var exceptions = new List<StopSignExceptionRule>();

            exceptions.Add(GetShortcutsException());
            exceptions.Add(GetReservedWordsException());
            exceptions.Add(GetRegexException());

            return exceptions;
        }

        #endregion Override Methods

        #region Private Methods

        private StopSignExceptionRule GetShortcutsException()
        {
            var shortcuts = DataStore.GetDotShortcuts();
            StopSignExceptionRule shortcutsException = new ComparisonExceptionRule(shortcuts);

            return shortcutsException;
        }

        private StopSignExceptionRule GetReservedWordsException()
        {
            var reservedWords = DataStore.GetDotReservedWords();

            StopSignExceptionRule reservedWordsException = new ComparisonExceptionRule(reservedWords);

            return reservedWordsException;
        }

        private StopSignExceptionRule GetRegexException()
        {
            var regexList = DataStore.GetDotRegexList();
            StopSignExceptionRule regexException = new RegexExceptionRule(regexList);

            return regexException;
        }
        #endregion Private Methods
    }
}
