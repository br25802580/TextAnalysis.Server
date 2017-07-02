using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis.Model;

namespace TextAnalysis.BL
{
    /// <summary>
    /// Configuration business layer
    /// </summary>
    class ConfigurationBL
    {
        #region Public Methods

        /// <summary>
        /// Init Configuration
        /// </summary>
        /// <returns></returns>
        public AnalysisConfiguration Init()
        {
            var configuration = new AnalysisConfiguration();
            var StopSignConfigurations = configuration.StopSignConfigurations;

            AddStopSignConfiguration(StopSignConfigurations, new DotConfigurationFactory());
            AddStopSignConfiguration(StopSignConfigurations, new QuestionMarkConfigurationFactory());
            StopSignConfigurations.Add(Consts.EXCLAMATION_MARK_SIGN, null);

            return configuration;
        }

        #endregion Public Methods

        #region Private Methods

        private void AddStopSignConfiguration(IDictionary<char, StopSignConfiguration> signConfigurations, SignConfigurationFactory signConfigurationFactory)
        {
            var signConfiguration = signConfigurationFactory.CreateSignConfiguration();
            var sign = signConfigurationFactory.Sign;

            signConfigurations.Add(sign, signConfiguration);
        }

        #endregion Private Methods
    }
}
