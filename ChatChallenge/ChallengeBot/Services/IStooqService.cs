using ChallengeBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeBot.Services
{
    public interface IStooqService
    {
        /// <summary>
        /// Get Stooq data by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ResultBot<StooqModel> GetStooq(string code);
    }
}
