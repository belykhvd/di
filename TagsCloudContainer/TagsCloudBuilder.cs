using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    internal class TagsCloudBuilder
    {
        private readonly IWordsSource wordsSource;
        private readonly IWordsPreprocessor wordsPreprocessor;

        private readonly ITagsCloudLayouter layouter;
        private readonly ITagsCloudFormatter formatter;
        private readonly ITagsCloudSaver saver;
        
        private readonly Bitmap cloudImage;

        public TagsCloudBuilder(IWordsSource wordsSource, IWordsPreprocessor wordsPreprocessor, 
            ITagsCloudLayouter layouter, ITagsCloudFormatter formatter, ITagsCloudSaver saver)
        {
            this.wordsSource = wordsSource;
            this.wordsPreprocessor = wordsPreprocessor;
            this.layouter = layouter;
            this.formatter = formatter;
            this.saver = saver;            
        }

        private TagsCloudBuilder(TagsCloudBuilder coreBuilder, Bitmap cloudImage)
        {
            wordsSource = coreBuilder.wordsSource;
            wordsPreprocessor = coreBuilder.wordsPreprocessor;
            layouter = coreBuilder.layouter;
            formatter = coreBuilder.formatter;
            saver = coreBuilder.saver;
            this.cloudImage = cloudImage;
        }

        public TagsCloudBuilder Build(string filename)
        {
            var words = wordsSource.GetWords(filename);
            var preprocessedWords = wordsPreprocessor.ProcessWords(words);
            
            var bitmap = new Bitmap(formatter.ImageSize.Width, formatter.ImageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(formatter.BackgroundBrush,
                new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            
            var fontSizeMapping = GetFrequencyBasedFontSizeMapping(preprocessedWords, 1, 5);
            foreach (var mapping in fontSizeMapping)
            {
                var word = mapping.Item1;
                var fontSize = mapping.Item2;

                var font = new Font(formatter.FontFamily, 15 + fontSize * 5);
                var size = Size.Ceiling(graphics.MeasureString(word, font));                
                graphics.DrawString(word, font, formatter.FontBrush, layouter.PutNextRectangle(size));
            }                        

            return new TagsCloudBuilder(this, bitmap);
        }

        private static IEnumerable<Tuple<string, int>> GetFrequencyBasedFontSizeMapping(
            IEnumerable<string> words, int minFontSize, int maxFontSize)
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

            var sizesCount = maxFontSize - minFontSize + 1;
            var groupsCount = sortedFrequencyGroups.Count;
            var ratio = (int) Math.Ceiling((double)groupsCount / sizesCount);

            IEnumerable<Tuple<string, int>> mapping = new List<Tuple<string, int>>();            
            for (var i = 0; i < groupsCount; i++)
            {
                var currentSize = maxFontSize - (i + 1) / ratio;
                mapping = mapping
                    .Concat(sortedFrequencyGroups[i].Select(pair => Tuple.Create(pair.Key, currentSize)));
            }

            return mapping;
        }
                
        public void SaveToFile(string filename)
        {
            saver.Save(cloudImage, filename);            
        }
    }
}
