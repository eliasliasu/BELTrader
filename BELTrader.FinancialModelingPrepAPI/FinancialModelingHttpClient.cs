using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BELTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingHttpClient : HttpClient
    {
        private readonly string _apikey; //apiKey = 

        public FinancialModelingHttpClient(string apikey)
        {
            this.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
            this._apikey = apikey;
        }

        public async Task<List<T>> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync($"{uri}?apikey={_apikey}");
            string jsonRespone = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<T>>(jsonRespone);
        }
    }
}
