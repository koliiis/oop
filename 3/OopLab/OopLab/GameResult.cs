using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab
{
    // Клас, що представляє результат однієї гри.
    public class GameResult
    {
        // Ім'я супротивника.
        public string OpponentName { get; set; }

        // Результат гри ("Won" - перемога, "Lost" - поразка, "Draw" - нічия).
        public string Won { get; set; }

        // Зміна рейтингу гравця після гри.
        public int RatingChange { get; set; }

        // Конструктор класу, що ініціалізує об'єкт з усіма параметрами.
        public GameResult(string opponentName, string won, int ratingChange)
        {
            OpponentName = opponentName;
            Won = won;
            RatingChange = ratingChange;
        }

        // Конструктор класу, що ініціалізує об'єкт без параметра "RatingChange".
        public GameResult(string opponentName, string won)
        {
            OpponentName = opponentName;
            Won = won;
        }

        // Порожній конструктор, який може бути використаний для створення об'єкта зі значеннями за замовчуванням.
        public GameResult()
        {

        }
    }
}