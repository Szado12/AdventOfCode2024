# [Day25](https://adventofcode.com/2024/day/25)

## Input
Input 7 lines per Lock/Key separated by empty line.
Example input:

```
#####
.####
.####
.####
.#.#.
.#...
.....

.....
.....
#.#..
###..
###.#
###.#
#####

.....
.....
.....
#....
#.#..
#.#.#
#####
```

## Part 1

### Problem

The problem involves determining how many unique key-lock pairs fit together in a virtual five-pin tumbler lock system. Each lock and key is represented as a schematic, where:

- **Locks** have pins extending downward from the top.
- **Keys** have cuts extending upward from the bottom.
- A key fits into a lock if, in every column, the combined height of the pin and the key cut does not exceed a predefined limit.

### Solution

To determine valid key-lock pairs:
Iterate through all possible key-lock pairs.
Check if the key fits into the lock without overlapping in any column.
Count and return the number of valid pairs.

The solution outputs the number of valid key-lock pairs that fit together without any overlapping in their respective columns.

Example code Implementation (C#)

```csharp

private long CheckKeysAndLocks()
{
    var output = 0L;
    foreach (var key in _keys)
    {
        foreach (var @lock in _locks)
        {
            bool fits = true;
            for (int i = 0; i < key.Count; i++)
            {
                if (key[i] + @lock[i] > 5)
                {
                    fits = false;
                    break;
                }
            }

            if (fits)
                output++;
        }
    }

    return output;
}
```

## Part 2

### Problem
Part 2 of day 25 required collecting 49 stars - completing all previous tasks

### Solution
Solve all previous problems :D


