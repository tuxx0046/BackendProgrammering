using BigShop.Models.Manufacturer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IManufacturerRepository
    {
        Task<int> CreateAsync(ManufacturerCreate manufacturerCreate);
        Task<int> DeleteAsync(int manufacturerId);
        Task<List<Manufacturer>> GetAllAsync();
        Task<Manufacturer> GetByIdAsync(int manufacturerId);
        Task<int> UpdateAsync(Manufacturer updatedManufacturer);
    }
}