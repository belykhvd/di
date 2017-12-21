using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommandLine;
using TagsCloudContainer.Dependencies;
using TagsCloudContainer.Interfaces;

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
        }

        private static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            Parser.Default.ParseArguments(args, options);

            var container = new WindsorContainer()
                .Register(Component.For<IWordsSource>().ImplementedBy<RawTextReader>()
                    .DependsOn(Dependency.OnValue("sourceFilePath", options.InputFile)));

            var stopWords = new HashSet<string>(File.ReadLines("stopwords.txt"));
            container.Register(Component.For<IWordsNormalizer>().ImplementedBy<DefaultWordsNormalizer>());
            container.Register(Component.For<IWordsFilter>().ImplementedBy<DefaultWordsFilter>()
                .DependsOn(Dependency.OnValue("stopWords", stopWords)));

            container
                .Register(Component.For<ITagsCloudFormatter>().ImplementedBy<DefaultTagsCloudFormatter>()
                    .DependsOn(
                        Property.ForKey("FontFamily").Eq(FontFamily.GenericMonospace),
                        Property.ForKey("BackgroundBrush").Eq(Brushes.Black),
                        Property.ForKey("FontBrush").Eq(Brushes.Yellow),                        
                        Property.ForKey("ImageSize").Eq(new Size(700, 700))
                    ))
                .Register(Component.For<ITagsCloudLayouter>().ImplementedBy<CircularCloudLayouter>()
                    .DynamicParameters((k, d) =>
                        {
                            var imageSize = container.Resolve<ITagsCloudFormatter>().ImageSize;
                            d["center"] = new Point(imageSize.Width / 2, imageSize.Height / 2);
                        }
                    ))
                .Register(Component.For<ITagsCloudRenderer<Bitmap>>().ImplementedBy<DefaultTagsCloudRenderer<Bitmap>>());

            container.Register(Component.For<ITagsCloudSaver>().ImplementedBy<PngTagsCloudSaver>()
                .DependsOn(Dependency.OnValue("filename", options.OutputFile)));

            try
            {
                var words = container.Resolve<IWordsSource>().GetWords().GetValueOrThrow();
                var normalizedWords = words
                    .Select(word => container.Resolve<IWordsNormalizer>().Normalize(word).GetValueOrThrow());
                var tokens = normalizedWords
                    .Where(word => container.Resolve<IWordsFilter>().IsNotStopWord(word).GetValueOrThrow());
                var layout = container.Resolve<ITagsCloudLayouter>().GetLayout(tokens).GetValueOrThrow();
                var tagsCloudBitmap = container.Resolve<ITagsCloudRenderer<Bitmap>>().Render(layout).GetValueOrThrow();
                container.Resolve<ITagsCloudSaver>().Save(tagsCloudBitmap).GetValueOrThrow();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey(true);
            }            
        }
    }
}