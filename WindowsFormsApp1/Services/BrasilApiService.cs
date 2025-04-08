using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WindowsFormsApp1.Services
{
    public class FornecedorCnpj
    {
        [JsonProperty("razao_social")] 
        public string nome { get; set; }

        public string cnpj { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string municipio { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
    }

    public class BrasilApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<FornecedorCnpj> ConsultarCnpjAsync(string cnpj)
        {
            try
            {
                var url = $"https://brasilapi.com.br/api/cnpj/v1/{cnpj}";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                var fornecedor = JsonConvert.DeserializeObject<FornecedorCnpj>(json);

                return fornecedor;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
