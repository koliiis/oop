using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab.Games;
using OopLab.Services.Base;

namespace OopLab.DB.Entity
{
    public class GameAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; } // Ім'я гравця
        public int CurrentRating { get; set; } = 100; // Поточний рейтинг гравця
        public List<GameResult> GameHistory; // Історія ігор гравця
        public int GamesCount { get; set; } // Кількість ігор гравця
        public int WinStreek { get; set; } = 0;
        public IGameAccountService _service { get; set; }
        public int Indicator { get; set; }
        // Конструктор класу GameAccount, де можливо вказати початкову кількість ігор (за замовчуванням - 0).
        public GameAccount(IGameAccountService service, int ID, int gamesCount = 0, int indicator = 0)
        {
            _service = service;
            GamesCount = gamesCount;
            GameHistory = _service.GetHistory(this);
            Id = ID;
            Indicator = indicator;
        }
        public void Win(string opponentName, Game game)
        {
            int rating = PointsCalculate(game.getPlayRating(this));
            GamesCount++;
            CurrentRating += rating;
            var result = new GameResult(opponentName, "Перемога", rating);
            GameHistory.Add(result);
            WinStreek++;
            _service.Update(this);
        }
        public void Win(string opponentName)
        {

            GamesCount++;
            var result = new GameResult(opponentName, "Перемога");
            GameHistory.Add(result);
            WinStreek++;
            _service.Update(this);
        }

        public void Lose(string opponentName, Game game)
        {
            int rating = PointsCalculate(game.getPlayRating(this));
            GamesCount++;
            CurrentRating -= rating;
            var result = new GameResult(opponentName, "Поразка", rating);
            GameHistory.Add(result);
            WinStreek = 0;
            _service.Update(this);
        }
        public void Lose(string opponentName)
        {

            GamesCount++;
            var result = new GameResult(opponentName, "Поразка");
            GameHistory.Add(result);
            WinStreek = 0;
            _service.Update(this);
        }
        public void draw(string opponentName)
        {
            GamesCount++;
            var result = new GameResult(opponentName, "Нічия");
            GameHistory.Add(result);
            _service.Update(this);
        }
        // Виведення статистики гравця на консоль.
        public void GetStats()
        {
            var stats = "";
            Console.OutputEncoding = Encoding.UTF8;
            stats += "\n.................................\n";
            if (GameHistory == null)
            {
                Console.WriteLine($"Ім'я:{UserName}, Id: {Id}");
                
            }

            stats += $"Ім'я:{UserName}, Id: {Id}\n";
            for (int i = 0; i < GameHistory.Count; i++)
            {

                var result = _service.GetHistory(this)[i];
                String matchResult;
                if (result.Won == null)
                {
                    stats += $"Гра {i + 1}: \n" +
                  $"Опонент: {result.OpponentName}\n" +
                  $"Результат: Нічия\n" +
                  $"Зміна рейтингу: {result.RatingChange}\n";
                }
                stats += $"Гра {i + 1}: \n" +
                                  $"Опонент: {result.OpponentName}\n" +
                                  $"Результат: {(result.Won)}\n" +
                                  $"Зміна рейтингу: {result.RatingChange}\n";
            }
            stats += $"Поточний рейтинг для {UserName}: {CurrentRating}\n" +
                              $"Кількість ігор: {GamesCount}\n";
             Console.WriteLine(stats);
        }

        public virtual int PointsCalculate(int rating)
        {
            return rating;
        }
    }
}
