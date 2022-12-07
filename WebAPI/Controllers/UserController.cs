using GRPC.Bruger;
using Grpc.Core;
using GRPCService.LogicImpl;
using GRPCService.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Bruger = Domain.Models.Bruger;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController
{
   
    private readonly IBrugerLogic brugerLogic;

        public UserController(IBrugerLogic brugerLogic)
        {
            this.brugerLogic = brugerLogic;
        }
        
   
    }
    
    
