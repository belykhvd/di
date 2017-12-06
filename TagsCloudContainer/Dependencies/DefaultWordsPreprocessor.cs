using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudContainer
{
    internal class DefaultWordsPreprocessor : IWordsPreprocessor
    {
        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}