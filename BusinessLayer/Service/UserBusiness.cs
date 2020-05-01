using BusinessLayer.Interface;
using CommonLayer.Models;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository signUpRepository;
        public UserBusiness(IUserRepository isignUpRepository)
        {
            signUpRepository = isignUpRepository;
        }
        public ResponseModel CreateAccount(SignUpRequestModel userSignUp)
        {
            ResponseModel responseData = signUpRepository.CreateAccount(userSignUp);
            return responseData;
        }

        public ResponseModel UserLogin(LoginRequest login)
        {
            ResponseModel responseData = signUpRepository.userLogin(login);
            return responseData;
        }
    }
}
