namespace Domain.Models;

public class Transaktion
{
    public int ID { get; set; }

    public string username { get; set; }

    public string Aktienavn { get; set; }

    public int antal { get; set; }

    public DateTime Date { get; set; }
}