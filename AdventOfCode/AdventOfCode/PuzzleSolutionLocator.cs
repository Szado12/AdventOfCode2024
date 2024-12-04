namespace AdventOfCode;

internal class PuzzleSolutionLocator
{
    public static IEnumerable<IPuzzleSolution> GetAll()
    {
        return typeof(PuzzleSolutionLocator).Assembly.GetTypes()
            .Where(type => typeof(IPuzzleSolution).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .Select(type => (IPuzzleSolution)Activator.CreateInstance(type)!);
    }
}