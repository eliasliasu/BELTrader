using BELTrader.Domain.Models;
using BELTrader.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BELTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory;

        public MajorIndexService(FinancialModelingPrepHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using(FinancialModelingHttpClient client = _httpClientFactory.CreatHttpClient())
            {
                //https://financialmodelingprep.com/api/v3/quote/%5EDJI?apikey=ddd6c9e912f30cf18847c9253f7c1a38

                string uri = "quote/%5E" + GetUriSuffice(indexType);

                List<MajorIndex> majorIndex = new List<MajorIndex>();
               
                 majorIndex = await client.GetAsync<MajorIndex>(uri); 

                var majorIndexEntity = majorIndex[0];
                majorIndexEntity.Type = indexType;

                return majorIndexEntity;
            }
        }

        private string GetUriSuffice(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return "DJI";
                case MajorIndexType.Nasdaq:
                    return "IXIC";
                case MajorIndexType.SP500:
                    return "GSPC";
                default:
                    throw new Exception("Majorindex type does not have a suffix defined");
            }
        }
    }
}