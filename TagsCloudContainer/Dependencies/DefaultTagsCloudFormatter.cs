using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class DefaultTagsCloudFormatter : ITagsCloudFormatter
    {
        public FontFamily FontFamily { get; }
        public Brush BackgroundBrush { get; }
        public Brush FontBrush { get; }
        public Size ImageSize { get; }

        public DefaultTagsCloudFormatter(FontFamily fontFamily, Brush backgroundBrush, Brush fontBrush, Size imageSize)
        {
            FontFamily = fontFamily;
            BackgroundBrush = backgroundBrush;
            FontBrush = fontBrush;            
            ImageSize = imageSize;
        }
    }
}