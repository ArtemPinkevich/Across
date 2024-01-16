using Microsoft.AspNetCore.SignalR;
using UseCases.Handlers.Authorization;

namespace Across.SignalRHubs
{
    public class SignalrUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(JwtClaimsTypes.Id)?.Value;
        }
    }
}
