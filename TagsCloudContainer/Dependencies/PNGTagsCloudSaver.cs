using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer
{
    internal class PNGTagsCloudSaver : ITagsCloudSaver
    {
        public void Save(Bitmap image, string filename)
        {
            image.Save(filename, ImageFormat.Png);
        }
    }
}
