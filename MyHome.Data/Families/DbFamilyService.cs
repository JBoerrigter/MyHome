using System;
using MyHome.Shared;
using MyHome.Shared.ViewModels;

namespace MyHome.Data.Families
{
    public class DbFamilyService : IFamilyService
    {
        private readonly ApplicationDbContext dbContext;

        public DbFamilyService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Guid Create(string name, Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentNullException(nameof(name));

            ApplicationUser? user = dbContext.Users.Find(userId);
            if (user is null) throw new KeyNotFoundException("User not found");

            if (user.FamilyId is not null) throw new ArgumentException("Only one family allowed");

            Family entity = new Family
            {
                Name = name
            };

            var result = dbContext.Families.Add(entity);
            dbContext.SaveChanges();

            user.FamilyId = result.Entity.Id;
            dbContext.SaveChanges();

            return result.Entity.Id;
        }

        public FamilyViewModel? Get(Guid userId)
        {
            var user = dbContext.Users.Find(userId);
            if (user is null) throw new KeyNotFoundException("User not found");

            Family? entity = dbContext.Families.Find(user.FamilyId);
            FamilyViewModel? viewModel = null;

            if (entity is not null)
            {
                viewModel = new FamilyViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }

            return viewModel;
        }

        public Guid Join(Guid familyId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}

