using App.IServices;
using DataAccess.IRepositories;
using DataAccess.Models;

namespace App.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public List<User?> FindAll()
    {
        return userRepository.FindAll();
    }
    
    public User? GetById(int id)
    {
        return userRepository.GetById(id);
    }
    
    public User? GetByUserName(string userName)
    {
        return userRepository.GetByUserName(userName);
    }

    public User? Create(User? model)
    {
        return userRepository.Create(model ?? throw new ArgumentNullException(nameof(model)));
    }

    public User Update(User? model)
    {
        return userRepository.Update(model ?? throw new ArgumentNullException(nameof(model)));
    }

    public User Delete(int id)
    {
        return userRepository.Delete(id);
    }
}