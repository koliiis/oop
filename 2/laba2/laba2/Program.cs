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
            Console.WriteLine("\nВиберіть тип акаунту: ");
            Console.WriteLine("1. Стандартний акаунт.");
            Console.WriteLine("2. аккаунт, у якого змніюється рейтинг/2.");
            Console.WriteLine("3. Додаткові бали за низку перемог.\n");
            int temp = Convert.ToInt32(Console.ReadLine());
            switch (temp)
            {
                case 1:
                    return new GameAccount();
                    break;
                case 2:
                    return new EasyLoss();
                    break;
                case 3:
                    return new SeriesOfVictory();
                    break;
                default:
                    Console.WriteLine("\nТакого методу нема :(");
                    break;
            }
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
            switch (temp)
            {
                case 1:
                    return gameFactory.CreateGame(player1, player2);
                    break;
                case 2:
                    return gameFactory.CreateGameWithoutRating(player1, player2);
                    break;
                case 3:
                    return gameFactory.CreateGameOnePlayerRating(player1, player2);
                    break;
                default:
                    Console.WriteLine("\nВведене некоректне значення!");
                    break;
            }
            return TypeOfGame(player1, player2);
        }
    }
}