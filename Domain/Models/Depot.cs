namespace Domain.Models;

public class Depot
{
    public int ID { get; set; }

    public string AktieNavn { get; set; }

    public int Antal { get; set; }

    public double købspris { get; set; }

    public double? difference { get; set; }
}