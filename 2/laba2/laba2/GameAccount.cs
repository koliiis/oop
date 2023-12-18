using Laba2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    public class GameAccount
    {
        public string UserName { get; set; }
        public int CurrentRating { get; set; }
        private List<GameResult> gameHistory;
        public int GamesCount { get; set; }
        public int VictorySeries { get; set; } = 0;

        // Class constructor
        public GameAccount(int gamesCount = 0)
        {
            GamesCount = gamesCount;
            gameHistory = new List<GameResult>();
        }

        // Method when player win
        public void WinGame(string opponentName, Game game)
        {
            int rating = Points(game.getPlayRating(this));
            GamesCount++;
            CurrentRating += rating;
            gameHistory.Add(new GameResult(opponentName, true, rating));
            VictorySeries++;
        }

        public void WinGame(string opponentName)
        {
            GamesCount++;
            VictorySeries++;
            gameHistory.Add(new GameResult(opponentName, true));
        }

        // Method when player lose
        public void LoseGame(string opponentName, Game game)
        {
            VictorySeries = 0;
            int rating = Points(game.getPlayRating(this));
            GamesCount++;
            CurrentRating -= rating;
            gameHistory.Add(new GameResult(opponentName, false, rating));
        }

        public void LoseGame(string opponentName)
        {
            GamesCount++;
            VictorySeries = 0;
            gameHistory.Add(new GameResult(opponentName, false));
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

        public virtual int Points(int rating)
        {
            return rating;
        }
    }

    public class EasyLoss : GameAccount
    {
        public override int Points(int rating)
        {
            return rating /= 2;
        }
    }
    public class SeriesOfVictory : GameAccount
    {
        public override int Points(int rating)
        {
            return rating *= (1 + VictorySeries);
        }
    }
}