namespace Domain.DTOs;

public class UserLoginDTO
{
    public UserLoginDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public UserLoginDTO()
    {
    }

    public string Username { get; init; }
    public string Password { get; init; }
}