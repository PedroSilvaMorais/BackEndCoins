using Coins.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Business.Interfaces
{
    public interface ICoinRepository
    {
        Task AddItem(Coin moeda);
        Task<Coin> GetLastItemAdicionado();
        Task DeleteItem(long id);
        Task<int> Commit();
    }
}
