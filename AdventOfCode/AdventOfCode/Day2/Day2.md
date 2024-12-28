# [Day1](https://adventofcode.com/2024/day/2)

## Input
Each line represents a single report. Each report contains a number of numeric values.

## Part 1

### Problem
Check if all numeric values in a report are:
- either all increasing or all decreasing
- differ by at least one and at most three from adjacent values

### Solution
If the report contains single value, report is correct.
If report contains 2 or more values:
- calculate whether the values are decreasing or increasing basing on first 2 numbers:
```cs
var firstDiff = report[0] - report[1];
var expectedSign = firstDiff / int.Abs(firstDiff);
```
- check if first difference is not greater than 3 and not smaller than 1
- continue checking pairs for adjacent numbers, comparing if the difference sign is the same as for first pair and if the differences in 1-3 range.

## Part 2
### Problem
Check if the not correct reports can be fixed by removing single numeric value from them.

e.g.
```1 3 2 4 5``` is not correct as at the first two numbers are growing 1 -> 3, but 2nd to 3rd 3->2 number decrease.
It can be fixed by removing 2nd number ```1 2 4 5``` or 3rd: ```1 3 4 5```.

### Solution
Modify 1st solution to return index of first number in incorrect pair of numbers.
Check if removing:
- 1st index in report fixes the issue e.g. ```1 3 2 1``` -> ```3 2 1```
- check if removing the first number of incorrect pair fixes the issue ```1 3 3 5``` -> ```1 3 5```
- check if removing the first number of incorrect pair fixes the issue ```9 8 4 6 5``` -> ```9 8 6 5```



