using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public IEnumerable<string> GetWords()
            => File.ReadAllText(sourceFilePath)
                .Split(new [] {"\n","\t","\r"," "}, StringSplitOptions.RemoveEmptyEntries)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim());
    }
}