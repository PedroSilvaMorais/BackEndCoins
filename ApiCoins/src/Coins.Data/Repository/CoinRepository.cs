using Coins.Business.Interfaces;
using Coins.Business.Models;
using Coins.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repository
{
    public class CoinRepository : ICoinRepository
    {
        protected readonly MoedasDbContext _db;

        public CoinRepository(MoedasDbContext db)
        {
            _db = db;
        }
        public async Task<Coin> GetLastItemAdicionado()
        {
            return await _db.Moeda.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task AddItem(Coin moeda)
        {
            _db.Moeda.Add(moeda);
            await Commit();
        }

        public async Task DeleteItem(long id)
        {
            _db.Moeda.Remove(new Coin { Id = id });
            await Commit();
        }

        public async Task<int> Commit()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
