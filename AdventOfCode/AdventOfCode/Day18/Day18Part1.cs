
using System.Text;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day18;

public class Day18Part1 : IPuzzleSolution
{
    private string _input = "../../../Day18/input.txt";
    private static int _width = 71;
    private static int _height = 71;
    private int _byteNumber = 1024;
    private Dictionary<Point, bool> _map = new ();
    private Point _start = new(0, 0);
    private Point _end = new(_width-1, _height-1);
    
    private List<Point> _directions =
    [
        new(0, -1), //up
        new(1, 0), //right
        new(0, 1), //down
        new(-1, 0) //left
    ];
    public string Solve()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _map[new(x, y)] = false;
            }
        }
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            int lineIndex = 0;
            while (inputReader.ReadLine() is {} line && lineIndex < _byteNumber)
            {
               var cords= line.Split(",").Select(cord => Int32.Parse(cord)).ToArray();
               _map[new(cords[0], cords[1])] = true;
               lineIndex++;
            }
        }
        
        return SolveMaze().ToString();
    }

    private int SolveMaze()
    {
        PriorityQueue<Point,int> queue = new();
        queue.Enqueue(_start,0);
        var checkedPoints = new HashSet<Point>();
            
        while (queue.TryDequeue(out var currentPoint, out var priority))
        {
            if (currentPoint == _end)
                return priority;

            if(checkedPoints.Contains(currentPoint))
                continue;

            checkedPoints.Add(currentPoint);

            foreach (var direction in _directions)
            {
                var nextPoint = currentPoint + direction;
                
                if(IsPointOutMap(nextPoint) || _map[nextPoint])
                    continue;
                
                queue.Enqueue(nextPoint, priority + 1);
            }
        }
        return -1;
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

