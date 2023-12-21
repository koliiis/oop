
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lecture_23_10_2023_Alt.DB.Entity.GameAccounts;

namespace OopLab.DB.Entity.Games
{
    // Клас, що представляє базовий об'єкт гри в базі даних
    public class GameEntity
    {
        // Ідентифікатор гри
        public int Id { get; set; }

        // Гравець 1
        public GameAccount Player1 { get; set; }

        // Гравець 2
        public GameAccount Player2 { get; set; }

        // Рейтинг гри 
        public int PlayRating { get; set; }
    }
}