using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class DefaultWordsNormalizer : IWordsNormalizer
    {
        public string Normalize(string word) => RemovePunctuation(word.ToLower());

        private static string RemovePunctuation(string word)
        {
            const string punctuation = ",.!?();";
            return punctuation.Aggregate(word, (current, c) => current.Replace(c.ToString(), ""));
        }
    }
}