using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppChat.Models;

namespace WebAppChat.Services
{
    public interface IBotService
    {
        ResultBot<StooqModel> BotIntercept(string message);
    }
}
