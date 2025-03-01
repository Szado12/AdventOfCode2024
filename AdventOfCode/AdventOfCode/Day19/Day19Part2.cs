namespace AdventOfCode.Day19;

public class Day19Part2 : IPuzzleSolution
{
    private string _input = "../../../Day19/input.txt";
    private HashSet<string> _towels = [];
    private List<string> _words = new();
    private Dictionary<string, long> _createdWords = new();
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            _towels = inputReader.ReadLine()!.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToHashSet();

            inputReader.ReadLine();
            
            while (inputReader.ReadLine() is {} line)
            {
                _words.Add(line);
            }
        }
        
        long wordsCreated = 0;
        foreach (var word in _words)
        {
            wordsCreated += CanWordBeCreatedWithTowels(word);
        }

        return wordsCreated.ToString();
    }

    private long CanWordBeCreatedWithTowels(string word)
    {
        if (_createdWords.TryGetValue(word, out var options))
            return options;

        if (word.Length == 0)
            return 1;

        var wordOptions = 0L;
        foreach (var towel in _towels)
        {
            if (word.StartsWith(towel))
            {
                var subWord = word.Substring(towel.Length);
                wordOptions += CanWordBeCreatedWithTowels(subWord);
            }
        }

        _createdWords[word] = wordOptions;
        return wordOptions;
    }
}