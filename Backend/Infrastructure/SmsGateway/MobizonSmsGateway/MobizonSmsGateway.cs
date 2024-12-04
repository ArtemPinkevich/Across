using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Interfaces;
using Infrastructure.SmsGateway.Exceptions;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Wrap;

namespace Infrastructure.SmsGateway;

public class MobizonSmsGateway : ISmsGateway
{
    private const int DELAY_BETWEEN_FAILED_ATTEMPTS = 2;
    private const int MAX_ATTEMPTS_TO_SEND_FALIED_SMS = 3;
    private const string _messageTemplate = "recipient={0}&text={1} код подтверждения Compass&params[validity]=10";
    private const string _apiKey = "kzb558c4bed24d5de983db1b4a1ad4b131f67be83c79c292376153997df5c832118ab2";
    private const string _requestTemplate = "https://api.mobizon.kz/service/message/sendSmsMessage?output=json&api=v1&apiKey={0}";
    
    private readonly SmsGatewayConfiuration _smsGatewayConfiguration;
    private readonly HttpClient _httpClient;
    
    public MobizonSmsGateway(IOptions<SmsGatewayConfiuration> smsGatewayConfiguration)
    {
        _httpClient = new HttpClient();
        _smsGatewayConfiguration = smsGatewayConfiguration.Value ?? throw new ArgumentNullException(nameof(smsGatewayConfiguration));
    }
    
    public async Task SendSms(string phone, string text)
    {
        await TrySendSms(phone, text);
        return;
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
        if (!_smsGatewayConfiguration.UseRealGateway)
            return;

        var url = CreateUrl();
        var content = CreateContent(phone, text);
        _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
        {
            NoCache = true
        };
        var result = await _httpClient.PostAsync(CreateUrl(), CreateContent(phone, text));
        //if (result.StatusCode != HttpStatusCode.OK)
        //    throw new SendSmsErrorException(phone, (int)result.StatusCode, "Send SMS error");
    }
    
    private string CreateUrl()
    {
        return String.Format(_requestTemplate, HttpUtility.UrlEncode(_apiKey));
    }

    private StringContent CreateContent(string phone, string text)
    {
        return new StringContent(String.Format(_messageTemplate, phone, HttpUtility.UrlEncode(text)), Encoding.UTF8,
            "application/x-www-form-urlencoded");
    }
    
    private Task LogError()
    {
        //TODO сделать логирование ошибок SMS Gateway
        return Task.CompletedTask;
    }
}