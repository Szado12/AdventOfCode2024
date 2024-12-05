namespace AdventOfCode.Day4;

public class Day4Part2 : IPuzzleSolution
{
    private List<string> _lines = null!;
    private int _width;
    private int _height;
    public string Solve()
    {
        string path = "../../../Day4/input.txt";
        _lines = new List<string>();
        var word = "MAS";

        var directions = new List<(int x, int y)>
        {
            (-1, -1),
            (1, 1),
            (-1, 1),
            (1, -1)
        };

        var occurrences = 0;
        
        using (StreamReader inputReader = new StreamReader(path))
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
                    int diagonalOccurrences = 0;
                    foreach (var direction in directions)
                    {
                        var x = j + direction.x * -(word.Length/2);
                        var y = i + direction.y * -(word.Length/2);
                        if (SearchWord(x, y, direction, word))
                            diagonalOccurrences++;
                    }

                    if (diagonalOccurrences == 2)
                        occurrences++;
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