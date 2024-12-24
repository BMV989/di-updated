using System.Globalization;
using System.Text;
using CommandLine;
using SkiaSharp;

namespace TagCloudClient;

public class Options
{
    [Value(0, Required = true, HelpText = "Source file path")]
    public string FilePath { get; set; }
    
    [Option('e', "encoding",
           Required = false,
           HelpText = "Source encoding")]
    public string EncodingName { get; set; } = "utf-8";
    public Encoding UsingEncoding => Encoding.GetEncoding(EncodingName); 
    
    [Option('s', "size", 
        Required = false, 
        HelpText = "Image size")]
    public SKSizeI Size { get; set; } = new(1920, 1080);

    [Option('f', "font", 
        Required = false, 
        HelpText = "Words font")]
    public string FontFamilyName { get; set; } = "Arial";
    public SKTypeface FontFamily => SKTypeface.FromFamilyName(FontFamilyName);

    [Option('b', "background-color",
        Required = false,
        HelpText = "Background color")]
    public SKColor BackgroundColor { get; set; } = SKColors.Black; 
    
    [Option('c', "word-color", 
            Required = false, 
            HelpText = "Words color")]
    public SKColor ForegroundColor { get; set; } = SKColors.White;
    
    [Option("image-name", 
            Required = false, 
            HelpText = "Image name")]
    public string ImageName { get; set; } = "result";

    [Option("image-format", 
        Required = false, 
        HelpText = "Image format")]
    public string ImageFormat { get; set; } = "png";
    
    [Option("angle-offset", 
        Required = false, 
        HelpText = "Offset of the angle for the spiral.")]
    public double AngleOffset { get; set; } = 0.5;

    [Option("radius",
        Required = false,
        HelpText = "Radius of the spiral")]
    public int Radius { get; set; } = 1;
    
    [Option("center",
            Required = false,
            HelpText = "The center of the cloud in the image")]
    public SKPoint Center { get; set; } = new(1920 / 2f, 1080 / 2f);

    [Option("culture", 
        Required = false, 
        HelpText = "CSV culture information")]
    public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
}