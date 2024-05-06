using System;
namespace MyHome.Shared
{
    public interface IHomeService
	{
		HomeViewModel Get(Guid id);
		Guid Create(Guid familyId, string street, string number, string postalCode, string city);
		void Delete(Guid id);
	}
}

