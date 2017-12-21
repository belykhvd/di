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

        public Result<IEnumerable<string>> GetWords()
        {
            return !File.Exists(sourceFilePath) 
                ? Result.Fail<IEnumerable<string>>($"Cannot find file with words: {sourceFilePath}") 
                : Result.Ok(File.ReadLines(sourceFilePath));
        }
    }
}