﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Across.IdentityTokenProviders
{
    [Obsolete("Temporary Crutch to emulate sms gateway")]
    public class CrutchPhoneTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser>
        where TUser : class
    {
        private const int CrutchPhoneVerificationCode = 123456;

        public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            var phoneNumber = await manager.GetPhoneNumberAsync(user);

            return !string.IsNullOrWhiteSpace(phoneNumber) && await manager.IsPhoneNumberConfirmedAsync(user);
        }

        public override Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            return Task.FromResult(CrutchPhoneVerificationCode.ToString("D6", CultureInfo.InvariantCulture));
        }

        public override Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager,
            TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if(token == CrutchPhoneVerificationCode.ToString("D6", CultureInfo.InvariantCulture))
                return Task.FromResult(true);

            return Task.FromResult(false);
        }
    }
}
