using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis.Model;

namespace TextAnalysis.BL
{
    /// <summary>
    /// Abstract class for create  any SignConfiguration
    /// </summary>
    abstract class SignConfigurationFactory
    {
        #region Properties

        public abstract char Sign { get; }

        #endregion Properties

        #region Public Methods

        public StopSignConfiguration CreateSignConfiguration()
        {
            StopSignConfiguration configuation = new StopSignConfiguration();

            configuation.Sign = Sign;
            configuation.Exceptions = GetExceptions();

            return configuation;
        }

        #endregion Public Methods

        #region Virtual Methods

        protected virtual IList<StopSignExceptionRule> GetSentenceStartExceptions()
        {
            return null;
        }

        protected virtual IList<StopSignExceptionRule> GetSentenceAnywhereExceptions()
        {
            return null;
        }

        protected virtual IList<StopSignExceptionRule> GetSentenceMidExceptions()
        {
            return null;
        }

        #endregion Virtual Methods

        #region Private Methods

        private Dictionary<WordLocation, IList<StopSignExceptionRule>> GetExceptions()
        {
            Dictionary<WordLocation, IList<StopSignExceptionRule>> exceptions = new Dictionary<WordLocation, IList<StopSignExceptionRule>>();

            var SentenceStartExceptions = GetSentenceStartExceptions();
            var SentenceAnywhereExceptions = GetSentenceAnywhereExceptions();

            exceptions[WordLocation.Start] = GetSentenceStartExceptions();
            exceptions[WordLocation.Mid] = GetSentenceMidExceptions();
            exceptions[WordLocation.Anywhere] = GetSentenceAnywhereExceptions();

            return exceptions;
        }

        #endregion Private Methods
    }
}
