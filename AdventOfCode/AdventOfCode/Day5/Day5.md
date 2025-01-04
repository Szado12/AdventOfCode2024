# [Day5](https://adventofcode.com/2024/day/5)

## Input
Lines with list of rules (47|53).
Lines with comma separated numbers. 

## Part 1

### Problem
Check if all numbers in line are arranged to according rules.
Rule `47|53` means number 47 must be before number 53.

### Solution
Create a dictionary with key number, value list of numbers that should not be placed before key number.
e.g. rules:
`47|53`
`97|13`
`97|61`
`97|47`

will create dictionary:
`[47] = {53}`
`[97] = {13, 47, 61}`

For each list of numbers, check whether all numbers after the checked value are contained in the rule dictionary.
e.g.
Dictionary:
`[47] = {13, 61}`
`[61] = {13}`
`[97] = {13, 47, 61}`

Numbers: `97, 47, 61, 13`

- Check if all numbers after `97` are contained in Dictionary[`97`].
- Check if all numbers after `47` are contained in Dictionary[`47`].
- Check if all numbers after `61` are contained in Dictionary[`61`].

Only the last number should not be a key in the dictionary, so if any non-last number is missing an entry in the dictionary the order of numbers is incorrect.


## Part 2
### Problem
Fix the numbers in incorrect order.

### Solution
Find the numbers ordered incorrectly by using solution from 1st part.

For each number, check if all numbers after the `currentNumber`, are contained in `Dictionary[currentNumber]`
- if yes, continue to next number
- if not, swap the `currentNumber` with number not contained in list in Dictionary. After swap, check the same index once more.
Continue till all numbers are correctly ordered.
- 
e.g.
Dictionary:
`[47] = {13, 61}`
`[61] = {13}`
`[97] = {13, 47, 61}`

Numbers: `97, 13, 47, 61`

- `97` is correct
- `13` is incorrect and doesn't have an entry in dictionary so should be last - swapping `13` with `61` ->   `97, 61, 47, 13`
- `61` is incorrect as `47` is not contained in `Dictionary[61]` - swapping `61` with `47` -> `97, 47, 61, 13`
- `47` is correct
- `61` is correct
- `13` is correct
Numbers `97, 47, 61, 13` are ordered correctly.
