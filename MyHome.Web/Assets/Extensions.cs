using System.Security.Claims;

namespace MyHome.Web
{
    public static class Extensions
    {

        public static string GetId(this ClaimsPrincipal principal)
        {
            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return null;
            }

            return userId.Value;
        }

    }
}
