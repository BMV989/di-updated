using SkiaSharp;

namespace TagCloud.Visualization;

public interface ITagCloudSaver
{
    string Save(SKBitmap bitmap);
}