namespace ProjectNameCQRS.Users.Requests;

public class CreateUserRequest
{
    public string UserName { get; set; }

    public string Account { get; set; }

    public string PassWord { get; set; }

    public string Email { get; set; }
}