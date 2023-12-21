using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    // Клас для зберігання результатів гри
    public class GameResult
    {
        // Властивість - ім'я опонента
        public string OpponentName { get; }

        // Властивість - чи виграв гравець гру
        public bool Won { get; }

        // Властивість - зміна рейтингу
        public int RatingChange { get; }

        // Конструктор класу
        public GameResult(string opponentName, bool won, int ratingChange)
        {
            OpponentName = opponentName;
            Won = won;
            RatingChange = ratingChange;
        }

        // Додатковий конструктор без зміни рейтингу
        public GameResult(string opponentName, bool won)
        {
            OpponentName = opponentName;
            Won = won;
        }
    }
}