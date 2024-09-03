using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class ChangeDocumentStatusCommandHandler: IRequestHandler<ChangeDocumentStatusCommand, ProfileResultDto>
{
    private readonly UserManager<User> _userManager;

    public ChangeDocumentStatusCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<ProfileResultDto> Handle(ChangeDocumentStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Include(x => x.Documents).FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
            return CreateErrorResult($"user not found with id {request.UserId}");

        var documents = user.Documents;
        var document = documents.FirstOrDefault(x => x.DocumentType == (DocumentType)request.DocumentType);
        if (document == null)
            return CreateErrorResult($"no document found with type {request.DocumentType}");

        var newDocumentStatus = (DocumentStatus)request.DocumentStatus;

        document.DocumentStatus = (DocumentStatus)request.DocumentStatus;
        document.Comment = request.Comment;

        if (newDocumentStatus == DocumentStatus.Rejected)
        {
            user.UserStatus = UserStatus.Unconfirmed;
        }

        await _userManager.UpdateAsync(user);

        return new ProfileResultDto() { Result = ApiResult.Success, };
    }
    
    private ProfileResultDto CreateErrorResult(string errorMessage)
    {
        return new ProfileResultDto()
        {
            Result = ApiResult.Failed,
            Reasons = new[] { errorMessage }
        };
    }
}