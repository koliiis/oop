using OopLab.DB.Entity.Games;
using OopLab.DB.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.DB.Repositories
{
    // Клас репозиторію для роботи з іграми
    public class GameRepository : IGameRepository
    {
        // Об'єкт контексту бази даних
        private DbContext context;

        // Конструктор, який приймає об'єкт контексту бази даних
        public GameRepository(DbContext context)
        {
            this.context = context;
        }

        // Метод для створення нового об'єкту гри
        public void Create(GameEntity entity)
        {
            context.Games.Add(entity);
        }

        // Метод для видалення об'єкту гри
        public void Delete(GameEntity entity)
        {
            // Видалення об'єкту гри за індексом
            context.Games.RemoveAt(entity.Id);

            // Перенумерація індексів об'єктів гри після видалення
            int ID = 1;
            foreach (var game in context.Games)
            {
                context.Games[ID].Id = ID;
                ID++;
            }
        }

        // Метод для отримання всіх об'єктів гри
        public List<GameEntity> GetAll()
        {
            return context.Games;
        }

        // Метод для отримання об'єкту гри за ідентифікатором
        public GameEntity GetById(int Id)
        {
            return context.Games[Id];
        }

        // Метод для оновлення інформації про об'єкт гри
        public void Update(GameEntity entity)
        {
            // Видалення старого об'єкту гри та вставлення нового на його місце
            context.Games.RemoveAt(entity.Id);
            context.Games.Insert(entity.Id, entity);
        }
    }
}