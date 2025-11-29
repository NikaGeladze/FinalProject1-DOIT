namespace NumberGuessingGame;

class Program
{
    static void Main(string[] args)
    {
        GuessingGame game = new(@"../../../players.csv");
        game.Play();
    }
}
