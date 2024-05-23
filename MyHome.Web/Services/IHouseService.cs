using MyHome.Web.Data;
using MyHome.Web.ViewModels;

namespace MyHome.Web.Services
{
    public interface IHouseService
    {
        Task<House?> GetHouseAsync(string id);
        Task<House?> GetHouseAndReadingsAsync(string id);
        Task<House?> CreateHouseAsync(string familyId, HouseViewModel model);
        Task DeleteAsync(string id);
    }
}
