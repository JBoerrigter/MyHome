using MyHome.Shared;

namespace MyHome.Data.Services
{
    public class MockUserService : IUserService<ApplicationUser>
    {
        public ApplicationUser? Authenticate(string username, string password)
        {
            if (username == "test@test.de" && password == "test123")
            {
                return new ApplicationUser(username);
            }
            return null;
        }

        public UserViewModel? Create(string username, string email, string password, string passwordConfirmation)
        {
            if (password != passwordConfirmation) return null;

            // simulate db
            return new UserViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Email = email
            };

        }

    }
}
