using MediatR;
using System.Collections.Generic;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Queries
{
    public class GetShippersAndDriversQuery : IRequest<List<ProfileDto>>
    {
    }
}