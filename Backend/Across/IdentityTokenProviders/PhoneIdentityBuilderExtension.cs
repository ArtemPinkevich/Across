using System;
using Microsoft.AspNetCore.Identity;

namespace Across.IdentityTokenProviders
{
    [Obsolete("Temporary Crutch to emulate sms gateway")]
    public static class CrutchIdentityBuilderExtension
    {
        public static IdentityBuilder AddCrutchTokenProviders(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var phoneNumberProviderType = typeof(CrutchPhoneTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider(TokenOptions.DefaultPhoneProvider, phoneNumberProviderType);
        }
    }
}
