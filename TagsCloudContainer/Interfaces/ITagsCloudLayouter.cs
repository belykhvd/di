using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagsCloudLayouter
    {                
        WordsLayout GetLayout(IEnumerable<string> words);
    }
}