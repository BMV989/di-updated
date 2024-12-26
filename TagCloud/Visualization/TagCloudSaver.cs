using SkiaSharp;
using TagCloud.Visualization.Settings;

namespace TagCloud.Visualization;

public class TagCloudSaver(string imageName, string imageFormat) : ITagCloudSaver
{
    private const int ImageQuality = 80;

    private readonly Dictionary<string, SKEncodedImageFormat> supportedFormats = new()
    {
        { "png", SKEncodedImageFormat.Png },
        { "jpeg", SKEncodedImageFormat.Jpeg },
        { "bmp", SKEncodedImageFormat.Bmp },
        { "webp", SKEncodedImageFormat.Webp },
    };

    public TagCloudSaver(TagCloudSaverSettings settings)
        : this(settings.ImageName, settings.ImageFormat)
    { }
    
    public string Save(SKBitmap bitmap)
    {
        var pathToFile = $"{Path.Combine(Directory.GetCurrentDirectory(), imageName)}.{
            (supportedFormats.ContainsKey(imageFormat) ? imageFormat : "png")}";
        using var file = File.OpenWrite(pathToFile);
        bitmap.Encode(supportedFormats.GetValueOrDefault(imageFormat, SKEncodedImageFormat.Png),
            ImageQuality).SaveTo(file);
        
        return pathToFile;
    }
}