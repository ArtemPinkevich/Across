using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ISmsGateway
    {
        Task SendSms(string phone, string text);
    }
}
