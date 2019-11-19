using System;
using System.Collections.Generic;
using SeaBattle2TelegramServer.MessageHandlers;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace SeaBattle2TelegramServer
{
    class TelegramNegotiator
    {
        TelegramBotClient _bot;
        readonly SessionsStore _sessionsStore = new SessionsStore();
        readonly MessageСonveyor _messageСonveyor = new MessageСonveyor();
        
        public void StartConversation(string botToken )
        {
            SetUpMessagingPipeline();
            StartBot(botToken);
        }
        
      
        private void OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            try
            {
                Console.WriteLine($"Пришло сообщение {e.Message.Text}");
                TelegramSession currentSession = _sessionsStore.GetTelegramSession(message);
                _messageСonveyor.HandleMessage(message, currentSession, _bot);
            }
            catch (Exception eee)
            {
                _bot.SendTextMessageAsync(message.From.Id, $"Я навернулся. Ошибка: {eee.Message}");
            }
        }
        
        private void SetUpMessagingPipeline()
        {
            //Фильтровать текст по командам
            _messageСonveyor.AddHandler(new CommandHandler());
            //Фильтровать текст по координатам
            _messageСonveyor.AddHandler(new CoordinatesHandler());
            //Непонятно что пришло
            _messageСonveyor.AddHandler(new IncomprehensibleMessageHandler());
        }
        
        private void StartBot(string botToken )
        {
            _bot = new TelegramBotClient(botToken);
            _bot.OnMessage += OnMessage;
            _bot.StartReceiving(new List<UpdateType>
            {
                UpdateType.Message
            }.ToArray());
        }
    }
}

