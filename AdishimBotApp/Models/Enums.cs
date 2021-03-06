﻿namespace AdishimBotApp.Models
{
    public enum Languages: byte { Uyghur, Russian, Uzbek }
    public enum Scripts { Arabic, Cyr, Uly }
    public enum LetterPos { Start, Center, End, Spec }
    public enum Special { NoSpec, IsCroro, IsAlert }
    public enum GameType { RuToUy, UrToRu, UrCroco }
}
