using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    public class GameFactory
    {
        public Game CreateGame(GameAccount player1, GameAccount player2)
        {
            return new Game(player1, player2);
        }
        public Game CreateGameWithoutRating(GameAccount player1, GameAccount player2)
        {
            return new GameWithoutRating(player1, player2);
        }

        public Game CreateGameOnePlayerRating(GameAccount player1, GameAccount player2)
        {
            return new GameOnePlayerRating(player1, player2);
        }
    }
}
