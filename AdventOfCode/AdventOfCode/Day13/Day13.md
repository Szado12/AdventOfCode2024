# [Day13](https://adventofcode.com/2024/day/13)

## Input
Multiple lines. Each puzzle contains 3 lines:
- Instruction for button A
- Instruction for button B
- Coordinates of prize  
Puzzles are separated by blank line.
## Part 1

### Problem
The game have 2 buttons A and B.
Each button moves claw on X and Y axis. 
Clicking button A cost 3 tokens while clicking button B cost only 1.
Find games that can be won in the cheapest way. Not every game can be won.


### Solution
The solution is trivial at the whole problem can be transitioned to simple systems of two equations:
e.g. for input:
Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

The problem can be saved as:
$$ \begin{cases} 94a + 22b = 8400 \\ 34a + 67b = 5400 \end{cases} $$

after solving it we got:
$$ \begin{cases} a = 80 \\ b = 40 \end{cases} $$

The solution of the puzzle (token cost) is $$3a + b = 3*80 + 40 = 280$$


## Part 2
### Problem
Problem is completely the same difference is that the prizes are moved by 10000000000000 in both axis.
The restrictions of clicking the buttons max 100 times is removed.

### Solution
Use the solution from 1st part, just correct the prize position, and removed 100 click restrictions.

