using Manga.Application.Authentication;
using Manga.Application.Boundaries.Register;
using Manga.Application.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Manga.Infrastructure.IdentityAuthentication
{
    public sealed class RegisterUser: IRegisterUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IGenerateToken generateToken;
        private RegisterOutput Output { get; set; }
        public RegisterUser(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IGenerateToken generateToken)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.generateToken = generateToken;
        }
        public RegisterOutput Execute(string username,string password,string email,string mobile)
        {
            return RegistrationAsync(username, password,email,mobile).Result;
        }
        public async Task<IdentityUser> GetEmailUser(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityUser> GetNameUser(string name)
        {
            return await userManager.FindByNameAsync(name);
        }
        public async Task<IdentityUser> GetMobileUser(string mobile)
        {
            return userManager.Users.SingleOrDefault(m => m.PhoneNumber == mobile);
        }
        private async Task<RegisterOutput> RegistrationAsync(string username, string password,string email,string mobile)
        {
            var user = new IdentityUser { UserName = username,Email=email,PhoneNumber=mobile};
            var Result = await userManager.CreateAsync(user, password);
            if(Result.Succeeded)
            {
                //var debug = await generateToken.GetToken(username, user);
                //return new Guid(user.Id);
                await signInManager.SignInAsync(user, isPersistent: false);

                var token = await generateToken.GetToken(username, user);


                return new RegisterOutput { CustomerId = new Guid(user.Id), Token = token };
            }
            return null;
        }
    }
}
