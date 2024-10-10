using CQRSModels.Models;

namespace AAAaCQRSApi.IRepository;

public interface IOrderReadRepository
{
    
    
    Task<Order> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync();
}
