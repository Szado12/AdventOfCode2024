# [Day12](https://adventofcode.com/2024/day/12)

## Input
Multiple lines, describing a 2D garden map.

## Part 1

### Problem
The maps describe a grid of garden plots. The different plant types should be separated by the fence.
The same type plants can grow in different regions. Find the price of the fence, required to separate regions.

e.g.
Garden:

| O   | O   | O   | O   | O   |
| --- | --- | --- | --- | --- |
| O   | X   | O   | X   | O   |
| O   | O   | O   | O   | O   |
| O   | X   | O   | X   | O   |
| O   | O   | O   | O   | O   |

Has 1 region of `O` plants and 4 regions of `X` plants.

Cost of the fence is equal to multiplying region's area by its perimeter.

For example garden the cost of the fence is:  
O plants has price 21 * 36 = 756  
single X region has price 1 * 4 = 4  
Total price = 756 + 4 + 4 + 4 + 4 = 772


### Solution
Use flood fill algorithm, for searching regions.
Create a list ot mark points that already belong to region.
Start a flood algorithm on each point that doesn't belong to any region.
Check all directions from current point (up, down, left, right), if the next point is the same type of plant add the point to the stack, if not this side is a fence, so increment the fence length by 1.
Increment area by 1, on each loop run.

e.g code:
```csharp
Hashset<Point> alreadyCheckedPoints = [];
Dictionary<Point, char> garden = ReadGarden();

foreach(var point in garden.Keys)
{
    if(alreadyCheckedPoints.Contains(point))
    {
        continue;
    }
    var result = Search(point, alreadyCheckedPoints);
    cost = result.area * result.perimeter;
}

(int area, int perimeter) Search(Point point, Hashset<Point> alreadyCheckedPoints)
{
    var stack = new Stack<Point>();
    int area = 0;
    int perimeter = 0;
    stack.Push(point);
    while (stack.Count > 0)
    {
        Point currentPoint = stack.Pop();
        if(alreadyCheckedPoints.Contains(currentPoint))
            continue;
        
        alreadyCheckedPoints.Add(currentPoint);
        foreach(var direction in directions)
        {
            Point nextPoint = currentPoint + direction;
            if(IsOutOfRegion(nextPoint)) //point is out of garden or the plant type is different the current region plan type
            {
                perimeter++;
            }
            else
            {
                area++;    
            }
        }
    }
    return (area, perimeter);
}   

```

## Part 2
### Problem
The fence is sold in bulk discount. Instead of the perimeter calculate the number of sides each region has.

### Solution
Do the same algorithm as for 1 st part. Instead of saving parameter save the point and direction the fence is facing.
e.g

| X\Y | 1 | 2 | 3 | 4 | 5 |
|-----|---|---|---|---|---|
| 1   | E | E | E | E | E |
| 2   | E | X | X | X | E |
| 3   | E | E | E | E | E |
| 4   | E | X | X | X | E |
| 5   | E | E | E | E | E |

The point X:5,Y:1 should be added 3 times with fence facing up, right, down.
Iterate through the saved fence data, for each fence point remove any point that is in the same line and is facing same direction.

```csharp

List<(Point point, Point direction)> regionFence = [...];
regionFenceClone = new List(regionFence);

foreach((Point, Point) pointWithFacingDir in regionFence)
{
    if(regionFenceClone.Contains(pointWithFacingDir) == false) //Skip removed points to prevent side from being deleted completly
        continue;
        
    fenceDirections = pointWithFacingDir.direction.PerpendicularDirections; //for up direction its left and right;
    int i = 0;
    while (true) //Remove all the points in same sides to 1st direction
    {
        i++;
        var pointToRemove = (perimeterWithDirection.point + perpendicularDirections[0] * i, perimeterWithDirection.direction);
        if (!perimeter.Remove(pointToRemove))
            break;
    }
    i = 0;
    while (true) //Remove all the points in same sides to 2nd direction
    {
        i++;
        var pointToRemove = (perimeterWithDirection.point + perpendicularDirections[1] * i, perimeterWithDirection.direction);
        if (!perimeter.Remove(pointToRemove))
            break;
    }
}

return regionFenceClone.Count; //Fence now contains single value for each side
```

