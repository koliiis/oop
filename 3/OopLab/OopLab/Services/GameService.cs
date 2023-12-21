using Aloop;
using Lecture_23_10_2023_Alt.DB.Entity.GameAccounts;
using OopLab.DB;
using OopLab.DB.Entity;
using OopLab.DB.Entity.Games;
using OopLab.DB.Repositories;
using OopLab.DB.Repositories.Base;
using OopLab.Games;
using OopLab.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Services
{
    public class GameService : IGameService
    {
        GameRepository repository;
        GameAccountService _serviceAcc;
        public GameService(DbContext context)
        {
            repository = new GameRepository(context);
            _serviceAcc = new GameAccountService(context);
        }
        public void Create(Game entity)
        {
            repository.Create(Map(entity));
        }
        public void Delete(Game entity)
        {
            repository.Delete(Map(entity));
        }
        public List<Game> GetAll()
        {
            var list = repository.GetAll().Select(x => Map(x)).ToList();
            return list;
        }
        public Game GetById(int id)
        {
            var game = Map(repository.GetById(id));
            return game;
        }
        public void Update(Game entity)
        {
            repository.Update(Map(entity));
        }
        private GameEntity Map(Game game)
        {
            return new GameEntity
            {
                Id = game.Id,
                Player1 = game.Player1,
                Player2 = game.Player2,
                PlayRating = game.playRating,
                Indicator = game.Indicator,
            };

        }
        private Game Map(GameEntity game)
        {

            if (game.Indicator == 0)
            {
                return new Game(game.Player1, game.Player2, this)
                {
                    Id = game.Id,
                    Player1 = game.Player1,
                    Player2 = game.Player2,
                    playRating = game.PlayRating,
                    Indicator = game.Indicator,
                };
            }
            else if (game.Indicator == 1)
            {
                return new GameOnePlayerRating(game.Player1, game.Player2, this)
                {
                    Id = game.Id,
                    Player1 = game.Player1,
                    Player2 = game.Player2,
                    playRating = game.PlayRating,
                    Indicator = game.Indicator,
                };
            }
            else
            {
                return new GameWithoutRating(game.Player1, game.Player2, this)
                {
                    Id = game.Id,
                    Player1 = game.Player1,
                    Player2 = game.Player2,
                    playRating = game.PlayRating,
                    Indicator = game.Indicator,
                };
            }
        }

    }
}
