# [Day 20](https://adventofcode.com/2024/day/20)

## Input

The input consists of a map with the following characters:
- `.` representing track (the race course)
- `#` representing walls
- `S` representing the start position
- `E` representing the end position

Example map:

| # | # | # | # | # | # | # | # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| # | S | . | . | . | # | # | . | # | # | # | # | # | # | # |
| # | . | # | . | . | # | # | . | # | . | . | . | # | # | # |
| # | . | # | . | # | # | . | # | # | . | # | . | # | # | # |
| # | . | # | . | . | # | . | . | . | # | # | # | # | # | # |
| # | . | # | # | . | . | . | # | # | # | . | . | # | . | # |
| # | . | # | # | # | . | . | . | . | # | # | . | # | . | # |
| # | # | . | . | # | . | . | # | # | . | . | . | # | # | # |
| # | # | # | . | # | . | . | . | . | # | . | # | # | # | # |
| # | . | . | . | . | # | # | # | . | . | . | . | . | E | # |
| # | # | # | # | # | # | # | # | # | # | # | # | # | # | # |


## Part 1

### Problem

In this part of the puzzle, you need to find the number of possible cheats that will save at least 100 picoseconds. 
You can cheat by traversing through walls, but only once, and only for a limited number of steps (2 steps per cheat). 
You need to determine how many cheats would save at least 100 picoseconds based on the map of the racetrack.

### Solution

To solve this, we start by reading the map and parsing the start (`S`) and end (`E`) points. 
Then, we create the default path from `S` to `E` by using a breadth-first search (BFS) approach.

We iterate through each point on the path and look for potential cheats by checking neighboring points that are walls and can be traversed.
For each cheat, we calculate the time saved and store it if the time saved is greater than or equal to 100 picoseconds.

Example code for BFS search:

```csharp
private void CreateDefaultPath()
{
    _defaultPath = new();
    _defaultPath[_start] = 0;
    var step = 0;
    var (nextPoint, direction) = NextPoint(_start, null);
    
    while (nextPoint != _end)
    {
        _defaultPath[nextPoint] = ++step;
        (nextPoint, direction) = NextPoint(nextPoint, direction);
    }
    
    _defaultPath[_end] = ++step;
}
```

Example code for finding cheat ways:

```csharp
private int FindPossibleCheats()
{
    Dictionary<(Point, Point),int> _cheats = new ();

    foreach (var (point, step) in _defaultPath)
    {
        foreach (var direction in Directions.DirectionsWithoutDiagonals)
        {
            var wallPoint = point + direction;
            var trackPoint = point + direction*2;
            
            if(trackPoint.IsOutOfRange(_width, _height))
                continue;
            
            if(!_map[wallPoint] && _map[trackPoint])
            {
                _cheats[(point, trackPoint)] = _defaultPath[trackPoint] - step - 2;
            }
        }
    }
    return _cheats.Values.Count(x => x >= 100);
}
```

## Part 2

### Problem

In part 2, the cheating rule has changed. Instead of cheating for 2 picoseconds, the program can now cheat for up to 20 picoseconds. 
The goal is to find the number of cheats that would save at least 100 picoseconds based on the updated rule.

### Solution

The solution is similar to Part 1 but with an updated rule for the maximum cheat duration.
We now check for possible cheats from each point with the possibility of cheating for up to 20 picoseconds.
We return the count of cheats that save at least 100 picoseconds.

Example code:

```csharp
private List<(Point trackPoint, int distance)> GetPossibleCheatsFromPoint(Point startPoint)
{
    return _defaultPath.Keys
        .Select(point => (point, startPoint.DistanceXY(point)))
        .Where(pointDistance => pointDistance.Item2 <= 20)
        .ToList();
}
    
private int FindPossibleCheats()
{
    Dictionary<(Point, Point),int> _cheats = new ();

    foreach (var (point, step) in _defaultPath)
    {
        var cheatPoints = GetPossibleCheatsFromPoint(point);
        foreach (var (cheatPoint , distance) in cheatPoints)
        {
            _cheats[(point, cheatPoint)] = _defaultPath[cheatPoint] - step - distance;
        }
    }
    return _cheats.Values.Count(x => x >= 100);
}
```
