using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Common.Entities;

namespace BuberDinner.Infrastructure.Persistance;


public class UserRepository : IUserRepository
{

    private static readonly List<User> Users = new();

    public void Add(User user)
    {
        Users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return Users.SingleOrDefault(x => x.Email == email);
    }
}