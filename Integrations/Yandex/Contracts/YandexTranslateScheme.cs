using System.Collections.Generic;

namespace TranslateHelperBot.Integrations.Yandex.Contracts
{
    public class YandexTranslateScheme
    {
        public int Code { get; set; }
        public string Lang { get; set; }
        public List<string> Text { get; set; }
    }
}
