using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OopLab.DB.Entity;
using OopLab.Services;

namespace OopLab.Games
{
    // Клас, що моделює гру між двома гравцями, з визначенням переможця, рейтингом гравців та можливістю збереження статистики.

    public class Game
    {
        // Унікальний ідентифікатор гри.
        public int Id { get; set; }

        // Гравець 1.
        public GameAccount Player1 { get; set; }

        // Гравець 2.
        public GameAccount Player2 { get; set; }

        // Гравець-переможець.
        public GameAccount Winner { get; set; }

        // Рейтинг, на який грається.
        public int PlayRating { get; set; } = 0;

        // Сервіс для обробки гри.
        public GameService _service { get; set; }

        // Конструктор класу Game.
        public Game(GameAccount player1, GameAccount player2, GameService service)
        {
            Player1 = player1;
            Player2 = player2;
            _service = service;
        }

        // Метод отримання рейтингу гравця.
        public virtual int GetPlayRating(GameAccount player) => PlayRating;

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

            Play();
        }

        // Метод, що виконує гру між гравцями.
        public virtual void Play()
        {
            Console.WriteLine("\n--------------------------------------------------------\n");
            Console.Write("Введіть рейтинг, на який граєте: ");
            PlayRating = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            // Перевірка коректності введеного рейтингу.
            if (PlayRating < 0)
            {
                Console.WriteLine("Некоректне значення. Введіть додатнє число.");
                Play();
                return;
            }

            if (PlayRating > Player1.CurrentRating - 1 || PlayRating > Player2.CurrentRating - 1)
            {
                Console.WriteLine("У одного з гравців недостатньо рейтингу.");
                Play();
                return;
            }

            // Симуляція кидання кубиків і визначення переможця.
            Random random = new Random();
            int player1Roll = random.Next(1, 7);
            int player2Roll = random.Next(1, 7);

            Console.WriteLine($"{Player1.UserName} кинув кубик і випало {player1Roll}");
            Console.WriteLine($"{Player2.UserName} кинув кубик і випало {player2Roll}");

            if (player1Roll > player2Roll)
            {
                Player1.Win(Player2.UserName, this);
                Player2.Lose(Player1.UserName, this);
                Winner = Player1;
                _service.Create(this);

                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else if (player1Roll < player2Roll)
            {
                Player2.Win(Player1.UserName, this);
                Player1.Lose(Player2.UserName, this);
                Winner = Player2;
                _service.Create(this);

                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else
            {
                Player1.draw(Player2.UserName);
                Player2.draw(Player1.UserName);
                Console.WriteLine("Нічия");
            }

            End();
        }

        // Метод завершення гри.
        public virtual void End()
        {
            Console.WriteLine("\n--------------------------------------------------------\n");
            Console.Write("Хочете зіграти ще одну гру? (Так/Ні): ");
            string playAgainResponse = Console.ReadLine().Trim();

            bool playAgain = playAgainResponse.Equals("Так", StringComparison.OrdinalIgnoreCase);

            if (playAgain)
                Play();
        }
    }
}