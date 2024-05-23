using Microsoft.EntityFrameworkCore;
using MyHome.Web.Data;
using MyHome.Web.ViewModels;

namespace MyHome.Web.Services
{
    public class HouseService : IHouseService
    {
        private readonly ILogger<HouseService> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HouseService(ILogger<HouseService> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<House?> GetHouseAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            return await _dbContext.Houses.FindAsync(id);
        }

        public async Task<House?> GetHouseAndReadingsAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            return await _dbContext.Houses
                .Include(h => h.MeterReadings)
                .ThenInclude(m => m.ReadingType)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<House?> CreateHouseAsync(string familyId, HouseViewModel model)
        {
            if (string.IsNullOrEmpty(familyId)) throw new ArgumentNullException(nameof(familyId));

            House? house = _dbContext.Houses.FirstOrDefault(h =>
                h.Street == model.Street &&
                h.Number == model.Number &&
                h.PostalCode == model.PostalCode);

            // todo: think about buying / selling
            if (house is not null) throw new Exception("House does already exist");

            house = new House
            {
                FamilyId = familyId,
                Street = model.Street,
                Number = model.Number,
                PostalCode = model.PostalCode,
                City = model.City
            };

            await _dbContext.Houses.AddAsync(house);
            await _dbContext.SaveChangesAsync();

            return house;
        }

        public async Task DeleteAsync(string id)
        {
            var house = await _dbContext.Houses.FindAsync(id);

            if (house is not null)
            {
                _dbContext.Houses.Remove(house);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
