using System.Drawing;

namespace TagsCloudContainer
{
    internal interface ITagsCloudFormatter
    {
        Brush BackgroundBrush { get; }
        Brush FontBrush { get; }
        FontFamily FontFamily { get; }
        Size ImageSize { get; }        
    }
}
