using AdishimBotApp.Extantions;

namespace AdishimBotApp.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string UrText { get; set; }
        public string RuText { get; set; }
        public int AuthorId { get; set; }

        public void Capitalize()
        {
            RuText = RuText.FirstCharToUpper();
            UrText = UrText.FirstCharToUpper();
        }
    }
}
