using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class CircularCloudLayouter : ITagsCloudLayouter
    {
        private readonly ITagsCloudFormatter formatter;

        private readonly List<Rectangle> layout = new List<Rectangle>();
        private double helixParameter;

        public readonly Point CenterCoordinates;        
        
        public CircularCloudLayouter(ITagsCloudFormatter formatter, Point center)
        {            
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Coordinates of cloud center must be non-negative");            
            
            CenterCoordinates = center;
            this.formatter = formatter;
        }

        public Result<WordsLayout> GetLayout(IEnumerable<string> words)
        {
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            var wordsLayout = new WordsLayout();            

            var fontSizeMapping = GetFrequencyBasedFontSizeMapping(words);
            foreach (var mapping in fontSizeMapping)
            {
                var word = mapping.Item1;
                var fontSize = mapping.Item2;
                var font = new Font(formatter.FontFamily, fontSize);
                var rectangleSize = Size.Ceiling(graphics.MeasureString(word, font));
                var rectangle = PutNextRectangle(rectangleSize);

                if (rectangle.Left < 0)
                    return Result.Fail<WordsLayout>("Cannot create words layout: size of cloud is bigger than bitmap");

                wordsLayout = wordsLayout.With((rectangle, font, word));              
            }
            return Result.Ok(wordsLayout);
        }

        private static IEnumerable<(string, int)> GetFrequencyBasedFontSizeMapping(
            IEnumerable<string> words, int sizesCount = 5)
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
                var currentSize = 10 + (sizesCount - (i + 1) / ratio) * 5;
                mapping = mapping.Concat(sortedFrequencyGroups[i].Select(pair => (pair.Key, currentSize)));
            }

            return mapping;
        }

        private Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Invalid size of rectangle: sides lengths must be positive");

            Func<double, Point> helixEquasion = t =>
                new Point(CenterCoordinates.X + (int) (t * Math.Cos(t)),
                    CenterCoordinates.Y + (int) (t * Math.Sin(t)));

            Rectangle rectangle;

            while (true)
            {
                var rectangleCenter = helixEquasion(helixParameter);
                var rectangleLocation = GetCoordinatesByCenter(rectangleCenter, rectangleSize);
                rectangle = new Rectangle(rectangleLocation, rectangleSize);

                helixParameter += 0.5;

                if (!layout.Any(r => r.IntersectsWith(rectangle)))
                    break;                
            }

            layout.Add(rectangle);
            return rectangle;            
        }

        private static Point GetCoordinatesByCenter(Point rectangleCenter, Size rectangleSize)
            => new Point(rectangleCenter.X - rectangleSize.Width / 2, rectangleCenter.Y - rectangleSize.Height / 2);
    }
}