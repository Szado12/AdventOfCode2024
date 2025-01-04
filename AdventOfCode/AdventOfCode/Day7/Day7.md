# [Day7](https://adventofcode.com/2024/day/7)

## Input
Multiple lines, each line contains a test value and list of comma separated numbers.

## Part 1

### Problem
Determine whether the remaining numbers can be combined with operators `+` and `*` to produce the test value.

### Solution
Generate all possible options to use operators. Number of options is equal to `2 ^ numbers.Count -1`.
Save each option as a binary number, where 0 is `+` operator and 1 is `*` operator.
Check if any of the generated options is equal to test value.

## Part 2
### Problem
Same issue as for Part1, but with an additional operator `||`. New operator allows connecting two numbers e.g. `22 || 24 = 2224`.

### Solution
Same solution as for Part1,but the number of options is equal to `3 ^ numbers.Count -1`.
Save each options as a number in system with base 3, where 0 is `+` operator, 1 is `*` operator and 2 is `||` operator.
Check if any of the generated options is equal to test value.