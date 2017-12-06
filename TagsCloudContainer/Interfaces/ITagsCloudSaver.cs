using System.Drawing;

namespace TagsCloudContainer
{
    internal interface ITagsCloudSaver
    {
        void Save(Bitmap image, string filename);
    }
}
