using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab.DB.Entity;
using OopLab.Services;

namespace Lecture_23_10_2023_Alt.DB.Entity.GameAccounts
{
    public class AccountWithStreek : GameAccount
    {
        GameAccountService _service;
        public AccountWithStreek(GameAccountService service, int ID, int gamesCount = 0) : base(service, ID, gamesCount)
        {
            _service = service;
            Id = ID;
        }
            public override int PointsCalculate(int rating)
        {
            return rating = rating * (1 + WinStreek);
        }
    }
}
