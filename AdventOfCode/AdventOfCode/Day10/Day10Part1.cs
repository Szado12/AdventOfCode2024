using AdventOfCode.Helpers;

namespace AdventOfCode.Day10;

public class Day10Part1 : IPuzzleSolution
{
    private string _input = "../../../Day10/input.txt";
    private List<string> _map = new();
    private List<Point> _startingPoints = new();
    private int _width;
    private int _height;
    
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
        HashSet<Point> finalPoints = new();
        foreach (var direction in Directions.DirectionsWithoutDiagonals)
        {
            SearchPathRec(startingPoint, direction, 1, finalPoints);
        }

        return finalPoints.Count;
    }

    private void SearchPathRec(Point startingPoint, Point direction, int i, HashSet<Point> finalPoints)
    {
        var nextPoint = startingPoint + direction;
        if (nextPoint.IsOutOfRange(_width, _height))
            return;


        if (_map[nextPoint.Y][nextPoint.X].ToInt() != i)
            return;
        
        if (i == 9)
        {
            finalPoints.Add(nextPoint);
            return;
        }

        var nextDirections = Directions.DirectionsWithoutDiagonals.Where(dir => dir != direction * -1); //Skip checking previous point
        foreach (var newDirection in nextDirections)
        {
            SearchPathRec(nextPoint, newDirection, i+1, finalPoints);
        }
    }
}