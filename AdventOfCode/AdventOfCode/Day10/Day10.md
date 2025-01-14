# [Day10](https://adventofcode.com/2024/day/10)

## Input
Multiple lines, describing a 2D topographic map, indicates the height at each position using a scale from 0 (lowest) to 9 (highest).

## Part 1

### Problem
Trails should start from the lowest point 0 to the highest point 9. Each trail step should go always up by 1.
Find all trailheads `0` that start a valid trail. Multiple trails starting from same 0 and ending at the same 9, are counted as a single trail.
The score of a trailhead is number of distinct valid trails.
Return sum of trailheads score e.g.

`10..9..`  
`2...8..`  
`3...7..`  
`4567654`  
`...8..3`  
`...9..2`  
`.....01`  

Value = 3
### Solution
Find all 0 in the map.
For each 0 start recurrent function searching in all directions for next number step value (1).
If methods finds 9 that means the valid trails was found, the position of 9 should be added to hashset to exclude other trails that reach the same position.

Return sum HashSet count for each 0 in the map.
```csharp
private void SearchPathRec(Point startingPoint, Point direction, int i, HashSet<Point> finalPoints)
    {
        var nextPoint = startingPoint + direction;
        if (nextPoint.IsOutOfRange(_width, _height))
            return;


        if (Int32.Parse(_map[nextPoint.Y][nextPoint.X].ToString()) != i)
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


```

## Part 2
### Problem
The score of trailhead should be equal to the number of valid trails - the distinct rule is removed.

### Solution
Same as for part1, but change HashSet to List.