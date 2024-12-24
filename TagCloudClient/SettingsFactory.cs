using TagCloud.CloudLayouter.Settings;
using TagCloud.Visualization.Settings;
using TagCloud.WordsReader.Settings;

namespace TagCloudClient;

public static class SettingsFactory
{
     public static FileReaderSettings BuildFileReaderSettings(Options options) => 
          new(options.FilePath, options.UsingEncoding);
     
      public static TagCloudBitmapGeneratorSettings BuildBitmapSettings(Options options) =>
          new(options.Size, options.FontFamily, options.BackgroundColor, options.ForegroundColor);

      public static CircularCloudLayouterSettings BuildCircularCloudLayouterSettings(Options options) => 
          new(options.Center, options.Radius, options.AngleOffset);

      public static WordFileReaderSettings BuildWordReaderSettings(Options options) => 
          new(options.FilePath);
      
      public static CsvFileReaderSettings BuildCsvReaderSettings(Options options) => 
          new(options.FilePath, options.Culture);

      public static TagCloudSaverSettings BuildTagCloudSaverSettings(Options options)
        => new(options.ImageName, options.ImageFormat);
}