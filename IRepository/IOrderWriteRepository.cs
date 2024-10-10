using CQRSModels.Models;

namespace AAAaCQRSApi.IRepository;

public interface IOrderWriteRepository
{
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
}