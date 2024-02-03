using Assignment_API.Models;
using Assignment_API.Models.Dto;

namespace Assignment_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<LocalUser> Register(RegisterationRequestDto registerationRequestDto);
    }
}
