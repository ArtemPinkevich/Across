namespace UseCases.Exceptions;

public enum NotAuthorizedErrorCode
{
    None,
    PhoneNumberIsNotConfirmed,
    AccessTokenExpired,
    RefreshTokenExpired,
    NoUserFound,
    InternalServerError
}