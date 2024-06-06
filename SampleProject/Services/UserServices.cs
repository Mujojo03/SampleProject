using SampleProject.IServices;
using SampleProject.Models;

namespace SampleProject.Services
{
    public class UserServices : IUserServices //interface implementation
    {
        //global variable
        private readonly List<User> _users = new List<User>();

        //to get a list of all users
        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        //to get the users by id
        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        //to get user by username
        public User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        //to get user by email
        public User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        //to get user by password
        public User GetUserByPassword(string password)
        {
            return _users.FirstOrDefault(u => u.Password == password);
        }

        //to add users using id
        public void AddUser(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
        }

        //for updating users
        public void UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
            }
        }

        //to delete a user
        public void DeleteUser(string email, string password)
        {
            var user = _users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                _users.Remove(user);
            }
        }
    }
}
