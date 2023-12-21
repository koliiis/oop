﻿using Lecture_23_10_2023_Alt.DB.Entity.GameAccounts;
using OopLab.DB;
using OopLab.DB.Entity;
using OopLab.DB.Repositories;
using OopLab.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Services
{
    public class GameAccountService : IGameAccountService
    {
        GameAccountRepository repository;
        public GameAccountService(DbContext context)
        {
            repository = new GameAccountRepository(context);
        }
        public void AddGameResult(GameResult gameResult, GameAccount entity)
        {
            repository.AddGameResult(MapGameResult(gameResult), Map(entity));
        }
        public void Create(GameAccount entity)
        {
            repository.Create(Map(entity));
        }
        public void Delete(GameAccount entity)
        {
            repository.Delete(Map(entity));
        }
        public List<GameAccount> GetAll()
        {
            var list = repository.GetAll()
            .Select(x => x != null ? Map(x) : null)
            .ToList();
            return list;
        }
        public GameAccount GetById(int id)
        {
            GameAccount acc = Map(repository.GetById(id));
            return acc;
        }
        public List<GameResult> GetHistory(GameAccount entity)
        {
            return MapGameResults(repository.GetHistory(Map(entity)));
        }
        public void Update(GameAccount entity)
        {
            repository.Update(Map(entity));
        }
        private GameAccount Map(GameAccountEntity gameAccount)
        {
            if (gameAccount.Indicator == 0)
            {
                return new GameAccount(this, gameAccount.Id)
                {
                    _service = this,
                    Id = gameAccount.Id,
                    UserName = gameAccount.UserName,
                    CurrentRating = gameAccount.CurrentRating,
                    GamesCount = gameAccount.GamesCount,
                    GameHistory = MapGameResults(gameAccount.GameHistory),
                    Indicator = gameAccount.Indicator,
                };
            }
            else if (gameAccount.Indicator == 1)
            {
                return new AccountHalfRating(this, gameAccount.Id)
                {
                    _service = this,
                    Id = gameAccount.Id,
                    UserName = gameAccount.UserName,
                    CurrentRating = gameAccount.CurrentRating,
                    GamesCount = gameAccount.GamesCount,
                    GameHistory = MapGameResults(gameAccount.GameHistory),
                    Indicator = gameAccount.Indicator,
                };
            }
            else
            {
                return new AccountWithStreek(this, gameAccount.Id)
                {
                    _service = this,
                    Id = gameAccount.Id,
                    UserName = gameAccount.UserName,
                    CurrentRating = gameAccount.CurrentRating,
                    GamesCount = gameAccount.GamesCount,
                    GameHistory = MapGameResults(gameAccount.GameHistory),
                    Indicator = gameAccount.Indicator,
                };
            }
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
                GameHistory = MapGameResults(gameAccount.GameHistory),
                Indicator = gameAccount.Indicator,
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

    }
}
