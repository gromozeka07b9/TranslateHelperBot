
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using TranslateHelperBot.Integrations.Yandex.Contracts;

namespace TranslateHelperBot.Integrations.Yandex
{
    public class DictionaryService
    {
        public async Task<YandexDictionaryScheme> Translate(string direction, string sourceString)
        {
            string apiKey = "dict.1.1.20151121T213256Z.ed41c64573f0a13d.96329576b18faa0c58e42b40bd81905b80f6d8a6";
            //string direction = "en-ru";
            string dictionaryQueryUrl = "https://dictionary.yandex.net/api/v1/dicservice.json";
            
            var requestResponse = await dictionaryQueryUrl
                .AppendPathSegment("lookup")
                .SetQueryParam("key", apiKey)
                .SetQueryParam("lang", direction)
                .SetQueryParam("text", sourceString)
                .GetAsync();

            var result = await requestResponse.GetStringAsync();
            return JsonSerializer.Deserialize<YandexDictionaryScheme>(result) ?? new YandexDictionaryScheme();
            //return res?.def?.FirstOrDefault()?.tr?.FirstOrDefault()?.text??string.Empty;
        }
    }
}