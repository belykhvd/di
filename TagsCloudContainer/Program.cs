using System.Drawing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommandLine;

namespace TagsCloudContainer
{
    internal class Program
    {
        private class CommandLineOptions
        {
            [Option('i', "input", Required = true, HelpText = "Input file with words")]
            public string InputFile { get; set; }

            [Option('o', "output", Required = true, HelpText = "Output file where to write tags cloud")]
            public string OutputFile { get; set; }

            [Option("back", HelpText = nameof(BackgroundBrush))]
            public Brush BackgroundBrush { get; set; }

            [Option("font", HelpText = nameof(FontBrush))]
            public Brush FontBrush { get; set; }

            [Option("fontfamily", HelpText = nameof(FontFamily))]
            public FontFamily FontFamily { get; set; }

            [Option("size", HelpText = "Size of the image")]
            public Size ImageSize { get; set; }
        }

        private static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            Parser.Default.ParseArguments(args, options);

            var container = new WindsorContainer();
            container.Register(Component.For<IWordsSource>().ImplementedBy<RawTextReader>());
            container.Register(Component.For<IWordsPreprocessor>().ImplementedBy<DefaultWordsPreprocessor>());
            
            container.Register(Component.For<ITagsCloudFormatter>().ImplementedBy<DefaultTagsCloudFormatter>()
                .DependsOn(
                    Property.ForKey(nameof(options.BackgroundBrush)).Eq(options.BackgroundBrush ?? Brushes.Black),
                    Property.ForKey("FontBrush").Eq(Brushes.DarkOrange),
                    Property.ForKey("FontFamily").Eq(FontFamily.GenericMonospace),
                    Property.ForKey("ImageSize").Eq(new Size(300, 300))
                ));                        

            container.Register(Component.For<ITagsCloudLayouter>().ImplementedBy<CircularCloudLayouter>()
                .DynamicParameters((k, d) =>
                    {
                        var imageSize = container.Resolve<ITagsCloudFormatter>().ImageSize;
                        d["center"] = new Point(imageSize.Width / 2, imageSize.Height / 2);
                    }
                ));

            container.Register(Component.For<ITagsCloudSaver>().ImplementedBy<PNGTagsCloudSaver>());
            container.Register(Component.For<TagsCloudBuilder>().ImplementedBy<TagsCloudBuilder>());

            var tagsCloudBuilder = container.Resolve<TagsCloudBuilder>();
            tagsCloudBuilder.Build(options.InputFile).SaveToFile(options.OutputFile);
        }
    }
}