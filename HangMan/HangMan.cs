using System.Text;

namespace HangMan;

public class HangMan
{
    private List<string> words = new List<string>
    {
        "apple", "banana", "orange", "grape", "kiwi",
        "strawberry", "pineapple", "blueberry", "peach", "watermelon"
    };

    private int _initialAttempts = 6;
    private string _secretWord;
    private StringBuilder _currentWordTemplate;
    private Player _currentPlayer;
    private string path;

    public HangMan(List<string> words,string path)
    {
        this.words = words;
        Initialize();
        this.path = path;
    }

    public void Play()
    {
        _currentPlayer = new Player(ReadName("Enter your name:"),0);
        bool hasWon = false;
        while (_currentPlayer.attempts < _initialAttempts)
        {
            _currentPlayer.attempts++;
            Console.WriteLine($"Current word is {_currentWordTemplate}\n");
            Choice choice = ReadChoice("Enter 0 for guessing letter\nEnter 1 for guessing word");

            if (choice == Choice.GuessLetter)
            {
                hasWon = GuessLetter();
                if(!hasWon) continue;
            }
            else if (choice == Choice.GuessWord)
            {
                ReadWord(out hasWon);
            }
            if (hasWon)
            {
                GameWon();
                break;
            }
            else
            {
                GameLost();
                break;
            }
        }

        if (!hasWon && _currentPlayer.attempts >= 6)
        {
            GameLost();
        }
        GameOver();
    }

    private void GameOver()
    {
        Leaderboard.PrintTop10(path);
    }

    private void GameWon()
    {
        Console.WriteLine($"{_currentPlayer.name} has won the game with {_currentPlayer.attempts} attempts! Word was {_secretWord}");
        XmlSaver.SavePlayer(path,_currentPlayer);
    }

    private void GameLost()
    {
        Console.WriteLine($"{_currentPlayer.name} has lost the game!.Word was {_secretWord}");
    }

    private void ReadWord(out bool hasWon)
    {
        hasWon = false;
        
        string word = ReadName("Enter your guess word");
        if (word.Equals(_secretWord,StringComparison.CurrentCultureIgnoreCase))
        {
            hasWon = true;
        }
    }

    private bool GuessLetter()
    {
        char chosenLetter = ReadLetter("Enter your guess letter");
        IEnumerable<int> indexes = _secretWord.Select((c, i) => new { Char = c, Index = i }).Where((c) => c.Char == chosenLetter).Select((x) => x.Index);
        if (indexes.Any())
        {
            foreach (int index in indexes)
            {
                _currentWordTemplate[index] = chosenLetter;
            }
        }

        if (_currentWordTemplate.ToString().Any((x) => x == 'X'))
        {
            return false;
        }


        return true;
    }
    
    public char ReadLetter(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            bool parsed = char.TryParse(Console.ReadLine(), out char value);
            if (parsed && char.IsLetter(value))
            {
                return value;
            }
            Console.WriteLine("letter was not in correct format.Try Again");
        }
    }
    
    private string ReadName(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name) && char.IsLetter(name[0]))
            {
                return name;
            }
            Console.WriteLine("Name not in correct format try again.");
        }
    }
    
    private Choice ReadChoice(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string diff = Console.ReadLine();
            if (diff.Equals("0",StringComparison.CurrentCultureIgnoreCase)) 
            {
                return Choice.GuessLetter;
            }
            if (diff.Equals("1",StringComparison.CurrentCultureIgnoreCase))
            {
                return Choice.GuessWord;
            }
            Console.WriteLine("choice was not in correct format try again.");
        }
    }

    private void Initialize()
    {
        _secretWord = GenerateSecretWord();
        _currentWordTemplate = new StringBuilder(new string('X', _secretWord.Length));
    }

    private string GenerateSecretWord()
    {
        Random rand = new Random();
        int size = words.Count;
        int randIndex = rand.Next(0, size);

        return words[randIndex];
    }
}