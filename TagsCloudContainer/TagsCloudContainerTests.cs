using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using FakeItEasy;
using ApprovalTests;
using ApprovalTests.Reporters;
using TagsCloudContainer.Dependencies;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    [UseReporter(typeof(DiffReporter))]
    internal class TagsCloudContainerTests
    {
        [Test]
        public void WordsNormalizer_ShouldDoLowerCase()
        {            
            var normalizer = new DefaultWordsNormalizer();            
            var normalizedWord = normalizer.Normalize("lowerCase");
            normalizedWord.Should().Be("lowercase");
        }

        [Test]
        public void WordsFilter_ShouldFilterStopWords()
        {
            var filter = new DefaultWordsFilter(new[] {"i", "am"});
            var words = "i am stopword".Split(' ');

            var filteredWords = words.Where(filter.IsNotStopWord);
            filteredWords.Should().BeEquivalentTo("stopword");            
        }

        [Test]
        public void ApprovalFunctionalTest_ShouldRenderUsingFormatter()
        {
            var formatterMock = new Mock<ITagsCloudFormatter>();            
            formatterMock.SetupGet(m => m.FontFamily).Returns(FontFamily.GenericMonospace);
            formatterMock.SetupGet(m => m.BackgroundBrush).Returns(Brushes.Black);
            formatterMock.SetupGet(m => m.FontBrush).Returns(Brushes.Yellow);
            formatterMock.SetupGet(m => m.ImageSize).Returns(new Size(700, 700));

            var formatter = formatterMock.Object;

            var layouter = new CircularCloudLayouter(formatter, new Point(300, 300));
            var layout = layouter.GetLayout(new[] {"word", "word", "word", "anotherWord", "oneMoreWord"});

            var renderer = new DefaultTagsCloudRenderer<Bitmap>(formatter);
            var bitmap = renderer.Render(layout).GetRenderingResult();

            var filename = $"{nameof(ApprovalFunctionalTest_ShouldRenderUsingFormatter)}.png";
            var bitmapSaver = new PngTagsCloudSaver(filename);
            bitmapSaver.Save(bitmap);      
                        
            Approvals.VerifyFile(filename);
        }
    }
}