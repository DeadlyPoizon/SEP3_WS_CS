namespace Domain.Models;

public class Bruger
{
    public Bruger()
    {
    }

    public Bruger(string username, string password)
    {
        Username = username;
        Password = password;
        Saldo = 100000;
        DepotID = 8;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public int DepotID { get; set; }
    public double Saldo { get; set; }
}