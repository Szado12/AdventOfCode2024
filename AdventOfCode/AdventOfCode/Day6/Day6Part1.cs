using AdventOfCode.Helpers;

namespace AdventOfCode.Day6;

public class Day6Part1 : IPuzzleSolution
{
    private string _input = "../../../Day6/input.txt";
    private HashSet<Point> _obstacles;
    private int _width;
    private int _height;
    
    public string Solve()
    {
        _obstacles = new ();
        
        Point? startingPoint = null;
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '#')
                    {
                        _obstacles.Add(new(i, _height));
                    }
                    else if (startingPoint is null && line[i] == '^')
                    {
                        startingPoint = new(i, _height);
                        _width = line.Length;
                    }
                }   
                _height++;
            }
        }

        var guardPoint = FindGuardPath(startingPoint!);
        return guardPoint.Count.ToString();
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
}