using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    internal interface ITagsCloudFormatter
    {
        FontFamily FontFamily { get; }
        Brush BackgroundBrush { get; }
        Brush FontBrush { get; }        
        Size ImageSize { get; }
    }
}