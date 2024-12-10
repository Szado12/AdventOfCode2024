using AdventOfCode.Helpers;

namespace AdventOfCode.Day10;

public class Day10Part2 : IPuzzleSolution
{
    private string _input = "../../../Day10/input.txt";
    private List<string> _map = new();
    private List<Point> _startingPoints = new();
    private int _width;
    private int _height;
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
            while (inputReader.ReadLine() is { } line)
            {
                _map.Add(line);

                for (int i = 0; i < line.Length; i++)
                {
                    if(line[i] == '0')
                        _startingPoints.Add(new(i, _height));
                }
                
                _height ++;
            }
            _width = _map.First().Length;
        }

        var sum = 0;
        foreach (var startingPoint in _startingPoints)
        {
            sum += SearchPath(startingPoint);
        }
        
        return sum.ToString();
    }

    private int SearchPath(Point startingPoint)
    {
        List<Point> finalPoints = new();
        foreach (var direction in _directions)
        {
            SearchPathRec(startingPoint, direction, 1, finalPoints);
        }

        return finalPoints.Count;
    }

    private void SearchPathRec(Point startingPoint, Point direction, int i, List<Point> finalPoints)
    {
        var nextPoint = startingPoint + direction;
        if (IsPointOutMap(nextPoint))
            return;


        if (Int32.Parse(_map[nextPoint.Y][nextPoint.X].ToString()) != i)
            return;
        
        if (i == 9)
        {
            finalPoints.Add(nextPoint);
            return;
        }

        var nextDirections = _directions.Where(dir => dir != direction * -1); //Skip checking previous point
        foreach (var newDirection in nextDirections)
        {
            SearchPathRec(nextPoint, newDirection, i+1, finalPoints);
        }
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