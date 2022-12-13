using System.Security.Cryptography;
using System.Text;
using Domain.Models;
using GRPCService.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IBrugerLogic brugerLogic;

    public UserController(IBrugerLogic brugerLogic)
    {
        this.brugerLogic = brugerLogic;
    }

    private static string Hasher(string input)
    {
        using (var sha256 = SHA256.Create())
        {
            var temp = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            var builder = new StringBuilder();
            for (var i = 0; i < temp.Length; i++) builder.Append(temp[i].ToString("x2"));

            return builder.ToString();
        }
    }

    [HttpPost]
    public async Task<ActionResult<Bruger>> CreateAsync(Bruger bruger)
    {
        try
        {
            bruger.Password = Hasher(bruger.Password);
            var user = await brugerLogic.CreateBruger(bruger);
            return Created($"/users/{user.Response}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await brugerLogic.resetBruger(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}