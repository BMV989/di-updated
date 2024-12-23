using SkiaSharp;
using TagCloud.CloudLayouter.Settings;
using TagCloud.PointsGenerator;

namespace TagCloud.CloudLayouter;

public class CircularCloudLayouter(SKPoint center, double radius = 1, double angleOffset = 0.5) : ICloudLayouter
{
    
    private readonly List<SKRect> rectangles = [];
    private readonly SpiralPointsGenerator pointsGenerator = new(center, radius, angleOffset);
    
    public SKPoint Center => center;

    public CircularCloudLayouter(CircularCloudLayouterSettings settings) : this(settings.Center, settings.Radius,
        settings.AngleOffset)
    { }
    
    public SKRect PutNextRectangle(SKSize rectangleSize)
    {
        while (true)
        {
            var rectanglePosition = pointsGenerator.GetNextPoint();
            var rectangle = CreateRectangleWithCenter(rectanglePosition, rectangleSize);

            if (rectangles.Any(rectangle.IntersectsWith)) continue;
            
            rectangles.Add(rectangle);
            
            return rectangle;
        }
    }

    private static SKRect CreateRectangleWithCenter(SKPoint center, SKSize rectangleSize)
    {
        var left = center.X - rectangleSize.Width / 2;
        var top = center.Y - rectangleSize.Height / 2;
        
        return new SKRect(left, top, left + rectangleSize.Width, top + rectangleSize.Height);
    }
}

