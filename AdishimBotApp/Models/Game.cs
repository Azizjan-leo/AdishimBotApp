using System;

namespace AdishimBotApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        //public string InlineMessageId { get; set; }
        public int StarterUserId { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime? EndUtc { get; set; }
        public string Question { get; set; }
        public GameType Type { get; set; }
        public int WinnerUserId { get; set; }
        public bool Closed { get; set; }
    }
}
