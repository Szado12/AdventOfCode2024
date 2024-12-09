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
        List<int> emptyIndexes = new();
        var currentIndex = 0;
        
        CreateDisk(diskMap, disk, currentIndex, emptyIndexes);

        OptimaiseDisk(disk, emptyIndexes);
        
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

    private void OptimaiseDisk(List<int> disk, List<int> emptyIndexes)
    {
        for (int i = disk.Count-1; i >= emptyIndexes.First(); i--)
        {
            if(disk[i] == -1)
                continue;

            disk[emptyIndexes.First()] = disk[i];
            disk[i] = -1;
            emptyIndexes.RemoveAt(0);
        }
    }

    private static void CreateDisk(string diskMap, List<int> disk, int currentIndex, List<int> emptyIndexes)
    {
        for (int i = 0; i < diskMap.Length; i++)
        {
            var value = Int32.Parse(diskMap[i].ToString());
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
                    emptyIndexes.Add(currentIndex);
                    currentIndex++;
                }
            }
        }
    }
}