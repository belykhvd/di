namespace TagsCloudContainer.Interfaces
{
    internal interface ITagsCloudRenderer<T>
    {
        ITagsCloudRenderer<T> Render(WordsLayout wordsLayout);
        T GetRenderingResult();
    }
}