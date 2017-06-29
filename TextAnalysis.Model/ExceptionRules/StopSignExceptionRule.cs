using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public abstract class StopSignExceptionRule
    {
        public string Sign { get; set; }

        public abstract bool IsMatch(AnalysisProcessContext processContext);

    }
}
