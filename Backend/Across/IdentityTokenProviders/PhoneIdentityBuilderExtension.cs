using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWashMe.IdentityTokenProviders
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
