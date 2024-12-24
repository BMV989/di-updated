using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using SkiaSharp;
using TagCloud;
using TagCloud.CloudLayouter;
using TagCloud.Visualization;
using TagCloud.WordsFilter;
using TagCloud.WordsReader;

namespace TagCloudTests;

[TestFixture]
[UseReporter(typeof(RiderReporter))]
[ApprovalTests.Namers.UseApprovalSubdirectory("Samples/Snapshots")]
public class TagCloudGeneratorTest
{
    [Test]
    public void TagCloudGenerator_GenerateTagCloud_ShouldGenerateFile()
    {
        var generator = InitGenerator();
        
        var savePath = generator.GenerateTagCloud();
        
        File.Exists(savePath).Should().BeTrue();
        Approvals.VerifyFile(savePath);
    }

    private static TagCloudGenerator InitGenerator()
    {
        var fileReader = new FileReader("../../../Samples/sample.txt", Encoding.UTF8);
        var imageSaver = new TagCloudSaver("test", "png");
        var layouter = new CircularCloudLayouter(new SKPoint(1920 / 2f, 1080 / 2f));
        var imageGenerator = new TagCloudBitmapGenerator(new SKSizeI(1920, 1080), SKTypeface.FromFamilyName("Arial"), 
            SKColors.Black, SKColors.Azure, layouter);
         List<IWordsFilter> filters = [new LowercaseFilter(), new BoringWordsFilter()];
         
         return new TagCloudGenerator(fileReader, imageSaver, imageGenerator, filters);
    }
}