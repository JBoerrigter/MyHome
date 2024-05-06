using System;
using MyHome.Shared.ViewModels;

namespace MyHome.Shared
{
	public interface IFamilyService
	{
		FamilyViewModel Get(Guid userId);
		Guid Create(string name, Guid userId);
		Guid Join(Guid familyId, Guid userId);
	}
}

