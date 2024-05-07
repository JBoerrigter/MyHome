using MyHome.Shared;

namespace MyHome.Data.Homes
{
    public class DbHomeService : IHomeService
    {
        private readonly ApplicationDbContext dbContext;

        public DbHomeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Guid Create(Guid familyId, string street, string number, string postalCode, string city)
        {
            if (string.IsNullOrEmpty(street)) throw new ArgumentNullException(nameof(street));
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException(nameof(number));
            if (string.IsNullOrEmpty(postalCode)) throw new ArgumentNullException(nameof(postalCode));
            if (string.IsNullOrEmpty(city)) throw new ArgumentNullException(nameof(city));
            if (!dbContext.Families.Any(f => f.Id == familyId)) throw new KeyNotFoundException("Family does not exist");

            House entity = new House
            {
                FamilyId = familyId,
                Street = street,
                Number = number,
                PostalCode = postalCode,
                City = city
            };

            var result = dbContext.Houses.Add(entity);

            dbContext.SaveChanges();

            return result.Entity.Id;
        }

        public void Delete(Guid id)
        {
            House? entity = dbContext.Houses.Find(id);

            if (entity is not null)
            {
                dbContext.Houses.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<HomeViewModel> GetByFamilyId(Guid familyId)
        {
            return dbContext.Houses
                .Where(h => h.FamilyId == familyId)
                .Select(h => new HomeViewModel
                {
                    Id = h.Id,
                    Street = h.Street,
                    Number = h.Number,
                    PostalCode = h.Number,
                    City = h.City
                });
        }
    }
}

