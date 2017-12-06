using System.Drawing;

namespace TagsCloudContainer
{
    internal class DefaultTagsCloudFormatter : ITagsCloudFormatter
    {
        public Brush BackgroundBrush { get; }
        public Brush FontBrush { get; }
        public FontFamily FontFamily { get; }
        public Size ImageSize { get; }

        public DefaultTagsCloudFormatter(Brush backgroundBrush, Brush fontBrush, FontFamily fontFamily, Size imageSize)
        {
            BackgroundBrush = backgroundBrush;
            FontBrush = fontBrush;
            FontFamily = fontFamily;
            ImageSize = imageSize;
        }
    }
}
