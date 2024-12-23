using System.Drawing;
using SkiaSharp;
using TagCloud.Visualization.Settings;

namespace TagCloud.Visualization;

public class TagCloudBitmapGenerator(SKSizeI size, SKTypeface fontFamily, SKColor background, SKColor foreground)
{
    public TagCloudBitmapGenerator(TagCloudBitmapGeneratorSettings settings) : 
        this(settings.Size, settings.FontFamily, settings.Background, settings.Foreground)
    { }
    
    public SKBitmap GenerateBitmap(List<WordTag> tags)
    {
        if (fontFamily == null)
            throw new ArgumentNullException($"Could not find font family: {fontFamily}");
        
        var bitmap = new SKBitmap(size.Width,  size.Height);
        var canvas = new SKCanvas(bitmap);
        var paint = new SKPaint 
        { 
            Color = foreground,
            Style = SKPaintStyle.Stroke
        };
        
        canvas.Clear(background);

        var font = new SKFont(fontFamily, tags[0].FontSize);
        
        
       // TODO: Implement drawing of tags on canvas 

        return bitmap;
    }
}