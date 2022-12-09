using GRPC.Bruger;
using GRPCService.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Aktie = Domain.Models.Aktie;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AktieController : ControllerBase
{

    private readonly IAktieLogic aktieLogic;

    public AktieController(IAktieLogic aktieLogic)
    {
        this.aktieLogic = aktieLogic;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Aktie>>> GetAsync()
    {
        try
        {

            List<Aktie> aktier = await aktieLogic.getAllAktier();
                
            return Ok(aktier);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<AktieResponse>> CreateAsync(int antal, int depotID, GRPC.Bruger.Aktie aktie)
    {
        try
        {
            
            AktieResponse aktiee = await aktieLogic.buyAktie(antal,depotID,aktie);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}