using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class DefaultTagsCloudRenderer : ITagsCloudRenderer
    {
        private readonly ITagsCloudLayouter layouter;
        private readonly ITagsCloudFormatter formatter;
        
        public DefaultTagsCloudRenderer(ITagsCloudLayouter layouter, ITagsCloudFormatter formatter)
        {
            this.layouter = layouter;
            this.formatter = formatter;
        }
     
        public Bitmap Render(IEnumerable<string> tokens)
        {
            var bitmap = new Bitmap(formatter.ImageSize.Width, formatter.ImageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(formatter.BackgroundBrush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            var fontSizeMapping = GetFrequencyBasedFontSizeMapping(tokens);
            foreach (var mapping in fontSizeMapping)
            {
                var word = mapping.Item1;
                var fontSize = mapping.Item2;

                var font = new Font(formatter.FontFamily, fontSize);
                var size = Size.Ceiling(graphics.MeasureString(word, font));
                graphics.DrawString(word, font, formatter.FontBrush, layouter.PutNextRectangle(size));
            }

            return bitmap;
        }

        private static IEnumerable<(string, int)> GetFrequencyBasedFontSizeMapping(
            IEnumerable<string> words, int sizesCount=5)
        {            
            var frequencyCounter = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyCounter.ContainsKey(word))
                    frequencyCounter[word] = 0;
                frequencyCounter[word]++;
            }

            var sortedFrequencyGroups = frequencyCounter.GroupBy(pair => pair.Value).ToList();
            sortedFrequencyGroups.Sort((firstGroup, secondGroup) => -firstGroup.Key.CompareTo(secondGroup.Key));
            
            var groupsCount = sortedFrequencyGroups.Count;
            var ratio = (int)Math.Ceiling((double)groupsCount / sizesCount);
                        
            IEnumerable<(string, int)> mapping = new List<(string, int)>();
            for (var i = 0; i < groupsCount; i++)
            {
                var currentSize = 15 + (sizesCount - (i + 1) / ratio) * 5;
                mapping = mapping
                    .Concat(sortedFrequencyGroups[i].Select(pair => (pair.Key, currentSize)));
            }

            return mapping;
        }
    }
}
