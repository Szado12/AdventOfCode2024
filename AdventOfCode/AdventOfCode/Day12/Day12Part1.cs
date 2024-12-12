using AdventOfCode.Helpers;

namespace AdventOfCode.Day12;

public class Day12Part1 : IPuzzleSolution
{
    private string _input = "../../../Day12/input.txt";
    private int _width;
    private int _height;
    private List<Point> _directions =
    [
        new(0, -1), //up
        new(1, 0), //right
        new(0, 1), //down
        new(-1, 0) //left
    ];

    private Dictionary<Point, (char value, int group)> _garden = new();
    private Dictionary<int, int> _perimeter = new();
    private Dictionary<int, int> _area = new();
    public string Solve()
    {
        _height = 0;
        List<string> stones;
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is {} line)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    _garden[new(i, _height)] = (line[i], -1);
                }

                _width = line.Length;
                _height++;
            }
        }

        int sum = 0;
        int group = 0;
        foreach (var point in _garden.Keys)
        {
            if (_garden[point].group != -1) //already checked
                continue;
            SearchRegionByPoint(point, group);
            sum += _perimeter[group] * _area[group];
            group++;
        }
        
        return sum.ToString();
    }

    private void SearchRegionByPoint(Point point, int group)
    {
        var stack = new Stack<Point>();
        stack.Push(point);
        var expectedValue = _garden[point].value;
        _perimeter[group] = 0;
        _area[group] = 0;
        
        while (stack.Count > 0)
        {
            var currentPoint = stack.Pop();
            if (_garden[currentPoint].group != -1) //already checked
                continue;

            _garden[currentPoint] = (_garden[currentPoint].value, group);
            _area[group]++;
            
            foreach (var direction in _directions)
            {
                var nextPoint = currentPoint + direction;
                if(IsPointOutOfRegion(currentPoint+direction, expectedValue))
                    _perimeter[group]++;
                else
                {
                    stack.Push(nextPoint);
                }
            }
        }
    }
    
    private bool IsPointOutOfRegion(Point point, char expectedValue)
    {
        return
            point.X < 0 ||
            point.Y < 0 ||
            point.X >= _width ||
            point.Y >= _height ||
            _garden[point].value != expectedValue;
    }
}
