using AdventOfCode.Helpers;

namespace AdventOfCode.Day6;

public class Day6Part2 : IPuzzleSolution
{
    private string _input = "../../../Day6/input.txt";
    private HashSet<Point> _obstacles;
    private HashSet<Point> _guardPoints;
    private int _width;
    private int _height;
    
    public string Solve()
    {
        _obstacles = new ();
        _guardPoints = new();
        
        Point? startingPoint = null;
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '#')
                    {
                        _obstacles.Add(new (i, _height));
                    }
                    else if (startingPoint is null && line[i] == '^')
                    {
                        startingPoint = new (i, _height);
                        _width = line.Length;
                    }
                }   
                _height++;
            }
        }

        _guardPoints = FindGuardPath(startingPoint!);

        var loops = 0;
        foreach (var point in _guardPoints)
        {
            if (IsGuardInLoop(startingPoint!, [.._obstacles, point]))
                loops++;
        }

        return loops.ToString();
    }

    private HashSet<Point> FindGuardPath(Point startingPoint)
    {
        var guardPoints = new HashSet<Point>();
        
        var currentPoint = startingPoint;
        var directionIndex = 0;
        while (true)
        {
            if (currentPoint.IsOutOfRange(_width, _height))
                break;
            
            guardPoints.Add(currentPoint);
            
            var nextPoint = currentPoint + Directions.DirectionsWithoutDiagonals[directionIndex];
            if (_obstacles.Contains(nextPoint))
                directionIndex = (directionIndex + 1) % Directions.DirectionsWithoutDiagonals.Count;
            else
            {
                currentPoint = nextPoint;
            }
        }
        
        return guardPoints;
    }
    
    private bool IsGuardInLoop(Point startPoint, HashSet<Point> obstacles)
    {
        var points = new HashSet<(Point, Point)>();
        var directionIndex = 0;
        var currentPoint = startPoint;
        
        while (true)
        {
            if (currentPoint.IsOutOfRange(_width, _height))
                return false;

            if (!points.Add((currentPoint, Directions.DirectionsWithoutDiagonals[directionIndex])))
                return true;
            
            var nextPoint = currentPoint + Directions.DirectionsWithoutDiagonals[directionIndex];
            if (obstacles.Contains(nextPoint))
                directionIndex = (directionIndex + 1) % Directions.DirectionsWithoutDiagonals.Count;
            else
            {
                currentPoint = nextPoint;
            }
        }
    }
}