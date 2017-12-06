using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    internal interface ITagsCloudLayouter
    {        
        Rectangle PutNextRectangle(Size rectangleSize);
        IEnumerable<Rectangle> GetLayout();
    }
}
