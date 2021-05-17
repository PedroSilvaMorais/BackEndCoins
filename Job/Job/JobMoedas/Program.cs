using JobMoedas.Interfaces;
using JobMoedas.Repository;
using System;
using System.Diagnostics;
using System.Threading;

namespace JobMoedas
{
    class Program
    {
        private const string apiUrl = "https://localhost:44319/api/moedas";
        static void Main(string[] args)
        {
            IHttpRepository _HttpRepository = new HttpRepository();
            ICsvRepository _csvRepository = new CsvRepository();
            string filePath = AppDomain.CurrentDomain.BaseDirectory;

            while (true)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.WriteLine($"{DateTime.Now} - Processo Iniciado...");
                Console.WriteLine("\n");

                var ResultMoeda = _HttpRepository.Get(apiUrl);

                if (ResultMoeda.Moeda != null)
                    _csvRepository.Gravar(filePath, ResultMoeda);


                stopwatch.Stop();
                Console.WriteLine($"{DateTime.Now} - Processo Finalizado - Tempo Total Decorrido : {stopwatch.Elapsed}");
                Console.WriteLine($"{DateTime.Now} - O próximo processo iniciará em 2 minutos. Aguarde...");
                Console.WriteLine("\n");
                Thread.Sleep(120000);
            }
        }
    }
}
