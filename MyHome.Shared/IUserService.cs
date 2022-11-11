using System.Threading.Tasks;

namespace MyHome.Shared
{
    public interface IUserService<T>
    {
        UserViewModel Create(string username, string email, string password, string passwordConfirmation);
        Task<T> AuthenticateAsync(string username, string password);
    }
}
