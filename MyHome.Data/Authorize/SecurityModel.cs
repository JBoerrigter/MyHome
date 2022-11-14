namespace MyHome.Data.Authorize
{
    public class SecurityModel
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public int DaysToExpire { get; set; }
    }
}
