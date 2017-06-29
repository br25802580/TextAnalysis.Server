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

        public AnalysisGeneralSettings GeneralSettings { get; set; }

        public ICollection<char> Signs
        {
            get { return StopSignConfigurations?.Keys; }
        }

        public AnalysisConfiguration()
        {
            GeneralSettings = new AnalysisGeneralSettings();
            StopSignConfigurations = new Dictionary<Char, StopSignConfiguration>();
        }
    }
}
