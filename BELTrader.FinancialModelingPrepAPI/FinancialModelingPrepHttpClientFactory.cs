using System;
using System.Collections.Generic;
using System.Text;

namespace BELTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClientFactory
    {
        private readonly string _apiKey;

        public FinancialModelingPrepHttpClientFactory(string apiKey)
        {
            _apiKey = apiKey;
        }

        public FinancialModelingHttpClient CreatHttpClient()
        {
            return new FinancialModelingHttpClient(_apiKey);
        }
    }
}
