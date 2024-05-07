using System;
using System.Collections.Generic;
namespace MyHome.Shared
{
    public interface IHomeService
	{
		IEnumerable<HomeViewModel> GetByFamilyId(Guid familyId);
		Guid Create(Guid familyId, string street, string number, string postalCode, string city);
		void Delete(Guid id);
	}
}

