namespace NumberGuessingGame;

public class GuessingGame
{
    private GameDifficulty _difficulty;
    private int _secretNumber;
    private string _currentPlayerName;
    private int _attempts = 10;
    private string path;

    public GuessingGame(string path)
    {
        Initialize();
        this.path = path;
    }

    public void Play()
    {
        int currentAttempts = 0;
        bool hasWon = false;
        while (currentAttempts < _attempts)
        {
            currentAttempts++;
            int number = ReadNumber("Enter your guess");
            if (number == _secretNumber)
            {
                WinGame(currentAttempts);
                hasWon = true;
                break;
            }
            else if (number > _secretNumber)
            {
                Console.WriteLine("Number is greater than secret number");
            }
            else
            {
                Console.WriteLine("Number is lower than secret number");
            }
        }
        
        if (!hasWon)
        {
            GameLost(_currentPlayerName, _secretNumber);
        }
        
        GameOver();
    }


    private void GameOver()
    {
        LeaderBoard.PrintTopTen(path);
    }
    private void GameLost(string name,int number)
    {
        Console.WriteLine($"Player {_currentPlayerName} has lost the game.secret number was {_secretNumber}");
    }

    private void WinGame(int attempts)
    {
        Console.WriteLine($"Player \"{_currentPlayerName}\" Won the game with {attempts} attempts, secret number was {_secretNumber}");
        CsvSaver.SaveHighestScore(path,new Player(_currentPlayerName,attempts));
    }

    private int ReadNumber(string messege)
    {
        while (true)
        {
            Console.WriteLine(messege);
            bool parsed = int.TryParse(Console.ReadLine(), out int number);
            if (parsed)
            {
                return number;
            }
            Console.WriteLine("Number was invalid.Try again");
        }
    }

    private void Initialize()
    {
        _currentPlayerName = ReadName("Enter your name: ");
        _difficulty = ReadDifficulty("Enter difficulty(Easy,Medium,Hard)");
        _secretNumber = GenerateNumber();
        
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
    
    private GameDifficulty ReadDifficulty(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string diff = Console.ReadLine();
            if (diff.Equals("Easy",StringComparison.CurrentCultureIgnoreCase))
            {
                return GameDifficulty.Easy;
            }
            if (diff.Equals("Medium",StringComparison.CurrentCultureIgnoreCase))
            {
                return GameDifficulty.Medium;
            }
            if (diff.Equals("Hard",StringComparison.CurrentCultureIgnoreCase))
            {
                return GameDifficulty.Hard;
            }
            Console.WriteLine("difficulty was not in correct format try again.");
        }
    }

    private int GenerateNumber()
    {
        Random rand = new Random();
        switch (_difficulty)
        {
            case GameDifficulty.Easy:
                return rand.Next(1, 15);
            case GameDifficulty.Medium:
                return rand.Next(1, 30);
            case GameDifficulty.Hard:
                return rand.Next(1, 50);
        }

        return 1;
    }
}