﻿using System;
using MyHome.Shared;
using MyHome.Shared.ViewModels;

namespace MyHome.Data.Services
{
    public class DbFamilyService : IFamilyService
    {
        private readonly ApplicationDbContext dbContext;

        public DbFamilyService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Create(string name, string userId)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            ApplicationUser? user = dbContext.Users.Find(userId);
            if (user is null) throw new KeyNotFoundException("User not found");

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

        public FamilyViewModel? Get(int id)
        {
            Family? entity = dbContext.Families.Find(id);
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

        public int Join(string familyId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
