using Exercise.DataBase;
using Exercise.Dto;
using Exercise.Models;
using Exercise.Respo.Interface;
using Exercise.Service.Interface;
using Microsoft.EntityFrameworkCore;
using WebApi.TokenConfig;

namespace Exercise.Service.Service
{
    public class AdminService : IAdminService
    {
        private readonly IRepo _irepo;
        private readonly DBContext _dBContext;
        private readonly Token _token;
        public AdminService(DBContext dbContext, IRepo repo, Token token)
        {
            _dBContext = dbContext;
            _irepo = repo;
            _token = token;
        }
        public Admin Login(UserDto user)
        {
            try
            {
                var userlogin = _irepo.GetByUserName(user.UserName);

                if (user.UserName == null)
                {
                    throw new ArgumentNullException(nameof(user), "UserName doesn't existe !!");
                }
                if (!BCrypt.Net.BCrypt.Verify(user.Password, userlogin.Password))
                {
                    throw new Exception("Wrong password!");

                }

                string token = _token.CreateToken(userlogin);

                return userlogin;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }

        public string Register(UserDto user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "UserDto cannot be null");
                }

                Admin admin = new Admin
                {
                    UserName = user.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                };

                _dBContext.Add(admin);
                _dBContext.SaveChanges();

                return ("Register Success!!");
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }



    }
}
