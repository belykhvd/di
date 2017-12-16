namespace TagsCloudContainer.Interfaces
{
    public interface ITagsCloudRenderer<T>
    {
        ITagsCloudRenderer<T> Render(WordsLayout wordsLayout);
        T GetRenderingResult();
    }
}