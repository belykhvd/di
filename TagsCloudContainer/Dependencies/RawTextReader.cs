using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    internal class RawTextReader : IWordsSource
    {
        public IEnumerable<string> GetWords(string sourceFilePath) => File.ReadAllLines(sourceFilePath);
    }
}