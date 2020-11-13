﻿using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using AdishimBotApplication.Models;
using System.Threading.Tasks;

namespace AdishimBotApplication.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")] // webhook uri part
        public async Task<OkResult> Post([FromBody]Update update)
        {
            if (update == null) return Ok() ;
            var commands = Bot.Commands;
            var message  = update.Message;
            //Logger.Messages.Add($"{message.From.FirstName} writed in {message.Chat.Id} " + message.Text);
            var client   = await Bot.Get();
            
            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    await command.Execute(message, client);
                    break;
                }
            }

            return Ok();
        }
    }
}
