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
        public List<Moeda> LerDadosMoeda(string filePath, Coin moeda)
        {
            using (var reader = new StreamReader($"{filePath}\\DadosMoeda.csv"))
            {
                Console.WriteLine($"{DateTime.Now} - Lendo o arquivo DadosMoeda.csv...");

                List<Moeda> moedasCsv = new List<Moeda>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (values[0] != "ID_MOEDA")
                    {
                        moedasCsv.Add(new Moeda() { IdMoeda = values[0], DataRef = Convert.ToDateTime(values[1]) });
                    }
                }

                List<Moeda> moedasFilter = moedasCsv.FindAll(x => x.DataRef >= moeda.Data_Inicio && x.DataRef <= moeda.Data_Fim);

                Console.WriteLine($"{DateTime.Now} - Leitura finalizada.");

                return moedasFilter;
            }
        }

        public List<MoedaDTO> LerDadosCotacao(string filePath, Coin coin)
        {
            var lmoedas = LerDadosMoeda(filePath, coin);

            using (var reader = new StreamReader($"{filePath}\\DadosCotacao.csv"))
            {
                Console.WriteLine($"{DateTime.Now} - Iniciando retorno dos valores do arquivo DadosCotacao.csv...");
                Console.WriteLine($"{DateTime.Now} - Este arquivo é um pouco grande, aguarde um pouco por gentileza...");

                List<Cotacao> cotacoesCsv = new List<Cotacao>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (values[0] != "vlr_cotacao")
                    {
                        cotacoesCsv.Add(new Cotacao() { ValCotacao = values[0], CodCotacao = Convert.ToByte(values[1]), DataCotacao = Convert.ToDateTime(values[2]) });
                    }
                }

                List<MoedaDTO> cotacaoFiltrada = new List<MoedaDTO>();

                var lmoedasAgrupadas = lmoedas
                    .OrderBy(x => x.IdMoeda)
                    .GroupBy(x => x.IdMoeda)
                    .Select(a => a.ToList())
                    .ToList();

                foreach (var moeda in lmoedasAgrupadas)
                {
                    foreach (var item in moeda)
                    {
                        int codCotacao = (int)Enum.Parse(typeof(CodCotacaoEnum.CodCotacao), moeda[0].IdMoeda);

                        var cotacao = cotacoesCsv.FindAll(x => x.CodCotacao == codCotacao && x.DataCotacao == item.DataRef).FirstOrDefault();


                        cotacaoFiltrada.Add(new MoedaDTO { IdMoeda = item.IdMoeda, DataRef = item.DataRef, ValCotacao = cotacao.ValCotacao });
                    }
                }

                Console.WriteLine($"{DateTime.Now} - Valores do arquivo DadosCotacao.csv retornados...");
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
                    sw.WriteLine($"{moeda.IdMoeda}; {moeda.DataRef.ToString()}; {moeda.ValCotacao}");
                }

                Console.WriteLine($"{DateTime.Now} - Arquivo Salvo! Este se encontra no caminho {filePath}\\{nomeAquivo}.csv");
            }
        }
    }
}
