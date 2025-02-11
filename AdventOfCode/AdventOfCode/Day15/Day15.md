# [Day15](https://adventofcode.com/2024/day/15)

## Input
Input is divided into 2 parts:
- lines describing 2 2d map of a warehouse
- sequence of robot movement
## Part 1

### Problem
There is a robot in a warehouse. It can move up, down left and right.
The warehouse contains single robot and multiple boxes.
Robot can push one or more boxes, but not if any of the box would be pushed into a wall.

e.g
@ - robot
O - boxes
. - empty positions
\# - wall

| # | # | # | # | # | # | 
|---|---|---|---|---|---| 
| # | . | . | . | . | # | 
| # | @ | O | O | . | # | 
| # | . | . | . | . | # | 
| # | # | # | # | # | # | 

Moving once to the right will move both boxes.

| # | # | # | # | # | # | 
|---|---|---|---|---|---| 
| # | . | . | . | . | # | 
| # | . | @ | O | O | # | 
| # | . | . | . | . | # | 
| # | # | # | # | # | # | 

Moving second time to the right will not change any positions as the 2nd box is touching the wall.


Result is sum of all boxes' GPS coordinate (the Y coordinate needs to be multiplied by 100).
### Solution
Simulate the robot movement if next position is:
- empty - move the robot
- wall - robot position is not changed
- box - check recursively whether the box can be moved.

Code for simulating the robot movement:
```csharp
private void Simulate(Point movement)
{
    var nextPoint = _robotPoint + movement;
    
    if (_map[nextPoint] == _wall)
        return;
    
    if (_map[nextPoint] == _empty)
    {
        _map[_robotPoint] = _empty;
        _map[nextPoint] = _robot;
        _robotPoint = nextPoint;
        return;
    }

    if (_map[nextPoint] == _box)
    {
        if (CheckIfBoxCanBeMoved(nextPoint, movement))
        {
            _map[_robotPoint] = _empty;
            _map[nextPoint] = _robot;
            _robotPoint = nextPoint;
        }
    }
}
```
Code for checking whether the box can be moved:
```csharp
private bool CheckIfBoxCanBeMoved(Point boxPoint, Point movement)
{
    var nextPoint = boxPoint + movement;
    
    if (_map[nextPoint] == _wall)
        return false;
    
    if (_map[nextPoint] == _empty)
    {
        _map[nextPoint] = _box;
        return true;
    }

    return CheckIfBoxCanBeMoved(nextPoint, movement);
}
```

## Part 2
### Problem
The warehouse is now wider:
- If the tile is `#,` the new map contains `##` instead.
- If the tile is `O,` the new map contains `[]` instead.
- If the tile is `.,` the new map contains `..` instead.
- If the tile is `@,` the new map contains `@.` instead.

Moving boxes to the left or right doesn't change much, boxes can be moved if there is empty position after all box parts.
e.g.

| # | # | # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|---|---|
| # | . | . | . | . | . | . | . | . | # |
| # | @ | [ | ] | [ | ] | . | [ | ] | # |
| # | . | . | . | . | . | . | . | . | # |
| # | # | # | # | # | # | # | # | # | # |

Moving once to the right will move both boxes.

| # | # | # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|---|---|
| # | . | . | . | . | . | . | . | . | # |
| # | . | @ | [ | ] | [ | ] | [ | ] | # |
| # | . | . | . | . | . | . | . | . | # |
| # | # | # | # | # | # | # | # | # | # |


Moving second time to the right will not change any positions as the 3rd box is touching the wall.

Moving up and down boxes is more complex, as any part of the box can touch other box.
e.g.

| # | # | # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|---|---|
| # | . | . | . | . | [ | ] | . | . | # |
| # | . | . | . | [ | ] | . | . | . | # |
| # | . | [ | ] | . | . | . | . | . | # |
| # | . | . | [ | ] | . | . | . | . | # |
| # | . | [ | ] | . | . | . | . | . | # |
| # | . | @ | . | . | . | . | . | . | # |
| # | # | # | # | # | # | # | # | # | # |

Moving the robot up once will move 3 boxes 1 tile up.


| # | # | # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|---|---|
| # | . | . | . | . | [ | ] | . | . | # |
| # | . | [ | ] | [ | ] | . | . | . | # |
| # | . | . | [ | ] | . | . | . | . | # |
| # | . | [ | ] | . | . | . | . | . | # |
| # | . | @ | . | . | . | . | . | . | # |
| # | . | . | . | . | . | . | . | . | # |
| # | # | # | # | # | # | # | # | # | # |

Moving once again up is not possible, as there is a blocking box touching the wall.

### Solution
Logic for moving the robot to the wall or empty tile stays the same.
Pushing the boxes in horizontal direction, doesn't change much, now the box tile can be `[` or `]`.
Pushing the boxes in vertical direction requires checking whether both parts of the box can be moved.

```csharp

private bool Simulate(Point foodPoint, Point movement)
{
    var nextPoint = foodPoint + movement;
    
    if (_map[nextPoint] == _wall)
        return false;

    if (_map[nextPoint] == _empty)
    {
        return true;
    }

    if (movement.Y == 0) //Horizontal movement
    {
        if (Simulate(nextPoint, movement))
        {
            _pointsToChange.Add((nextPoint + movement, _map[nextPoint]));
            _pointsToClear.Add((nextPoint, _empty));
            return true;
        }

        return false;
    }

    Point boxRightPoint;
    Point boxLeftPoint;

    if (_map[nextPoint] == _boxRight) //Vertical movement
    {
        boxRightPoint = nextPoint;
        boxLeftPoint = nextPoint - new Point(1, 0);
    }
    else
    {
        boxLeftPoint = nextPoint;
        boxRightPoint = nextPoint + new Point(1, 0);
    }

    if (Simulate(boxRightPoint, movement) && Simulate(boxLeftPoint, movement))
    {
        _pointsToChange.Add((boxRightPoint + movement , _boxRight));
        _pointsToChange.Add((boxLeftPoint + movement, _boxLeft));
        _pointsToClear.Add((boxRightPoint, _empty));
        _pointsToClear.Add((boxLeftPoint, _empty));
        return true;
    }
    return false;
}
```



