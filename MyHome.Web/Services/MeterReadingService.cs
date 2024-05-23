using Microsoft.EntityFrameworkCore;
using MyHome.Web.Data;
using MyHome.Web.ViewModels;

namespace MyHome.Web.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly ILogger<MeterReadingService> _logger;
        private readonly ApplicationDbContext _dbContext;

        public MeterReadingService(ILogger<MeterReadingService> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IQueryable<MeterReadingType> GetTypesQueryable()
        {
            return _dbContext.MetersReadingTypes.AsQueryable();
        }

        public async Task<IEnumerable<MeterReadingType>> GetTypesAsync()
        {
            return await _dbContext.MetersReadingTypes.ToListAsync();
        }

        public async Task<MeterReadingType?> CreateTypeAsync(MeterReadingTypeViewModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var type = new MeterReadingType
            {
                Name = model.Name,
                Unit = model.Unit
            };

            await _dbContext.MetersReadingTypes.AddAsync(type);
            await _dbContext.SaveChangesAsync();

            return type;
        }

        public async Task DeleteTypeAsync(string id)
        {
            var entity = await _dbContext.MetersReadingTypes.FindAsync(id);

            if (entity is not null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<MeterReading?> CreateMeterReadingAsync(MeterReadingViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserId)) throw new ArgumentNullException(nameof(model.UserId));
            if (string.IsNullOrEmpty(model.HouseId)) throw new ArgumentNullException(nameof(model.HouseId));
            if (string.IsNullOrEmpty(model.MeterReadingTypeId)) throw new ArgumentNullException(nameof(model.MeterReadingTypeId));

            // todo: more validation

            MeterReading reading = new()
            {
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,

                // References
                UserId = model.UserId,
                HouseId = model.HouseId,
                ReadingTypeId = model.MeterReadingTypeId,

                // Values
                Year = model.Year,
                Value = model.Value,
                Description = model.Description,
                Image = model.Image,
            };

            await _dbContext.MetersReadings.AddAsync(reading);
            await _dbContext.SaveChangesAsync();

            return reading;
        }

        public async Task DeleteMeterReadingAsync(string id)
        {
            var entity = await _dbContext.MetersReadings.FindAsync(id);

            if (entity is not null)
            {
                _dbContext.MetersReadings.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<MeterReading?> GetMeterReadingAsync(string id)
        {
            return await _dbContext.MetersReadings
                .Include(m => m.ReadingType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
