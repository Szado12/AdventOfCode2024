namespace AdventOfCode.Day19;

public class Day19Part1 : IPuzzleSolution
{
    private string _input = "../../../Day19/input.txt";
    private HashSet<string> _towels;
    private List<string> _words = new();
    private Dictionary<string, bool> _createdWords = new();
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

        foreach (var towel in _towels)
        {
            _createdWords[towel] = true;
        }
        
        int wordsCreated = 0;
        foreach (var word in _words)
        {
            if (CanWordBeCreatedWithTowels(word))
                wordsCreated++;
        }

        return wordsCreated.ToString();
    }

    private bool CanWordBeCreatedWithTowels(string word)
    {
        foreach (var towel in _towels)
        {
            if (_createdWords.TryGetValue(word, out var created))
                return created;

            if (word.StartsWith(towel))
            {
                var subWord = word.Substring(towel.Length);
                if (CanWordBeCreatedWithTowels(subWord))
                {
                    _createdWords[word] = true;
                    return true;
                }
                else
                {
                    _createdWords[subWord] = false;
                }
            }
        }

        return false;
    }
}

