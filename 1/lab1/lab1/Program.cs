using System.Text;

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        PlayerAccount player1 = new PlayerAccount();
        PlayerAccount player2 = new PlayerAccount();
        GameMatch game = new GameMatch(player1, player2);

        game.StartMatch();

        player1.DisplayStats();
        player2.DisplayStats();
    }
}

public class PlayerAccount
{
    public string PlayerName { get; set; }
    public int CurrentRating { get; set; }
    private List<MatchResult> matchHistory;
    public int MatchesCount { get; set; }

    public PlayerAccount(int matchesCount = 0)
    {
        MatchesCount = matchesCount;
        matchHistory = new List<MatchResult>();
    }

    public void WinMatch(string opponentName, int rating)
    {
        MatchesCount++;
        CurrentRating += rating;
        matchHistory.Add(new MatchResult(opponentName, true, rating));
    }

    public void LoseMatch(string opponentName, int rating)
    {
        MatchesCount++;
        CurrentRating -= rating;
        matchHistory.Add(new MatchResult(opponentName, false, rating));
    }

    public void DisplayStats()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("\n.................................\n");
        Console.WriteLine($"ІСТОРІЯ ІГОР для {PlayerName}:");
        for (int i = 0; i < matchHistory.Count; i++)
        {
            var result = matchHistory[i];
            string matchResult = result.Won ? "Перемога" : "Поразка";
            Console.WriteLine($"Гра {i + 1}: \n" +
                              $"Опонент: {result.OpponentName}\n" +
                              $"Результат: {matchResult}\n" +
                              $"Зміна рейтингу: {result.RatingChange}\n");
        }
        Console.WriteLine($"Поточний рейтинг для {PlayerName}: {CurrentRating}\n" +
                          $"Кількість ігор: {MatchesCount}\n");
    }
}

public class MatchResult
{
    public string OpponentName { get; }
    public bool Won { get; }
    public int RatingChange { get; }

    public MatchResult(string opponentName, bool won, int ratingChange)
    {
        OpponentName = opponentName;
        Won = won;
        RatingChange = ratingChange;
    }
}

public class GameMatch
{
    public PlayerAccount Player1 { get; set; }
    public PlayerAccount Player2 { get; set; }

    public GameMatch(PlayerAccount player1, PlayerAccount player2)
    {
        Player1 = player1;
        Player2 = player2;
    }

    public void StartMatch()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Ласкаво просимо до гри!\n");

        Console.Write("Введіть ім'я першого гравця: ");
        Player1.PlayerName = Console.ReadLine().Trim();

        Console.Write("Введіть ім'я другого гравця: ");
        Player2.PlayerName = Console.ReadLine().Trim();

        Console.Write("\nВведіть початковий рейтинг: ");
        int startRating = Convert.ToInt32(Console.ReadLine());
        while (startRating <= 0)
        {
            Console.WriteLine("Початковий рейтинг повинен бути більше 0");
            Console.Write("Введіть початковий рейтинг: ");
            startRating = Convert.ToInt32(Console.ReadLine());
        }
        Player1.CurrentRating = startRating;
        Player2.CurrentRating = startRating;

        Play();
    }

    public void Play()
    {
        Console.WriteLine("\n--------------------------------------------------------\n");
        Console.Write("Введіть рейтинг на який граєте: ");
        int rating = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();

        if (rating < 0)
        {
            Console.WriteLine("Некоректне значення. Введіть додатнє число.");
            Play();
            return;
        }

        if (rating > Player1.CurrentRating - 1 || rating > Player2.CurrentRating - 1)
        {
            Console.WriteLine("У одного з гравців недостатньо рейтингу.");
            Play();
            return;
        }

        Random random = new Random();
        int player1Roll = random.Next(1, 7);
        int player2Roll = random.Next(1, 7);
        Console.WriteLine($"{Player1.PlayerName} кинув кубик і випало {player1Roll}");
        Console.WriteLine($"{Player2.PlayerName} кинув кубик і випало {player2Roll}");

        if (player1Roll > player2Roll)
        {
            Player1.WinMatch(Player2.PlayerName, rating);
            Player2.LoseMatch(Player1.PlayerName, rating);
            Console.WriteLine($"Переміг {Player1.PlayerName}!");
            Player1.DisplayStats();
            Player2.DisplayStats();
        }
        else if (player1Roll < player2Roll)
        {
            Player2.WinMatch(Player1.PlayerName, rating);
            Player1.LoseMatch(Player2.PlayerName, rating);
            Console.WriteLine($"Переміг {Player2.PlayerName}!");
            Player1.DisplayStats();
            Player2.DisplayStats();
        }
        else
        {
            Console.WriteLine("Нічия");
        }

        Console.WriteLine("\n--------------------------------------------------------\n");
        Console.Write("Хочете зіграти ще одну гру? (Так/Ні): ");
        string playAgainResponse = Console.ReadLine().Trim();

        bool playAgain = true;
        if (!playAgainResponse.Equals("Так", StringComparison.OrdinalIgnoreCase))
        {
            playAgain = false;
        }
        if (playAgain) Play();
    }
}