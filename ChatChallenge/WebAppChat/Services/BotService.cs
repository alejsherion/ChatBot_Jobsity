using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAppChat.Models;

namespace WebAppChat.Services
{
    public class BotService: IBotService
    {
        private HttpClient client { get; set; }
        public BotService(HttpClient _client)
        {
            client = _client;
        }

        public ResultBot<StooqModel> BotIntercept(string message)
        {
            try
            {
                if (Regex.IsMatch(message, "^/Stock=[A-Z0-9.,_-]+"))
                {
                    string code = message.Replace("/Stock=", "");

                    using (HttpResponseMessage response = client.GetAsync($"https://localhost:44303/api/StockInfo/GetStooq?stock_code={code}").Result)
                    using (HttpContent content = response.Content)
                    {
                        string serviceResponse = content.ReadAsStringAsync().Result;
                        if (response.StatusCode != System.Net.HttpStatusCode.OK)
                            return ResultBot<StooqModel>.SetError(serviceResponse);

                        return ResultBot<StooqModel>.SetSuccess(JsonConvert.DeserializeObject<StooqModel>(serviceResponse));
                    }
                }

                return ResultBot<StooqModel>.SetUnsuccess();
            }
            catch (Exception ex)
            {
                return ResultBot<StooqModel>.SetError(ex.Message);
            }
        }
    }
}
