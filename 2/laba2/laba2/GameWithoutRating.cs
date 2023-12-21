using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    // Клас гри без рейтингу, який наслідує клас Game
    public class GameWithoutRating : Game
    {
        // Конструктор класу, який викликає конструктор базового класу (Game)
        public GameWithoutRating(GameAccount player1, GameAccount player2) : base(player1, player2)
        {
        }

        // Метод для початку гри
        override public void StartGame()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Вітаємо у грі!\n");

            // Введення імені першого гравця
            Console.Write("Введіть ім'я першого гравця: ");
            Player1.UserName = Console.ReadLine().Trim();

            // Введення імені другого гравця
            Console.Write("Введіть ім'я другого гравця: ");
            Player2.UserName = Console.ReadLine().Trim();

            // Розпочаток гри
            Play();
        }

        // Метод для проведення гри
        override public void Play()
        {
            Console.WriteLine("\n###############################\n");

            // Створення об'єкта для генерації випадкових чисел
            Random random = new Random();
            int player1Roll = random.Next(1, 7);
            int player2Roll = random.Next(1, 7);

            // Виведення результатів кидка кубика для обох гравців
            Console.WriteLine($"{Player1.UserName} кинув кубик і випало {player1Roll}");
            Console.WriteLine($"{Player2.UserName} кинув кубик і випало {player2Roll}");

            // Визначення переможця та оновлення статистики
            if (player1Roll > player2Roll)
            {
                Player1.WinGame(Player2.UserName);
                Player2.LoseGame(Player1.UserName);
                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else if (player1Roll < player2Roll)
            {
                Player2.WinGame(Player1.UserName);
                Player1.LoseGame(Player2.UserName);
                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else
            {
                Console.WriteLine("Нічия");
            }

            // Питання про ще одну гру
            Console.WriteLine("\n###################################\n");
            Console.Write("Хочете зіграти ще одну гру? (Так/Ні): ");
            string playAgainResponse = Console.ReadLine().Trim();

            bool playAgain = true;
            if (playAgainResponse.ToLower() != "так")
            {
                playAgain = false;
            }
            if (playAgain) Play();
        }
    }
}