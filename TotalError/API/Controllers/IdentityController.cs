using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Infrastructure.DtoModels;
using Infrastructure.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        public IIdentityUser Identityuser { get; }

        public IdentityController(IIdentityUser identityuser)
        {
            Identityuser = identityuser;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserVM model)
        {
            if (ModelState.IsValid)
            {
                var result = Identityuser.Register(model);
                if (result.Result.Length > 0)
                {
                    return Ok(result);
                }
                return BadRequest("Register attempt is failed. Check email and password!");
            }
            return BadRequest("Incorrect register data!");
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserVM request)
        {
            var res = Identityuser.Login(request).Result;
            if (res.Length > 0)
            {
                Request.Headers.Add("Authorization", res);
                return Ok(res);
            }
            return Unauthorized();
        }
    }
}
