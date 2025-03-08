namespace AdventOfCode.Day23;

public class Day23Part1 : IPuzzleSolution
{
    private string _input = "../../../Day23/input.txt";
    private Dictionary<string, HashSet<string>> _computers = new();
    private HashSet<(string, string, string)> _lans = new();
    
  
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is {} line)
            {
                var computers = line.Split("-");
                AddComputer(computers[0], computers[1]);
                AddComputer(computers[1], computers[0]);
            }
        }

        
        foreach (var tComputers in _computers.Keys.Where(key => key.StartsWith('t')))
        {
            FindLans(tComputers);
        }

        return _lans.Count.ToString();
    }

    private void FindLans(string startComputer)
    {
        var tNeighbours = _computers[startComputer];

        foreach (var tNeighbour in tNeighbours)
        {
           var commonNeighbours = tNeighbours.Intersect(_computers[tNeighbour]);
           foreach (var commonNeighbour in commonNeighbours)
           {
               var lanName = new List<string> {commonNeighbour, tNeighbour, startComputer}.Order().ToArray();
               _lans.Add((lanName[0], lanName[1], lanName[2]));
           }
        }
    }

    private void AddComputer(string computer1, string computer2)
    {
        if (_computers.ContainsKey(computer1))
            _computers[computer1].Add(computer2);
        else
            _computers[computer1] = [computer2];
    }
}

