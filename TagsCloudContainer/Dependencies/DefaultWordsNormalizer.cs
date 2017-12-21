using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class DefaultWordsNormalizer : IWordsNormalizer
    {
        public Result<string> Normalize(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return Result.Fail<string>("Cannot normalize word: it's null or whitespace");

            var normalizedWord = RemovePunctuation(word.ToLower());
            return Result.Ok(normalizedWord);
        }

        private static string RemovePunctuation(string word)
        {
            const string punctuation = ",.!?();";
            return punctuation.Aggregate(word, (current, c) => current.Replace(c.ToString(), ""));
        }
    }
}