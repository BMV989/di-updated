namespace TagCloud.WordsFilter;

public interface IWordsFilter
{
    // Applying some function on list of words
    List<string> ApplyFilter(List<string> words);
}