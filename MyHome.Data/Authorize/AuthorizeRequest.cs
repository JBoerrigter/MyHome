namespace MyHome.Data.Authorize;

public record AuthorizeRequest (
    string UserName,
    string Password
);