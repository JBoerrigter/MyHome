using Microsoft.EntityFrameworkCore;
using MyHome.Shared;
using MyHome.Shared.Exceptions;

namespace MyHome.Data.Users
{
    public class DbUserService : IUserService<ApplicationUser>
    {
        private readonly ApplicationDbContext dbContext;

        public DbUserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ApplicationUser> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null!;

            // generate the hash
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // get all users matching
            var users = from user in dbContext.Users
                        where user.UserName == username || user.Email == username
                        select user;

            // if user does not exist, authentication was not successful
            if (!users.Any()) return null!;

            // database is corrupt
            if (users.Count() > 1) throw new Exception("Please contact the administrator");

            var foundUser = await users.FirstAsync();

            // verify password
            if (!BCrypt.Net.BCrypt.Verify(password, foundUser.PasswordHash)) return null!;

            return new ApplicationUser
            {
                UserName = foundUser.UserName,
                Email = foundUser.Email,
                FamilyId = foundUser.FamilyId,
                TwoFactorEnabled = foundUser.TwoFactorEnabled,  // not sure if needed
                SecurityStamp = foundUser.SecurityStamp,        // not sure if needed
            };
        }

        public UserViewModel? Create(string username, string email, string password, string passwordConfirmation)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(passwordConfirmation)) throw new ArgumentNullException(nameof(passwordConfirmation));
            if (password != passwordConfirmation) throw new InvalidDataException("Passwords not matching");

            if (dbContext.Users.Any(u => u.UserName == username || u.Email == email))
            {
                Exception ex = new ExistsException("User");
                ex.Data.Add(nameof(username), username);
                ex.Data.Add(nameof(email), email);
                throw ex;
            }

            ApplicationUser user = new(username)
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            var result = dbContext.Users.Add(user);

            dbContext.SaveChanges();

            return new UserViewModel
            {
                Id = result.Entity.Id,
                Username = result.Entity.UserName,
                Email = result.Entity.Email
            };
        }
    }
}
