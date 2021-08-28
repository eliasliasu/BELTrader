using BELTrader.Domain.Exceptions;
using BELTrader.Domain.Models;
using BELTrader.Domain.Services;
using BELTrader.FinancialModelingPrepAPI.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BELTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory;

        public StockPriceService(FinancialModelingPrepHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<double> GetPrice(string symbol)
        {

            using (FinancialModelingHttpClient client = _httpClientFactory.CreatHttpClient())
            {
                //https://financialmodelingprep.com/api/v3/quote-short/AAPL?apikey=ddd6c9e912f30cf18847c9253f7c1a38


                string uri = "quote-short/" + symbol;                

                List<StockPriceResult> stockPriceResult = new List<StockPriceResult>();

                stockPriceResult = await client.GetAsync<StockPriceResult>(uri);

                if(stockPriceResult.Count == 0)
                {
                    //if (stockPriceResult[0].price == 0)
                    //{
                    //    throw new InvalidSymbolException(symbol);
                    //}

                    throw new InvalidSymbolException(symbol);
                }


                return stockPriceResult[0].price;
            }

        }
    }
}
