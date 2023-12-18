using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    public class GameOnePlayerRating : Game
    {
        int playRating1 { get; set; }
        int playRating2 { get; set; }
        public GameOnePlayerRating(GameAccount player1, GameAccount player2) : base(player1, player2)
        {
        }

        public override int getPlayRating(GameAccount player)
        {
            if (player.UserName == Player1.UserName) { return playRating1; }
            if (player.UserName == Player2.UserName) { return playRating2; }
            return 0;
        }

        // Method to start game
        override public void StartGame()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Вітаємо у грі!\n");

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
        // Method to play game
        override public void Play()
        {
            Console.WriteLine("\n###############################\n");
            Console.Write("Введіть рейтинг на який граєте: ");
            playRating = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            if (playRating < 0)
            {
                Console.WriteLine("Некоректне значення. Введіть додатнє число.");
                Play();
                return;
            }
            if (playRating > Player1.CurrentRating - 1 && playRating > Player2.CurrentRating - 1)
            {
                Console.WriteLine("У одного з гравців недостатньо рейтингу.");
                Play();
                return;
            }
            ChosePlayer();


            // Симуляція кидання кубиків і визначення переможця.
            Random random = new Random();
            int player1Roll = random.Next(1, 7);
            int player2Roll = random.Next(1, 7);
            Console.WriteLine($"{Player1.UserName} кинув кубик і випало {player1Roll}");
            Console.WriteLine($"{Player2.UserName} кинув кубик і випало {player2Roll}");
            if (player1Roll > player2Roll)
            {
                Player1.WinGame(Player2.UserName, this);
                Player2.LoseGame(Player1.UserName, this);
                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            if (player1Roll < player2Roll)
            {
                Player2.WinGame(Player1.UserName, this);
                Player1.LoseGame(Player1.UserName, this);
                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            if (player1Roll == player2Roll)
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
        public void ChosePlayer()
        {

            Console.Write("Виберіть, у якого гравця не зміниться рейтинг (1 або 2): ");
            int temp = Convert.ToInt32(Console.ReadLine());
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
