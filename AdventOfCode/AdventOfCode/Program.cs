using AdventOfCode;

foreach (var puzzle in PuzzleSolutionLocator.GetPuzzleByDay(11))
{
    Console.WriteLine($"{puzzle.GetType().Name}: {puzzle.Solve()}");
}