# TextAnalysis.Server

This project was generated with VisualStudio 2015.
Please ensure you have VisualStudio 2015/17 installation on your computer.

## Instalation & Run
Please download the project, and run it with VisualStudio 2015+

## What the tool does?
Divide a segment into sentences by predefined separator characters, currently the specified separators are: period, exclamation mark, question mark.

## Support:
- A delimiter which is part of regex does not end the sentence (For example: "There is a meeting on 01.02.18")
- A delimiter which is part of a reserved word or a shortcut does not end the sentence. (For example: "Mr. David has M.A degree")
- Checking whether the sign ends the sentence, taking into consideration the location of the word relative to the sentence. For example, automatic numbering at the beginning of a sentence does not end the sentence.
- A sequence of characters of the same type is supported . (For example: "How are you???" )
- There is support for the situation when the user forgets to put space between sentences. (For example: "How are you?I'm fine." )
- A linebreak also causes a new sentence to start.
- Extra spaces placed after the end of the sentence are deleted. (For example: "How are you?\r\n  \r\nHow are you?" are 2 sentences) 
- In some cases, a space is inserted in the middle of a sentence, according to the logical context. (For example: "Mr.Cohen went" => "Mr. Cohen went")

## Does not support:
- Currently a test against a shortcut or a reserved word, is performed in the form of a case sensitive
- A reserved word as M.A. Requires a point at the end of a sentence. It is not supported a case the user does not write a last point of a reserved word.
- Regex list is not perfect.
- See Further Development section

## Configuration Models

#### AnalysisConfiguration 
```IDictionary<char, StopSignConfiguration> StopSignConfigurations```- A different configuration foreach stop sign.

```Bool EnableLinebreakSeperator```- Does a Linebreak should cause a new sentence to start.

#### StopSignConfiguration
```char Sign```- The sign for which the configuration is intended

```IDictionary<WordLocation,IList<StopSignExceptionRule>> Exceptions```- Different exception rules depend on the position of the word relative to the sentence.


#### StopSignExceptionRule
An abstract class that contains an examination of whether a character appearing in a specific word is unusual and does not cause a new sentence to begin.
The code is flexible, and a developer user can add different classes that inherit from this class, and contain different comparison logic.
For example, you can add a comparison for a mark that has a parallel bracket sign (parentheses or quotation marks) and raise a flag within the processContext.result, so that the outer processing logic knows that the internal text should be divided into sentences.

```cs
    public abstract class StopSignExceptionRule
    {
        public string Sign { get; set; }

        public abstract bool IsMatch(AnalysisProcessContext processContext);

    }
```

#### ComparisonExceptionRule : StopSignExceptionRule
Currently shared for testing of shortcuts and reserved words. In a real system, there should be separation

```IList<string> ReservedWords```- List of of shortcuts and reserved words.

#### RegexExceptionRule : StopSignExceptionRule
Checks whether a word or part matches one item from a list of regex. 
It is the user's responsibility to submit a regex list in a logical order, so as not to disrupt the text parsing of sentences.
For example: the regex of the ip will appear before the regex of the decimal number.

```IList<string> RegexList```- List of of regex.

## Further Development:
- Different configuration for different languages.
- Regexes should take into account localization setup. For example, floating point of a decimal number is displayed as a comma in some languages.
- It should be supported by an opening sign which has a parallel closing sign. For example: "", () [].
- Through dedicated api, it is appropriate that each user be able to configure a different configuration.
The end-user configuration should be convenient but dynamic, so that it will be easy for the end-user to edit it without knowing the configuration model which exists today.
- The code should be modified, so if regex is suitable for multiple signs (eg. url sign), it will be checked only once.
- A configuration which contains more details. For example: whether to add spaces according to the context, whether to delete unnecessary spaces, whether a sign without a consecutive space, can finish a sentence.
- Capturing exceptions in the server and client code
- For now, documentation into code available only in some of the files (for example into "ProcessBL" class)
