using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagsCloudSaver
    {
        Result<None> Save(Bitmap image);
    }
}