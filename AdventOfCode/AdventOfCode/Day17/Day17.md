# [Day17](https://adventofcode.com/2024/day/17)

## Input
5 Lines:
- starting value for register A
- starting value for register B
- starting value for register C
- program - sequence of operations
## Part 1

### Problem
Simulation of a computer with 8 operations and 3 registers.  
Registers:
- A
- B
- C

Operands:
- Literal operand: The value is the operand itself.
- Combo operand: Could be a small literal (0–3) or the value of register A, B, or C (4–6).

Operations:
- adv (opcode 0) — Division:
  - Operand type: Combo
  - Divides register A by 2 raised to the value of the combo operand. Stores the result in A. 
- bxl (opcode 1) — Bitwise XOR with literal:
  - Operand type: Literal
  - XORs register B with the literal operand. Stores the result in B.
 - bst (opcode 2) — Set B to value modulo 8:
   - Operand type: Combo
   - Sets register B to the value of the combo operand modulo 8.
 - jnz (opcode 3) — Jump if nonzero:
   - Operand type: Literal
   - If A is nonzero, jumps to the instruction at the operand’s position.
- bxc (opcode 4) — Bitwise XOR with C:
  - Operand type: (Ignored)
  - XORs registers B and C. Stores the result in B. Operand is read but not used.
- out (opcode 5) — Output value:
  - Operand type: Combo
  - Outputs the value of the combo operand modulo 8.
- bdv (opcode 6) — Division, store in B:
  - Operand type: Combo
  - Divides A by 2 raised to the value of the combo operand. Stores the result in B.
- cdv (opcode 7) — Division, store in C:
  - Operand type: Combo
  - Divides A by 2 raised to the value of the combo operand. Stores the result in C.

### Solution
There are many solutions possible how to do that, my solution uses dictionary with opcodes as keys and operations as values:
```csharp
private Dictionary<int, Action<int>> _operations = new()
  {
      [0] = operand => _regA = _regA / (int) Math.Pow(2, ReadComboOperand(operand)),
      [1] = operand => _regB = _regB ^ operand,
      [2] = operand => _regB = ReadComboOperand(operand) % 8,
      [4] = _ => _regB ^= _regC,
      [5] = operand => _output += ReadComboOperand(operand) % 8 + ",",
      [6] = operand => _regB = _regA / (int) Math.Pow(2, ReadComboOperand(operand)),
      [7] = operand => _regC = _regA / (int) Math.Pow(2, ReadComboOperand(operand)),
  };
```

Main loop moves the pointer that reads the program if the pointer is outside the program range break the loop.
For all operations besides jnz, pointer moves by 2 (reading opcode and operand).
Special case is for operation jnz that can move pointer to  it literal operand value if registry A is not null.

```csharp
private void RunProgram()
{
    var pointer = 0;
    while (pointer < _program.Count)
    {
        if (_program[pointer] == 3)
        {
            if (_regA != 0)
                pointer = _program[pointer + 1];
            else
                pointer += 2;
        }
        else
        {
            _operations[_program[pointer]](_program[pointer + 1]);
            pointer += 2;
        }
    }
}
```
## Part 2
### Problem
For Program:
```
Register A: 32916674
Register B: 0
Register C: 0

Program: 2,4,1,1,7,5,0,3,1,4,4,0,5,5,3,0
```
Find starting value for registry A that will cause the out operation to print its own program.
e.g. program will print `2,4,1,1,7,5,0,3,1,4,4,0,5,5,3,0`

### Solution
First let see what the program is actually doing:
```
B = A % 8
B = B XOR 1
C = A / (2^B)
A = A / 2^3 -> A = A/8
B = B XOR 4
B = B XOR C
output B % 8
jump to start if A != 0
```

Program works in loop till A = 0. 
In each loop it divides the value in A registry by 8.
The initial registry A value is at least 8^15 as its need to print 16 numbers.

This approach searches numbers backwards e.g. checking the output from last to first number.
So first find a number that can output 16 numbers and the last one is 0.
Find value of 8^15 and multiply it by numbers form 1 to 7 (greater number than 7 will produce another output value).
Check program for all 7 starting values, the values which last number in output was 0 are stored in list.
In next loop generate values by multiplying 8^14 * numbers from to 0 to 7.
Check all generated values with already stored values in list.
If stored value + generated value gives now 2 correct last numbers add it to new list, after checking all possibilities override previous stored values with new ones.

Example code:
```csharp

List<long> matches = new List<long> {0}; 
for (int i = _program.Count - 1; i >= 0; i--)
{
    long pow8 = (long)Math.Pow(8, i);
    List<long> newMatches = new List<long>();
    foreach (var match in matches)
    {
        for (int j = 0; j < 8; j++)
        {
            var regAToCheck = match +  pow8 * j;
            ResetRegistry(regAToCheck);
            RunProgram();
            if(CheckOutput(_output, _program, _program.Count-i))
                newMatches.Add(regAToCheck);
        }
    }

    matches = newMatches;
}

return matches.Order().First();
```