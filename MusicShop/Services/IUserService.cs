using MusicShop.Entities;

namespace MusicShop.Services
{
    public interface IUserService
    {
        void RegisterUser(string username, string password, string fullName, string emailAddress);
        bool VerifyUser(string username, string password, out User user);
    }
}
