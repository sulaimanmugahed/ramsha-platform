using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Ramsha.Application.Constants;
using Ramsha.Application.Contracts.Identity;
using Ramsha.Application.Dtos.Permissions;
using Ramsha.Application.Helpers;
using Ramsha.Caching;
using Ramsha.Identity.Extensions;

namespace Ramsha.Identity.Services;

public class PermissionClaimsTransformation(IServiceScopeFactory serviceScopeFactory, IRamshaCache redisCacheService)
    : IClaimsTransformation
{

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {

        if (principal.Identity?.IsAuthenticated == true
            && !principal.HasClaim(c => c.Type == ApplicationClaims.Permission))
        {
            var username = principal.Identity.Name;
            var cacheKey = CacheKeysHelper.PermissionsCacheKeys.GetUserPermissionsKey(username);
            var permissions = await redisCacheService.GetOrCreateAsync<IEnumerable<PermissionDto>>(
                cacheKey,
                async (ct) =>
                {
                    using var scope = serviceScopeFactory.CreateScope();
                    var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
                    return await permissionService.GetPermissionsForUserAsync(username);
                },
                new RamshaCacheEntryOptions
                {
                    Expiration = TimeSpan.FromHours(6)
                },
                [CacheKeysHelper.PermissionsCacheKeys.Tag]);


            var claims = permissions.Select(permission => new Claim(ApplicationClaims.Permission, ((int)permission.PermissionType).ToString()));
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);
            principal.AddIdentity(claimsIdentity);
        }

        return principal;
    }
}
