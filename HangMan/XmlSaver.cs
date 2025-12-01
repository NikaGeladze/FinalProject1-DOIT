using System.Xml.Linq;

namespace HangMan;

public class XmlSaver
{
    public const string rootNameXML = "PlayerHistory";
    public const string playerName = "Player";
    public const string playerNameXML = "Name";
    public const string LowestAttemptsXML = "LowestAttempts";
    public static void SavePlayer(string path, Player player)
    {
        XElement root;

        if (!File.Exists(path))
        {
            root =new XElement(rootNameXML,
                new XElement(playerName, new XElement(playerNameXML, player.name), new XElement(LowestAttemptsXML, player.attempts)));
        }
        else
        {
            root = XElement.Load(path);
            bool wasFound = false;
            foreach (var element in root.Elements(playerName))
            {
                if (element.Element(playerNameXML).Value.Equals(player.name))
                {
                    if (int.Parse(element.Element(LowestAttemptsXML).Value) > player.attempts)
                    { 
                        element.Element(LowestAttemptsXML).Value = player.attempts.ToString();
                    }
                    wasFound = true;
                    break;
                }
            }
                
            if(!wasFound) root.Add( new XElement(playerName, new XElement(playerNameXML, player.name), new XElement(LowestAttemptsXML, player.attempts)));
        }
        
        root.Save(path);
    }
    
}
