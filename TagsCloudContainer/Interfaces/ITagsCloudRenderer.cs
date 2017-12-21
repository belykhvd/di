namespace TagsCloudContainer.Interfaces
{
    public interface ITagsCloudRenderer<T>
    {
        Result<T> Render(WordsLayout wordsLayout);        
    }
}