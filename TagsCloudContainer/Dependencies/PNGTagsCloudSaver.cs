using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Dependencies
{
    internal class PngTagsCloudSaver : ITagsCloudSaver
    {
        private readonly string filename;

        public PngTagsCloudSaver(string filename)
        {
            this.filename = filename;
        }

        public void Save(Bitmap image)
        {
            image.Save(filename, ImageFormat.Png);
        }
    }
}