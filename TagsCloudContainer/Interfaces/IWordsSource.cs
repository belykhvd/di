using System.Collections.Generic;

namespace TagsCloudContainer
{
    internal interface IWordsSource
    {
        IEnumerable<string> GetWords(string sourceFilePath);
    }
}