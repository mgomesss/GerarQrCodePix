using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace GerarQrCodePix.Models
{
    public class SalvarDados
    {

        public static bool Salvar(string nome, string cidade, string chave)
        {
            string Inf = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\Inf\\";

            if (!Directory.Exists(Inf))
            {
                Directory.CreateDirectory(Inf);
            }
            else
            {
                Directory.Delete(Inf, true);
                Directory.CreateDirectory(Inf);
            }

            string fullPath = Path.Combine(Inf, "dados.txt");

            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine(nome);
                writer.WriteLine(cidade);
                writer.WriteLine(chave);
            }
            
            return true;
        }

        public static string api(string nome, string cidade, string chave, string valor, string tipo)
        {
            string link = "https://gerarqrcodepix.com.br/api/v1?nome=" + nome + "&cidade=" + cidade + "&valor=" + valor + "&saida=" + tipo + "&chave=" + chave + "";
            var client = new RestClient(link);
            
            var request = new RestRequest("", Method.Get);
            var response = client.Get(request);

            var content = response.Content;
            var jsonContent1 = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            var brcode = jsonContent1.Values.First();

            return brcode;
        }

    }
}