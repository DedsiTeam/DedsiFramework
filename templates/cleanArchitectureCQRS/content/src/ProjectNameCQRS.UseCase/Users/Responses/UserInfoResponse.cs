namespace ProjectNameCQRS.Users.Responses;

public class UserInfoResponse
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; }
    public string Account { get; set; }
    public string Email { get; set; }
}