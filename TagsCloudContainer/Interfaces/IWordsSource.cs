using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordsSource
    {
        Result<IEnumerable<string>> GetWords();
    }
}