using Licensing.Models;

namespace Licensing.IRepositories
{
    public interface IUserRepository
    {
        public User createUser(User user);
        public bool updateUser(User updateUser);
        public List<User> getUserDetails();
        public List<string> getUserList();
    }
}
