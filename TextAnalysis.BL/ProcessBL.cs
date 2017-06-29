using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalysis.Model;

namespace TextAnalysis.BL
{
    /// <summary>
    /// Business logic for process an input string, and seperate it to multi sentences
    /// </summary>
    public class ProcessBL
    {
        #region Public Methods

        /// <summary>
        /// Divide text into sentences, using separators like a dot
        /// </summary>
        /// <param name="text">Input text for processing</param>
        /// <returns>Sentences generated from text processing</returns>
        public IList<string> Process(string text)
        {
            AnalysisConfiguration analysisConfiguration = TextAnalysisRepository.Instance.AnalysisConfiguration;
            AnalysisProcessContext processContext = new AnalysisProcessContext();

            processContext.AnalysisConfiguration = analysisConfiguration;

            //If enter key (\n or \r\n) declared as seperator- We seperate input text to segments 
            if (analysisConfiguration.GeneralSettings.EnableEnterSeperator)
            {
                IList<string> segments = GetTextSegments(text);

                //Processing all segments contained in the input text
                foreach (var segment in segments)
                {
                    if (!string.IsNullOrWhiteSpace(segment))
                        ProcessTextSegment(segment, processContext);
                }
            }
            else
                //Process input text as one segmant
                ProcessTextSegment(text, processContext);

            return processContext.Output.SentenceListResult;
        }

        #endregion Public Methods

        #region private Methods

        /// <summary>
        /// Seperate text into segments
        /// </summary>
        /// <param name="text">Input text to seperate it into segments</param>
        /// <returns>segments contained in the input text</returns>
        private IList<string> GetTextSegments(string text)
        {
            return text.Split(new string[] { Consts.NEW_LINE, Consts.NEW_LINE_RIGHT }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Process one text segment, and divide it into sentences
        /// </summary>
        /// <param name="segment">Input segment to process it</param>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        private void ProcessTextSegment(string segment, AnalysisProcessContext processContext)
        {
            string currentSentence;
            IList<string> sentenceListResult = processContext.Output.SentenceListResult;

            processContext.AllText = segment;

            //While current segment is not empty,
            //We search for next sentence, and append it to sentenceListResult
            while (processContext.AllText.Count() > 0)
            {
                currentSentence = FindNextSentence(processContext);
                sentenceListResult.Add(currentSentence);
            }
        }

        /// <summary>
        /// Find next sentence of the segment.
        /// Note: Each time a sentence is found, it is deleted from the segment, 
        /// so the function actually searches for the first sentence that the segment contains
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <returns></returns>
        private string FindNextSentence(AnalysisProcessContext processContext)
        {
            string currentSentence = string.Empty;
            int stopIndex = 0;
            int currentIndex = 0;
            bool stopSignCompleteSentenceFound = false;

            processContext.AvailableStopSigns = processContext.AnalysisConfiguration.Signs.ToArray();

            //As long as the closing sign which ends the sentence has not yet been found, we continue search for it
            while (!stopSignCompleteSentenceFound)
            {
                stopSignCompleteSentenceFound = SearchNextStopSign(processContext, out stopIndex, ref currentIndex);
            }

            string text = processContext.AllText;

            //Stop sign whice complete sentence was found!

            //If stop sign has found into sentence
            if (stopIndex > -1)
            {
                //Set currentSentence member with substring from text beginning till stopIndex
                currentSentence = text.Substring(0, stopIndex + 1);

                //Remove the found sentence from the total text
                text = processContext.AllText.Remove(0, stopIndex + 1).TrimStart();
            }
            //If stop sign has not found into sentence
            else
            {
                //Set currentSentence to the entire text
                currentSentence = processContext.AllText.TrimEnd();
                //Clear text;
                text = string.Empty;
            }

            //Set processContext.AllText to remaining text without the sentence which found
            processContext.AllText = text;

            return currentSentence;
        }

        /// <summary>
        /// Search for next stop sign into current segment
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <param name="stopIndex">out param which contains index of the found sign</param>
        /// <param name="startIndex">ref param, in order to update startIndex to search from there</param>
        /// <returns></returns> 
        private bool SearchNextStopSign(AnalysisProcessContext processContext, out int stopIndex, ref int startIndex)
        {
            bool stopSignCompleteSentenceFound = false;
            string text = processContext.AllText;

            //Remove leading spaces
            text = text.TrimStart();

            //Search for one of the closing signs, 
            //the closest to the beginning of the sentence
            stopIndex = text.IndexOfAny(processContext.AvailableStopSigns, startIndex);

            //If a closing sign found- We check if it complete a sentence
            if (stopIndex > -1)
            {
                //Find word which includes current stopIndex
                WordMetadata wordMetadata = FindWord(text, stopIndex);

                //Set processContext by found stopSign and its parent word
                SetContextScope(processContext, wordMetadata, stopIndex);

                //Check whether the word is exceptional, and therefore does not complete a sentence
                stopSignCompleteSentenceFound = !IsExceptionalWord(processContext);

                //If stop sign not complete a sentence-
                //Update neccessary dadta due to processContext output
                if (!stopSignCompleteSentenceFound)
                    HandleOutputContext(processContext, ref stopIndex, ref startIndex, ref stopSignCompleteSentenceFound, ref text);
            }

            //If no closing sign found- Athe whole remaining text is a new sentence
            else
            {
                stopSignCompleteSentenceFound = true;
            }

            return stopSignCompleteSentenceFound;
        }

        /// <summary>
        /// Update neccessary dadta due to processContext output
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <param name="stopIndex"></param>
        /// <param name="startIndex"></param>
        /// <param name="stopSignCompleteSentenceFound"></param>
        /// <param name="text"></param>
        private static void HandleOutputContext(AnalysisProcessContext processContext, ref int stopIndex, ref int startIndex, ref bool stopSignCompleteSentenceFound, ref string text)
        {

            int? stopSignIndexGap = processContext.Output.StopSignIndexGap;

            //If stop sign which complete sentence was found in a specifix index gap-
            if (stopSignIndexGap.HasValue)
            {
                //Set correct stop index, and assign stopIndexFound to true
                stopSignCompleteSentenceFound = true;
                stopIndex = stopIndex + stopSignIndexGap.Value;
                processContext.Output.StopSignIndexGap = null;
            }
            else
                //If stopSignIndexGap is empty - Set startIndex to the following char of stopIndex variable
                startIndex = stopIndex + 1;

            int? seperateWordAtIndex = processContext.Output.SeperateWordAtIndex;

            //If there is a need to add a space separator within the text
            if (seperateWordAtIndex.HasValue)
            {
                //Insert space at a specific index into text 
                startIndex = stopIndex + seperateWordAtIndex.Value + 1;
                text = text.Insert(startIndex, " ");
                processContext.AllText = text;

                processContext.Output.SeperateWordAtIndex = null;
            }
        }

        /// <summary>
        /// Set process context scope- Update variables ny found word and sign
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <param name="wordMetadata"></param>
        /// <param name="stopIndex"></param>
        private void SetContextScope(AnalysisProcessContext processContext, WordMetadata wordMetadata, int stopIndex)
        {
            processContext.Word = wordMetadata.Word;
            processContext.WordWithEndTrim = processContext.Word.TrimEnd(processContext.AvailableStopSigns);
            processContext.Sign = processContext.AllText[stopIndex];
            processContext.WordLocation = wordMetadata.WordLocation;
            processContext.StopSignIndexIntoWord = processContext.Word.IndexOf(processContext.Sign);
        }

        /// <summary>
        /// Find out if a word is an exception, and therefore does not complete a sentence
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <returns></returns>
        private bool IsExceptionalWord(AnalysisProcessContext processContext)
        {
            var sign = processContext.Sign;
            var stopSignConfigurations = processContext.AnalysisConfiguration.StopSignConfigurations;
            StopSignConfiguration stopSignConfiguration = null;
            bool isExceptionalWord = false;

            if (stopSignConfigurations.TryGetValue(processContext.Sign, out stopSignConfiguration))
            {
                processContext.StopSignConfiguration = stopSignConfiguration;

                //Find out if a word is an exception, by exception which correspond to the location of the word
                isExceptionalWord = IsAnyExceptionMatch(processContext, processContext.WordLocation);

                if (!isExceptionalWord)
                    //Find out if a word is an exception, by general exceptions
                    isExceptionalWord = IsAnyExceptionMatch(processContext, WordLocation.Anywhere);
            }

            return isExceptionalWord;
        }

        /// <summary>
        /// Find out if any exception is matched to current word.
        /// Compare word to exceptions which correspond to the location of the word
        /// </summary>
        /// <param name="processContext">A processing context which lives until the process is finished, 
        /// and stores data for the process</param>
        /// <param name="wordLocation">A place of a word relative to a sentence: at first, in the middle or at the end</param>
        /// <returns></returns>
        private bool IsAnyExceptionMatch(AnalysisProcessContext processContext, WordLocation wordLocation)
        {
            IList<StopSignExceptionRule> exceptions = null;
            bool isAnyExceptionMatch = false;
            bool? exceptionsFound = processContext?.StopSignConfiguration?.Exceptions?.TryGetValue(wordLocation, out exceptions);

            exceptionsFound = exceptionsFound.HasValue && exceptions != null && exceptions.Count() > 0;

            if (exceptionsFound == true)
                isAnyExceptionMatch = exceptions.Any(exception => exception.IsMatch(processContext));

            return isAnyExceptionMatch;
        }

        /// <summary>
        /// Extract a word from text that is located across a particular index
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="index">An index in which the word located across</param>
        /// <returns></returns>
        private WordMetadata FindWord(string text, int index)
        {
            WordMetadata wordMetadata = new WordMetadata();
            string word = string.Empty;

            //Find prevSpaceIndex relative to given index
            int prevSpaceIndex = text.LastIndexOf(Consts.SPACE_SIGN, index, index + 1);
            //Find nextSpaceIndex relative to given index
            int nextSpaceIndex = text.IndexOf(Consts.SPACE_SIGN, index);

            if (prevSpaceIndex == -1)
            {
                wordMetadata.WordLocation = WordLocation.Start;
                prevSpaceIndex = 0;
            }
            else
            {
                prevSpaceIndex++;
                wordMetadata.WordLocation = WordLocation.Mid;
            }

            if (nextSpaceIndex == -1)
                nextSpaceIndex = text.Count() - 1;

            //Get word by prevSpaceIndex and nextSpaceIndex
            wordMetadata.Word = text.Substring(prevSpaceIndex, nextSpaceIndex - prevSpaceIndex);
            wordMetadata.WordEndIndex = nextSpaceIndex;

            return wordMetadata;
        }

        #endregion Private Methods
    }
}
