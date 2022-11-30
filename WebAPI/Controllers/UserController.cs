using GRPC.Bruger;
using GRPCService.LogicImpl;
using GRPCService.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Bruger = Domain.Models.Bruger;

namespace WebAPI.Controllers;

public class UserController
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IBrugerLogic brugerLogic;

        public UsersController(IBrugerLogic brugerLogic)
        {
            this.brugerLogic = brugerLogic;
        }
        
        
        [HttpPost]
        public async Task<ActionResult<BrugerResponse>> CreateAsync()
        {
            try
            {
                BrugerResponse bruger = await brugerLogic.CreateBruger();
                 
                return Created($"/users/{bruger.}", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
    }
    
    
}