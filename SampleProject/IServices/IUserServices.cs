using SampleProject.Models;

namespace SampleProject.IServices
{
    public interface IUserServices
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        User GetUserByPassword(string password);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string email, string password);
    }
}
