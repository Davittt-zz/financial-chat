using AutoMapper;
using JobsityFinancialChat.Logic.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Logic
{
    public class StockService
    {
        public async Task<StockInfo> GetStock(string stockName)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringResult = await client
                .GetStringAsync($"https://stooq.com/q/l/?s="+stockName+"&f=sd2t2ohlcv&h&e=JSON");

            StookStockInfo result = JsonConvert.DeserializeObject<StookStockInfo>(stringResult);

            return result.Symbols.First();
        }
    }
}
