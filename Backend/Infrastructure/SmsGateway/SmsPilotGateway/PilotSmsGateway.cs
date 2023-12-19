using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.SmsGateway.Exceptions;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Wrap;

namespace Infrastructure.SmsGateway;

public class PilotSmsGateway : ISmsGateway
{
    private const int DELAY_BETWEEN_FAILED_ATTEMPTS = 2;
    private const int MAX_ATTEMPTS_TO_SEND_FALIED_SMS = 3;
    
    private const string _messageTemplate = "{0} - код подтверждения Pomoycar.";
    private const string _apiKey = "6E46LVM07602Q01L3CH1RD5HIJ0VBG7Q9Y8W25ZVC59L9STF2HEWA1Q5MO8H3UZ2";
    private const string _requestTemplate = "http://smspilot.ru/api.php?send={0}&to={1}&from=INFORM&apikey={2}";
    
    private readonly SmsGatewayConfiuration _smsGatewayConfiguration;
    private readonly HttpClient _httpClient;
    
    public PilotSmsGateway(IOptions<SmsGatewayConfiuration> smsGatewayConfiguration)
    {
        _httpClient = new HttpClient();
        _smsGatewayConfiguration = smsGatewayConfiguration.Value ?? throw new ArgumentNullException(nameof(smsGatewayConfiguration));
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
        if (!_smsGatewayConfiguration.UseRealGateway)
            return;
        
        var url = CreateUrl(phone, text);
        var result = await _httpClient.GetAsync(url);
        if (result.StatusCode != HttpStatusCode.OK)
            throw new SendSmsErrorException(phone, (int)result.StatusCode, "Send SMS error");
    }
    
    private string CreateUrl(string phone, string message)
    {
        var messageFormat = String.Format(_messageTemplate, message);
        return String.Format(_requestTemplate, messageFormat, phone, _apiKey);
    }
    
    private Task LogError()
    {
        //TODO сделать логирование ошибок SMS Gateway
        return Task.CompletedTask;
    }
}