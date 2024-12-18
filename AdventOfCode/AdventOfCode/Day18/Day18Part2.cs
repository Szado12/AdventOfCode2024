using AdventOfCode.Helpers;

namespace AdventOfCode.Day18;

public class Day18Part2 : IPuzzleSolution
{
    private string _input = "../../../Day18/input.txt";
    private static int _width = 71;
    private static int _height = 71;
    private Point _start = new(0, 0);
    private Point _end = new(_width-1, _height-1);
    private List<Point> _bytes = new();
    
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
            while (inputReader.ReadLine() is {} line)
            {
               var cords= line.Split(",").Select(cord => Int32.Parse(cord)).ToArray();
               _bytes.Add(new(cords[0], cords[1]));
            }
        }

        int byteIndex = _bytes.Count-1;
        for (; byteIndex >= 0; byteIndex--)
        {
            if (SolveMaze(byteIndex))
                break;
        }

        return _bytes[byteIndex].ToString();
    }

    private bool SolveMaze(int bytesIndex)
    {
        var _bytesToCheck = _bytes.Take(bytesIndex).ToList();
        PriorityQueue<Point,int> queue = new();
        queue.Enqueue(_start,0);
        var checkedPoints = new HashSet<Point>();
            
        while (queue.TryDequeue(out var currentPoint, out var priority))
        {
            if (currentPoint == _end)
                return true;

            if(checkedPoints.Contains(currentPoint))
                continue;

            checkedPoints.Add(currentPoint);

            foreach (var direction in _directions)
            {
                var nextPoint = currentPoint + direction;
                
                if(IsPointOutMap(nextPoint) || _bytesToCheck.Contains(nextPoint))
                    continue;
                
                queue.Enqueue(nextPoint, priority + 1);
            }
        }
        return false;
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

