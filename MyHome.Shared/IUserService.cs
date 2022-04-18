namespace MyHome.Shared
{
    public interface IUserService<T>
    {
        UserViewModel Create(string username, string email, string password, string passwordConfirmation);
        T Authenticate(string username, string password);
    }
}
