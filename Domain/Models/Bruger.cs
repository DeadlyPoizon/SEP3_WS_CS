namespace Domain.Models;

public class Bruger
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int DepotID { get; set; }
    public double Saldo { get; set; }


    public Bruger()
    {
        
    }
    
    public Bruger(String username,String password)
    {
        this.Username = username;
        this.Password = password;
        this.Saldo = 100000;
        this.DepotID = 8;
    }
    
}