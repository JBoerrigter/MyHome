using MyHome.Web.Data;

namespace MyHome.Web.Services
{
    public interface IFamilyService
    {
        Task<Family?> GetFamily(string id);
        Task<Family?> GetFamilyAndHouses(string userId);
        Task<Family?> GetFamilyAndMembers(string userId);
        Task<Family?> GetFamilyAndMembersAndHouses(string userId);
    }
}