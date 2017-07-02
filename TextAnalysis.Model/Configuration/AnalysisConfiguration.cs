using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis.Model
{
    public class AnalysisConfiguration
    {
        public IDictionary<char, StopSignConfiguration> StopSignConfigurations { get; set; }

        public ICollection<char> Signs
        {
            get { return StopSignConfigurations?.Keys; }
        }

        public bool EnableLinebreakSeperator { get; set; }
        public int? MaxShortcutLength { get; set; }

        public AnalysisConfiguration()
        {
            EnableLinebreakSeperator = true;
            StopSignConfigurations = new Dictionary<Char, StopSignConfiguration>();
        }
    }
}
