using JobMoedas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobMoedas.Interfaces
{
    public interface IHttpRepository
    {
        Coin Get(string apiUrl);
    }
}
