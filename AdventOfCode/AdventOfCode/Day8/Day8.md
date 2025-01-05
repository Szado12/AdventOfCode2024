# [Day8](https://adventofcode.com/2024/day/8)

## Input
Multiple lines describing 2d map.
`.` - empty position
any other character - antenna of character type

## Part 1

### Problem
Antennas of same type can create an antinode. Antinodes are placed in the same distance as the distance between pair of same type antennas.
If Position of antionde is out of map range, don't count this antinode. Overlapping antinodes should be counted once.
e.g.  
`| . | . | . | . | . |`  
`| . | T | T | . | . |`  
`| . | . | . | . | . |`  
`| . | . | G | . | . |`  
`| . | . | . | G | . |`  

`T` antennas will create 2 antiondes `t`. `G`  antennas will create only one `g` antinode, as 2nd is located out of map.

`| . | . | . | . | . |`  
`| t | T | T | t | . |`  
`| . | g | . | . | . |`  
`| . | . | G | . | . |`  
`| . | . | . | G | . |`

### Solution
Create a dictionary with key - antenna type -char, and value - List of antennas positions.
For each key value pair, check each antenna with other antenna in list. 
Calculate distance between antennas:

`diff = antena1 - antena2;`

Calculate positions of antinodes:

`antinode1 = antena1 + diff;`  
`antinode2 = antena2 - diff;`

If antinode is located in map range add it to Hashset (to remove overlapping positions) of positions.
After iterating through each antennas type, return antionode hashset count.

## Part 2
### Problem
Antinode can be created by interference of antinode and antenna. Two antennas will create many antinodes in same line. Antenna will become also the antinode!.
e.g.  
`| . | . | . | . | . |`  
`| . | T | T | . | . |`  
`| . | . | . | . | . |`  
`| . | . | G | . | . |`  
`| . | . | . | G | . |`

Result will be:  
`| .. | . | . | . | . |`  
`| tg | t | t | t | t |`  
`| .. | g | . | . | . |`  
`| .. | . | g | . | . |`  
`| .. | . | . | g | . |`



### Solution
Same approach as for part 1. Difference is in calculation of antinode positions.
Calculate distance between antennas:

`diff = antena1 - antena2;`

Calculate positions of antinodes in loop by adding distance:


```cs
antinode = antena1;
while(antinode.IsInMap)
{
    antinodes.Add(antionde);
    antinodes += diff;
}
```

And same loop but now remove distance:
```cs
antinode = antena2;
while(antinode.IsInMap)
{
    antinodes.Add(antionde);
    antinodes -= diff;
}
```
