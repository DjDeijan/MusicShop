using MusicShop.Data;

namespace MusicShop.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime DateRegistered { get; set; }

        public string Role { get; set; } = UserRoles.User;
    }
}
