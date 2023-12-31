﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab;
using OopLab.DB.Entity;
using OopLab.Games;
using OopLab.Services.Base;

namespace Aloop
{

    public class GameOnePlayerRating : Game
    {
        int playRating1 { get; set; }
        int playRating2 { get; set; }
        public GameOnePlayerRating(GameAccount Player1, GameAccount Player2, IGameService service, int indicator = 1) : base(Player1, Player2, service, indicator)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;
            _service = service;
        }

        public override int getPlayRating(GameAccount player)
        {
            if (player.UserName == Player1.UserName) { return playRating1; }
            if (player.UserName == Player2.UserName) { return playRating2; }
            return 0;
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

            Play();
        }
        override public void Play()
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
            if (playRating > Player1.CurrentRating - 1 && playRating > Player2.CurrentRating - 1)
            {
                Console.WriteLine("У одного з гравців недостатньо рейтингу.");
                Play();
                return;
            }
            ChosePlayer();

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
                Console.WriteLine($"Переміг {Player1.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            if (Player1Roll < Player2Roll)
            {
                Player2.Win(Player1.UserName, this);
                Player1.Lose(Player2.UserName, this);
                Console.WriteLine($"Переміг {Player2.UserName}!");
                Player1.GetStats();
                Player2.GetStats();
            }
            if (Player1Roll == Player2Roll)
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
