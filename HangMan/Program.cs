namespace HangMan;

class Program
{
    static void Main(string[] args)
    {
        HangMan game = new HangMan(new List<string>
        {
            "apple", "banana", "orange", "grape", "kiwi",
            "strawberry", "pineapple", "blueberry", "peach", "watermelon"
        },"@\"../../../../../players.xml");
        game.Play();
    }
}