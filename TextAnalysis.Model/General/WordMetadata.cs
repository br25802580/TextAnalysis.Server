using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis.Model;

namespace TextAnalysis.Model
{
    public class WordMetadata
    {
        public string Word { get; set; }
        public WordLocation WordLocation { get; set; }
        public int WordEndIndex { get; set; }
    }
}
