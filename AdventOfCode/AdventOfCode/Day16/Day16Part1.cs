using AdventOfCode.Helpers;

namespace AdventOfCode.Day16;

public class Day16Part1 : IPuzzleSolution
{
    private string _input = "../../../Day16/input.txt";
    private Dictionary<Point, char> _maze = new();
    private Point _start = null!;
    private Point _end = null!;
    
    private List<Point> _directions =
    [
        new(0, -1), //up
        new(1, 0), //right
        new(0, 1), //down
        new(-1, 0) //left
    ];
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            var y = 0;
            while (inputReader.ReadLine() is {} line)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    _maze[new(x, y)] = line[x];
                    if (line[x] == 'S')
                        _start = new(x, y);
                    else if (line[x] == 'E')
                        _end = new(x, y);
                }
                y++;
            }
        }

        return SolveMaze().ToString();
    }

    private long SolveMaze()
    {
        PriorityQueue<(Point currentPoint, int directionIndex) ,long> queue = new();
        queue.Enqueue((_start,  1),0);
        var checkedPoints = new Dictionary<(Point currentPoint, int directionIndex), long>();
        checkedPoints[(_start, 1)] = 0;
            
        while (queue.TryDequeue(out var roadOption, out var priority))
        {
            (var currentPoint, var directionIndex) = roadOption;
            if (currentPoint == _end)
                return priority;

            if(checkedPoints.GetValueOrDefault(roadOption, Int64.MaxValue) < priority)
                continue;
            
            checkedPoints[roadOption] = priority;
            
            for (int i = -1; i < 2; i++)
            {
                var nextDirectionIndex = directionIndex + i < 0 
                    ? (directionIndex + i +4)  % 4 
                    : (directionIndex + i)  % 4;
                var nextPoint = currentPoint + _directions[nextDirectionIndex];
                
                if(_maze[nextPoint] == '#')
                    continue;
                
                queue.Enqueue((nextPoint, nextDirectionIndex), priority + 1 + Math.Abs(i)*1000);
            }
        }
        return -1;
    }
}