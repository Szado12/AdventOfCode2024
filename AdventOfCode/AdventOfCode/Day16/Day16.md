# [Day16](https://adventofcode.com/2024/day/16)

## Input
Multiple lines, describing a 2d maze.
## Part 1

### Problem
Solve the maze.
Rules:
- moving forward cost 1
- rotating clockwise or counterclockwise 90 degrees at a time
- single rotation cost 1000

e.g. Maze with solution:
- S - starting point.
- E - end point.
- \# - walls 

|#|#|#|#|#|#|#|#|#|#|#|#|#|#|#|
|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|
|#|.|.|.|.|.|.|.|#|.|.|.|.|E|#|
|#|.|#|.|#|#|#|.|#|.|#|#|#|.|#|
|#|.|.|.|.|.|#|.|#|.|.|.|#|.|#|
|#|.|#|#|#|.|#|#|#|#|#|.|#|.|#|
|#|.|#|.|#|.|.|.|.|.|.|.|#|.|#|
|#|.|#|.|#|#|#|#|#|.|#|#|#|.|#|
|#|.|.|.|.|.|.|.|.|.|.|.|#|.|#|
|#|#|#|.|#|.|#|#|#|#|#|.|#|.|#|
|#|.|.|.|#|.|.|.|.|.|#|.|#|.|#|
|#|.|#|.|#|.|#|#|#|.|#|.|#|.|#|
|#|.|.|.|.|.|#|.|.|.|#|.|#|.|#|
|#|.|#|#|#|.|#|.|#|.|#|.|#|.|#|
|#|S|.|.|#|.|.|.|.|.|#|.|.|.|#|
|#|#|#|#|#|#|#|#|#|#|#|#|#|#|#|


|#| #  | #  | #  | #  | #  | #  | #    | #  | #  | #  | #  | #  |#|#|
|-|----|----|----|----|----|----|------|----|----|----|----|----|-|-|
|#| .  | .  | .  | .  | .  | .  | .    | #  | .  | .  | .  | .  |E|#|
|#| .  | #  | .  | #  | #  | #  | .    | #  | .  | #  | #  | #  |^|#|
|#| .  | .  | .  | .  | .  | #  | .    | #  | .  | .  | .  | #  |^|#|
|#| .  | #  | #  | #  | .  | #  | #    | #  | #  | #  | .  | #  |^|#|
|#| .  | #  | .  | #  | .  | .  | .    | .  | .  | .  | .  | #  |^|#|
|#| .  | #  | .  | #  | #  | #  | #    | #  | .  | #  | #  | #  |^|#|
|#| .  | .  | \> | \> | \> | \> | \>   | \> | \> | \> | v  | #  |^|#|
|#| #  | #  | ^  | #  | .  | #  | #    | #  | #  | #  | v  | #  |^|#|
|#| \> | \> | ^  | #  | .  | .  | .    | .  | .  | #  | v  | #  |^|#|
|#| ^  | #  | .  | #  | .  | #  | #    | #  | .  | #  | v  | #  |^|#|
|#| ^  | .  | .  | .  | .  | #  | .    | .  | .  | #  | v  | #  |^|#|
|#| ^  | #  | #  | #  | .  | #  | .    | #  | .  | #  | v  | #  |^|#|
|#| S  | .  | .  | #  | .  | .  | .    | .  | .  | #  | \> | \> |^|#|
|#| #  | #  | #  | #  | #  | #  | #    | #  | #  | #  | #  | #  |#|#|

The best path score is 7036. 
This can be achieved by taking a total of 36 steps forward and turning 90 degrees a total of 7 times.

### Solution
Use Dijkstra's algorithm, adding to priority queue all possible ways:
- rotating left + moving 1 tile
- rotating right + moving 1 tile
- moving 1 tile

```csharp
private long SolveMaze()
    {
        //priority queue
        PriorityQueue<(Point currentPoint, int directionIndex) ,long> queue = new();
        queue.Enqueue((_start,  1),0);
        
        //Already checked point, with cost required to get there
        var checkedPoints = new Dictionary<(Point currentPoint, int directionIndex), long>
        {
            [(_start, 1)] = 0
        };

        //Dijkstra's algorithm
        while (queue.TryDequeue(out var roadOption, out var priority))
        {
            var (currentPoint, directionIndex) = roadOption;
            //Final tile
            if (currentPoint == _end)
                return priority;

            //The point was already checked and required less cost
            if(checkedPoints.GetValueOrDefault(roadOption, Int64.MaxValue) < priority)
                continue;
            
            checkedPoints[roadOption] = priority;
            
            //adding new possible options
            for (int i = -1; i < 2; i++)
            {
                var nextDirectionIndex = directionIndex + i < 0 
                    ? (directionIndex + i + 4)  % 4 
                    : (directionIndex + i)  % 4;
                var nextPoint = currentPoint + Directions.DirectionsWithoutDiagonals[nextDirectionIndex];
                
                //If the next tile is wall skip
                if(_maze[nextPoint] == '#')
                    continue;
                
                queue.Enqueue((nextPoint, nextDirectionIndex), priority + 1 + Math.Abs(i)*1000);
            }
        }
        return -1;
    }
```

## Part 2
### Problem
Find all possible optimal paths. Count how many tiles belong at least 1 optimal path. 

### Solution
Use same code as in part 1.
Instead of rending loop after finding first optimal path, continue till queue is empty.
If current path score is greater than optimal path, skip calculations.

```csharp
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
```

