using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class RawTextReader : IWordsSource
    {
        private readonly string sourceFilePath;

        public RawTextReader(string sourceFilePath)
        {
            this.sourceFilePath = sourceFilePath;
        }

        public IEnumerable<string> GetWords() => File.ReadAllLines(sourceFilePath);
    }
}