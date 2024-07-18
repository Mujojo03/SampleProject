using SampleProject.Models;

namespace SampleProject.IServices
{
    public interface IUserServices
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
        User GetUserByPassword(string password);
        User GetUser(string email, string password); //change the parameters
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
