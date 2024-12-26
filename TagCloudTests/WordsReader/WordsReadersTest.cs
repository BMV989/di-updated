using System.Globalization;
using System.Text;
using FluentAssertions;
using TagCloud.WordsReader;

namespace TagCloudTests.WordsReader;

[TestFixture]
public class WordsReadersTest
{
    private const string FileContent = "Hello... Hello? Hello world!";
    
    private static IEnumerable<TestCaseData> WordsReadersTestCases
    {
        get
        {
            yield return new TestCaseData(new WordFileReader("WordsReader/Samples/text.docx"));
            yield return new TestCaseData(new FileReader("WordsReader/Samples/text.txt", Encoding.UTF8));
            yield return new TestCaseData(new CsvFileReader("WordsReader/Samples/text.csv", CultureInfo.InvariantCulture));
        }
    }
    
    [TestCaseSource(nameof(WordsReadersTestCases))]
    public void WordsReaders_ReadWords_ShouldReadAllWords(IWordsReader reader)
    {
        var words = reader.ReadWords();
        string.Join(" ", words).Should().Be(FileContent);
    }
}