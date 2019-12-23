# ChatBot_Jobsity
a Chatbot, for a simple solution with SignalR using a bot

# Features
● Allow registered users to log in and talk with other users in a chatroom.
● Allow users to post messages as commands into the chatroom with the following format
/stock=stock_code
● Create a decoupled bot that will call an API using the stock_code as a parameter
(https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is the
stock_code)
● The bot should parse the received CSV file and then it should send a message back into
the chatroom using a message broker like RabbitMQ. The message will be a stock quote
using the following format: “APPL.US quote is $93.42 per share”. The post owner will be
the bot.
● Have the chat messages ordered by their timestamps and show only the last 50
messages.
● Unit test the functionality you prefer

# Requirements
SDK .NetCore 3.1 or Higher
EntityFramwork 3.1 or Higher
https://dotnet.microsoft.com/download
Bot Framework
https://github.com/microsoft/botframework
Bot Framework Emulator v4
https://github.com/microsoft/BotFramework-Emulator

The project contains 3 Main projects
1. ExternalApi
It contains the logic for the consumer and obtains the Stooq information
2. ChallengeBot
It Contains the logic bot for execute Stooq information and generate a request for ExternalApi
(can be tested with the emulator)
3. WebAppChat
It Contains a chat room for registered members and logged in
