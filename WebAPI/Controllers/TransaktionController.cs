using Domain.Models;
using GRPCService.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TransaktionController : ControllerBase
{
    private readonly IAktieLogic aktieLogic;

    public TransaktionController(IAktieLogic aktieLogic)
    {
        this.aktieLogic = aktieLogic;
    }

    [HttpGet]
    public async Task<ActionResult<List<Transaktion>>> GetTransaktionerAsync(string username)
    {
        try
        {
            var transaktioner = await aktieLogic.getTransaktionerFraUsername(username);

            return Ok(transaktioner);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}