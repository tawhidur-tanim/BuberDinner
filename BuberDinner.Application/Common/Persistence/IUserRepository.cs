using BuberDinner.Domain.Common.Entities;

namespace BuberDinner.Application.Common.Persistence;


public interface IUserRepository
{
    void Add(User user);

    User? GetUserByEmail(string email);
}