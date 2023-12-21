using Lecture_23_10_2023_Alt.DB.Entity.GameAccounts;
using OopLab.DB.Entity;
using OopLab.DB.Entity.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.DB
{
    // Клас для роботи з базою даних гри
    public class DbContext
    {
        // Список ігрових об'єктів
        public List<GameEntity> Games { get; set; }

        // Список облікових записів гравців
        public List<GameAccountEntity> Accounts { get; set; }

        // Конструктор класу, ініціалізує порожні списки для ігрових об'єктів та облікових записів
        public DbContext()
        {
            Games = new List<GameEntity>();
            Accounts = new List<GameAccountEntity>();
        }
    }
}