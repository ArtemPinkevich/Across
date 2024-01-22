﻿using MediatR;

namespace UseCases.Handlers.Authorization.Queries;

public class DriverAuthorizationQuery: IRequest<AuthorizationDto>
{
    public string Phone { set; get; }
        
    public string Password { set; get; }
}