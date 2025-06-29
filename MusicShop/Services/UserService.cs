using MusicShop.Models;
using MusicShop.Entities;
using MusicShop.Extensions;

namespace MusicShop.Services
{
    public class UserService : IUserService
    {
        private readonly MusicShopContext _context;
        public UserService(MusicShopContext context)
        {
            _context = context;
        }

        public void RegisterUser(string username, string password, string fullName, string emailAddress)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username);

            if (user is not null)
                throw new ArgumentException("This username is already in use");
            if (username.Length < 4 || username.Length > 16)
                throw new ArgumentException("Username must be between 4 and 16 characters");
            if (!username.ContainsLatinCharsAndDigits())
                throw new ArgumentException("Username must contain only latin letters and digits");
            if (password.Length < 8)
                throw new ArgumentException("Password length must be equal to or greater than 8");
            if (!password.ContainsAtLeastOneLetterAndDigit())
                throw new ArgumentException("Password must contain at least one letter and one digit");
            if (!emailAddress.IsValidEmailAddress())
                throw new ArgumentException("Invalid email address");

            var newUser = new User()
            {
                Username = username,
                Password = Hasher.HashPasword(password),
                FullName = fullName,
                Email = emailAddress,
                DateRegistered = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public bool VerifyUser(string username, string password, out User user)
        {
            user = _context.Users.FirstOrDefault(x => x.Username == username);

            if (user is null)
                return false;

            string[] hashNSalt = user.Password.Split('$');

            return Hasher.VerifyPassword(password, hashNSalt[0], hashNSalt[1]);
        }
    }
}
