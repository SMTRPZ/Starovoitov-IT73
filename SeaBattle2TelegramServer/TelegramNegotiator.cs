using System;
using System.Collections.Generic;
using SeaBattle2TelegramServer.MessageHandlers;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;



namespace SeaBattle2TelegramServer
{
    class TelegramNegotiator
    {
        const string BotToken = "909771267:AAHfdJ5NN2efYFRFEzAaQFar757qQwSwlrA";
        TelegramBotClient _bot;
        private static readonly Dictionary<int, TelegramSession> Sessions = new Dictionary<int, TelegramSession>();
        private MessageHandler Handler;
        
        
        public void StartConversation()
        {
            //Настройка конвеера обработки сообщений
            //Фильтровать текст по командам
            Handler = new CommandHandler();
                
            //Фильтровать текст по координатам
                
            //Непонятно что пришло
            
            //Запуск бота
            _bot = new TelegramBotClient(BotToken);
            _bot.OnMessage += OnMessage;
            _bot.StartReceiving(new List<UpdateType>
            {
                UpdateType.Message
            }.ToArray());
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            try
            {
                Console.WriteLine($"Пришло сообщение {e.Message.Text}");
                TelegramSession currentSession = GetTelegramSession(message);

                Handler?.HandleMessage(message,currentSession, _bot);
            }
            catch (Exception eee)
            {
                _bot.SendTextMessageAsync(message.From.Id, $"Я навернулся. Ошибка: {eee.Message}");
            }
        }

        private TelegramSession GetTelegramSession(Message message)
        {
            TelegramSession currentSession;
            if ((currentSession = Sessions.GetValueOrDefault(message.From.Id) )== null)
            {
                currentSession = new TelegramSession();
                Sessions.Add(message.From.Id, currentSession);
            }

            return currentSession;
        }
        
    }
}

