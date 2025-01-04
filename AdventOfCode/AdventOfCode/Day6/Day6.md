# [Day6](https://adventofcode.com/2024/day/6)

## Input
Multiple lines describing 2d map of a room with a guard and obstacles.

## Part 1

### Problem
Find all positions that are on guard road.
The guard starts by moving up, if the road is blocked by an obstacle, the guard will rotate to the right and continue walking.

### Solution
Simulate the guard road, save positions in hashset.

## Part 2
### Problem
Find all possible positions to introduce an obstacle which will cause a guard to walk in a loop.

### Solution
Find the numbers ordered incorrectly by using solution from 1st part.
Try spawning an obstacle on each position from the original guard route.
Loop detection create a `HashSet<Point position, Point direction>` if adding a new tuple fails,
that means there is already the same tuple in hashset, so the guard started a loop from the beginning.
