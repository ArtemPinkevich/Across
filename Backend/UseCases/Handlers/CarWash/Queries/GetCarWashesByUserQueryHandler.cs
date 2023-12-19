namespace UseCases.Handlers.CarWash.Queries
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.CarWash.Dto;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;

    public class GetCarWashesByUserQueryHandler : IRequestHandler<GetCarWashesByUserQuery, CarWashesListResultDto>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GetCarWashesByUserQueryHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CarWashesListResultDto> Handle(GetCarWashesByUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users
                    .Include(item => item.CarWashes)
                    .FirstOrDefaultAsync(item => item.Id == request.UserId, cancellationToken);
                return _mapper.Map<CarWashesListResultDto>(user.CarWashes);
            }
            catch(Exception exc)
            {
                return null;
            }
        }
    }
}
