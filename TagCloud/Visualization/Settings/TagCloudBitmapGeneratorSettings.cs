using SkiaSharp;

namespace TagCloud.Visualization.Settings;

public record TagCloudBitmapGeneratorSettings(SKSizeI Size, SKTypeface FontFamily, SKColor Background, SKColor Foreground);