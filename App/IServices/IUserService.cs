using DataAccess.Models;

namespace App.IServices;

public interface IUserService
{
    List<User?> FindAll();
    User? GetById(int id);
    User? GetByUserName(string username);
    User? Create(User? model);
    User? Update(User? model);
    User? Delete(int id);
}