using BigShop.Models.Country;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface ICountryRepository
    {
        Task<int> CreateAsync(CountryCreate countryCreate);
        Task<int> DeleteAsync(int countryId);
        Task<List<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(int countryId);
        Task<int> UpdateAsync(Country updatedCountry);
    }
}