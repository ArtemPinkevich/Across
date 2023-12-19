namespace UseCases.Handlers.Profile.Commands
{
    using Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public UpdateProfileCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(item => item.Id == request.UserId, cancellationToken);
                if (user != null)
                {
                    user.Name = request.ProfileDto.Name;
                    user.Surname = request.ProfileDto.Surname;
                    user.Patronymic = request.ProfileDto.Patronymic;
                    user.BirthDate = request.ProfileDto.BirthDate;
                    user.Gender = request.ProfileDto.Gender;

                    await _userManager.UpdateAsync(user);
                }
            }
            catch (Exception exc)
            {
                return false;
            }

            return true;
        }

    }
}
