using OopLab.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Services.Abstracts
{
    // Інтерфейс IGameAccountService визначає контракт для сервісу, який працює з обліковими записами гри
    public interface IGameAccountService
    {
        // Метод для створення нового облікового запису гри
        public void Create(GameAccount entity);

        // Метод для отримання списку усіх існуючих облікових записів гри
        public List<GameAccount> GetAll();

        // Метод для отримання облікового запису гри за його ідентифікатором
        public GameAccount GetById(int id);

        // Метод для оновлення інформації про обліковий запис гри
        public void Update(GameAccount entity);

        // Метод для видалення облікового запису гри
        public void Delete(GameAccount entity);

        // Метод для отримання історії результатів гри для певного облікового запису
        public List<GameResult> GetHistory(GameAccount entity);

        // Метод для додавання результатів гри до облікового запису гри
        public void AddGameResult(GameResult gameResult, GameAccount entity);
    }
}