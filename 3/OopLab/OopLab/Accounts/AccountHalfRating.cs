using OopLab.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.DB.Entity
{
    public class AccountHalfRating : GameAccount
    {
        IGameAccountService _service;
        public AccountHalfRating(IGameAccountService service, int ID, int gamesCount = 0,int indicator=1) : base(service, ID, gamesCount,indicator)
        {
            _service = service;
            Id = ID;
        }

        public override int PointsCalculate(int rating)
        {
            return rating /= 2;
        }
    }
}
