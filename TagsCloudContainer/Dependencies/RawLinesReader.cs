using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class RawLinesReader : IWordsSource
    {
        private readonly string sourceFilePath;

        public RawLinesReader(string sourceFilePath)
        {
            this.sourceFilePath = sourceFilePath;
        }

        public IEnumerable<string> GetWords() => File.ReadAllLines(sourceFilePath);
    }
}