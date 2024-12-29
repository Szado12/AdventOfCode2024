using AdventOfCode.Helpers;

namespace AdventOfCode.Day4;

public class Day4Part2 : IPuzzleSolution
{
    private Dictionary<Point, char> _map = new();
    private int _width;
    private int _height;
    private string _input = "../../../Day4/input.txt";
    private const string Word = "MAS";
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

                _width = line.Length;
                _height++;
            }
        }
        
        
        var startingPoints = _map.Where(kvp => kvp.Value == Word[Word.Length/2]).Select(kvp => kvp.Key).ToList();
        
        var occurrences = 0;
        foreach (var startingPoint in startingPoints)
        {
            int diagonalOccurrences = 0;
            foreach (var direction in Directions.DirectionsOnlyDiagonals)
            {
                var searchPoint = startingPoint + direction * (-Word.Length / 2);
                if (SearchWord(searchPoint, direction, Word))
                    diagonalOccurrences++;
            }

            if (diagonalOccurrences > 1)
                occurrences++;
        }
        return occurrences.ToString();
    }

    private bool SearchWord(Point startingPoint, Point direction, string word)
    {
        for (int i = 0; i < word.Length; i++)
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