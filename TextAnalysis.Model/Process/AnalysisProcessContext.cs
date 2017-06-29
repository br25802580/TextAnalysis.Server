using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public class AnalysisProcessContext
    {
        public AnalysisConfiguration AnalysisConfiguration { get; set; }
        public StopSignConfiguration StopSignConfiguration { get; set; }
        public Char Sign { get; set; }
        public string Word { get; set; }
        public int StopSignIndexIntoWord { get; set; }
        public string WordWithEndTrim { get; set; }
        public string AllText { get; set; }
        public WordLocation WordLocation { get; set; }
        public ProcessContextResult Output { get; set; }
        public char[] AvailableStopSigns { get; set; }

        public AnalysisProcessContext()
        {
            Output = new ProcessContextResult();
        }
    }
}
