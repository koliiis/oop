using OopLab.DB.Entity;
using OopLab.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Services.Abstracts
{
    // Інтерфейс IGameService визначає контракт для сервісу, який працює з об'єктами типу Game
    public interface IGameService
    {
        // Метод для створення нової гри
        public void Create(Game entity);

        // Метод для отримання списку усіх існуючих ігор
        public List<Game> GetAll();

        // Метод для отримання гри за її ідентифікатором
        public Game GetById(int Id);

        // Метод для оновлення інформації про гру
        public void Update(Game entity);

        // Метод для видалення гри
        public void Delete(Game entity);
    }
}