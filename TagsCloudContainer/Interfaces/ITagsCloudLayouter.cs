using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    internal interface ITagsCloudLayouter
    {        
        Rectangle PutNextRectangle(Size rectangleSize);        
    }
}