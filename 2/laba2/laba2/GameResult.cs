using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
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

        public GameResult(string opponentName, bool won)
        {
            OpponentName = opponentName;
            Won = won;
        }
    }
}
