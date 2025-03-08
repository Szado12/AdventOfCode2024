namespace AdventOfCode.Day24;

public class Gate
{
    public string Input1 { get; set; }
    public string Input2 { get; set; }
    public string Operation { get; set; }
    public string Output { get; set; }

    public Gate(string input1, string input2, string operation, string output)
    {
        Input1 = input1;
        Input2 = input2;
        Operation = operation;
        Output = output;
    }
}

public class Day24Part2 : IPuzzleSolution
{
    private string _input = "../../../Day24/input.txt";
    private List<Gate> gates = new();
    
    private List<Gate> _xyAdd = new();
    private List<Gate> _xyCarries = new();
    private List<Gate> _outputs = new();
    private List<Gate> _ands = new();
    private List<Gate> _carries = new();
    private HashSet<Gate> _incorrect = new();
    
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
                    gates.Add(new (parameters[0], parameters[2], parameters[1], parameters[4]));
                }
            }
        }

        foreach (var gate in gates)
        {
            if (gate.Input1.StartsWith('x') 
                || gate.Input1.StartsWith('y') 
                || gate.Input2.StartsWith('x') 
                || gate.Input2.StartsWith('y'))
            {
                switch(gate.Operation) 
                {
                    case "XOR":
                        _xyAdd.Add(gate); //1st XOR connected to X and Y input
                        break;
                    case "AND":
                        _xyCarries.Add(gate); //AND gate should be connected to OR Gate
                        break;
                    default:
                        Console.WriteLine($"Incorrect gate {gate.Output}");
                        break;
                };
            }
            else
            {
                switch(gate.Operation) 
                {
                    case "XOR":
                        _outputs.Add(gate); //2nd XOR gate the output should be connected to z wire
                        break;
                    case "AND":
                        _ands.Add(gate); // 2nd AND Gate should be connected to OR Gate
                        break;
                    default:
                        _carries.Add(gate); // OR gate carrying the propagation to next module
                        break;
                };
            }
        }

        CheckInputs();
        CheckOutputs();

        return string.Join(",", _incorrect.Select(g => g.Output).Order());
    }

    private void CheckInputs()
    {
        foreach (var add in _xyAdd)
        {
            if (add.Input1 == "x00" || add.Input1 == "y00")
            { continue; }
        
            //If 1st XOR is connected to z output it's incorrect besides x00 and y00 case
            //If output from this gate is not connected to 2nd XOR that's incorrect
            if (add.Output.StartsWith('z') || !_outputs.Any(g =>g.Input1 == add.Output || g.Input2 == add.Output))
                _incorrect.Add(add);
        }
        
        foreach (var carry in _xyCarries)
        {
            if (carry.Input1 == "x00" || carry.Input1 == "y00")
            { continue; }
        
            //AND gate connected to x and y input
            //If the gate output is connected to z wire that incorrect
            //If output from this gate is not connected to OR that's incorrect
            if (carry.Output.StartsWith('z') || !_carries.Any(g =>g.Input1 == carry.Output || g.Input2 == carry.Output))
                _incorrect.Add(carry);
        }
    }
    
     private void CheckOutputs()
     {
         foreach (var output in _outputs)
         {
             //If 2nd XOR output is not connected to z wire it's incorrect
             if (!output.Output.StartsWith('z'))
                 _incorrect.Add(output);
         }
         
         foreach (var and in _ands)
         {
             //If any 2nd AND is connected to z wire it's incorrect
             if (and.Output.StartsWith('z'))
                 _incorrect.Add(and);
         }
    
         foreach (var carry in _carries )
         {
             //If any OR gate is connected to z wire it's incorrect, besides last carry z45
             if (carry.Output.StartsWith('z') && carry.Output != "z45")
                 _incorrect.Add(carry);
         }
    }
}

