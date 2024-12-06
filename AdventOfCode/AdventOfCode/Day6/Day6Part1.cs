namespace AdventOfCode.Day6;

public class Day6Part1 : IPuzzleSolution
{
    private string _input = "../../../Day6/input.txt";
    private HashSet<(int x, int y)> _obstacles;
    private HashSet<(int x, int y)> _guardPoints;
    private int _width = 0;
    private int _height = 0;

    private List<(int x, int y)> directions = new()
    {
        (0, -1), //up
        (1, 0), //right
        (0, 1), //down
        (-1, 0), //left
    };
    
    public string Solve()
    {
        _obstacles = new ();
        _guardPoints = new();
        
        var sum = 0;
        (int x, int y)? startingPoint = null;
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '#')
                    {
                        _obstacles.Add((i, _height));
                    }
                    else if (startingPoint is null && line[i] == '^')
                    {
                        startingPoint = (i, _height);
                        _width = line.Length;
                    }
                }   
                _height++;
            }
        }

        var currentPoint = startingPoint!.Value;
        var directionIndex = 0;
        while (true)
        {
            if (IsOut(currentPoint))
                break;
            
            _guardPoints.Add(currentPoint);
            
            var nextPoint = (currentPoint.x + directions[directionIndex].x, currentPoint.y + directions[directionIndex].y);
            if (_obstacles.Contains(nextPoint))
                directionIndex = (directionIndex + 1) % directions.Count;
            else
            {
                currentPoint = nextPoint;
            }
        }
        
        return _guardPoints.Count.ToString();
    }
    
    private bool IsOut((int x, int y) point)
    {
        return
            point.x < 0 ||
            point.y < 0 ||
            point.x >= _width ||
            point.y >= _height;
    }
}