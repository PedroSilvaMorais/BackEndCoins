using JobMoedas.Interfaces;
using JobMoedas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JobMoedas.Repository
{
    class HttpRepository : IHttpRepository
    {
        public Coin Get(string apiUrl)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("").Result;

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var ResultMoeda = JsonConvert.DeserializeObject<Coin>(response.Content.ReadAsStringAsync().Result);

                        if (ResultMoeda.Moeda != null)
                        {
                            Console.WriteLine($"{DateTime.Now} - Moeda retornada da api: {ResultMoeda.Moeda}, data inicio: {ResultMoeda.Data_Inicio.ToString("dd/MM/yyyy")}, data fim: {ResultMoeda.Data_Fim.ToString("dd/MM/yyyy")}");
                        }
                        else
                        {
                            Console.WriteLine($"{DateTime.Now} - Não existe moedas para serem cotadas ");
                        }

                        return ResultMoeda;
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine($"{DateTime.Now} - Falha ao receber retorno da api...");
                        return new Coin();
                    }
                }

                return new Coin();
            }
            catch (Exception a)
            {
                Console.WriteLine($"{DateTime.Now} - API não encontrada...");
                return new Coin();
            }
        }
    }
}
