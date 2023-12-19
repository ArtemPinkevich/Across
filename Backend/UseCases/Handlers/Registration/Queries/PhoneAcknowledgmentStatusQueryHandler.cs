namespace UseCases.Handlers.Registration.Queries
{
    using Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;

    public class PhoneAcknowledgmentStatusQueryHandler : IRequestHandler<PhoneAcknowledgmentStatusQuery, bool>
    {
        private readonly UserManager<User> _userManager;

        public PhoneAcknowledgmentStatusQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(PhoneAcknowledgmentStatusQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(query.PhoneNumber);
            return user?.PhoneNumberConfirmed ?? false;
        }
    }
}
