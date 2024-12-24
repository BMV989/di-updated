using Autofac;
using CommandLine;
using TagCloud;
using TagCloud.CloudLayouter;
using TagCloud.Visualization;
using TagCloud.WordsFilter;
using TagCloud.WordsReader;

namespace TagCloudClient;

internal class Program
{
    public static void Main(string[] args)
    {
         Parser.Default.ParseArguments<Options>(args)
             .WithParsed(settings => 
             {
                var container = BuildContainer(settings);
                var generator = container.Resolve<TagCloudGenerator>();
                Console.WriteLine("File saved in " + generator.GenerateTagCloud());
            });
    }
    
    private static IContainer BuildContainer(Options settings)
    {
        var builder = new ContainerBuilder();

        RegisterSettings(builder, settings);
        RegisterLayouter(builder, settings);
        RegisterWordsReaders(builder, settings);
        RegisterWordsFilters(builder, settings);

        builder.RegisterType<TagCloudGenerator>().AsSelf();
        builder.RegisterType<TagCloudBitmapGenerator>().AsSelf();
        builder.RegisterType<TagCloudSaver>().AsSelf();

        return builder.Build();
    }
    
    private static void RegisterSettings(ContainerBuilder builder, Options settings) 
    { 
        builder.RegisterInstance(SettingsFactory.BuildBitmapSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildTagCloudSaverSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildCsvReaderSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildWordReaderSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildFileReaderSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildCircularCloudLayouterSettings(settings)).AsSelf(); 
    }
   
    private static void RegisterWordsReaders(ContainerBuilder builder, Options settings)
    { 
        builder
            .RegisterType<FileReader>().As<IWordsReader>()
            .OnlyIf(_ => Path.GetExtension(settings.FilePath) == ".txt");

        builder
            .RegisterType<CsvFileReader>().As<IWordsReader>()
            .OnlyIf(_ => Path.GetExtension(settings.FilePath) == ".csv");
        
        builder
            .RegisterType<WordFileReader>().As<IWordsReader>()
            .OnlyIf(_ => Path.GetExtension(settings.FilePath) == ".docx");
    }
    
    private static void RegisterWordsFilters(ContainerBuilder builder, Options settings)
    {
        builder.RegisterType<LowercaseFilter>().As<IWordsFilter>();
        builder.RegisterType<BoringWordsFilter>().As<IWordsFilter>();
    }

    private static void RegisterLayouter(ContainerBuilder builder, Options settings)
    {
        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
    }
}