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
public class UserController
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
        
   
    }
    
    
