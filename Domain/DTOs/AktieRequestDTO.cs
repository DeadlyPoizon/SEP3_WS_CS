using Domain.Models;

namespace Domain.DTOs;

public class AktieRequestDTO
{
    public string param { get; set; }
    public int antal { get; set; }
    public int depotID { get; set; }
    public Aktie aktie { get; set; }
}