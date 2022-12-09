using System.Security.Cryptography;
using System.Text;
using GRPC.Bruger;
using Grpc.Core;
using GRPCService.LogicImpl;
using GRPCService.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Bruger = Domain.Models.Bruger;


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

        static string Hasher(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] temp = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < temp.Length; i++)
                {
                    builder.Append(temp[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Domain.Models.Bruger>> CreateAsync(Bruger bruger)
        {
            try
            {
                bruger.Password = Hasher(bruger.Password);
                BrugerResponse user = await brugerLogic.CreateBruger(bruger);
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
    
    
