# [Day1](https://adventofcode.com/2024/day/1)

## Input
Left and right list of numbers. Each line contains 2 numbers separated by space.
First number belongs to left list, second number to right list.

## Part 1

### Problem
Calculate difference between the smallest number in the left list with the smallest number in the right list,
then the second-smallest left number with the second-smallest right number, and so on.

### Solution
Sort both lists and iterate by each pair, adding the absolute difference to the final result.

## Part 2
### Problem
Calculate how often each number from the left list appears in the right list.
The number of occurence multiply by the number value.

e.g.

| Left list | Right list |
|-----------|------------|
| 1         | 0          |
| 2         | 2          |
| 3         | 2          |
| 4         | 3          |

Number 1 occurs 0 times, 2 - 2 times, 3 - 1 time, 4 - 0 times
Output:  1 * 0 + 2 * 2 + 3 * 1 + 4 * 0 = 7

### Solution
Create a dictionary from right list, the key is the number and value is how many times it occured in list.
Then iterate by each value from left list if the dictionary doesn't contain the key, it means right list doesn't contain this value.
For each key existing in dictionary add to the sum multiplication of the key and value.



