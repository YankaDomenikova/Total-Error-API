using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Infrastructure.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        public RegionController(IMainAPIService mainAPIService)
        {
            MainAPIService = mainAPIService;
        }

        public IMainAPIService MainAPIService { get; }

        [Authorize]
        public IActionResult GetRegions()
        {
            var res = MainAPIService.GetRegions();
            return Ok(res);
        }
    }
}
