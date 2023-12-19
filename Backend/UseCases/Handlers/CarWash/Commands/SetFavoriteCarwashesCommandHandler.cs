namespace UseCases.Handlers.CarWash.Queries
{
    using DataAccess.Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class SetFavoriteCarwashesCommandHandler : IRequestHandler<SetFavoriteCarwashesCommand, bool>
    {
        private readonly IRepository<CarWash> _carWashesRepository;
        private readonly UserManager<User> _userManager;

        public SetFavoriteCarwashesCommandHandler(IRepository<CarWash> carWashesRepository, UserManager<User> userManager)
        {
            _carWashesRepository = carWashesRepository;
            _userManager = userManager;
        }

        public async Task<bool> Handle(SetFavoriteCarwashesCommand request, CancellationToken cancellationToken)
        {
            List<CarWash> carWashes = await _carWashesRepository.GetAllAsync(o => request.CarWashIds.Contains(o.Id));

            try
            {
                var user = await _userManager.Users.Include(item => item.FavouriteCarWashes).FirstOrDefaultAsync(item => item.Id == request.UserId, cancellationToken);
                if (user != null)
                {
                    user.FavouriteCarWashes = carWashes;
                }

                await _userManager.UpdateAsync(user);
            }
            catch (Exception exc)
            {
                return false;
            }

            return true;
        }
    }
}
