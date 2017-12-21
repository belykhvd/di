namespace TagsCloudContainer.Interfaces
{
    public interface IWordsFilter
    {
        Result<bool> IsNotStopWord(string word);
    }
}