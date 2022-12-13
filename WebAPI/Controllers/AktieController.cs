using Domain.DTOs;
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
    public async Task<ActionResult<Aktie>> GetAktieAsync(string name)
    {
        try
        {
            var aktier = await aktieLogic.getAktie(name);

            return Ok(aktier);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Aktie>>> GetAktierAsync()
    {
        try
        {
            var aktier = await aktieLogic.getAllAktier();

            return Ok(aktier);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("depot")]
    public async Task<ActionResult<List<Aktie>>> GetDepotAsync(int depotID)
    {
        try
        {
            var aktier = await aktieLogic.getAllAktierFromDepot(depotID);

            return Ok(aktier);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("buy")]
    public async Task<ActionResult<AktieResponse>> CreateAsync([FromBody] AktieRequestDTO requestDto)
    {
        try
        {
            var aktiee =
                await aktieLogic.buyAktie(requestDto.antal, requestDto.depotID, requestDto.aktie);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("sell")]
    public async Task<ActionResult> UpdateAsync([FromBody] AktieRequestDTO requestDto)
    {
        try
        {
            await aktieLogic.sellAktie(requestDto.antal, requestDto.depotID, requestDto.aktie);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}