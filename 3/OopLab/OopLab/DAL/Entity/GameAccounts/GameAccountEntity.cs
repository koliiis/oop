using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.DB.Entity
{
    // Клас, що представляє об'єкт акаунту в базі даних
    public class GameAccountEntity
    {
        // Ідентифікатор акаунту
        public int Id { get; set; }

        // Ім'я гравця
        public string UserName { get; set; }

        // Поточний рейтинг гравця
        public int CurrentRating { get; set; }

        // Кількість ігор гравця 
        public int GamesCount { get; set; } = 0;

        // Список результатів ігор для даного гравця
        public List<GameResultEntity> GameHistory { get; set; }
    }
}