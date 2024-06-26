﻿using Microsoft.AspNetCore.Identity;

namespace MyHome.Web.Data
{
    public class Income
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string IncomeTypeId { get; set; }
        public IncomeType? IncomeType { get; set; }
    }
}
