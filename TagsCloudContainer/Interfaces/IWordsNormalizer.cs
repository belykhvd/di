namespace TagsCloudContainer.Interfaces
{
    public interface IWordsNormalizer
    {
        Result<string> Normalize(string word);
    }
}