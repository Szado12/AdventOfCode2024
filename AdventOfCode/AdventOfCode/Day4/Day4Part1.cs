namespace AdventOfCode.Day4;

public class Day4Part1 : IPuzzleSolution
{
    private List<string> _lines = null!;
    private int _width;
    private int _height;
    private string _input = "../../../Day4/input.txt";
    private const string Word = "XMAS";
    private readonly List<(int, int)> _directions = new List<(int, int)>
    {
        (-1, 0),
        (1, 0),
        (0, 1),
        (0, -1),
        (-1, -1),
        (1, 1),
        (-1, 1),
        (1, -1)
    };
    
    public string Solve()
    {
        _lines = new List<string>();
        
        
        var occurrences = 0;
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                _lines.Add(line);
            }

            _width = _lines.First().Length;
            _height = _lines.Count;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    foreach (var direction in _directions)
                    {
                        if (SearchWord(j, i, direction, Word))
                            occurrences++;
                    }
                }
            }
        }
        return occurrences.ToString();
    }

    private bool SearchWord(int x, int y, (int x, int y) direction, string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            var currentXPos = x + i * direction.x;
            var currentYPos = y + i * direction.y;

            if (currentXPos < 0 ||
                currentXPos >= _width ||
                currentYPos < 0 ||
                currentYPos >= _height ||
                _lines[currentYPos][currentXPos] != word[i])
            {
                return false;
            }
        }
        return true;
    }
}