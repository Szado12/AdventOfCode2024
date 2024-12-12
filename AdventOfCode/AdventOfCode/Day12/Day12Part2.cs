using AdventOfCode.Helpers;

namespace AdventOfCode.Day12;

public class Day12Part2 : IPuzzleSolution
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
    private Dictionary<int, List<(Point, Point)>> _perimeter = new();
    private Dictionary<int, int> _sides = new();
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
            sum += _area[group] * CalculateSides(_perimeter[group]);
            group++;
        }
        
        return sum.ToString();
    }

    private int CalculateSides(List<(Point point, Point direction)> perimetersWithDirection)
    {
        var perimeter = new List<(Point point, Point direction)>(perimetersWithDirection);
        
        foreach (var perimeterWithDirection in perimetersWithDirection)
        {
            if(!perimeter.Contains(perimeterWithDirection))//The point was already removed
                continue;
            
            var directions = _directions.Where(dir => dir != perimeterWithDirection.direction && dir*-1 != perimeterWithDirection.direction).ToArray();
            int i = 0;
            while (true) //Remove all the points in same sides to 1st direction
            {
                i++;
                var pointToRemove = (perimeterWithDirection.point + directions[0] * i, perimeterWithDirection.direction);
                if (!perimeter.Remove(pointToRemove))
                    break;
            }
            i = 0;
            while (true) //Remove all the points in same sides to 2nd direction
            {
                i++;
                var pointToRemove = (perimeterWithDirection.point + directions[1] * i, perimeterWithDirection.direction);
                if (!perimeter.Remove(pointToRemove))
                    break;
            }
        }

        return perimeter.Count;
    }

    private void SearchRegionByPoint(Point point, int group)
    {
        var stack = new Stack<Point>();
        stack.Push(point);
        var expectedValue = _garden[point].value;
        _perimeter[group] = new ();
        _area[group] = 0;
        _sides[group] = 0;
        
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
                    _perimeter[group].Add((currentPoint, direction));
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
