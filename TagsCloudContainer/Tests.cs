using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using TagsCloudContainer.Dependencies;

namespace TagsCloudContainer
{
    internal class Tests
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
    }
}