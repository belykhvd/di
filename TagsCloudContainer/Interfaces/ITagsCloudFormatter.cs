using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagsCloudFormatter
    {
        FontFamily FontFamily { get; }
        Brush BackgroundBrush { get; }
        Brush FontBrush { get; }        
        Size ImageSize { get; }
    }
}