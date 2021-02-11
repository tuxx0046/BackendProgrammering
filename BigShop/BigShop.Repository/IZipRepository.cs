using BigShop.Models.Zip;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IZipRepository
    {
        Task<int> CreateAsync(ZipCreate zipCreate);
        Task<int> DeleteAsync(int zipId);
        Task<List<Zip>> GetByCountryIdAsync(int countryId);
        Task<Zip> GetByIdAsync(int zipId);
        Task<int> UpdateAsync(Zip updatedZip);
    }
}