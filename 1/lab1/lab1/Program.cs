using System;
using System.Collections.Generic;
using System.Text;

namespace Laba1
{
    // Class for result
    public class GameResult
    {
        public string OpponentName { get; }
        public bool Won { get; }
        public int RatingChange { get; }

        // Class constructor
        public GameResult(string opponentName, bool won, int ratingChange)
        {
            OpponentName = opponentName;
            Won = won;
            RatingChange = ratingChange;
        }
    }

    // Class for accounting player games
    public class GameAccount
    {
        public string UserName { get; set; }
        public int CurrentRating { get; set; }
        private List<GameResult> gameHistory;
        public int GamesCount { get; set; }

        // Class constructor
        public GameAccount(int gamesCount = 0)
        {
            GamesCount = gamesCount;
            gameHistory = new List<GameResult>();
        }

        // Method when player win
        public void WinGame(string opponentName, int rating)
        {
            GamesCount++;
            CurrentRating += rating;
            gameHistory.Add(new GameResult(opponentName, true, rating));
        }

        // Method when player lose
        public void LoseGame(string opponentName, int rating)
        {
            GamesCount++;
            CurrentRating -= rating;
            gameHistory.Add(new GameResult(opponentName, false, rating));
        }

        // Method to get statistic for player
        public void GetStats()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\n#########################\n");
            Console.WriteLine($"ІСТОРІЯ ІГОР для {UserName}:");
            string matchResult;
            for (int i = 0; i < gameHistory.Count; i++)
            {
                var result = gameHistory[i];
                if (result.Won)
                    matchResult = "Перемога";
                else
                    matchResult = "Програш";
                Console.WriteLine($"Гра {i + 1}: \n" +
                                  $"Суперник: {result.OpponentName}\n" +
                                  $"Результат: {matchResult}\n" +
                                  $"Зміна рейтингу: {result.RatingChange}\n");
            }
            Console.WriteLine($"Поточний рейтинг для {UserName}: {CurrentRating}\n" +
                              $"Кількість ігор: {GamesCount}\n");
        }
    }

    // Class to show game
    public class Game
    {
        public Guid GameId { get; }
        public GameAccount Player1 { get; set; }
        public GameAccount Player2 { get; set; }

        // Game constructor
        public Game(GameAccount player1, GameAccount player2)
        {
            GameId = Guid.NewGuid();
            Player1 = player1;
            Player2 = player2;
        }

        // Method to play game
        public void Play()
        {
            Console.WriteLine("\n###############################\n");
            Console.Write("Введіть рейтинг на який граєте: ");
            int rating;
            if (!int.TryParse(Console.ReadLine(), out rating) || rating < 0)
            {
                Console.WriteLine("Некоректне значення. Введіть додатнє число.");
                Play();
                return;
            }

            // Check if rating is enough
            if (rating > Player1.CurrentRating - 1 || rating > Player2.CurrentRating - 1)
            {
                Console.WriteLine("У одного з гравців недостатньо рейтингу.");
                Play();
                return;
            }

            // Creation object for generating random numbers
            Random random = new Random();
            int player1Roll = random.Next(1, 7);
            int player2Roll = random.Next(1, 7);
            Console.WriteLine($"{Player1.UserName} кинув кубик і випало {player1Roll}");
            Console.WriteLine($"{Player2.UserName} кинув кубик і випало {player2Roll}");

            // Who win and update stats
            if (player1Roll > player2Roll)
            {
                Player1.WinGame(Player2.UserName, rating);
                Player2.LoseGame(Player1.UserName, rating);
                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else if (player1Roll < player2Roll)
            {
                Player2.WinGame(Player1.UserName, rating);
                Player1.LoseGame(Player2.UserName, rating);
                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else
            {
                Console.WriteLine("Нічия");
            }

            // Another game?
            Console.WriteLine("\n###################################\n");
            Console.Write("Хочете зіграти ще одну гру? (Так/Ні): ");
            string playAgainResponse = Console.ReadLine().Trim();

            bool playAgain = true;
            if (playAgainResponse.ToLower() != "так")
            {
                playAgain = false;
            }
            if (playAgain) Play();

        }

        // Method to start game
        public void StartGame()
        {
            Console.WriteLine("Вітаємо у грі!\n");

            Console.Write("Введіть ім'я першого гравця: ");
            Player1.UserName = Console.ReadLine().Trim();

            Console.Write("Введіть ім'я другого гравця: ");
            Player2.UserName = Console.ReadLine().Trim();

            // Write start rating
            Console.Write("\nВведіть стартовий рейтинг: ");
            int startRating;
            while (!int.TryParse(Console.ReadLine(), out startRating) || startRating <= 0)
            {
                Console.WriteLine("Початковий рейтинг повинен бути більше 0");
                Console.Write("Введіть початковий рейтинг: ");
            }
            Player1.CurrentRating = startRating;
            Player2.CurrentRating = startRating;

            Play();
        }
    }

    // Main program class
    public class Program
    {
        // Main method
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Creation players and game objects
            GameAccount player1 = new GameAccount();
            GameAccount player2 = new GameAccount();
            Game game = new Game(player1, player2);

            game.StartGame();
            player1.GetStats();
            player2.GetStats();
        }
    }
}