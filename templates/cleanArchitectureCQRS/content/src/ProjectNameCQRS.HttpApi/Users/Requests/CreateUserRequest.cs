namespace ProjectNameCQRS.Users.Requests;

public record CreateUserRequest(string UserName, string Account, string PassWord, string Email);