using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class DefaultWordsNormalizer : IWordsNormalizer
    {
        public string Normalize(string word) => word.ToLower();
    }
}