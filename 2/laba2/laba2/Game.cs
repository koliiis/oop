using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    // Клас для представлення гри
    public class Game
    {
        // Властивість - перший гравець
        public GameAccount Player1 { get; set; }

        // Властивість - другий гравець
        public GameAccount Player2 { get; set; }

        // Властивість - рейтинг для гри
        public int playRating { get; set; } = 0;

        // Конструктор класу гри
        public Game(GameAccount player1, GameAccount player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
        }

        // Метод для проведення гри
        public virtual void Play()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\n###############################\n");
            Console.Write("Введіть рейтинг на який граєте: ");
            int rating;
            if (!int.TryParse(Console.ReadLine(), out rating) || rating < 0)
            {
                Console.WriteLine("Некоректне значення. Введіть додатнє число.");
                Play();
                return;
            }

            // Перевірка, чи є достатньо рейтингу
            if (rating > Player1.CurrentRating - 1 || rating > Player2.CurrentRating - 1)
            {
                Console.WriteLine("У одного з гравців недостатньо рейтингу.");
                Play();
                return;
            }
            playRating = rating;

            // Створення об'єкта для генерації випадкових чисел
            Random random = new Random();
            int player1Roll = random.Next(1, 7);
            int player2Roll = random.Next(1, 7);
            Console.WriteLine($"{Player1.UserName} кинув кубик і випало {player1Roll}");
            Console.WriteLine($"{Player2.UserName} кинув кубик і випало {player2Roll}");

            // Визначення переможця та оновлення статистики
            if (player1Roll > player2Roll)
            {
                Player1.WinGame(Player2.UserName, this);
                Player2.LoseGame(Player1.UserName, this);
                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else if (player1Roll < player2Roll)
            {
                Player2.WinGame(Player1.UserName, this);
                Player1.LoseGame(Player2.UserName, this);
                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            else
            {
                Console.WriteLine("Нічия");
            }

            // Ще одна гра?
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

        // Віртуальний метод для отримання рейтингу гравця
        public virtual int getPlayRating(GameAccount player) { return playRating; }

        // Метод для початку гри
        public virtual void StartGame()
        {
            Console.WriteLine("Вітаємо у грі!\n");

            // Введення імені першого гравця
            Console.Write("Введіть ім'я першого гравця: ");
            Player1.UserName = Console.ReadLine().Trim();

            // Введення імені другого гравця
            Console.Write("Введіть ім'я другого гравця: ");
            Player2.UserName = Console.ReadLine().Trim();

            // Введення початкового рейтингу
            Console.Write("\nВведіть стартовий рейтинг: ");
            int startRating;
            while (!int.TryParse(Console.ReadLine(), out startRating) || startRating <= 0)
            {
                Console.WriteLine("Початковий рейтинг повинен бути більше 0");
                Console.Write("Введіть початковий рейтинг: ");
            }
            Player1.CurrentRating = startRating;
            Player2.CurrentRating = startRating;

            // Початок гри
            Play();
        }
    }
}