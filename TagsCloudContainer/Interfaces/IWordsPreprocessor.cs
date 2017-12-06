using System.Collections.Generic;

namespace TagsCloudContainer
{
    internal interface IWordsPreprocessor
    {
        IEnumerable<string> ProcessWords(IEnumerable<string> words);
    }
}