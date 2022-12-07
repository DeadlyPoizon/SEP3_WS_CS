using Domain.Models;
using GRPCService.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<Aktie>>> GetAsync([FromQuery] string? navn)
    {
        try
        {

            Aktie aktie = await aktieLogic.getAktie(navn);
                
            return Ok(aktie);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}