namespace TagsCloudContainer.Interfaces
{
    internal interface IWordsFilter
    {
        bool IsNotStopWord(string word);
    }
}