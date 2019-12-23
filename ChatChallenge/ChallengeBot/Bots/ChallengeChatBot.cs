// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ChallengeBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace ChallengeBot.Bots
{
    public class ChallengeChatBot : ActivityHandler
    {
        private IStooqService stooqService;

        public ChallengeChatBot(IStooqService _stooqService)
        {
            stooqService = _stooqService;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                string input = turnContext.Activity.Text;
                if (Regex.IsMatch(input, "^/Stock=[A-Z0-9.,_-]+"))
                {
                    var resultStooq = stooqService.GetStooq(input.Replace("/Stock=", ""));
                    if (resultStooq.IsSuccessful)
                        await turnContext.SendActivityAsync($"{resultStooq.Result.Symbol} quote is {resultStooq.Result.Close.ToString("C2", CultureInfo.CurrentCulture)} per share.");
                    else
                        await turnContext.SendActivityAsync($"SimpleBot: {resultStooq.Message}");
                }
                else
                {
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak($"Echo: {turnContext.Activity.Text}"), cancellationToken);
                }
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak($"Hello and welcome!"), cancellationToken);
                }
            }
        }

        private IActivity CreateActivityWithTextAndSpeak(string message)
        {
            var activity = MessageFactory.Text(message);
            string speak = @"<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
              <voice name='Microsoft Server Speech Text to Speech Voice (en-US, JessaRUS)'>" +
              $"{message}" + "</voice></speak>";
            activity.Speak = speak;
            return activity;
        }
    }
}
