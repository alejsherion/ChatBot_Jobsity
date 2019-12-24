using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppChat.Services;

namespace WebAppChat.Hubs
{
    public class ChatHub: Hub
    {
        private const string BOTNAME = "AlejoBot";

        private readonly IBotService botService;

        public ChatHub(IBotService _botService)
        {
            botService = _botService;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

            var botResponse = botService.BotIntercept(message);
            if (botResponse.IsError)
                await Clients.All.SendAsync("ReceiveMessage", BOTNAME, $"¡error! {botResponse.Message} ¡error!");
            else if (botResponse.IsSuccessful)
                await Clients.All.SendAsync("ReceiveMessage", BOTNAME, $"{botResponse.Result.Symbol} quote is {botResponse.Result.Close} per share");
        }
    }
}
