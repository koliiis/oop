using OopLab.DB.Entity.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.DB.Repositories.Base
{
    // Інтерфейс для взаємодії з об'єктами типу GameEntity в базі даних
    public interface IGameRepository
    {
        // Метод для створення нового об'єкта гри  в базі даних
        public void Create(GameEntity entity);

        // Метод для отримання списку всіх об'єктів гри  з бази даних
        public List<GameEntity> GetAll();

        // Метод для отримання об'єкта гри за його ідентифікатором
        public GameEntity GetById(int Id);

        // Метод для оновлення об'єкта гри в базі даних
        public void Update(GameEntity entity);

        // Метод для видалення об'єкта гри з бази даних
        public void Delete(GameEntity entity);
    }
}