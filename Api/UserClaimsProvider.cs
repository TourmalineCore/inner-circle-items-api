﻿using System.Security.Claims;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace Api
{
    public class UserClaimsProvider : IUserClaimsProvider
    {
        public const string PermissionClaimType = "permissions";

        public const string AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed = "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed";
        public const string CanManageItemsTypes = "CanManageItemsTypes";
        public const string CanViewItemsTypes = "CanViewItemsTypes";

        public Task<List<Claim>> GetUserClaimsAsync(string login)
        {
            throw new NotImplementedException();
        }
    }
}
