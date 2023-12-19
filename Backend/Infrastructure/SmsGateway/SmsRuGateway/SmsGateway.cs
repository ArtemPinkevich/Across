using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.SmsGateway.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Polly.Wrap;

namespace Infrastructure.SmsGateway
{
    /// <summary>
    /// https://sms.ru
    /// Sms Gateway from https://sms.ru
    /// </summary>
    public class SmsGateway : ISmsGateway
    {
        private const int DELAY_BETWEEN_FAILED_ATTEMPTS = 2;
        private const int MAX_ATTEMPTS_TO_SEND_FALIED_SMS = 3;

        private readonly HttpClient _httpClient;

        private readonly SmsGatewayConfiuration _smsGatewayConfiuration;

        private const string _requestTemplate =
            "https://sms.ru/sms/send?api_id={0}&to={1}&msg={2}&json=1";

        public SmsGateway(IOptions<SmsGatewayConfiuration> smsGatewayConfiuration)
        {
            _httpClient = new HttpClient();
            _smsGatewayConfiuration = smsGatewayConfiuration.Value ?? throw new ArgumentNullException(nameof(smsGatewayConfiuration));
        }

        public async Task SendSms(string phone, string text)
        {
            var policyWrap = CreatePolicyWrap();
            await policyWrap.ExecuteAsync(async () =>
            {
                await TrySendSms(phone, text);
            });
        }

        private AsyncPolicyWrap CreatePolicyWrap()
        {
            return Policy.WrapAsync(CreateFallbackPolicy(), CreateRetryPolicy());
        }

        private IAsyncPolicy CreateRetryPolicy()
        {
            return Policy
                    .Handle<HttpRequestException>()
                    .Or<SendSmsErrorException>()
                    .WaitAndRetryAsync(
                    MAX_ATTEMPTS_TO_SEND_FALIED_SMS,
                    i => TimeSpan.FromSeconds(DELAY_BETWEEN_FAILED_ATTEMPTS));
        }

        private IAsyncPolicy CreateFallbackPolicy()
        {
            return Policy
                    .Handle<HttpRequestException>()
                    .Or<SendSmsErrorException>()
                    .FallbackAsync(async cancelationToken =>
                    {
                        await LogError();
                    });
        }

        private async Task TrySendSms(string phone, string text)
        {
            if (!_smsGatewayConfiuration.UseRealGateway)
                return;

            await using (Stream stream = await _httpClient.GetStreamAsync(CreateUrl(phone, text)))
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new JsonSerializer();
                SendSmsResponse p = serializer.Deserialize<SendSmsResponse>(reader);
                if (p.Status != SmsStatus.Ok)
                    throw new SendSmsErrorException(phone, p.StatusCode, "Send SMS error");
            }
        }

        private string CreateUrl(string phone, string message)
        {
            var queryMessage = message.Replace(' ', '+');
            return String.Format(_requestTemplate, _smsGatewayConfiuration.ApiId, phone, queryMessage);
        }

        private Task LogError()
        {
            //TODO сделать логирование ошибок SMS Gateway
            return Task.CompletedTask;
        }
    }
}
