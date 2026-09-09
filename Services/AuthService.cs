using BookManagementAPI.DTOs.Auth;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);

        if (user == null)
        {
            throw new Exception("Invalid username or password.");
        }

        if (user.PasswordHash != dto.Password)
        {
            throw new Exception("Invalid username or password.");
        }

        return "JWT_TOKEN_WILL_BE_HERE";
    }
}