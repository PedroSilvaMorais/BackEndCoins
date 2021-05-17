using JobMoedas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMoedas.Interfaces
{
    interface ICsvRepository
    {
        void Gravar(string filePath, Coin coin);
    }
}
