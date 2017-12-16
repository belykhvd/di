using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordsSource
    {
        IEnumerable<string> GetWords();
    }
}