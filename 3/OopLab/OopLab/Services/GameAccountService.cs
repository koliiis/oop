using Lecture_23_10_2023_Alt.DB.Entity.GameAccounts;
using OopLab;
using OopLab.DB;
using OopLab.DB.Entity;
using OopLab.DB.Repositories;
using OopLab.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Services
{
    // Клас, який реалізує сервіс для обробки облікових записів гри
    public class GameAccountService : IGameAccountService
    {
        // Репозиторій для доступу до бази даних
        GameAccountRepository repository;

        // Конструктор, приймає контекст бази даних та ініціалізує репозиторій
        public GameAccountService(DbContext context)
        {
            repository = new GameAccountRepository(context);
        }

        // Метод для додавання результатів гри до облікового запису
        public void AddGameResult(GameResult gameResult, GameAccount entity)
        {
            repository.AddGameResult(MapGameResult(gameResult), Map(entity));
        }

        // Метод для створення нового облікового запису
        public void Create(GameAccount entity)
        {
            repository.Create(Map(entity));
        }

        // Метод для видалення облікового запису
        public void Delete(GameAccount entity)
        {
            repository.Delete(Map(entity));
        }

        // Метод для отримання всіх облікових записів
        public List<GameAccount> GetAll()
        {
            var list = repository.GetAll()
                .Select(x => x != null ? Map(x) : null)
                .ToList();
            return list;
        }

        // Метод для отримання облікового запису за його ідентифікатором
        public GameAccount GetById(int id)
        {
            return Map(repository.GetById(id));
        }

        // Метод для отримання історії результатів гри для певного облікового запису
        public List<GameResult> GetHistory(GameAccount entity)
        {
            return MapGameResults(repository.GetHistory(Map(entity)));
        }

        // Метод для оновлення облікового запису
        public void Update(GameAccount entity)
        {
            repository.Update(Map(entity));
        }

        // Приватний метод для відображення об'єкту GameAccountEntity на GameAccount
        private GameAccount Map(GameAccountEntity gameAccount)
        {
            return new GameAccount(this, gameAccount.Id)
            {
                _service = this,
                Id = gameAccount.Id,
                UserName = gameAccount.UserName,
                CurrentRating = gameAccount.CurrentRating,
                GamesCount = gameAccount.GamesCount,
                GameHistory = MapGameResults(gameAccount.GameHistory)
            };
        }

        // Інші приватні методи для відображення різних типів об'єктів
        private List<GameResult> MapGameResults(List<GameResultEntity> gameResultEntities)
        {
            List<GameResult> gameResults = new List<GameResult>();
            if (gameResultEntities == null)
            {
                return new List<GameResult>();
            }
            foreach (var gameResultEntity in gameResultEntities)
            {
                gameResults.Add(new GameResult
                {
                    OpponentName = gameResultEntity.OpponentName,
                    Won = gameResultEntity.Won,
                    RatingChange = gameResultEntity.RatingChange
                });
            }

            return gameResults;
        }
        private GameAccountEntity Map(GameAccount gameAccount)
        {
            if (gameAccount == null)
            {
                return null;
            }
            return new GameAccountEntity
            {
                Id = gameAccount.Id,
                UserName = gameAccount.UserName,
                CurrentRating = gameAccount.CurrentRating,
                GamesCount = gameAccount.GamesCount,
                GameHistory = MapGameResults(gameAccount.GameHistory)
            };
        }
        private GameResultEntity MapGameResult(GameResult gameResult)
        {
            if (gameResult == null)
            {
                return null;
            }

            return new GameResultEntity
            {
                OpponentName = gameResult.OpponentName,
                Won = gameResult.Won,
                RatingChange = gameResult.RatingChange
            };
        }
        private List<GameResultEntity> MapGameResults(List<GameResult> gameResults)
        {
            if (gameResults == null)
            {
                return null;
            }

            List<GameResultEntity> gameResultEntities = new List<GameResultEntity>();

            foreach (var gameResult in gameResults)
            {
                gameResultEntities.Add(new GameResultEntity
                {
                    OpponentName = gameResult.OpponentName,
                    Won = gameResult.Won,
                    RatingChange = gameResult.RatingChange
                });
            }

            return gameResultEntities;
        }

        private AccountHalfRating Map(AccountHalfRatingEntity gameAccount)
        {
            if (gameAccount == null)
            {
                return null;
            }
            return new AccountHalfRating(this, gameAccount.Id)
            {
                _service = this,
                Id = gameAccount.Id,
                UserName = gameAccount.UserName,
                CurrentRating = gameAccount.CurrentRating,
                GamesCount = gameAccount.GamesCount,
                GameHistory = MapGameResults(gameAccount.GameHistory)
            };
        }

        private AccountHalfRatingEntity Map(AccountHalfRating gameAccount)
        {
            if (gameAccount == null)
            {
                return null;
            }
            return new AccountHalfRatingEntity
            {
                Id = gameAccount.Id,
                UserName = gameAccount.UserName,
                CurrentRating = gameAccount.CurrentRating,
                GamesCount = gameAccount.GamesCount,
                GameHistory = MapGameResults(gameAccount.GameHistory)
            };
        }
        private AccountWithStreek Map(AccountWithStreekEntity gameAccount)
        {
            if (gameAccount == null)
            {
                return null;
            }
            return new AccountWithStreek(this, gameAccount.Id)
            {
                _service = this,
                Id = gameAccount.Id,
                UserName = gameAccount.UserName,
                CurrentRating = gameAccount.CurrentRating,
                GamesCount = gameAccount.GamesCount,
                GameHistory = MapGameResults(gameAccount.GameHistory)
            };
        }

        private AccountWithStreekEntity Map(AccountWithStreek gameAccount)
        {
            if (gameAccount == null)
            {
                return null;
            }
            return new AccountWithStreekEntity
            {
                Id = gameAccount.Id,
                UserName = gameAccount.UserName,
                CurrentRating = gameAccount.CurrentRating,
                GamesCount = gameAccount.GamesCount,
                GameHistory = MapGameResults(gameAccount.GameHistory)
            };
        }
    }
}
