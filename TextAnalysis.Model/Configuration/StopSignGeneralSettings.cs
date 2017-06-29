using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public class StopSignGeneralSettings
    {
        public IList<string> RegexList { get; set; }
        public IList<string> Shortcuts { get; set; }
        public IList<string> ReservedWords { get; set; }

        public StopSignGeneralSettings()
        {
            //RegexList = new List<string>();
            //Shortcuts = new List<string>();
            //ReservedWords = new List<string>();
        }
    }
}
