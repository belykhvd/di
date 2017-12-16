using System;
using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class DefaultTagsCloudRenderer<T> : ITagsCloudRenderer<T>
    {
        private readonly ITagsCloudFormatter formatter;
       
        public DefaultTagsCloudRenderer(ITagsCloudFormatter formatter)
        {            
            this.formatter = formatter;
        }

        private readonly Bitmap renderedBitmap;

        private DefaultTagsCloudRenderer(DefaultTagsCloudRenderer<T> renderer, Bitmap bitmap)
        {
            formatter = renderer.formatter;
            renderedBitmap = bitmap;
        }
        
        public T GetRenderingResult()
        {
            if (renderedBitmap == null)
                throw new InvalidOperationException("You need call Render method first");
            if (!(renderedBitmap is T))
                throw new InvalidCastException("Passed generic parameter is not appropriate for this renderer");

            var bitmapAsT = (T)Convert.ChangeType(renderedBitmap, typeof(T));
            return bitmapAsT;
        }

        public ITagsCloudRenderer<T> Render(WordsLayout layout)
        {
            var bitmap = new Bitmap(formatter.ImageSize.Width, formatter.ImageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(formatter.BackgroundBrush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            foreach (var mapping in layout.RectanglesWordsMapping)
            {
                var rectangle = mapping.Item1;
                var font = mapping.Item2;
                var word = mapping.Item3;

                graphics.DrawString(word, font, formatter.FontBrush, rectangle);
            }
            
            return new DefaultTagsCloudRenderer<T>(this, bitmap);
        }        
    }
}