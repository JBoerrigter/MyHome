using System;
namespace MyHome.Shared
{
	public interface IHomeService
	{
		HomeViewModel Get(int id);
		int Create(int familyId, string street, string number, string postalCode, string city);
		void Delete(int id);
	}
}

