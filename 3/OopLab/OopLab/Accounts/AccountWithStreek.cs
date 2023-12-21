using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab.DB.Entity;
using OopLab.Services.Base;

namespace Lecture_23_10_2023_Alt.DB.Entity.GameAccounts
{
    public class AccountWithStreek : GameAccount
    {
        IGameAccountService _service;
        public AccountWithStreek(IGameAccountService service, int ID, int gamesCount = 0, int indicator = 2) : base(service, ID, gamesCount, indicator)
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
