using AdventOfCode.Helpers;

namespace AdventOfCode.Day12;

public class Day12Part2 : IPuzzleSolution
{
    private string _input = "../../../Day12/input.txt";
    private int _width;
    private int _height;

    
    private Dictionary<Point, char> _garden = new();
    private HashSet<Point> _alreadyCheckedPoints = new();
    private Dictionary<int, List<(Point, Point)>> _perimeter = new();
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
                    _garden[new(i, _height)] = line[i];
                }

                _width = line.Length;
                _height++;
            }
        }

        int sum = 0;
        foreach (var point in _garden.Keys)
        {
            if (_alreadyCheckedPoints.Contains(point))
                continue;
            
            var (area, perimeter) = SearchRegionByPoint(point);
            sum += area * CalculateSides(perimeter);
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
            var directions = Directions.DirectionsWithoutDiagonals.Where(dir => dir != perimeterWithDirection.direction && dir*-1 != perimeterWithDirection.direction).ToArray();
            
            int i = 1;
            var pointToRemove = (perimeterWithDirection.point + directions[0] * i, perimeterWithDirection.direction);
            while (perimeter.Remove(pointToRemove)) //Remove all the points in same sides to 1st direction
            {
                i++;
                pointToRemove = (perimeterWithDirection.point + directions[0] * i, perimeterWithDirection.direction);
            }
            
            i = 1;
            pointToRemove = (perimeterWithDirection.point + directions[1] * i, perimeterWithDirection.direction);
            while (perimeter.Remove(pointToRemove)) //Remove all the points in same sides to 2nd direction
            {
                i++;
                pointToRemove = (perimeterWithDirection.point + directions[1] * i, perimeterWithDirection.direction);
            }
        }

        return perimeter.Count;
    }

    private (int area, List<(Point, Point)> perimeter) SearchRegionByPoint(Point point)
    {
        var stack = new Stack<Point>();
        stack.Push(point);
        var perimeter = new List<(Point, Point)>();
        var area = 0;
        
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
                    perimeter.Add((currentPoint, direction));
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
