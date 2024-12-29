# [Day3](https://adventofcode.com/2024/day/3)

## Input
Single line representing corrupted memory of program.

## Part 1

### Problem
Find all occurrences of multiplication operations in memory, e.g. `mul(11,23)`. Return sum of all multiplications.

### Solution
Simple regex search by pattern `mul\(([0-9]{1,3}),([0-9]{1,3})\)`. 
For each match, add to the result value of multiplication of number in group 1 and 2.
## Part 2
### Problem
The program contains additionally 2 more operations:
- `do()` - turn on multiplication operation
- `don't()` turn off multiplication operation
While the multiplication operation is turned off, result of multiplication should not be added to final result.
Multiplication operation is turned on at the start of the program.

e.g.
`dsd don't() smul(1,3)sd do() xafmul(3,6)` should return result 18.

### Solution
Additionally to previous regex search, search also for all do and don't command.
Merge the indexes of found do and don't command into a single array.
Loop through all multiplication matches, if the multiplication match index is greater than current do/don't command index, move to next do/don't command.
Add the value of multiplication only if the operation of multiplication is turned on.



