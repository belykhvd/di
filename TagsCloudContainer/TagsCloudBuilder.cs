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

            //TODO: words to rectangles
            var bitmap = new Bitmap(700, 700);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, 700, 700));

            foreach (var word in preprocessedWords)
            {
                layouter.PutNextRectangle(new Size(15, 15));
            }
            graphics.DrawRectangles(new Pen(Color.DarkBlue), layouter.GetLayout().ToArray());

            return new TagsCloudBuilder(this, bitmap);
        }

        //private IReadOnlyDictionary<>

        public void SaveToFile(string filename)
        {
            saver.Save(cloudImage, filename);            
        }
    }
}
