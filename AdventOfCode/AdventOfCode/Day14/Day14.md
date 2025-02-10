# [Day14](https://adventofcode.com/2024/day/14)

## Input
Multiple lines. Each describing robot starting position and robot movement.
## Part 1

### Problem
There are several robots moving always in a straight line. The area is a rectangle 101 tiles wide and 103 tiles tall.
Multiple robots can be in the same position at the same time. If the robots is going outside the rectangle it will teleport to the other site.
Each robot moves every seconds by his specified movement. 
Simulate robot positions after 100 seconds.
Count number of robots in each quadrant. Robots that are exactly in the middle (horizontally or vertically) don't count as being in any quadrant.
The result is a multiplication of number of robots in each quadrant.

e.g

| .   | .   | .   | .   | .   |     | 2   | .   | .   | 1   | .   |   |
| --- | --- | --- | --- | --- |-----| --- | --- | --- | --- | --- |---|
| .   | .   | .   | .   | .   |     | .   | .   | .   | .   | .   | . |
| 1   | .   | .   | .   | .   |     | .   | .   | .   | .   | .   | . |
|     |     |     |     |     |     |     |     |     |     |     |   |
| .   | .   | .   | .   | .   |     | .   | .   | .   | .   | .   | . |
| .   | .   | 1   | 2   | .   |     | .   | .   | .   | .   | .   | . |
| .   | 1   | .   | .   | .   |     | 1   | .   | .   | .   | .   | . |

1st quadrant (up-left): 1  
2nd quadrant (up-right): 3  
3rd quadrant (down-left): 4  
4th quadrant (down-right): 1  
The result is $1*3*4*1 = 12$

### Solution
Simulate position of each robot by adding its movement multiplied by 100 to starting position. Calculated position fix to be in area range:
e.g.
```csharp
foreach (var robot in _robots)
{
    var x = (robot.position.X + robot.movement.X * seconds) % _width;
    if (x < 0)
        x += _width;
    var y = (robot.position.Y + robot.movement.Y * seconds) % _height;
    if (y < 0)
        y += _height;
    var robotPosition = new Point(x,y);
    positions.Add(robotPosition);
}
```
Then calculate number of robots in each quadrant and return multiplication.
```csharp
private int CalculateQuadrants(List<Point> robotPositions)
{
    var q1 = robotPositions.Count(point => point.X < _width / 2 && point.Y < _height / 2);
    var q2 = robotPositions.Count(point => point.X > _width / 2 && point.Y < _height / 2);
    var q3 = robotPositions.Count(point => point.X < _width / 2 && point.Y > _height / 2);
    var q4 = robotPositions.Count(point => point.X > _width / 2 && point.Y > _height / 2);
    return q1 * q2 * q3 * q4;
}
```

## Part 2
### Problem
2nd problem is an easter egg. Robots after a number of iteration would create a picture of a Christmas tree.
The solution is number of iterations.

### Solution
The simplest way was to save the image created by robots in each iteration to text file, and preview the text files in windows explorer.
It can be found by code when the robots create a 3x3 square. 



