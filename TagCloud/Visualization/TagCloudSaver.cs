using SkiaSharp;
using TagCloud.Visualization.Settings;

namespace TagCloud.Visualization;

public class TagCloudSaver(string imageName, string imageFormat)
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
        if (!supportedFormats.TryGetValue(imageFormat, out var skFormat))
            throw new ArgumentException($"Unsupported image format: {imageFormat}");
        
        var pathToFile = $"{Path.Combine(Directory.GetCurrentDirectory(), imageName)}.{imageFormat}";
        using var file = File.OpenWrite(pathToFile);
        bitmap.Encode(skFormat, ImageQuality).SaveTo(file);
        
        return pathToFile;
    }
}