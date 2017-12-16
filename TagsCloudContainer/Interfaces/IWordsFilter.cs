namespace TagsCloudContainer.Interfaces
{
    public interface IWordsFilter
    {
        bool IsNotStopWord(string word);
    }
}