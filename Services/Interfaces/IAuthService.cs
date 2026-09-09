using BookManagementAPI.DTOs.Auth;

namespace BookManagementAPI.Services.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginDto dto);
}