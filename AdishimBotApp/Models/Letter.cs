
namespace AdishimBotApp.Models
{
    public class Letter
    {
        public readonly int Index;
        public readonly string Arab, ArabStart, ArabCenter, ArabEnd, CyrUp, CyrDown, UlyUp, UlyDown;
        public readonly bool ConnNext, IsCenter, IsLast;
        public readonly int[] CharCode;
        public readonly char[] ArabChar;

        public Letter(int index, int[] arabChar, string cyrUp, string cyrDown, string ulyUp, string ulyDown, 
                        string arab, string arabStart, string arabCenter, string arabEnd,
                        bool connNext, bool isCenter, bool isLast, int[] charCode = null)
        {
            Index = index;
            ArabChar = new char[arabChar.Length];
            for (int i = 0; i < arabChar.Length; i++)
            {
                ArabChar[i] = (char)arabChar[i];
            }
            Arab = arab;
            ArabStart = arabStart;
            ArabCenter = arabCenter;
            ArabEnd = arabEnd;
            CyrUp = cyrUp;
            CyrDown = cyrDown;
            UlyUp = ulyUp;
            UlyDown = ulyDown;

            ConnNext = connNext;
            IsCenter = isCenter;
            IsLast = isLast;

            CharCode = charCode ?? new int[0];
        }

        public string GetInCase(bool start, bool Uly) => start ? Uly ? UlyUp : CyrUp : Uly ? UlyDown : CyrDown;
        
    }
}
