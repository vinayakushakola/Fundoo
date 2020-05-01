using CommonLayer.Models;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.ADbcontext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public ResponseModel CreateAccount(SignUpRequestModel userSignUp)
        {
            try
            {
                //byte[] encodePassword = ASCIIEncoding.ASCII.GetBytes(userSignUp.Password);
                //string Password = Convert.ToBase64String(encodePassword);

                //byte[] bytesToEncode = Encoding.UTF8.GetBytes(userSignUp.Password);

                //string Password = Convert.ToBase64String(bytesToEncode);



                UserDetails user = new UserDetails()
                {
                    FirstName = userSignUp.FirstName,
                    LastName = userSignUp.LastName,
                    Email = userSignUp.Email,
                    Password = userSignUp.Password,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                ResponseModel responseData = new ResponseModel()
                {
                    ID = user.ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                return responseData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel userLogin(LoginRequest login)
        {
            try
            {
                string Password;
                byte[] decodedBytes;
                ResponseModel responseData = null;
                var users = _context.Users;
                foreach(UserDetails user in users)
                {
                    //decodePassword = Convert.FromBase64String(user.Password);
                    //Password = ASCIIEncoding.ASCII.GetString(decodePassword);
                    //decodedBytes = Convert.FromBase64String(user.Password);
                    //Password = Encoding.UTF8.GetString(decodedBytes);
                    if (user.Email == login.Email && user.Password == login.Password)
                    {
                        responseData = new ResponseModel()
                        {
                            ID = user.ID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email
                        };
                    }
                }
                return responseData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
