using ChallengeBot.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace ChallengeBot.Services
{
    public class StooqService : IStooqService
    {
        #region Members
        public HttpClient client { get; }
        #endregion

        #region Builder
        public StooqService(HttpClient _client)
        {
            client = _client;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get Stooq data by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ResultBot<StooqModel> GetStooq(string code)
        {
            try
            {
                using (HttpResponseMessage response = client.GetAsync($"https://localhost:44303/api/StockInfo/GetStooq?stock_code={code}").Result)
                using (HttpContent content = response.Content)
                {
                    string serviceResponse = content.ReadAsStringAsync().Result;
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        return ResultBot<StooqModel>.SetError(serviceResponse);

                    return ResultBot<StooqModel>.SetSuccess(JsonConvert.DeserializeObject<StooqModel>(serviceResponse));
                }
            }
            catch (Exception ex)
            {
                return ResultBot<StooqModel>.SetError(ex.Message);
            }
        }
        #endregion
    }
}
