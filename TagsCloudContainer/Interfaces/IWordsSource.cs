using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    internal interface IWordsSource
    {
        IEnumerable<string> GetWords();
    }
}