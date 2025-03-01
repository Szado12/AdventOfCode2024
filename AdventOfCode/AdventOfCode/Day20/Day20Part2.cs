using AdventOfCode.Helpers;

namespace AdventOfCode.Day20;

public class Day20Part2 : IPuzzleSolution
{
    private string _input = "../../../Day20/input.txt";
    private HashSet<string> _towels;
    private List<string> _words = new();
    private Dictionary<Point, bool> _map = new();
    private Dictionary<Point, int> _defaultPath = new();
    private int _width;
    private int _height;
    private Point _start = new(0,0);
    private Point _end = new(0,0);
    public string Solve()
    {
        _height = 0;
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is {} line)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '.')
                    {
                        _map[new(x, _height)] = true;
                        continue;
                    }

                    if (line[x] == 'E')
                    {
                        _map[new(x, _height)] = true;
                        _end = new(x, _height);
                        continue;
                    }
                    if (line[x] == 'S')
                    {
                        _map[new(x, _height)] = true;
                        _start = new(x, _height);
                        continue;
                    }

                    _map[new(x, _height)] = false;
                }

                _width = line.Length;
                _height++;
            }
        }

        CreateDefaultPath();
        return FindPossibleCheats().ToString();
    }

    private List<(Point trackPoint, int distance)> GetPossibleCheatsFromPoint(Point startPoint)
    {
        return _defaultPath.Keys
            .Select(point => (point, startPoint.DistanceXY(point)))
            .Where(pointDistance => pointDistance.Item2 <= 20)
            .ToList();
    }
    
    private int FindPossibleCheats()
    {
        Dictionary<(Point, Point),int> _cheats = new ();

        foreach (var (point, step) in _defaultPath)
        {
            var cheatPoints = GetPossibleCheatsFromPoint(point);
            foreach (var (cheatPoint , distance) in cheatPoints)
            {
                _cheats[(point, cheatPoint)] = _defaultPath[cheatPoint] - step - distance;
            }
        }
        return _cheats.Values.Count(x => x >= 100);
    }
    
    private void CreateDefaultPath()
    {
        _defaultPath = new();
        _defaultPath[_start] = 0;
        var step = 0;
        var (nextPoint, direction) = NextPoint(_start, null);
        
        while (nextPoint != _end)
        {
            _defaultPath[nextPoint] = ++step;
            (nextPoint, direction) = NextPoint(nextPoint, direction);
        }
        
        _defaultPath[_end] = ++step;
    }

    private (Point nextPoint, Point direction) NextPoint(Point currentPoint, Point? prevDirection)
    {
        foreach (var direction in Directions.DirectionsWithoutDiagonals)
        {
            if(prevDirection is not null && direction == prevDirection *-1)
                continue;

            var nextPoint = currentPoint + direction;
            if (_map[nextPoint])
                return (nextPoint, direction);
        }

        return new(new (-1, -1), new(-1, -1)); //Impossible
    }
}

