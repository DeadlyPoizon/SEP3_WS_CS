namespace Domain.DTOs;

public class UserLoginDTO
{
    public string Username { get; init; }
    public string Password { get; init; }
    
    public UserLoginDTO(String username,String password)
    {
        this.Username = username;
        this.Password = password;
        
    }
    
    public UserLoginDTO()
    {
       
        
    }
}