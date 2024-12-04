using AdventOfCode;

foreach (var puzzle in PuzzleSolutionLocator.GetAll())
{
    Console.WriteLine($"{puzzle.GetType().Name}: {puzzle.Solve()}");
}