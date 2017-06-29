using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public class AnalysisGeneralSettings
    {
        public bool EnableEnterSeperator { get; set; }
        public int? MaxShortcutLength { get; set; }

        public AnalysisGeneralSettings()
        {
            EnableEnterSeperator = true;
        }
    }
}
