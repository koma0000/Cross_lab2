using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cross_Kamil.Models;

namespace Cross_Kamil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("token")]
        public string GetToken()
        {
            return AuthOptions.GenerateToken();
        }
        [HttpGet("token/secret")]
        public string GetAdminToken()
        {
            return AuthOptions.GenerateToken(true);
        }
    }
}