namespace NumberGuessingGame;

public class LeaderBoard
{
    public static string[] GetTopTenLowestScores(string path)
    {
        if (!File.Exists(path))
            return Array.Empty<string>();

        string[] allLines = File.ReadAllLines(path)
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

        string[] top10 = allLines.OrderBy(line =>
            {
                string[] parts = line.Split(',');
                return int.Parse(parts[1]);
            })
            .Take(10)
            .ToArray();

        return top10;
    }

    public static void PrintTopTen(string path)
    {
        var topTen = GetTopTenLowestScores(path);
        
        Console.WriteLine("\n-----Top 10 Lowest Attempts Players(player,score)---------");
        foreach (var line in topTen)
        {
            Console.WriteLine(line);
        }
    }
}