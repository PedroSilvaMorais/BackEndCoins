using System;
using System.Collections.Generic;
using System.Text;

namespace Coins.Business.Models
{
    public class Coin
    {
        public long Id { get; set; }
        public string Moeda { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
