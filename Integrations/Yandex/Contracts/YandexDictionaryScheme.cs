using System.Collections.Generic;

namespace TranslateHelperBot.Integrations.Yandex.Contracts
{
    public class Tr
    {
        public string text { get; set; }
        public List<Ex> ex { get; set; }
        public int fr { get; set; }
        public string gen { get; set; }
        public List<Mean> mean { get; set; }
        public string pos { get; set; }
        public List<Syn> syn { get; set; }
        public string asp { get; set; }
    }

    public class Ex
    {
        public string text { get; set; }
        public List<Tr> tr { get; set; }
    }

    public class Mean
    {
        public string text { get; set; }
    }

    public class Syn
    {
        public int fr { get; set; }
        public string gen { get; set; }
        public string pos { get; set; }
        public string text { get; set; }
        public string asp { get; set; }
    }
    
    public class Def
    {
        public string pos { get; set; }
        public string text { get; set; }
        public List<Tr> tr { get; set; }
        public string ts { get; set; }
    }

    public class Head
    {
    }

    public class YandexDictionaryScheme
    {
        public List<Def> def { get; set; }
        public Head head { get; set; }
    }
}