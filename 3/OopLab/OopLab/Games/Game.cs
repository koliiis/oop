using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OopLab.DB.Entity;
using OopLab.Services.Base;

namespace OopLab.Games
{
    // Клас, що представляє гру між двома гравцями.
    public class Game
    {
        public int Id { get; set; }   
        public GameAccount Player1 { get; set; }
        public GameAccount Player2 { get; set; }
        public GameAccount Winner { get; set; }
        public int playRating { get; set; } = 0;
        public IGameService _service { get; set; }
        public int Indicator { get; set; }
        public Game(GameAccount Player1, GameAccount Player2, IGameService service, int indicator=0)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;
            _service = service;
            Indicator = indicator;
        }

        public virtual int getPlayRating(GameAccount player) { return playRating; }
        // Розпочати гру між гравцями.
        public virtual void StartGame()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Ласкаво просимо до гри!\n");

            // Введення імен гравців та початкового рейтингу.
            Console.Write("Введіть ім'я першого гравця: ");
            Player1.UserName = Console.ReadLine().Trim();

            Console.Write("Введіть ім'я другого гравця: ");
            Player2.UserName = Console.ReadLine().Trim();

            Console.Write("\nВведіть початковий рейтинг: ");
            int startRating = Convert.ToInt32(Console.ReadLine());
            while (startRating <= 0)
            {
                Console.WriteLine("Початковий рейтинг повинен бути більше 0");
                Console.Write("Введіть початковий рейтинг: ");
                startRating = Convert.ToInt32(Console.ReadLine());
            }
            Player1.CurrentRating = startRating;
            Player2.CurrentRating = startRating;

            Play();
        }

        // Метод, що виконує гру між гравцями.
        public virtual void Play()
        {

            Console.WriteLine("\n--------------------------------------------------------\n");
            Console.Write("Введіть рейтинг на який граєте: ");
            playRating = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            if (playRating < 0)
            {
                Console.WriteLine("Некоректне значення. Введіть додатнє число.");
                Play();
                return;
            }
            if (playRating > Player1.CurrentRating - 1 || playRating > Player2.CurrentRating - 1)
            {
                Console.WriteLine("У одного з гравців недостатньо рейтингу.");
                Play();
                return;
            }

            // Симуляція кидання кубиків і визначення переможця.
            Random random = new Random();
            int Player1Roll = random.Next(1, 7);
            int Player2Roll = random.Next(1, 7);
            Console.WriteLine($"{Player1.UserName} кинув кубик і випало {Player1Roll}");
            Console.WriteLine($"{Player2.UserName} кинув кубик і випало {Player2Roll}");
            if (Player1Roll > Player2Roll)
            {
                Player1.Win(Player2.UserName, this);
                Player2.Lose(Player1.UserName, this);
                Winner = Player1;
                _service.Create(this);
                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            if (Player1Roll < Player2Roll)
            {
                Player2.Win(Player1.UserName, this);
                Player1.Lose(Player2.UserName, this);
                Winner = Player2;
                _service.Create(this);
                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            if (Player1Roll == Player2Roll)
            {
                Player1.draw(Player2.UserName);
                Player2.draw(Player1.UserName);
                Console.WriteLine("Нічия");
            }
            End();
           
        }
        public virtual void End() 
        {
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

