namespace AdventOfCode;

internal class PuzzleSolutionLocator
{
    public static IEnumerable<IPuzzleSolution> GetAll()
    {
        return typeof(PuzzleSolutionLocator).Assembly.GetTypes()
            .Where(type => typeof(IPuzzleSolution).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .Select(type => (IPuzzleSolution)Activator.CreateInstance(type)!);
    }
    
    public static IEnumerable<IPuzzleSolution> GetPuzzleByDay(int day)
    {
        return typeof(PuzzleSolutionLocator).Assembly.GetTypes()
            .Where(type => typeof(IPuzzleSolution).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract && (type.Namespace?.Contains($"Day{day}") ?? false))
            .Select(type => (IPuzzleSolution)Activator.CreateInstance(type)!);
    }
}