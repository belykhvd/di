using System.Drawing;

namespace TagsCloudContainer
{
    internal interface ITagsCloudLayouter
    {        
        Rectangle PutNextRectangle(Size rectangleSize);        
    }
}
