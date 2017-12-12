using System.Collections.Generic;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class DefaultWordsFilter : IWordsFilter
    {
        private readonly HashSet<string> stopWords;

        public DefaultWordsFilter(IEnumerable<string> stopWords)
        {
            this.stopWords = new HashSet<string>(stopWords);
        }

        public bool IsNotStopWord(string word) => !stopWords.Contains(word);
    }
}