using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public class ProcessContextResult
    {
        public int? WordCompleteSentenceAtIndex { get; set; }
        public int? AddSpaceAtIndex { get; set; }
        public IList<string> SentenceListResult { get; set; }

        public ProcessContextResult()
        {
            SentenceListResult = new List<string>();
        }
    }
}
