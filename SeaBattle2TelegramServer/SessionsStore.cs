using System.Collections.Generic;
using Telegram.Bot.Types;

namespace SeaBattle2TelegramServer
{
    public class SessionsStore
    {
        private static readonly Dictionary<int, TelegramSession> Sessions = new Dictionary<int, TelegramSession>();
        public TelegramSession GetTelegramSession(Message message)
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