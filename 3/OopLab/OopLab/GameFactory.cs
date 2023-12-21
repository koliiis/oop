using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab.DB.Entity;
using OopLab.Games;
using OopLab.Services;

namespace Aloop
{
    // Фабрика для створення різних типів ігор.
    public class GameFactory
    {
        // Метод для створення звичайної гри.
        public Game CreateGame(GameAccount player1, GameAccount player2, GameService service)
        {
            return new Game(player1, player2, service);
        }

        // Метод для створення гри без рейтингу.
        public Game CreateGameWithoutRating(GameAccount player1, GameAccount player2, GameService service)
        {
            return new GameWithoutRating(player1, player2, service);
        }

        // Метод для створення гри, в якій один гравець грає на рейтинг.
        public Game CreateGameOnePlayerRating(GameAccount player1, GameAccount player2, GameService service)
        {
            return new GameOnePlayerRating(player1, player2, service);
        }
    }
}