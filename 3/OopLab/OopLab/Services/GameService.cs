using Aloop;
using OopLab.DB;
using OopLab.DB.Entity.Games;
using OopLab.DB.Repositories;
using OopLab.DB.Repositories.Base;
using OopLab.Games;
using OopLab.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Services
{
    // Сервіс для взаємодії з іграми в базі даних.
    public class GameService : IGameService
    {
        // Репозиторій для роботи з іграми.
        GameRepository repository;

        // Сервіс для взаємодії з обліковими записами гравців.
        GameAccountService _serviceAcc;

        // Конструктор класу, який ініціалізує репозиторій та сервіс облікових записів гравців.
        public GameService(DbContext context)
        {
            repository = new GameRepository(context);
            _serviceAcc = new GameAccountService(context);
        }

        // Метод для створення запису гри в базі даних.
        public void Create(Game entity)
        {
            repository.Create(Map(entity));
        }

        // Метод для видалення запису гри з бази даних.
        public void Delete(Game entity)
        {
            repository.Delete(Map(entity));
        }

        // Метод для отримання списку всіх ігор з бази даних.
        public List<Game> GetAll()
        {
            // Отримання всіх записів та їх мапінг на об'єкти гри.
            var list = repository.GetAll().Select(x => Map(x)).ToList();
            return list;
        }

        // Метод для отримання ігри за її ідентифікатором з бази даних.
        public Game GetById(int id)
        {
            return Map(repository.GetById(id));
        }

        // Метод для оновлення запису гри в базі даних.
        public void Update(Game entity)
        {
            repository.Update(Map(entity));
        }

        // Метод для мапінгу об'єкта гри на об'єкт, який може бути збережений в базі даних.
        private GameEntity Map(Game game)
        {
            return new GameEntity
            {
                Id = game.Id,
                Player1 = game.Player1,
                Player2 = game.Player2,
                PlayRating = game.playRating,
            };
        }

        // Метод для мапінгу об'єкта гри в базі даних на об'єкт гри.
        private Game Map(GameEntity game)
        {
            return new Game(game.Player1, game.Player2, this)
            {
                Id = game.Id,
                Player1 = game.Player1,
                Player2 = game.Player2,
                playRating = game.PlayRating,
            };
        }

        // Методи для мапінгу об'єктів інших типів ігор (з рейтингом, без рейтингу) на відповідні об'єкти бази даних і навпаки.
        private GameOnePlayerRatingEntity Map(GameOnePlayerRating game)
        {
            return new GameOnePlayerRatingEntity
            {
                Id = game.Id,
                Player1 = game.Player1,
                Player2 = game.Player2,
                PlayRating = game.playRating,
            };
        }

        private GameOnePlayerRating Map(GameOnePlayerRatingEntity game)
        {
            return new GameOnePlayerRating(game.Player1, game.Player2, this)
            {
                Id = game.Id,
                Player1 = game.Player1,
                Player2 = game.Player2,
                playRating = game.PlayRating,
            };
        }

        private GameWithoutRatingEntity Map(GameWithoutRating game)
        {
            return new GameWithoutRatingEntity
            {
                Id = game.Id,
                Player1 = game.Player1,
                Player2 = game.Player2,
                PlayRating = game.playRating,
            };
        }

        private GameWithoutRating Map(GameWithoutRatingEntity game)
        {
            return new GameWithoutRating(game.Player1, game.Player2, this)
            {
                Id = game.Id,
                Player1 = game.Player1,
                Player2 = game.Player2,
                playRating = game.PlayRating,
            };
        }
    }
}