using CommonLayer.Models;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        ResponseModel CreateAccount(SignUpRequestModel userSignUp);

        ResponseModel UserLogin(LoginRequest login);

        List<ResponseModel> GetUsersData();

        ResponseModel ForgotPassword(ForgotPassword forgotPassword);

    }
}
