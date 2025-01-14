using AdventOfCode.Helpers;

namespace AdventOfCode.Day9;

public class Day9Part1 : IPuzzleSolution //More visual version
{
    private string _input = "../../../Day9/input.txt";
    public string Solve()
    {
        string diskMap;
        using (StreamReader inputReader = new StreamReader(_input))
        {
            diskMap = inputReader.ReadToEnd();
        }

        List<int> disk = new();
        Queue<int> emptyIndexes = new();
        CreateDisk(diskMap, disk, emptyIndexes);

        OptimiseDisk(disk, emptyIndexes);
        
        return CalculateValue(disk).ToString();
    }

    private long CalculateValue(List<int> disk)
    {
        long sum = 0;
        
        for (int i = 0; i < disk.Count; i++)
        {
            if (disk[i] == -1)
                break;
            sum += disk[i] * i;
        }

        return sum;
    }

    private void OptimiseDisk(List<int> disk, Queue<int> emptyIndexes)
    {
        for (int i = disk.Count-1; i >= emptyIndexes.Peek(); i--)
        {
            if(disk[i] == -1)
                continue;

            disk[emptyIndexes.Dequeue()] = disk[i];
            disk[i] = -1;
        }
    }

    private static void CreateDisk(string diskMap, List<int> disk, Queue<int> emptyIndexes)
    {
        var currentIndex = 0;
        for (int i = 0; i < diskMap.Length; i++)
        {
            var value = diskMap[i].ToInt();
            if (i % 2 == 0) //file
            {
                for (int j = 0; j < value; j++)
                {
                    disk.Add(i/2);
                    currentIndex++;
                }
            }
            else //empty space
            {
                for (int j = 0; j < value; j++)
                {
                    disk.Add(-1);
                    emptyIndexes.Enqueue(currentIndex);
                    currentIndex++;
                }
            }
        }
    }
}