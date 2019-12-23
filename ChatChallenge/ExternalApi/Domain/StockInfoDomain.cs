using ExternalApi.Domain.Contract;
using ExternalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalApi.Domain
{
    public class StockInfoDomain : IStockInfoDomain
    {
        #region Members
        public HttpClient client { get; }
        #endregion

        public StockInfoDomain(HttpClient _client)
        {
            client = _client;
        }

        public StooqModel GetStooq(string stock_code)
        {
            using (HttpResponseMessage response = client.GetAsync($"https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv").Result)
            using (HttpContent content = response.Content)
            {
                string serviceResponse = content.ReadAsStringAsync().Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new ArgumentException(serviceResponse);

                string data = serviceResponse.Substring(serviceResponse.IndexOf(Environment.NewLine, StringComparison.Ordinal)+2);
                string[] splitedData = data.Split(',');

                return new StooqModel()
                {
                    Symbol = splitedData[0],
                    Date = !splitedData[1].Contains("N/D") ? Convert.ToDateTime(splitedData[1]) : default,
                    Time = !splitedData[2].Contains("N/D") ? Convert.ToDateTime(splitedData[2]).TimeOfDay : default,
                    Open = !splitedData[3].Contains("N/D") ? Convert.ToDouble(splitedData[3]) : default,
                    High = !splitedData[4].Contains("N/D") ? Convert.ToDouble(splitedData[4]) : default,
                    Low = !splitedData[5].Contains("N/D") ? Convert.ToDouble(splitedData[5]) : default,
                    Close = !splitedData[6].Contains("N/D") ? Convert.ToDouble(splitedData[6]) : default,
                    Volume = !splitedData[7].Contains("N/D") ? Convert.ToDouble(splitedData[7]) : default,
                };
            }
        }
    }
}
