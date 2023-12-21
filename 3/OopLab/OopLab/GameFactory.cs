using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab.DB.Entity;
using OopLab.Games;
using OopLab.Services.Base;

namespace Aloop
{
    public class GameFactory
    {
        public Game CreateGame(GameAccount player1, GameAccount player2, IGameService service)
        {
            return new Game(player1, player2, service);
        }
        public Game CreateGameWithoutRating(GameAccount player1, GameAccount player2, IGameService service)
        {
            return new GameWithoutRating(player1, player2, service);
        }

        public Game CreateGameOnePlayerRating(GameAccount player1, GameAccount player2, IGameService service)
        {
            return new GameOnePlayerRating(player1, player2, service);
        }

    }
}
