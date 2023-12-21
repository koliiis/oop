using Aloop;
using Lecture_23_10_2023_Alt.DB.Entity.GameAccounts;
using OopLab.DB;
using OopLab.DB.Entity;
using OopLab.Games;
using OopLab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopLab
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Ініціалізація об'єкту DbContext та служб гравців і гри.
            var context = new DbContext();
            var accountService = new GameAccountService(context);
            var gameService = new GameService(context);

            // Встановлення кодування консолі на UTF-8 для підтримки спеціальних символів.
            Console.OutputEncoding = Encoding.UTF8;

            // Запуск гри.
            Start(accountService, gameService);
        }

        public static void Start(GameAccountService accountService, GameService gameService)
        {
            // Вибір акаунта для гравців.
            GameAccount player1 = ChoseAccount(accountService);
            GameAccount player2 = ChoseAccount(accountService);

            // Вибір типу гри.
            Game game = TypeOfGame(player1, player2, gameService);

            // Розпочаток гри.
            game.StartGame();

            // Виведення статистики гравців після завершення гри.
            Show(accountService);
        }

        public static void Show(GameAccountService accountService)
        {
            // Отримання та виведення статистики всіх акаунтів.
            var listAccounts = accountService.GetAll();
            foreach (var account in listAccounts)
            {
                if (account != null)
                    account.GetStats();
            }
        }

        private static GameAccount ChoseAccount(GameAccountService service)
        {
            // Вибір типу акаунта для гравця.
            Console.WriteLine("\nВиберіть акаунт гравця: ");
            Console.WriteLine("1) базовий акаунт;");
            Console.WriteLine("2) акаунт, у якого змінюється у два рази менше балів;");
            Console.WriteLine("3) акаунт, в якому нараховується додаткові бали за серію перемог;\n");
            int temp = Convert.ToInt32(Console.ReadLine());
            var id = service.GetAll().Count();

            // Створення вибраного акаунта та його додавання до сервісу.
            if (temp == 1)
            {
                var gameAccount = new GameAccount(service, id);
                service.Create(gameAccount);
                return gameAccount;
            }
            if (temp == 2)
            {
                var gameAccount = new AccountHalfRating(service, id);
                service.Create(gameAccount);
                return gameAccount;
            }
            if (temp == 3)
            {
                var gameAccount = new AccountWithStreek(service, id);
                service.Create(gameAccount);
                return gameAccount;
            }
            else
                Console.WriteLine("\nВведене некоректне значення!");

            // Рекурсивний виклик методу для повторення введення при некоректних даних.
            return ChoseAccount(service);
        }

        private static Game TypeOfGame(GameAccount player1, GameAccount player2, GameService service)
        {
            // Вибір типу гри для гравців.
            Console.WriteLine("\nВиберіть вид гри: ");
            Console.WriteLine("1) звичайна гра;");
            Console.WriteLine("2) гра без рейтингу;");
            Console.WriteLine("3) гра, у якій один гравець грає на рейтинг;\n");
            int temp = Convert.ToInt32(Console.ReadLine());

            // Створення вибраного типу гри за допомогою фабрики гри.
            GameFactory gameFactory = new GameFactory();
            if (temp == 1)
                return gameFactory.CreateGame(player1, player2, service);
            if (temp == 2)
                return gameFactory.CreateGameWithoutRating(player1, player2, service);
            if (temp == 3)
                return gameFactory.CreateGameOnePlayerRating(player1, player2, service);
            else
                Console.WriteLine("\nВведене некоректне значення!");

            // Рекурсивний виклик методу для повторення введення при некоректних даних.
            return TypeOfGame(player1, player2, service);
        }
    }
}