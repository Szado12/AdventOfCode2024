using AdventOfCode.Helpers;

namespace AdventOfCode.Day8;

public class Day8Part1 : IPuzzleSolution
{
    private string _input = "../../../Day8/input.txt";
    private Dictionary<char, List<Point>> _antennas;
    private HashSet<Point> _antinodes = new ();
    private int _height;
    private int _width;
    
    public string Solve()
    {
        _antennas = new();
        _height = 0;
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != '.')
                    {
                        if (_antennas.ContainsKey(line[i]))
                            _antennas[line[i]].Add(new Point(i, _height));
                        else
                            _antennas[line[i]] = [new(i, _height)];
                    }
                }

                _width = line.Length;
                _height ++;
            }

            foreach (var antena in _antennas.Values)
            {
                CheckForAntiNode(antena);
            }
        }

        return _antinodes.Count.ToString();
    }

    private void CheckForAntiNode(List<Point> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i+1; j < points.Count; j++)
            {
                var diff = points[i] - points[j];
                var antinode1 = points[i] + diff;
                var antinode2 = points[j] - diff;

                if (antinode1.IsOutOfRange(_width, _height))
                    _antinodes.Add(antinode1);
                
                if (antinode2.IsOutOfRange(_width, _height))
                    _antinodes.Add(antinode2);
            }
        }
    }
}