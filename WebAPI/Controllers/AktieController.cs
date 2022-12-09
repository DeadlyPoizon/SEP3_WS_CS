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
    
}