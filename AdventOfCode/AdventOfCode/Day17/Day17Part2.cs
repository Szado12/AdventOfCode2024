namespace AdventOfCode.Day17;

public class Day17Part2 : IPuzzleSolution
{
    private string _input = "../../../Day17/input.txt";
    private string _regAPattern = "Register A: (\\d*)";
    private string _regBPattern = "Register B: (\\d*)";
    private string _regCPattern = "Register C: (\\d*)";
    private static long _regA = 0;
    private static long _regB = 0;
    private static long _regC = 0;
    private List<int> _program = new();
    private static string _output = "";

    private Dictionary<int, Action<long>> _operations = new()
    {
        [0] = operand => _regA = _regA / (int) Math.Pow(2, ReadComboOperand(operand)),
        [1] = operand => _regB = _regB ^ operand,
        [2] = operand => _regB = ReadComboOperand(operand) % 8,
        [4] = _ => _regB ^= _regC,
        [5] = operand => _output += _output.Length == 0 ? ReadComboOperand(operand) % 8 : "," + ReadComboOperand(operand) % 8,
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
                if(y == 4)
                {
                    _program = line.Replace("Program: ", "").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(str => Int32.Parse(str)).ToList();
                }

                y++;
            }
        }

        SearchRegistryA();
        
        return SearchRegistryA().ToString();
    }

    private static void ResetRegistry(long regAValue)
    {
        _output = "";
        _regA = regAValue;
        _regB = 0;
        _regC = 0;
    }

    private long SearchRegistryA()
    {
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
    }

    private bool CheckOutput(string output, List<int> program, int index)
    {
        var outputInt = output.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(str => Int32.Parse(str)).ToList();
        if (outputInt.Count < index)
            return false;
        return outputInt[^index] == program[^index];
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


    private static long ReadComboOperand(long operand)
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