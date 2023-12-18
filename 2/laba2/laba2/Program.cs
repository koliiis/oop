using laba2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laba2
{
    // Main program class
    public class Program
    {
        // Main method
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Creation players and game objects
            GameAccount player1 = ChoseAccount();
            GameAccount player2 = ChoseAccount();
            Game game = TypeOfGame(player1, player2);

            game.StartGame();
            player1.GetStats();
            player2.GetStats();
        }

        private static GameAccount ChoseAccount()
        {
            Console.WriteLine("\nВиберіть метод гри: ");
            Console.WriteLine("1. Стандартний акаунт.");
            Console.WriteLine("2. Легкий програш.");
            Console.WriteLine("3. Додаткові бали за низку перемог.\n");
            int temp = Convert.ToInt32(Console.ReadLine());
            if (temp == 1)
                return new GameAccount();
            if (temp == 2)
                return new EasyLoss();
            if (temp == 3)
                return new SeriesOfVictory();
            else
                Console.WriteLine("\nТакого методу нема :(");
            return ChoseAccount();
        }

        private static Game TypeOfGame(GameAccount player1, GameAccount player2)
        {
            Console.WriteLine("\nВиберіть вид гри: ");
            Console.WriteLine("1) звичайна гра;");
            Console.WriteLine("2) гра без рейтингу;");
            Console.WriteLine("3) гра, у якій один гравець грає на рейтинг;\n");
            int temp = Convert.ToInt32(Console.ReadLine());
            GameFactory gameFactory = new GameFactory();
            if (temp == 1)
                return gameFactory.CreateGame(player1, player2);
            if (temp == 2)
                return gameFactory.CreateGameWithoutRating(player1, player2);
            if (temp == 3)
                return gameFactory.CreateGameOnePlayerRating(player1, player2);
            else
                Console.WriteLine("\nВведене некоректне значення!");
            return TypeOfGame(player1, player2);
        }
    }
}