using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using FakeItEasy;
using TagsCloudContainer.Dependencies;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
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
            //formatter.Setup(fmt => fmt.FontFamily).Returns(FontFamily.GenericMonospace);
            //formatter.Object.FontFamily.Should().Be(FontFamily.GenericMonospace);
            /*var formatter = Mock.Of<ITagsCloudFormatter>(d =>
                d.FontFamily == FontFamily.GenericMonospace);
            TestContext.WriteLine(formatter.FontFamily);*/
            var formatter = A.Fake<ITagsCloudFormatter>();
            A.CallTo(() => formatter.FontFamily).Returns(FontFamily.GenericMonospace);
            formatter.FontFamily.Should().Be(FontFamily.GenericMonospace);
        }
    }
}