# [Day4](https://adventofcode.com/2024/day/4)

## Input
Multiple lines.

## Part 1

### Problem
Word search - search for word `XMAS`.
Word search allows words to be horizontal, vertical, diagonal, written backwards, or even overlapping other words.

### Solution
Map input to point(x,y) - character dictionary. Find all `X` characters in input, and use as starting points.
For each starting point search in all directions. For each direction check if next character is equal to next character in word is so continue.

e.g.

| X/Y | 1 | 2 | 3 | 4 | 5 |
|-----|---|---|---|---|---|
| 1   | . | X | X | S | . |
| 2   | . | S | A | M | X |
| 3   | . | . | S | A | . |
| 4   | . | . | . | . | S |

Starting Points = [(2,1),(3,1),(5,2)]

For point (2,1) all neighbourhood positions are out of range or contains not `A` character.
Same for point (5,2).
For point (3,1) searching in left directions found XMAS word.

## Part 2
### Problem
Word search for two crossing MAS words.
e.g.

| X/Y | 1 | 2 | 3 |
|-----|---|---|---|
| 1   | M | . | M |
| 2   | . | A | . | 
| 3   | S | . | S |


### Solution
Find all points with value equal to `A` character.
For each potential crossings search in diagonal directions. Starting point should be modified by word length divided by 2 in opposite direction to search direction.

e.g.

If search direction is down-right (1,1), and A character is in point (2,2) start search in point:
`startPoint = (2,2) - (1,1) * 'MAS'.Length/2`  
`startPoint = (2,2) - (1,1)`  
`startPoint = (1,1)`  



