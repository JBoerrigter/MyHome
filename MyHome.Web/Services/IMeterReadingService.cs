using MyHome.Web.Data;
using MyHome.Web.ViewModels;

namespace MyHome.Web.Services
{
    public interface IMeterReadingService
    {
        // Types
        IQueryable<MeterReadingType> GetTypesQueryable();
        Task<IEnumerable<MeterReadingType>> GetTypesAsync();
        Task<MeterReadingType?> CreateTypeAsync(MeterReadingTypeViewModel model);
        Task DeleteTypeAsync(string id);

        // Readings
        Task<MeterReading?> GetMeterReadingAsync(string id);
        Task<MeterReading?> CreateMeterReadingAsync(MeterReadingViewModel model);
        Task DeleteMeterReadingAsync(string id);
    }
}
