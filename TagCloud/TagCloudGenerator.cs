using TagCloud.Visualization;
using TagCloud.WordsFilter;
using TagCloud.WordsReader;

namespace TagCloud;

public class TagCloudGenerator(IWordsReader reader, ITagCloudSaver saver, TagCloudBitmapGenerator bitmapGenerator, 
    IEnumerable<IWordsFilter> filters)
{
    private const int MinFontSize = 10;
    private const int MaxFontSize = 80;
    public string GenerateTagCloud()
    {
       var words = reader.ReadWords();
       
       var freqDict = filters
           .Aggregate(words, (c, f) => f.ApplyFilter(c))
           .GroupBy(w => w)
           .OrderByDescending(g => g.Count())
           .ToDictionary(g => g.Key, g => g.Count());
       
       var maxFreq = freqDict.Values.Max();
       var tagsList = freqDict.Select(pair => ToWordTag(pair, maxFreq)).ToList();
       
       return saver.Save(bitmapGenerator.GenerateBitmap(tagsList)); 
    } 
    private static int TransformFreqToSize(int freq, int maxFreq) =>
          (int)(MinFontSize + (float)freq / maxFreq * (MaxFontSize - MinFontSize)); 
    private static WordTag ToWordTag(KeyValuePair<string, int> pair, int maxFreq) => 
            new(pair.Key, TransformFreqToSize(pair.Value, maxFreq));
}