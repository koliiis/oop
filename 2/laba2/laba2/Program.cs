using laba2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laba2
{
    // Головний клас програми
    public class Program
    {
        // Головний метод
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Створення гравців і об'єктів гри
            GameAccount player1 = ChoseAccount();
            GameAccount player2 = ChoseAccount();
            Game game = TypeOfGame(player1, player2);

            // Початок гри
            game.StartGame();

            // Вивід статистики гравця 1
            player1.GetStats();

            // Вивід статистики гравця 2
            player2.GetStats();
        }

        // Метод для вибору типу акаунту
        private static GameAccount ChoseAccount()
        {
            Console.WriteLine("\nВиберіть тип акаунту: ");
            Console.WriteLine("1. Стандартний акаунт.");
            Console.WriteLine("2. Акаунт, у якого змінюється рейтинг на половину.");
            Console.WriteLine("3. Додаткові бали за низку перемог.\n");
            int temp = Convert.ToInt32(Console.ReadLine());
            switch (temp)
            {
                case 1:
                    return new GameAccount();
                case 2:
                    return new EasyLoss();
                case 3:
                    return new SeriesOfVictory();
                default:
                    Console.WriteLine("\nТакого методу нема :(");
                    break;
            }

            // Рекурсивний виклик для повторення вводу
            return ChoseAccount();
        }

        // Метод для вибору типу гри
        private static Game TypeOfGame(GameAccount player1, GameAccount player2)
        {
            Console.WriteLine("\nВиберіть вид гри: ");
            Console.WriteLine("1) Звичайна гра;");
            Console.WriteLine("2) Гра без рейтингу;");
            Console.WriteLine("3) Гра, у якій один гравець грає на рейтинг;\n");
            int temp = Convert.ToInt32(Console.ReadLine());
            GameFactory gameFactory = new GameFactory();
            switch (temp)
            {
                case 1:
                    return gameFactory.CreateGame(player1, player2);
                case 2:
                    return gameFactory.CreateGameWithoutRating(player1, player2);
                case 3:
                    return gameFactory.CreateGameOnePlayerRating(player1, player2);
                default:
                    Console.WriteLine("\nВведене некоректне значення!");
                    break;
            }

            // Рекурсивний виклик для повторення вводу
            return TypeOfGame(player1, player2);
        }
    }
}