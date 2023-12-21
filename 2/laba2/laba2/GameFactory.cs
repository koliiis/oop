using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    // Фабрика для створення об'єктів гри
    public class GameFactory
    {
        // Метод для створення звичайної гри
        public Game CreateGame(GameAccount player1, GameAccount player2)
        {
            return new Game(player1, player2);
        }

        // Метод для створення гри без рейтингу
        public Game CreateGameWithoutRating(GameAccount player1, GameAccount player2)
        {
            return new GameWithoutRating(player1, player2);
        }

        // Метод для створення гри з рейтингом для одного гравця
        public Game CreateGameOnePlayerRating(GameAccount player1, GameAccount player2)
        {
            return new GameOnePlayerRating(player1, player2);
        }
    }
}