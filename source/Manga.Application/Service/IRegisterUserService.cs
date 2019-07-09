using Manga.Application.Boundaries.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Application.Service
{
    public interface IRegisterUserService
    {
        //Guid Execute(string username, string password);
        RegisterOutput Execute(string username, string password);
    }
}
