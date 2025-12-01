namespace HangMan;

public class Player
{
    public string name;
    public int attempts;

    public Player(string name, int attempts)
    {
        this.name = name;
        this.attempts = attempts;
    }
}