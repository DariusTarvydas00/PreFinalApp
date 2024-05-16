using DataAccess.Models;

namespace DataAccess.IRepositories;

public interface IUserRepository
{
    List<User?> FindAll();
    User? GetById(int id);
    User? GetByUserName(string userName);
    User? Create(User entity);
    User Update(User entity);
    User Delete(int id);
}