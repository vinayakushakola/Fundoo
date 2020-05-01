using CommonLayer.Models;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        ResponseModel CreateAccount(SignUpRequestModel userSignUp);

        ResponseModel userLogin(LoginRequest login);

    }
}
