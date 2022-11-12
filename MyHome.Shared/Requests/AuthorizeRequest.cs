namespace MyHome.Shared.Authorize;

public record AuthorizeRequest (
    string UserName,
    string Password
);