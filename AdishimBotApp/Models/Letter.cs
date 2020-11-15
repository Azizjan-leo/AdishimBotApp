using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdishimBotApp.Models
{
    public class Letter
    {
        public readonly int Index;
        public readonly string Arab, ArabStart, ArabCenter, ArabEnd, CyrUp, CyrDown, UlyUp, UlyDown;
        public readonly bool ConnNextFirst, ConnNextCenter, ConnPrev;

        public Letter(int index, string cyrUp, string cyrDown, string ulyUp, string ulyDown, 
                        string arab, string arabStart, string arabCenter, string arabEnd,
                        bool connNextFirst, bool connNextCenter, bool connPrev)
        {
            Index = index;
            Arab = arab;
            ArabStart = arabStart;
            ArabCenter = arabCenter;
            ArabEnd = arabEnd;
            CyrUp = cyrUp;
            CyrDown = cyrDown;
            UlyUp = ulyUp;
            UlyDown = ulyDown;

            ConnNextFirst = connNextFirst;
            ConnNextCenter = connNextCenter;
            ConnPrev = connPrev;
        }
    }
}
