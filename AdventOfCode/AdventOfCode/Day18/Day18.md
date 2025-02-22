# [Day18](https://adventofcode.com/2024/day/18)

## Input
Multiple lines, each containing single coordinate: X,Y
## Part 1

### Problem
You are inside a 70×70 memory grid, starting at (0,0) (top-left), and need to reach (70,70) (bottom-right).
However, bytes fall every nanosecond, corrupting specific coordinates. Corrupted locations cannot be entered.
Your task is to simulate the first 1024 falling bytes and determine the shortest path from (0,0) to (70,70) while avoiding corrupted cells.

### Solution
Solve the labyrinth. Proposed approach - reuse the Dijkstra algorithm  from day 16. 

```csharp
PriorityQueue<Point,int> queue = new();
    queue.Enqueue(_start,0);
    var checkedPoints = new HashSet<Point>();
        
    while (queue.TryDequeue(out var currentPoint, out var priority))
    {
        if (currentPoint == _end)
            return priority;

        if(!checkedPoints.Add(currentPoint))
            continue;

        foreach (var direction in Directions.DirectionsWithoutDiagonals)
        {
            var nextPoint = currentPoint + direction;
            
            if(nextPoint.IsOutOfRange(_width, _height) || _map[nextPoint])
                continue;
            
            queue.Enqueue(nextPoint, priority + 1);
        }
    }
    return -1;
```
## Part 2
### Problem
The bytes continue to fall, some might completely cut off access to the exit. 
Your task is to find the first byte that, when it falls, makes it impossible to reach (70,70).

### Solution
Use the solution form 1st path. Start with scenario when all bytes already fallen, and remove them one by one till there is possible path.
Last removed byte before the path was found, is the byte that makes impossible to travel to point (70,70).