using System;

namespace AdishimBotApp.Models
{
    public enum GameType { RuToUy, UyToRu }

    public class Game
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime? EndUtc { get; set; }
        public string Question { get; set; }
        public GameType Type { get; set; }
        public string RightAnswer { get; set; }
        public string WinnerUsername { get; set; }
        public bool Closed { get; set; }
    }
}
