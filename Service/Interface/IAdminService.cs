using Exercise.Dto;
using Exercise.Models;

namespace Exercise.Service.Interface
{
    public interface IAdminService
    {
        string Register(UserDto user);
        Admin Login(UserDto user);
    }
}
