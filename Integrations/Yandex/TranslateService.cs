
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using TranslateHelperBot.Integrations.Yandex.Contracts;

namespace TranslateHelperBot.Integrations.Yandex
{
    public class TranslateService
    {
        public async Task<YandexTranslateScheme> TranslateAsync(string direction, string sourceString)
        {
            string apiKey = "xxx";// нет аккаунта!

            string dictionaryQueryUrl = "https://translate.yandex.net/api/v1.5/tr.json";
            
            var requestResponse = await dictionaryQueryUrl
                .AppendPathSegment("translate")
                .SetQueryParam("format", "plain")
                .SetQueryParam("key", apiKey)
                .SetQueryParam("lang", direction)
                .SetQueryParam("text", sourceString)
                .GetAsync();

            var result = await requestResponse.GetStringAsync();
            return JsonSerializer.Deserialize<YandexTranslateScheme>(result) ?? new YandexTranslateScheme();
            //return res?.def?.FirstOrDefault()?.tr?.FirstOrDefault()?.text??string.Empty;
        }
    }
}