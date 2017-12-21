using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagsCloudLayouter
    {                
        Result<WordsLayout> GetLayout(IEnumerable<string> words);
    }
}