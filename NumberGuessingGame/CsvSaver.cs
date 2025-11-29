namespace NumberGuessingGame;

public class CsvSaver
{
    public static void SaveHighestScore(string path, Player p)
    {
        List<string> lines = new();

        if (File.Exists(path))
        {
            lines = File.ReadAllLines(path).ToList();
            
            if (lines.Count == 0)
                lines.Add("Player,Best Score");
        }
        else
        {
            lines.Add("Player,Highest Score");
        }

        int index = lines.FindIndex(line => line.StartsWith(p.name + ","));

        if (index != -1)
        {
            string[] parts = lines[index].Split(',');
            int oldScore = int.Parse(parts[1]);

            if (p.score < oldScore)
            {
                lines[index] = $"{p.name},{p.score}";
            }
        }
        else
        {
            lines.Add($"{p.name},{p.score}");
        }

        File.WriteAllLines(path, lines);
    }

    
}