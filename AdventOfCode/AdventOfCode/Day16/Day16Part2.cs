using AdventOfCode.Helpers;

namespace AdventOfCode.Day16;

public class Day16Part2 : IPuzzleSolution
{
    private string _input = "../../../Day16/input.txt";
    private Dictionary<Point, char> _maze = new();
    private Point _start = null!;
    private Point _end = null!;
    
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
        //priority queue
        PriorityQueue<(Point currentPoint, int directionIndex, HashSet<Point> road) ,long> queue = new();
        queue.Enqueue((_start,  1, [_start]),0);
        
        //Already checked point, with cost required to get there
        var checkedPoints = new Dictionary<(Point currentPoint, int directionIndex), long>
        {
            [(_start, 1)] = 0
        };

        HashSet<Point> bestPlaces = new();
        long bestRoad = Int64.MaxValue;
            
        while (queue.TryDequeue(out var roadOption, out var priority))
        {
            var (currentPoint, directionIndex, road) = roadOption;
            
            //current path cost more than optimal path
            if(priority > bestRoad) 
                continue;
            
            //path is optimal, add all positions to hashset
            if (currentPoint == _end)
            {
                bestRoad = priority;
                bestPlaces.UnionWith(road);
            }

            if(checkedPoints.GetValueOrDefault((currentPoint, directionIndex), Int64.MaxValue) < priority)
                continue;
            
            checkedPoints[(currentPoint, directionIndex)] = priority;
            
            //adding new possible path options
            for (int i = -1; i < 2; i++)
            {
                var nextDirectionIndex = directionIndex + i < 0 
                    ? (directionIndex + i +4)  % 4 
                    : (directionIndex + i)  % 4;
                var nextPoint = currentPoint + Directions.DirectionsWithoutDiagonals[nextDirectionIndex];
                
                //If the next tile is wall skip
                if(_maze[nextPoint] == '#')
                    continue;
                
                queue.Enqueue((nextPoint, nextDirectionIndex, new HashSet<Point>(road){nextPoint}), priority + 1 + Math.Abs(i)*1000);
            }
        }

        return bestPlaces.Count;
    }
}