﻿using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repository.IRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
