namespace Domain.Models;

public class Transaktion
{
    public int ID { get; set; }
    
    public Bruger username { get; set; }
    
    public Aktie Aktienavn { get; set; }
    
    public int antal { get; set; }
    
    public DateTime Date { get; set; }
        
}