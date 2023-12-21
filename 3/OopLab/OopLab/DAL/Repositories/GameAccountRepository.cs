using Lecture_23_10_2023_Alt.DB.Entity.GameAccounts;
using OopLab.DB.Entity;
using OopLab.DB.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.DB.Repositories
{
    // Клас, який реалізує інтерфейс IGameAccountRepository для роботи з об'єктами GameAccountEntity
    public class GameAccountRepository : IGameAccountRepository
    {
        // Об'єкт DbContext для взаємодії з базою даних
        DbContext context;

        // Конструктор класу, який приймає об'єкт DbContext і ініціалізує його поле
        public GameAccountRepository(DbContext context)
        {
            this.context = context;
        }

        // Метод для додавання результату гри до історії гри акаунта
        public void AddGameResult(GameResultEntity gameResult, GameAccountEntity entity)
        {
            context.Accounts[entity.Id].GameHistory.Add(gameResult);
        }

        // Метод для створення нового акаунта в базі даних
        public void Create(GameAccountEntity entity)
        {
            context.Accounts.Add(entity);
        }

        // Метод для видалення акаунта з бази даних
        public void Delete(GameAccountEntity entity)
        {
            context.Accounts.RemoveAt(entity.Id);

            // Перенумерація ідентифікаторів після видалення облікового запису
            int ID = 1;
            foreach (var gameAccount in context.Accounts)
            {
                context.Accounts[ID].Id = ID;
                ID++;
            }
        }

        // Метод для отримання списку всіх акаунтів з бази даних
        public List<GameAccountEntity> GetAll()
        {
            return context.Accounts;
        }

        // Метод для отримання акаунта за ідентифікатором
        public GameAccountEntity GetById(int id)
        {
            return context.Accounts[id];
        }

        // Метод для отримання історії гри акаунта
        public List<GameResultEntity> GetHistory(GameAccountEntity entity)
        {
            return entity.GameHistory;
        }

        // Метод для оновлення акаунта в базі даних
        public void Update(GameAccountEntity entity)
        {
            context.Accounts.RemoveAt(entity.Id);
            context.Accounts.Insert(entity.Id, entity);
        }
    }
}