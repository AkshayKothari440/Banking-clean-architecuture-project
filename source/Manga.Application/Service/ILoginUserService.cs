using Manga.Application.Boundaries.Login;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Application.Services
{
    public interface ILoginUserService
    {
        LoginOutput Execute(IdentityUser user, string password);
        Task<IdentityUser> GetEmailUser(string email);
        Task<IdentityUser> GetNameUser(string name);
        Task<IdentityUser> GetMobileUser(string mobile);
    }
}