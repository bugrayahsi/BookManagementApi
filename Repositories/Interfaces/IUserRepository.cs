using BookManagementAPI.Models;

namespace BookManagementAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}