namespace AdventOfCode.Day9;

public class Day9Part2 : IPuzzleSolution
{
    private string _input = "../../../Day9/input.txt";
    public string Solve()
    {
        string diskMap;
        
        List<(int index, int length)> freeIndexes = new();
        List<(int index, int length, int originalValue)> takenIndexes = new(); 
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            diskMap = inputReader.ReadToEnd();
        }

        ReadDiskMap(diskMap, takenIndexes, freeIndexes);

        OptimiseDiskByBlocks(takenIndexes, freeIndexes);

        long sum = 0;
        foreach (var takenIndex in takenIndexes)
        {
            for (int i = 0; i < takenIndex.length; i++)
            {
                sum += (takenIndex.index + i) * takenIndex.originalValue;
            }
        }
        
        return sum.ToString();
    }

    private static void ReadDiskMap(string diskMap, List<(int index, int length, int originalValue)> takenIndexes, List<(int index, int length)> freeIndexes)
    {
        var diskIndex = 0;
        for (int i = 0; i < diskMap.Length; i++)
        {
            var length = Int32.Parse(diskMap[i].ToString());
            
            if(length == 0)
                continue;
            
            if (i % 2 == 0) //File
            {
                takenIndexes.Add((diskIndex, length, i/2));
            }
            else //EmptySpace
            {
                freeIndexes.Add((diskIndex, length));
            }

            diskIndex += length;
        }
    }

    private static void OptimiseDiskByBlocks(List<(int index, int length, int originalValue)> takenIndexes, List<(int index, int length)> freeIndexes)
    {
        for (int i = takenIndexes.Count-1; i >= 0; i--)
        {
            for (int j = 0; j < freeIndexes.Count && takenIndexes[i].index > freeIndexes[j].index; j++)
            {
                if (takenIndexes[i].length <= freeIndexes[j].length)//block can be moved
                {
                    takenIndexes[i] = (freeIndexes[j].index, takenIndexes[i].length, takenIndexes[i].originalValue);
                    freeIndexes[j] = (freeIndexes[j].index + takenIndexes[i].length, freeIndexes[j].length - takenIndexes[i].length);
                    break;
                }
            }
        }
    }
}