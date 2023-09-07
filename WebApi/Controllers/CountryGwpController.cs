using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("server/api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        private readonly ILogger<CountryGwpController> _logger;
        private readonly ICountryGwpService _countryGwpService;

        public CountryGwpController(ILogger<CountryGwpController> logger, ICountryGwpService countryGwpService)
        {
            _logger = logger;
            _countryGwpService = countryGwpService;
        }

        /// <summary>
        /// Returns average gross written premium per country and lines of business for years 2008-2015
        /// </summary>
        /// <param name="request">Country code and list of lines of business names</param>
        /// <returns>Average gross written premium for each line of business</returns>
        [HttpPost("avg")]
        public async Task<Dictionary<string, double>> Post([FromBody]CountryGwpRequest request)
        {
            _logger.LogInformation("avg is called. Country: {country}, Lob: {lob}", request.Country, string.Join(",", request.Lob));

            var result = await _countryGwpService.GetForLinesOfBusiness(request.Country, request.Lob);
            return result;
        }
    }
}