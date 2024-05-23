using MyHome.Web.Data;
using MyHome.Web.ViewModels;

namespace MyHome.Web.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly ILogger<IncomeService> _logger;
        private readonly ApplicationDbContext _dbContext;

        public IncomeService(ILogger<IncomeService> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IQueryable<IncomeType> GetIncomeTypesQueryable()
        {
            return _dbContext.IncomeTypes.AsQueryable();
        }

        public async Task<IncomeType?> CreateIncomeTypeAsync(IncomeTypeViewModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var type = new IncomeType
            {
                Name = model.Name
            };

            await _dbContext.IncomeTypes.AddAsync(type);
            await _dbContext.SaveChangesAsync();

            return type;
        }

        public async Task DeleteIncomeTypeAsync(string id)
        {
            var entity = await _dbContext.IncomeTypes.FindAsync(id);

            if (entity is not null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();

            }
        }
    }
}
