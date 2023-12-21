using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab;
using OopLab.DB.Entity;
using OopLab.Games;
using OopLab.Services;

namespace Aloop
{
    public class GameWithoutRating : Game
    {
        public GameWithoutRating(GameAccount Player1, GameAccount Player2, GameService service) : base(Player1, Player2, service)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;
            _service = service;
        }
        override public void StartGame()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Ласкаво просимо до гри!\n");

            // Введення імен гравців та початкового рейтингу.
            Console.Write("Введіть ім'я першого гравця: ");
            Player1.UserName = Console.ReadLine().Trim();

            Console.Write("Введіть ім'я другого гравця: ");
            Player2.UserName = Console.ReadLine().Trim();

            
            // Початок гри.
            Play();
        }
        override public void Play()
        {
            Player1.CurrentRating = 0;
            Player2.CurrentRating = 0;
            // Симуляція кидання кубиків і визначення переможця.
            Random random = new Random();
            int Player1Roll = random.Next(1, 7);
            int Player2Roll = random.Next(1, 7);
            Console.WriteLine($"{Player1.UserName} кинув кубик і випало {Player1Roll}");
            Console.WriteLine($"{Player2.UserName} кинув кубик і випало {Player2Roll}");
            if (Player1Roll > Player2Roll)
            {
                Player1.Win(Player2.UserName);
                Player2.Lose(Player1.UserName);
                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStatsWithoutRating();
                Player2.GetStatsWithoutRating();
            }
            if (Player1Roll < Player2Roll)
            {
                Player2.Win(Player1.UserName);
                Player1.Lose(Player2.UserName);
                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStatsWithoutRating();
                Player2.GetStatsWithoutRating();
            }
            if (Player1Roll == Player2Roll)
            {
                Player1.draw(Player2.UserName);
                Player2.draw(Player1.UserName);
                Console.WriteLine("Нічия");
            }

            // Питання про гру ще раз.
            Console.WriteLine("\n--------------------------------------------------------\n");
            Console.Write("Хочете зіграти ще одну гру? (Так/Ні): ");
            string playAgainResponse = Console.ReadLine().Trim();

            bool playAgain = true;
            if (!playAgainResponse.Equals("Так", StringComparison.OrdinalIgnoreCase))
            {
                playAgain = false;
            }
            if (playAgain) Play();
        }
    }
}
