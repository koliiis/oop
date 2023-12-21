using Lecture_23_10_2023_Alt.DB.Entity.GameAccounts;
using OopLab.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.DB.Repositories.Base
{
    // Інтерфейс для взаємодії з об'єктами типу GameAccountEntity в базі даних
    public interface IGameAccountRepository
    {
        // Метод для створення нового акаунту в базі даних
        public void Create(GameAccountEntity entity);

        // Метод для отримання списку всіх акаунтів з бази даних
        public List<GameAccountEntity> GetAll();

        // Метод для отримання акаунта за його ідентифікатором
        public GameAccountEntity GetById(int id);

        // Метод для оновлення акаунта в базі даних
        public void Update(GameAccountEntity entity);

        // Метод для видалення акаунта з бази даних
        public void Delete(GameAccountEntity entity);

        // Метод для отримання історії гри акаунта
        public List<GameResultEntity> GetHistory(GameAccountEntity entity);

        // Метод для додавання результату гри до історії гри акаунта
        public void AddGameResult(GameResultEntity gameResult, GameAccountEntity entity);
    }
}