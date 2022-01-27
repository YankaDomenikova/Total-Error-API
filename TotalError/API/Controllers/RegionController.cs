using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Infrastructure.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionController : ControllerBase
    {
        public RegionController(IMainAPIService mainAPIService)
        {
            MainAPIService = mainAPIService;
        }

        public IMainAPIService MainAPIService { get; }

        [HttpGet]
        [Route("all_regions")]
        public IActionResult GetRegions()
        {
            var res = MainAPIService.GetRegions();
            return Ok(res);
        }

        [HttpGet]
        [Route("all_countries")]
        public IActionResult GetCountries()
        {
            var res = MainAPIService.GetAllCountries();
            return Ok(res);
        }

        [HttpGet]
        [Route("country_by_name")]
        public IActionResult GetCountryByName([FromForm]string name)
        {
            var res = MainAPIService.GetCountryByName(name);
            return Ok(res);
        }

        [HttpGet]
        [Route("countries_by_region")]
        public IActionResult GetCountriesByRegion([FromForm]string id)
        {
            var res = MainAPIService.GetCountriesByRegion(id);
            return Ok(res);
        }

        [Route("countries_by_profit")]
        public IActionResult GetCountriesByTotalProfit()
        {
            var res = MainAPIService.GetCountriesByTotalProfit();
            return Ok(res);
        }

        [HttpGet]
        [Route("cost_of_country")]
        public IActionResult GetTotalCostOfCountry([FromForm] string name)
        {
            var res = MainAPIService.GetTotalCostOfCountry(name);
            return Ok(res);
        }

    }
}
