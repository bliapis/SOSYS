using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace LT.SO.Domain.Core.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}