using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    internal interface ITagsCloudLayouter
    {                
        WordsLayout GetLayout(IEnumerable<string> words);
    }
}