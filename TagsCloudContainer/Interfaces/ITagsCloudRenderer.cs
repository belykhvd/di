using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    internal interface ITagsCloudRenderer
    {
        Bitmap Render(IEnumerable<string> tokens);
    }
}