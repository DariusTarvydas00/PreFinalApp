using DataAccess.IRepositories;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class UserRepository(MainDbContext mainDbContext) : IUserRepository
{
    public List<User?> FindAll()
    {
        return mainDbContext.Users.ToList();
    }

    public User? GetById(int id)
    {
        return mainDbContext.Users.FirstOrDefault(user => user != null && user.Id == id);
    }

    public User? GetByUserName(string userName)
    {
        return mainDbContext.Users.FirstOrDefault(user => user != null && user.UserName.Equals(userName));
    }

    public User? Create(User? entity)
    {
        var user = mainDbContext.Users.Add(entity);
        mainDbContext.SaveChanges();
        return user.Entity;
    }

    public User Update(User entity)
    {
        var user = mainDbContext.Users.FirstOrDefault(user1 => user1 != null && user1.Id == entity.Id);
        if (user == null)
        {
            throw new InvalidDataException("User does not exist with such Id");
        }
        user.UserName = entity.UserName;
        user.PasswordHash = entity.PasswordHash;
        mainDbContext.SaveChanges();
        return user;
    }

    public User Delete(int id)
    {
        var user = mainDbContext.Users.FirstOrDefault(user1 => user1 != null && user1.Id == id);
        if (user == null)
        {
            throw new InvalidDataException("User does not exist with such Id");
        }
        mainDbContext.Users.Remove(user);
        return user;
    }
}