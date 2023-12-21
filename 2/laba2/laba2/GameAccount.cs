using Laba2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    // Клас для представлення гравця в грі
    public class GameAccount
    {
        // Властивість - ім'я гравця
        public string UserName { get; set; }

        // Властивість - поточний рейтинг гравця
        public int CurrentRating { get; set; }

        // Приватне поле - історія ігор гравця
        private List<GameResult> gameHistory;

        // Властивість - кількість зіграних ігор
        public int GamesCount { get; set; }

        // Властивість - серія перемог гравця
        public int VictorySeries { get; set; } = 0;

        // Конструктор класу
        public GameAccount(int gamesCount = 0)
        {
            GamesCount = gamesCount;
            gameHistory = new List<GameResult>();
        }

        // Метод, що викликається при перемозі гравця
        public void WinGame(string opponentName, Game game)
        {
            int rating = Points(game.getPlayRating(this));
            GamesCount++;
            CurrentRating += rating;
            gameHistory.Add(new GameResult(opponentName, true, rating));
            VictorySeries++;
        }

        // Перевантажений метод для випадку перемоги без рейтингу
        public void WinGame(string opponentName)
        {
            GamesCount++;
            VictorySeries++;
            gameHistory.Add(new GameResult(opponentName, true));
        }

        // Метод, що викликається при програші гравця
        public void LoseGame(string opponentName, Game game)
        {
            VictorySeries = 0;
            int rating = Points(game.getPlayRating(this));
            GamesCount++;
            CurrentRating -= rating;
            gameHistory.Add(new GameResult(opponentName, false, rating));
        }

        // Перевантажений метод для випадку програшу без рейтингу
        public void LoseGame(string opponentName)
        {
            GamesCount++;
            VictorySeries = 0;
            gameHistory.Add(new GameResult(opponentName, false));
        }

        // Метод для отримання статистики гравця
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

        // Віртуальний метод для обчислення балів гравця
        public virtual int Points(int rating)
        {
            return rating;
        }
    }

    // Клас для представлення гравця з половинною втратою рейтингу при програші
    public class EasyLoss : GameAccount
    {
        // Перевизначений метод для обчислення балів гравця з половинною втратою рейтингу
        public override int Points(int rating)
        {
            return rating /= 2;
        }
    }

    // Клас для представлення гравця з більшими балами за низку перемог
    public class SeriesOfVictory : GameAccount
    {
        // Перевизначений метод для обчислення балів гравця з урахуванням серії перемог
        public override int Points(int rating)
        {
            return rating *= (1 + VictorySeries);
        }
    }
}