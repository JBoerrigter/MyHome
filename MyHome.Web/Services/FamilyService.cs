using Microsoft.EntityFrameworkCore;
using MyHome.Web.Data;

namespace MyHome.Web.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly ILogger<FamilyService> _logger;
        private readonly ApplicationDbContext _dbContext;

        public FamilyService(ILogger<FamilyService> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Family?> GetFamily(string id)
        {
            return await _dbContext.Families.FindAsync(id);
        }

        public async Task<Family?> GetFamilyAndMembers(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null) throw new KeyNotFoundException("User does not exist");
            return await _dbContext.Families.Include(f => f.Members).FirstOrDefaultAsync(f => f.Id == user.FamilyId);
        }

        public async Task<Family?> GetFamilyAndHouses(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null) throw new KeyNotFoundException("User does not exist");
            return await _dbContext.Families.Include(f => f.Houses).FirstOrDefaultAsync(f => f.Id == user.FamilyId);
        }

        public async Task<Family?> GetFamilyAndMembersAndHouses(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null) throw new KeyNotFoundException("User does not exist");
            return await _dbContext.Families.Include(f => f.Members).Include(f => f.Houses).FirstOrDefaultAsync(f => f.Id == user.FamilyId);
        }
    }
}
