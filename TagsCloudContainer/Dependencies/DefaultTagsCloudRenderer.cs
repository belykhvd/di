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
            
        public Result<T> Render(WordsLayout layout)
        {            
            var bitmap = new Bitmap(formatter.ImageSize.Width, formatter.ImageSize.Height);
            if (!(bitmap is T))
                return Result.Fail<T>($"Cannot render layout: used renderer render to Bitmap but T is {typeof(T)}");

            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(formatter.BackgroundBrush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            foreach (var mapping in layout.RectanglesWordsMapping)
            {
                var rectangle = mapping.Item1;
                var font = mapping.Item2;
                var word = mapping.Item3;

                graphics.DrawString(word, font, formatter.FontBrush, rectangle);
            }
                                    
            return (T)Convert.ChangeType(bitmap, typeof(T));
        }
    }
}