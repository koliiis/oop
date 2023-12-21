using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.DB.Entity
{
    // Клас, що представляє об'єкт результата гри в базі даних
    public class GameResultEntity
    {
        // Ім'я опонента
        public string OpponentName { get; set; }

        // Результат гри ("Won" - перемога, "Lost" - поразка, інші значення можливі залежно від контексту)
        public string Won { get; set; }

        // Зміна рейтингу гравця після гри
        public int RatingChange { get; set; }
    }
}