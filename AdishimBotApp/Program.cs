using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using AdishimBotApp.Commands;
using AdishimBotApp.Models;

namespace AdishimBotApp
{
    public class Program
    {
        /// <summary>  
        /// Declare Telegrambot object  
        /// </summary>  
        private static readonly TelegramBotClient bot = new TelegramBotClient("");
        private static readonly List<Command> commands = BotCommands.Get();
        public static void Main(string[] args)
        {
            bot.OnMessage += Csharpcornerbotmessage;
            bot.StartReceiving();
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>  
        /// Handle bot webhook  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private static void Csharpcornerbotmessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
                PrepareQuestionnaires(e);
        }

        public static void PrepareQuestionnaires(MessageEventArgs e)
        {
            try
            {
                var message = e.Message;
                foreach (var command in commands)
                {
                    if (command.Contains(message.Text))
                    {
                        command.Execute(message, bot);
                        break;
                    }
                }
            }catch(Exception ex)
            {
                Logger.Messages.Add(ex.Message);
            }
      
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
