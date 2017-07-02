using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis.BL
{
    /// <summary>
    /// DataStore as replacement to DB
    /// </summary>
    public class DataStore
    {
        #region Public Methods

        public static IList<string> GetDotShortcuts()
        {
            var shortcuts = new List<string>();

            shortcuts.Add("Mr.");
            shortcuts.Add("Mss.");
            shortcuts.Add("St.");

            return shortcuts;
        }

        public static IList<string> GetDotReservedWords()
        {
            var reservedWords = new List<string>();

            reservedWords.Add("B.&.D.");
            reservedWords.Add("M.A.");
            reservedWords.Add("B.A.");

            return reservedWords;
        }

        public static IList<string> GetDotRegexList()
        {
            var regexList = new List<string>();

            //email
            regexList.Add(@"^(?()(.+?(?<!\\)@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            //url
            regexList.Add(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)");
            //ip
            regexList.Add(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            //phone number regex
            //This regex will validate a 10 - digit North American telephone number. Separators are not required, but can include spaces, hyphens, or periods. Parentheses around the area code are also optional.
            regexList.Add(@"^((([0-9]{1})*[- .(]*([0-9]{3})[- .)]*[0-9]{3}[- .]*[0-9]{4})+)*$");
            regexList.Add(@"^(([0-9]{1})*[- .(]*([0-9]{3})[- .)]*[0-9]{3}[- .]*[0-9]{4})+$");

            //data (MM.DD.YYYY)
            regexList.Add(@"^(0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)?[0-9]{2}$");

            //date (YYYY.MM.DD)
            regexList.Add(@"^(19|20)?[0-9]{2}[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])$");
            regexList.Add(@"^[0-9]{1,2}([,.][0-9]{1,2})?$");//1.1

            //decimal with float point
            regexList.Add(@"^-?\d*(\.\d+)?$");

            return regexList;
        }

        public static IList<string> GetDotRegexListForStartSentence()
        {
            var regexList = new List<string>();

            regexList.Add("^([0-9]{1,2}[.])$");//1.

            return regexList;
        }

        #endregion Public Methods
    }
}
