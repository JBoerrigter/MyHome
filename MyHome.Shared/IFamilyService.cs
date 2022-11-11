using System;
using MyHome.Shared.ViewModels;

namespace MyHome.Shared
{
	public interface IFamilyService
	{
		FamilyViewModel Get(int id);
		int Create(string name, string userId);
		int Join(string familyId, string userId);
	}
}

