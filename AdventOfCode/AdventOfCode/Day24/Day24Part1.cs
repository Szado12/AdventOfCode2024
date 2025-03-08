using AdventOfCode.Helpers;

namespace AdventOfCode.Day24;

public class Day24Part1 : IPuzzleSolution
{
    private string _input = "../../../Day24/input.txt";
    private static Dictionary<string, int> _wires = new();
    private List<(string wire1, string wire2, string output, Func<string, string, int> operation)> _gates = new();
    
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            var readTasks = false;
            while (inputReader.ReadLine() is {} line)
            {
                if (string.IsNullOrEmpty(line))
                {
                    readTasks = true;
                    continue;
                }

                var parameters = line.Split(" ").ToArray();
                if (readTasks)
                {
                    _gates.Add((parameters[0], parameters[2], parameters[4], SelectOperation(parameters[1])));
                    continue;
                }

                var wire = parameters[0].Substring(0, 3);
                var value = parameters[1].ToInt();
                _wires[wire] = value;
            }
        }

        var gatesToCheck = _gates;
        while (gatesToCheck.Count > 0)
        {
            var tasksNotPossibleToProcess = new List<(string, string, string, Func<string, string, int>)>();
            foreach (var task in gatesToCheck)
            {
                if (!_wires.ContainsKey(task.wire1) || !_wires.ContainsKey(task.wire2))
                {
                    tasksNotPossibleToProcess.Add(task);
                    continue;
                }

                var taskValue = task.operation(task.wire1, task.wire2);
                _wires[task.output] = taskValue;
            }

            gatesToCheck = tasksNotPossibleToProcess;
        }

        long output = 0;
        foreach (var wire in _wires.Keys.Where(wire => wire.StartsWith('z')).OrderDescending())
        {
            output = output * 2 + _wires[wire];
        }

        return output.ToString();
    }

    private Func<string, string, int> SelectOperation(string operation)
    {
        return operation switch
        {
            "XOR" => (s1, s2) => _wires[s1] ^ _wires[s2],
            "AND" => (s1, s2) => _wires[s1] & _wires[s2],
            _ => (s1, s2) => _wires[s1] | _wires[s2],
        };
    }
}