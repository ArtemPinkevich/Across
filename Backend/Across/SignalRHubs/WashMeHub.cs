using System;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Across.SignalRHubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public partial class WashMeHub: Hub
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public WashMeHub(IMediator mediator,
                         UserManager<User> userManager)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        //клиенты могут вызывать метод Send сервера
        public async Task Send(string message)
        {
            //клиенты подписываются на метод Notify
            //сервер вызовет метод Notify у клиентов
            await Clients.All.SendAsync("Notify", message);
        }

        [Authorize(Roles = UserRoles.Admin)]
        //клиенты могут вызывать метод GetShedule сервера
        public async Task GetShedule(string message)
        {
            //клиенты подписываются на метод Notify
            //сервер вызовет метод Notify у клиентов
            await Clients.All.SendAsync("Notify", message);
        }



        public override async Task OnConnectedAsync()
        {
            //var context = Context;
            ////отсюда можно вытянуть все данные которые добавлены в jwt токене авторизации Claims
            //var user = Context.User;
            ////тут будет идентификатор пользователя как в БД, так как определен класс SignalrUserIdProvider
            //var userId = Context.UserIdentifier;

            ////например вот этот метод отправляет клиенту с указанным Id какие-то данные
            //await Clients.User(userId).SendAsync("MethodName", "MethodData");

            //await Clients.Caller.SendAsync("InitializationResponse", $"initialization response");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
