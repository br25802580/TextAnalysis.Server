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

        public StopSignConfiguration()
        {
            Exceptions = new Dictionary<WordLocation, IList<StopSignExceptionRule>>();
        }
    }
}
