using System.Xml.Linq;

namespace HangMan;

public class Leaderboard
{
    public static string[] GetTop10(string path)
    {
        if (File.Exists(path))
        {
            XElement root = XElement.Load(path);
            string[] top10 = root.Elements(XmlSaver.playerName)
                .OrderBy((x) => int.Parse(x.Element(XmlSaver.LowestAttemptsXML).Value)).Take(10).Select((x) =>
                    $"{x.Element(XmlSaver.playerNameXML).Value},{x.Element(XmlSaver.LowestAttemptsXML).Value}"
                ).ToArray();

            return top10;
        }

        return Array.Empty<string>();
    }

    public static void PrintTop10(string path)
    {
        Console.WriteLine("\n------Top10 Lowest Attempt Players(name,attempts)--------");
        foreach (string s in GetTop10(path))
        {
            Console.WriteLine(s);
        }
    }
}