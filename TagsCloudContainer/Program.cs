using System.Drawing;
using CommandLine.Parsing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagCloudVisualization;
using TagsCloudContainer.Dependencies;

namespace TagsCloudContainer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IWordsSource>().ImplementedBy<RawTextReader>());
            container.Register(Component.For<IWordsPreprocessor>().ImplementedBy<DefaultWordsPreprocessor>());                        
            container.Register(Component.For<ITagsCloudFormatter>().ImplementedBy<DefaultTagsCloudFormatter>());
            container.Register(Component.For<ITagsCloudSaver>().ImplementedBy<PNGTagsCloudSaver>());
            container.Register(Component.For<ITagsCloudLayouter>().ImplementedBy<CircularCloudLayouter>()
                .DynamicParameters((a, b) => b["center"] = new Point(300, 300)));
            container.Register(Component.For<TagsCloudBuilder>().ImplementedBy<TagsCloudBuilder>());


            var tagsCloudBuilder = container.Resolve<TagsCloudBuilder>();
            tagsCloudBuilder.Build("file.txt").SaveToFile("cloud.png");
        }
    }
}