using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JetBrains.Annotations;

namespace TagsCloudContainer
{
    public class WordsLayout
    {
        [NotNull]
        public IEnumerable<(Rectangle, Font, string)> RectanglesWordsMapping { get; }
            = Enumerable.Empty<(Rectangle, Font, string)>();

        public WordsLayout()
        {            
        }

        public WordsLayout(
            [NotNull] IEnumerable<Rectangle> rectangles,
            [NotNull] IEnumerable<Font> fonts,
            [NotNull] IEnumerable<string> words)
        {
            RectanglesWordsMapping = rectangles
                .Zip(fonts, (rectangle, font) => (rectangle, font))
                .Zip(words, (rectangleFontPair, word) => (rectangleFontPair.Item1, rectangleFontPair.Item2, word));            
        }

        public WordsLayout([NotNull] IEnumerable<(Rectangle, Font, string)> rectanglesWordsMapping)
        {
            RectanglesWordsMapping = rectanglesWordsMapping;
        }

        public WordsLayout With((Rectangle, Font, string) mapping) 
            => new WordsLayout(RectanglesWordsMapping.Append(mapping));
    }
}