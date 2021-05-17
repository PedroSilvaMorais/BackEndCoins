using System;


namespace JobMoedas.Models
{
    public class Coin
    {
        public long Id { get; set; }
        public string Moeda { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Fim { get; set; }
    }
}
