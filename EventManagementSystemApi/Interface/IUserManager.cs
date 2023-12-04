using EventManagementSystemApi.Models;

namespace EventManagementSystemApi.Interface
{
    public interface IUserManager
    {
        string AuthenticateUser(string email, string password);
        User GetUserByEmail(string email);
        string RegisterUser(string name, string email, string password);
        public List<object> GetUsers();
    }
}