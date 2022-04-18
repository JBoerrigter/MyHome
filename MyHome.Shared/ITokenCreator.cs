namespace MyHome.Shared
{
    public interface ITokenCreator<T> where T : class
    {
        string CreateToken(T user);
    }
}
