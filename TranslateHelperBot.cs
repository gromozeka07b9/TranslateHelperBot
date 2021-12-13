using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TranslateHelperBot.Integrations.Yandex;
using TranslateHelperBot.Integrations.Yandex.Contracts;

namespace TranslateHelperBot
{
    public class TranslateHelperBot
    {
        private readonly string _token;
        private readonly DictionaryService _dictionaryService;
        private string botUserName = String.Empty;

        public TranslateHelperBot(string token)
        {
            this._token = token;
            this._dictionaryService = new DictionaryService();
        }

        public async Task StartAsync()
        {
            var botClient = new TelegramBotClient(this._token);
            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            var me = await botClient.GetMeAsync(cancellationToken: cts.Token);
            this.botUserName = me.Username ?? String.Empty;
            Console.WriteLine($"User id:{me.Id}");
            Console.WriteLine($"User name :{me.Username}.");
            Console.WriteLine($"User language code :{me.LanguageCode}.");
            Console.WriteLine($"User CanReadAllGroupMessages :{me.CanReadAllGroupMessages}.");

            //DictionaryService srv = new DictionaryService();
            //await srv.Translate();
        }
        
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            string messageText = update.Message.Text ?? String.Empty;
            if (messageText.StartsWith("@") && !string.IsNullOrEmpty(this.botUserName))
            {
                messageText = messageText.Replace("@" + this.botUserName, "").Trim();
            }
            if (!string.IsNullOrEmpty(messageText))
            {
                string directionName = Regex.IsMatch(messageText, @"[^a-z]", RegexOptions.IgnoreCase) ? "ru-en" : "en-ru";
                var yandexDictionarySchemeResponse = await _dictionaryService.Translate(directionName, messageText);
                string tsCaption = yandexDictionarySchemeResponse.def.Any() ? $"[{yandexDictionarySchemeResponse.def.FirstOrDefault()?.ts}]" : "";
                string answer = $"<b>{messageText}</b>  <i>{tsCaption}</i>" + Environment.NewLine;

                if (yandexDictionarySchemeResponse.def.Any())
                {
                    foreach (var def in yandexDictionarySchemeResponse.def)
                    {
                        answer += def.pos + ": ";
                        answer += string.Join(", ", def.tr.Select(t => t.text).ToArray());
                        answer += Environment.NewLine;
                    }
                }
                else
                {
                    answer += "Не знаю такого слова.";
                }

                if (!string.IsNullOrEmpty(answer))
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: answer,
                        parseMode:ParseMode.Html,
                        replyMarkup:replyKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
            }            
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new []
        {
            new KeyboardButton[] { "/favorites", "/tests️" },
        })
        {
            ResizeKeyboard = true
        };

        InlineKeyboardMarkup inlineKeyboard = new(new []
        {
            // first row
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Добавить в избранное", callbackData: "11"),
                InlineKeyboardButton.WithCallbackData(text: "Удалить", callbackData: "12"),
            }
        });

    }
}