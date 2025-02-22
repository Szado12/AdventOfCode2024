using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day17;

public class Day17Part1 : IPuzzleSolution
{
    private string _input = "../../../Day17/input.txt";
    private string _regAPattern = "Register A: (\\d*)";
    private static int _regA = 0;
    private static int _regB = 0;
    private static int _regC = 0;
    private List<int> _program = new();
    private static string _output = "";

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
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            var y = 0;
            while (inputReader.ReadLine() is {} line)
            {
                if (y == 0)
                {
                    _regA = Regex.Match(line, _regAPattern).Groups[1].Value.ToInt();
                }
                
                else if(y == 4)
                {
                    _program = line.Replace("Program: ","").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(str => str.ToInt()).ToList();
                }

                y++;
            }
        }

        RunProgram();

        return _output.Remove(_output.Length-1);
    }

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


    private static int ReadComboOperand(int operand)
    {
        switch (operand){
            case 0:
                return 0;
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 3;
            case 4:
                return _regA;
            case 5:
                return _regB;
            case 6:
                return _regC;
        }

        throw new Exception("impossible input");
    }
    
}