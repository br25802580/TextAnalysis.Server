using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public class StopSignConfiguration
    {
        public char Sign { get; set; }
        public IDictionary<WordLocation,IList<StopSignExceptionRule>> Exceptions { get; set; }
        public StopSignGeneralSettings GeneralSettings { get; set; }

        public StopSignConfiguration()
        {
            GeneralSettings = new Model.StopSignGeneralSettings();
            Exceptions = new Dictionary<WordLocation, IList<StopSignExceptionRule>>();
        }

        //public IList<StopSignExceptionRule> Exceptions { get; set; }

        //public IList<StopSignExceptionRule> SentenceStartExceptions { get; set; }

        //public IList<StopSignExceptionRule> SentenceMidExceptions { get; set; }

    }
}
