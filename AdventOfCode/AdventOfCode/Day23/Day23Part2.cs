namespace AdventOfCode.Day23;

public class Day23Part2 : IPuzzleSolution
{
    private string _input = "../../../Day23/input.txt";
    private Dictionary<string, HashSet<string>> _computers = new();
    private List<string> biggestClique = new();
    
  
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
        
        BronKerbosch(new List<string>(), _computers.Keys.ToList(), new List<string>());
        return string.Join(",",biggestClique.Order());
    }
    
    private void BronKerbosch(List<string> currentClique, List<string> candidates, List<string> exclusionSet)
    {
        if (candidates.Count == 0 && exclusionSet.Count == 0)
        {
            if (currentClique.Count > biggestClique.Count)
                biggestClique = [..currentClique];
        }

        foreach (var candidate in new List<string>(candidates))
        {
            BronKerbosch([..currentClique, candidate], candidates.Intersect(_computers[candidate]).ToList(), exclusionSet.Intersect(_computers[candidate]).ToList());
            candidates.Remove(candidate);
            exclusionSet.Add(candidate);
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