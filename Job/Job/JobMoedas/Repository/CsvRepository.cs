using Grpc.Core;
using JobMoedas.Interfaces;
using JobMoedas.Models;
using JobMoedas.Models.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JobMoedas.Repository
{
    public class CsvRepository : ICsvRepository
    {
        private List<Moeda> LerDadosMoeda(string filePath, Coin moeda)
        {
            using (var reader = new StreamReader($"{filePath}\\DadosMoeda.csv"))
            {
                Console.WriteLine($"{DateTime.Now} - Lendo o arquivo DadosMoeda.csv...");

                var moedasCsv = TrazerDadosMoedaCsv(reader);

                var moedasFilter = moedasCsv.FindAll(x => x.DataRef >= moeda.Data_Inicio && x.DataRef <= moeda.Data_Fim);

                Console.WriteLine($"{DateTime.Now} - Leitura finalizada. \n");

                return moedasFilter;
            }
        }

        private List<MoedaDTO> LerDadosCotacao(string filePath, Coin coin)
        {
            var lmoedas = LerDadosMoeda(filePath, coin);

            using (var reader = new StreamReader($"{filePath}\\DadosCotacao.csv"))
            {
                Console.WriteLine($"{DateTime.Now} - Lendo o arquivo DadosCotacao.csv...");
                Console.WriteLine($"{DateTime.Now} - Esse passo pode demorar um pouco. Aguarde...");

                var cotacoesCsv = TrazerDadosCotacaoCsv(reader);

                var lmoedasAgrupadas = lmoedas
                    .OrderBy(x => x.IdMoeda)
                    .GroupBy(x => x.IdMoeda)
                    .Select(a => a.ToList())
                    .ToList();

                var cotacaoFiltrada = FiltrarCotacao(lmoedasAgrupadas, cotacoesCsv);

                Console.WriteLine($"{DateTime.Now} - Valores do arquivo DadosCotacao.csv retornados. \n");
                return cotacaoFiltrada;
            }
        }

        public void Gravar(string filePath, Coin coin)
        {
            var lMoedasDTO = LerDadosCotacao(filePath, coin);

            string nomeAquivo = $"Resultado_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
            
            Console.WriteLine($"{DateTime.Now} - Salvando arquivo '{nomeAquivo}.csv...'");

            using (TextWriter sw = new StreamWriter($"{filePath}\\{nomeAquivo}.csv"))
            {
                sw.WriteLine("ID_MOEDA; DATA_REF; vlr_cotacao");

                foreach (var moeda in lMoedasDTO)
                {
                    sw.WriteLine($"{moeda.IdMoeda}; {moeda.DataRef.ToString("dd/MM/yyyy")}; {moeda.ValCotacao}");
                }

                Console.WriteLine($"{DateTime.Now} - Arquivo Salvo! \n Este se encontra no caminho {filePath}\\{nomeAquivo}.csv \n");
            }
        }

        private List<Moeda> TrazerDadosMoedaCsv(StreamReader reader)
        {
            List<Moeda> dadosCsv = new List<Moeda>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (values[0] != "ID_MOEDA")
                {
                    dadosCsv.Add(new Moeda() { IdMoeda = values[0], DataRef = Convert.ToDateTime(values[1]) });
                }
            }
            return dadosCsv;
        }

        private List<MoedaDTO> FiltrarCotacao(List<List<Moeda>> lmoedasAgrupadas, List<Cotacao> cotacoesCsv)
        {
            List<MoedaDTO> dadosFiltrados = new List<MoedaDTO>();

            foreach (var moeda in lmoedasAgrupadas)
            {
                foreach (var item in moeda)
                {
                    int codCotacao = (int)Enum.Parse(typeof(CodCotacaoEnum.CodCotacao), moeda[0].IdMoeda);

                    var cotacao = cotacoesCsv.FindAll(x => x.CodCotacao == codCotacao && x.DataCotacao == item.DataRef).FirstOrDefault();

                    dadosFiltrados.Add(new MoedaDTO { IdMoeda = item.IdMoeda, DataRef = item.DataRef, ValCotacao = cotacao.ValCotacao });
                }
            }

            return dadosFiltrados;
        }

        private List<Cotacao> TrazerDadosCotacaoCsv(StreamReader reader)
        {
            List<Cotacao> dadosCsv = new List<Cotacao>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (values[0] != "vlr_cotacao")
                {
                    dadosCsv.Add(new Cotacao() { ValCotacao = values[0], CodCotacao = Convert.ToByte(values[1]), DataCotacao = Convert.ToDateTime(values[2]) });
                }
            }

            return dadosCsv;
        }
    }
}
