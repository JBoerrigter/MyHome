using MyHome.Web.Data;
using MyHome.Web.ViewModels;

namespace MyHome.Web.Services
{
    public interface IIncomeService
    {
        IQueryable<IncomeType> GetIncomeTypesQueryable();
        Task<IncomeType?> CreateIncomeTypeAsync(IncomeTypeViewModel model);
        Task DeleteIncomeTypeAsync(string id);
    }
}