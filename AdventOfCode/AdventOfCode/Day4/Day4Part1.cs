using AdventOfCode.Helpers;

namespace AdventOfCode.Day4;

public class Day4Part1 : IPuzzleSolution
{
    private Dictionary<Point, char> _map = new();
    private int _width;
    private int _height;
    private string _input = "../../../Day4/input.txt";
    private const string Word = "XMAS";
    
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            _height = 0;
            while (inputReader.ReadLine() is { } line)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    _map[new(x, _height)] = line[x];
                }
                _height++;
            }
        }

        _width = _map.Keys.Max(p => p.X) + 1;

        var occurrences = 0;
        var startingPoints = _map.Where(kvp => kvp.Value == Word[0]).Select(kvp => kvp.Key);
        
        foreach (var startingPoint in startingPoints)
        {
            foreach (var direction in Directions.DirectionsWithDiagonals)
            {
                if (SearchWord(startingPoint, direction, Word))
                    occurrences++;
            }
        }
        
        return occurrences.ToString();
    }

    private bool SearchWord(Point startingPoint, Point direction, string word)
    {
        for (int i = 1; i < word.Length; i++)
        {
            var currentPoint = startingPoint + i * direction;
            
            if (currentPoint.IsOutOfRange(_width, _height) ||
                _map[currentPoint] != word[i])
            {
                return false;
            }
        }
        return true;
    }
}