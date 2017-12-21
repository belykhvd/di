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

        public Result<None> Save(Bitmap image)
        {
            return Result.OfAction(() => image.Save(filename, ImageFormat.Png));
        }
    }
}