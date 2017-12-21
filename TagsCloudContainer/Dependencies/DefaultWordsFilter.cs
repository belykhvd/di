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

        public Result<bool> IsNotStopWord(string word)
        {            
            return Result.Ok(!string.IsNullOrWhiteSpace(word) && !stopWords.Contains(word));
        }
    }
}