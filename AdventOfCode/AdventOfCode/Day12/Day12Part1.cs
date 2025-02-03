using AdventOfCode.Helpers;

namespace AdventOfCode.Day12;

public class Day12Part1 : IPuzzleSolution
{
    private string _input = "../../../Day12/input.txt";
    private int _width;
    private int _height;

    private Dictionary<Point, char> _garden = new();
    private HashSet<Point> _alreadyCheckedPoints = new();
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
                    _garden[new(i, _height)] = line[i];
                }

                _width = line.Length;
                _height++;
            }
        }

        int sum = 0;
        foreach (var point in _garden.Keys)
        {
            if (_alreadyCheckedPoints.Contains(point)) //already checked
                continue;
            
            var result = SearchRegionByPoint(point);
            sum += result.area * result.perimeter;
        }
        
        return sum.ToString();
    }

    private (int area, int perimeter) SearchRegionByPoint(Point point)
    {
        var stack = new Stack<Point>();
        stack.Push(point);
        var area = 0;
        var perimeter = 0;
        
        while (stack.Count > 0)
        {
            var currentPoint = stack.Pop();
            
            if (_alreadyCheckedPoints.Add(currentPoint) == false)
                continue;

            area++;
            
            foreach (var direction in Directions.DirectionsWithoutDiagonals)
            {
                var nextPoint = currentPoint + direction;
                if(IsPointOutOfRegion(currentPoint+direction, _garden[point]))
                    perimeter++;
                else
                {
                    stack.Push(nextPoint);
                }
            }
        }

        return (area, perimeter);
    }
    
    private bool IsPointOutOfRegion(Point point, char expectedValue)
    {
        return
            point.X < 0 ||
            point.Y < 0 ||
            point.X >= _width ||
            point.Y >= _height ||
            _garden[point] != expectedValue;
    }
}
