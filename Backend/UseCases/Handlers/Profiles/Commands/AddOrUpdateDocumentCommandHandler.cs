﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class AddOrUpdateDocumentCommandHandler: IRequestHandler<AddOrUpdateDocument, ProfileResultDto>
{
    private readonly UserManager<User> _userManager;

    public AddOrUpdateDocumentCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<ProfileResultDto> Handle(AddOrUpdateDocument request, CancellationToken cancellationToken)
    {
        User user = await _userManager.Users
            .Include(x => x.Documents)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new Exception($"no user found with id {request.UserId}");
        }

        var doc = user.Documents.FirstOrDefault(x => x.DocumentType == request.DocumentType);
        if (doc == null)
        {
            user.Documents.Add(new Document()
            {
                DocumentType = request.DocumentType,
                DocumentStatus = DocumentStatus.Verification,
                Comment = request.Comment,
                UserId = user.Id
            });
        }
        else
        {
            doc.DocumentStatus = DocumentStatus.Verification;
        }

        await _userManager.UpdateAsync(user);

        return new ProfileResultDto()
        {
            Result = ApiResult.Success,
        };
    }
}