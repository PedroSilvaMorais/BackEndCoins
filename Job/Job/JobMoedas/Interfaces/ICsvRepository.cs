using JobMoedas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMoedas.Interfaces
{
    interface ICsvRepository
    {
        List<Moeda> LerDadosMoeda(string filePath, Coin moeda);

        List<MoedaDTO> LerDadosCotacao(string filePath, Coin coin);

        void Gravar(string filePath, Coin coin);
    }
}
