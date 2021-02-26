using BigShop.Models.Country;
using BigShop.Models.Zip;
using BigShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigShop.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IZipRepository _zipRepository;

        public CountriesController(ICountryRepository countryRepository, IZipRepository zipRepository)
        {
            _countryRepository = countryRepository;
            _zipRepository = zipRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Country))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCountry([FromBody] CountryCreate countryCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newCountryId = await _countryRepository.CreateAsync(countryCreate);
            if (newCountryId != -1)
            {
                var newCountry = await _countryRepository.GetByIdAsync(newCountryId);

                return CreatedAtRoute("GetByCountryId", new { countryId = newCountryId }, newCountry);
            }

            return StatusCode(500);
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCountry(int countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            if (country == null)
            {
                return NotFound($"Country with Id {countryId} does not exist");
            }

            var zipCodes = await _zipRepository.GetByCountryIdAsync(countryId);
            if (zipCodes.Count != 0)
            {
                return BadRequest("Cannot remove country while zip codes are attached to it");
            }

            int affectedRows = await _countryRepository.DeleteAsync(countryId);
            if (affectedRows > 0)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Country>>> GetAllCountries()
        {
            var countries = await _countryRepository.GetAllAsync();
            if (countries == null)
            {
                return StatusCode(500);
            }

            if (countries.Count == 0)
            {
                return Ok("No countries exist in database");
            }

            return Ok(countries);
        }

        [HttpGet("{countryId}", Name = "GetByCountryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> GetByCountryId(int countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            if (country == null)
            {
                return NotFound($"Country with Id {countryId} does not exist");
            }
            return Ok(country);
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCountry(int countryId, [FromBody] Country updatedCountry)
        {
            if (countryId != updatedCountry.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldCountry = await _countryRepository.GetByIdAsync(countryId);
            if (oldCountry == null)
            {
                return NotFound($"Country with Id {countryId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _countryRepository.UpdateAsync(updatedCountry);

            if (affectedRows > 0)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet("{countryId}/zipcodes")]
        public async Task<ActionResult<List<Zip>>> GetZipByCountryId(int countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            if (country == null)
            {
                return NotFound($"Country with Id {countryId} not found");
            }

            var zipCodes = await _zipRepository.GetByCountryIdAsync(countryId);
            if (zipCodes.Count == 0)
            {
                return Ok($"No zip codes yet for country \"{country.Name}\"");
            }

            return Ok(zipCodes);
        }
    }
}
