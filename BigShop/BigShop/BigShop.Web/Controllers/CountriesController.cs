using BigShop.Models.Country;
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
    //[Authorize]
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

        //[HttpPost]
        //public async Task<ActionResult> Create([FromBody]CountryCreate countryCreate)
        //{

        //}

        //[HttpDelete("{countryId}")]
        //public async Task<ActionResult> Delete(int countryId)
        //{
        //    var category = await _categoryRepository.GetByIdAsync(categoryId);
        //    if (category == null)
        //        return NotFound($"Category with Id {categoryId} does not exist");

        //    await _categoryRepository.DeleteAsync(categoryId);
        //    return NoContent();
        //}

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAll()
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
    }
}
