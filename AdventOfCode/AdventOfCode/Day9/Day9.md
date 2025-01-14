# [Day9](https://adventofcode.com/2024/day/9)

## Input
Single line describing disk.
Numbers on odd indexes describe memory blocks taken by programs.
Numbers on even indexes describe free memory blocks.

## Part 1

### Problem
Disk defragmentation - move program memory blocks to free memory blocks, by moving single digit.
e.g.
Input: 12345  
Disk:  
- . - empty space  
- number - memory taken by program  

`0..222....22222`

Disk after optimization: `022111222......`  
Return sum of multiplication bit index * bit value.  
Value: `60`
### Solution
Create a disk as a List of ints, save empty indexes to queue.
Create an index fo empty positions - emptyIndex. 
Loop through disk from end to the beginning:
- skip empty positions
- swap position of program memory with current empty index, increment emptyIndex.
- if emptyIndex is greater than current loop index, break the loop.

```csharp
for (int i = disk.Count-1; i >= emptyIndexes.Peek(); i--)
    {
        if(disk[i] == -1) //Empty space
            continue;

        disk[emptyIndexes.Dequeue()] = disk[i]; //swap
        disk[i] = -1;
    }

```

## Part 2
### Problem
Disk defragmentation - move program memory blocks to free memory blocks, by moving whole blocks.
e.g.  
Input: 1534314  
Disk: `0.....111....222.3333`  
Disk after optimization: `03333.111222.........`  


### Solution
Create a list of free spaces with startIndex and Length.
Create a list of taken space with startingIndex, length, and original value (need to calculate)

Loop trough list of taken places backwards. For each block check loop through free memory blocks,
it the free memory block is longer than program memory block, move it.
Moving is done by updating the startIndexes.
```csharp
takenIndexes[i] = (freeIndexes[j].index, takenIndexes[i].length, takenIndexes[i].originalValue);
freeIndexes[j] = (freeIndexes[j].index + takenIndexes[i].length, freeIndexes[j].length - takenIndexes[i].length);
```