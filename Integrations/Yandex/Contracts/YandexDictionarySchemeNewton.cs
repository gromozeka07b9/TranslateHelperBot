using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateHelperBot.Integrations.Yandex.Contracts
{
    //класс автоматически построен с помощью утилиты JSON to C#
    public class YandexDictionarySchemeNewton
    {
        //заголовок обычно пустой
        [JsonProperty("head")]
        private Dictionary<string, string> Head { get; set; }
        //варианты для разных частей речи
        [JsonProperty("def")]
        public List<Def> Def { get; private set; }
    }

    //данные вариантов по типам речи, элементов столько, сколько вариантов - сущ., предлог и т.д.
    public class Def
    {
        //исходное слово
        [JsonProperty("text")]
        public string Text { get; private set; }
        //тип речи
        [JsonProperty("pos")]
        public string Pos { get; private set; }
        //транскрипция
        [JsonProperty("ts")]
        public string Ts { get; private set; }

        [JsonProperty("tr")]
        public List<Tr> Tr { get; private set; }
    }

    //перевод
    public class Tr
    {
        [JsonProperty("text")]
        public string Text { get; private set; }
        [JsonProperty("pos")]
        public string Pos { get; private set; }
        [JsonProperty("syn")]
        public List<Syn> Syn { get; private set; }
        [JsonProperty("mean")]
        public List<Mean> Mean { get; private set; }
        [JsonProperty("ex")]
        public List<Ex> Ex { get; private set; }
    }

    //синонимы
    public class Syn
    {
        [JsonProperty("text")]
        public string Text { get; private set; }
        [JsonProperty("pos")]
        public string Pos { get; private set; }
    }

    //варианты значений
    public class Mean
    {
        [JsonProperty("text")]
        public string Text { get; private set; }
    }

    //примеры
    public class Ex
    {
        [JsonProperty("text")]
        public string Text { get; private set; }
        [JsonProperty("tr")]
        public List<ExTr> ExTr { get; private set; }
    }

    //перевод примера
    public class ExTr
    {
        [JsonProperty("text")]
        public string Text { get; private set; }
    }

}
