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
    // Клас GameOnePlayerRating успадковує функціональність базового класу Game та реалізує гру з рейтингом для одного гравця.
    public class GameOnePlayerRating : Game
    {
        // Поля для зберігання рейтингів гравців, які беруть участь в грі
        int playRating1 { get; set; }
        int playRating2 { get; set; }

        // Конструктор класу, який викликає конструктор базового класу та ініціалізує гравців та сервіс гри.
        public GameOnePlayerRating(GameAccount Player1, GameAccount Player2, GameService service) : base(Player1, Player2, service)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;
            _service = service;
        }

        // Метод для отримання рейтингу гравця в грі.
        public override int getPlayRating(GameAccount player)
        {
            if (player.UserName == Player1.UserName) { return playRating1; }
            if (player.UserName == Player2.UserName) { return playRating2; }
            return 0;
        }

        // Метод для початку гри з рейтингом для одного гравця.
        override public void StartGame()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Ласкаво просимо до гри!\n");

            // Введення імен гравців та початкового рейтингу.
            Console.Write("Введіть ім'я першого гравця: ");
            Player1.UserName = Console.ReadLine().Trim();

            Console.Write("Введіть ім'я другого гравця: ");
            Player2.UserName = Console.ReadLine().Trim();

            Play();
        }

        // Метод для проведення гри з рейтингом для одного гравця.
        override public void Play()
        {
            Console.WriteLine("\n--------------------------------------------------------\n");
            Console.Write("Введіть рейтинг на який граєте: ");
            playRating = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            // Перевірка коректності введеного рейтингу.
            if (playRating < 0)
            {
                Console.WriteLine("Некоректне значення. Введіть додатнє число.");
                Play();
                return;
            }

            // Перевірка чи у гравців достатньо рейтингу для гри.
            if (playRating > Player1.CurrentRating - 1 && playRating > Player2.CurrentRating - 1)
            {
                Console.WriteLine("У одного з гравців недостатньо рейтингу.");
                Play();
                return;
            }

            // Вибір гравця, чий рейтинг не зміниться після гри.
            ChosePlayer();

            // Симуляція кидання кубиків і визначення переможця.
            Random random = new Random();
            int Player1Roll = random.Next(1, 7);
            int Player2Roll = random.Next(1, 7);
            Console.WriteLine($"{Player1.UserName} кинув кубик і випало {Player1Roll}");
            Console.WriteLine($"{Player2.UserName} кинув кубик і випало {Player2Roll}");

            // Логіка визначення переможця та виведення результатів гри.
            if (Player1Roll > Player2Roll)
            {
                Player1.Win(Player2.UserName, this);
                Player2.Lose(Player1.UserName, this);
                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else if (Player1Roll < Player2Roll)
            {
                Player2.Win(Player1.UserName, this);
                Player1.Lose(Player2.UserName, this);
                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else
            {
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

        // Метод для вибору гравця, чий рейтинг не зміниться після гри.
        public void ChosePlayer()
        {
            Console.Write("Виберіть, у якого гравця не зміниться рейтинг (1 або 2): ");
            int temp = Convert.ToInt32(Console.ReadLine());

            // Логіка вибору гравця та встановлення рейтингу.
            if (temp == 1)
            {
                playRating1 = 0; playRating2 = playRating;
                return;
            }
            if (temp == 2)
            {
                playRating2 = 0; playRating1 = playRating;
                return;
            }
            else
            {
                Console.WriteLine("Введено некоректне значення!");
                ChosePlayer();
            }
        }
    }
}