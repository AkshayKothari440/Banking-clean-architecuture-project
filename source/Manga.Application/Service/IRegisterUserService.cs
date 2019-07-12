using Manga.Application.Boundaries.Register;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Application.Service
{
    public interface IRegisterUserService
    {
        //Guid Execute(string username, string password);
        RegisterOutput Execute(string username, string password,string email,string mobile);
        Task<IdentityUser> GetEmailUser(string email);
        Task<IdentityUser> GetNameUser(string name);
        Task<IdentityUser> GetMobileUser(string mobile);
    }
}
