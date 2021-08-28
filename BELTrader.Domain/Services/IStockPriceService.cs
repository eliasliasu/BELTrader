using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BELTrader.Domain.Services
{
    public interface IStockPriceService
    {
        Task<double> GetPrice(string symbol);
    }
}
