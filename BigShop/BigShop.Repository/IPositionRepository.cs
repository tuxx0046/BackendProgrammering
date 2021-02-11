using BigShop.Models.Position;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IPositionRepository
    {
        Task<int> CreateAsync(PositionCreate positionCreate);
        Task<int> DeleteAsync(int positionId);
        Task<List<Position>> GetAllAsync();
        Task<Position> GetByIdAsync(int paymentMethodId);
        Task<int> UpdateAsync(Position updatedPosition);
    }
}