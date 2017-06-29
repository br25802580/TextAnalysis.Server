using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis.Model;

namespace TextAnalysis.BL
{
    /// <summary>
    /// A Singleton class which contains system-wide objects
    /// </summary>
    public class TextAnalysisRepository
    {
        #region Private Fields

        private static volatile TextAnalysisRepository instance;
        private static object syncRoot = new Object();

        #endregion Private Fields

        #region Properties
        public static TextAnalysisRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new TextAnalysisRepository();
                    }
                }

                return instance;
            }
        }

        public AnalysisConfiguration AnalysisConfiguration { get; set; }

        #endregion Properties

        #region Ctor

        private TextAnalysisRepository()
        {
            AnalysisConfiguration = new ConfigurationBL().Init();
        }

        #endregion Ctor
    }
}
