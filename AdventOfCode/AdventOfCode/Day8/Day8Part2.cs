using System.Text;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day8;

public class Day8Part2 : IPuzzleSolution
{
    private string _input = "../../../Day8/input.txt";
    private Dictionary<char, List<Point>> _antenas;
    private HashSet<Point> _antinodes = new ();
    private int _height;
    private int _width;
    public string Solve()
    {
        _antenas = new();
        _height = 0;
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != '.')
                    {
                        if (_antenas.ContainsKey(line[i]))
                            _antenas[line[i]].Add(new Point(i, _height));
                        else
                            _antenas[line[i]] = new List<Point> {new(i, _height)};
                    }
                }

                _width = line.Length;
                _height ++;
            }

            foreach (var points in _antenas.Values)
            {
                CheckForAntiNode(points);
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

                FindAntiNodes(points[i], diff);
                FindAntiNodes(points[i], diff*-1);
            }
        }
    }

    private void FindAntiNodes(Point startingPoint, Point diff)
    {
        for (int n = 0;; n++)
        {
            var antinode = startingPoint + diff*n;
            if (IsPointOutOfMap(antinode))
                break;
            _antinodes.Add(antinode);
        }
    }
    
    private bool IsPointOutOfMap(Point point)
    {
        return
            point.X < 0 ||
            point.Y < 0 ||
            point.X >= _width ||
            point.Y >= _height;
    }
}