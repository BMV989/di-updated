using SkiaSharp;
using TagCloud.CloudLayouter;
using TagCloud.Visualization.Settings;

namespace TagCloud.Visualization;

public class TagCloudBitmapGenerator(SKSizeI size, SKTypeface fontFamily, SKColor background, SKColor foreground, 
    ICloudLayouter layouter)
{
    public TagCloudBitmapGenerator(TagCloudBitmapGeneratorSettings settings, ICloudLayouter layouter) : 
        this(settings.Size, settings.FontFamily, settings.Background, settings.Foreground, layouter)
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
            Style = SKPaintStyle.StrokeAndFill
        };
        
        canvas.Clear(background);
        
        foreach (var tag in tags)
        {
            var font = new SKFont(fontFamily, tag.FontSize);
            var wordSize = MeasureWord(tag.Word, font);
            
            var positionRect = layouter.PutNextRectangle(wordSize);
            canvas.DrawText(tag.Word, positionRect.Left, positionRect.Top + font.Metrics.CapHeight, font, paint);
        }
        
        return bitmap;
    }

    private static SKSize MeasureWord(string word, SKFont font)
    {
        font.MeasureText(word, out var bounds);
        return new SKSize(bounds.Width, bounds.Height);
    }
}