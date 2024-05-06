using System;
namespace MyHome.Shared.Requests
{
	public class UserCreateRequest
	{
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}

