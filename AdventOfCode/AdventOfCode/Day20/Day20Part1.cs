using AdventOfCode.Helpers;

namespace AdventOfCode.Day20;

public class Day20Part1 : IPuzzleSolution
{
    private string _input = "../../../Day20/input.txt";
    private Dictionary<Point, bool> _map = new();
    private Dictionary<Point, int> _defaultPath = new();
    private int _width;
    private int _height;
    private Point _start;
    private Point _end;
    private List<Point> _directions =
    [
        new(0, -1), //up
        new(1, 0), //right
        new(0, 1), //down
        new(-1, 0) //left
    ];
    
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

    private int FindPossibleCheats()
    {
        Dictionary<(Point, Point),int> _cheats = new ();

        foreach (var (point, step) in _defaultPath)
        {
            foreach (var direction in _directions)
            {
                var wallPoint = point + direction;
                var trackPoint = point + direction*2;
                
                if(IsPointOutMap(trackPoint))
                    continue;
                
                if(!_map[wallPoint] && _map[trackPoint])
                {
                    _cheats[(point, trackPoint)] = _defaultPath[trackPoint] - step - 2;
                }
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
        foreach (var direction in _directions)
        {
            if(prevDirection is not null && direction == prevDirection *-1)
                continue;

            var nextPoint = currentPoint + direction;
            if (_map[nextPoint])
                return (nextPoint, direction);
        }

        return new(new (-1, -1), new(-1, -1)); //Impossible
    }
    
    private bool IsPointOutMap(Point point)
    {
        return
            point.X < 0 ||
            point.Y < 0 ||
            point.X >= _width ||
            point.Y >= _height;
    }
}

